using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP3Manager
{
    class Program
    {
        public class ManagedMP3
        {
            public Id3.Net.Mp3File ID3File { get; set; }
            public string FileName { get; set; }
            public bool HasLatestID3 { get; set; }
            public bool HasID3 { get; set; }
            public string ErrorMessage { get; set; }
            public List<Id3.Net.Id3Tag> iTags { get; set; }
        }
        private static int finishedFiles = 0;
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            List<ManagedMP3> managedFiles = GetMP3s(@"D:\Media\Music");

            managedFiles = UpdateTags(managedFiles, @"D:\Media\Music");

            //managedFiles = GetInfo(managedFiles);

            var a = managedFiles.Where(mf => mf.HasID3 == true && mf.HasLatestID3 == true);

            Console.WriteLine((DateTime.Now.Subtract(start).TotalMilliseconds / (double)managedFiles.Count()).ToString());
        }

        private static int threads = 0;
        private static void blah(object o)
        {
            state s = (state)o;
            Id3.Net.Mp3File file = s.file;
            Id3.Net.Id3Tag newTag = s.newTag;
            file.WriteTag(newTag, 2, 3, Id3.Net.WriteConflictAction.Replace);
            //threads--;
            finishedFiles++;
            Console.WriteLine("Rewrote file #" + finishedFiles.ToString());

        }
        private static List<ManagedMP3> UpdateTags(List<ManagedMP3> managedFiles, string rootDirectory)
        {
            //Update Old Version To Current
            IEnumerable<ManagedMP3> oldID3 = managedFiles.Where(m => m.HasID3 == true && m.HasLatestID3 == false);
            Console.WriteLine(oldID3.Count().ToString());
            foreach(Id3.Net.Mp3File file in oldID3.Select(o => o.ID3File))
            {
                SaveOldTag(new state { file = file });
            }

            //reset managed files
            managedFiles = GetMP3s(rootDirectory);

            oldID3 = managedFiles.Where(m => m.HasID3 == true && m.HasLatestID3 == false);

            return managedFiles;
        }
        public class state
        {
            public System.Threading.ManualResetEvent reset { get; set; }
            public Id3.Net.Mp3File file { get; set; }
            public Id3.Net.Id3Tag newTag { get; set; }
        }
        private static void SaveOldTag(object f)
        {
            state s = (state)f;
            Id3.Net.Mp3File file = s.file;
            Id3.Net.Id3Tag[] ts = s.file.GetAllTags();
            if (ts != null && ts.Length > 0)
            {
                Id3.Net.Id3Tag oldTag = (from tags in file.GetAllTags()
                                         select new
                                         {
                                             tags,
                                             notNullProperties = tags.GetType().GetProperties().Where(p => p.GetValue(tags, null) != null).Count()
                                         }).OrderByDescending(t => t.notNullProperties).Select(t => t.tags).FirstOrDefault();

                Id3.Net.Id3v23Tag newTag = new Id3.Net.Id3v23Tag();

                newTag.Album.Value = oldTag.Album.Value ?? "";
                newTag.Artists.Value = oldTag.Artists.Value ?? "";
                newTag.Year.Value = oldTag.Year.Value ?? "";
                newTag.Band.Value = oldTag.Band.Value ?? "";
                newTag.BeatsPerMinute.Value = oldTag.BeatsPerMinute.Value ?? "";
                newTag.Composers.Value = oldTag.Composers.Value ?? "";
                newTag.Conductor.Value = oldTag.Conductor.Value ?? "";
                newTag.Track.Value = oldTag.Track.Value ?? "";
                newTag.Title.Value = oldTag.Title.Value ?? "";
                newTag.Publisher.Value = oldTag.Publisher.Value ?? "";
                newTag.Genre.Value = oldTag.Genre.Value ?? "";

                //while (threads > 63)
                //{
                //    System.Threading.Thread.Sleep(500);
                //}
                //System.Threading.Thread thr = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(blah));
                blah((new state { file = file, newTag = newTag }));


            }
            else
            {
                string a = "";
            }
        }

        private static List<ManagedMP3> GetInfo(List<ManagedMP3> managedFiles)
        {
            foreach (ManagedMP3 file in managedFiles.Where(mf => mf.HasID3 == true && mf.HasLatestID3 == false))
            {
                foreach (Id3.Net.Id3Tag tag in file.ID3File.GetAllTags())
                {
                    file.iTags = GetInternetTags(tag);
                    if (file.iTags.Count == 1 || file.iTags.Count == 2)
                    {
                        foreach (Id3.Net.Id3Tag t in file.iTags)
                        {
                            file.iTags.AddRange(GetInternetTags(t));
                        }
                    }
                }
            }
            return managedFiles;
        }

        private static List<Id3.Net.Id3Tag> GetInternetTags(Id3.Net.Id3Tag tag)
        {
            List<Id3.Net.Id3Tag> l = new List<Id3.Net.Id3Tag>();

            Id3.Net.Info.WindowsMedia.WindowsMediaInfoProvider wm = new Id3.Net.Info.WindowsMedia.WindowsMediaInfoProvider();
            l.AddRange(wm.GetInfo(tag));

            Id3.Net.Info.Discogs.DiscogsInfoProvider d = new Id3.Net.Info.Discogs.DiscogsInfoProvider();
            l.AddRange(d.GetInfo(tag));

            Id3.Net.Info.LyricWiki.LyricWikiInfoProvider w = new Id3.Net.Info.LyricWiki.LyricWikiInfoProvider();
            l.AddRange(w.GetInfo(tag).ToList());

            Id3.Net.Info.Amazon.AmazonInfoProvider a = new Id3.Net.Info.Amazon.AmazonInfoProvider();
            l.AddRange(a.GetInfo(tag).ToList());

            Id3.Net.Info.Filename.FilenameInfoProvider f = new Id3.Net.Info.Filename.FilenameInfoProvider();
            l.AddRange(f.GetInfo(tag).ToList());

            return l;
        }

        private static List<ManagedMP3> GetMP3s(string directory)
        {
            List<ManagedMP3> retVal = new List<ManagedMP3>();
            
            foreach (string file in System.IO.Directory.GetFiles(directory, "*.mp3", System.IO.SearchOption.AllDirectories))
            {
                ManagedMP3 f = new ManagedMP3 { FileName = file };
                try
                {
                    f.ID3File = new Id3.Net.Mp3File(file);
                    if (f.ID3File.HasTags)
                    {
                        IEnumerable<Id3.Net.Id3Tag> tags = f.ID3File.GetAllTags().Where(t =>
                                                    (t.Album ?? "") != ""
                                                    && (t.Artists ?? "") != ""
                                                    && (t.Title ?? "") != "");
                        if (tags.Where(t => t.MajorVersion == 2 && t.MinorVersion == 3).Count() > 0)
                        {
                            f.HasID3 = true;
                            f.HasLatestID3 = true;
                        }
                        else if (tags.Count() > 0)
                        {
                            f.HasID3 = true;
                            f.HasLatestID3 = false;
                        }
                        else
                        {
                            f.HasID3 = false;
                            f.HasLatestID3 = false;
                        }
                    }
                    else
                    {
                        f.HasID3 = false;
                        f.HasLatestID3 = false;
                    }
                }
                catch (Exception e)
                {
                    f.HasID3 = false;
                    f.HasLatestID3 = false;
                    f.ErrorMessage = e.Message;
                }
                retVal.Add(f);
            }
            return retVal;
        }
    }
}

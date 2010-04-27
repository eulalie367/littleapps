using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace MovieManager
{
    public static class extensions
    {
        public static string ReplaceAll(this List<Movie.ReplaceMent> ls, string FileName)
        {
            string retval = FileName;
            foreach (Movie.ReplaceMent replace in ls)
            {
                if (replace.UseRegeEx)
                {
                    Regex reg = new Regex(replace.Find);
                    retval = reg.Replace(retval, replace.Replace);
                }
                else
                    retval = retval.ToLower().Replace(replace.Find.ToLower(), replace.Replace.ToLower());
            }
            retval = retval.Replace("  ", " ");
            foreach (string s in retval.Split(' '))
            {
                if (!string.IsNullOrEmpty(s))
                {
                    string tmp = retval.Substring(retval.IndexOf(s) + s.Length);
                    tmp = tmp.Replace(s, "");
                    retval = retval.Substring(0, retval.IndexOf(s) + s.Length) + tmp;
                }
            }
            return retval;
        }
        public static bool Save<t>(this t ls, string FullPath) where t : class, new()
        {
            if (!string.IsNullOrEmpty(FullPath))
            {
                try
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(FullPath, System.IO.FileMode.OpenOrCreate))
                    {
                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        bf.Serialize(fs, ls);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        public static t Open<t>(this t ls, string FullPath) where t :  class, new()
        {
            if (!string.IsNullOrEmpty(FullPath))
            {
                try
                {
                    object f = null;
                    using (System.IO.FileStream fs = new System.IO.FileStream(FullPath, System.IO.FileMode.OpenOrCreate))
                    {
                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        f = bf.Deserialize(fs);
                        if (f != null)
                        {
                            ls = f as t;
                        }
                    }
                }
                catch
                {
                }
            }
            return ls;
        }
        internal static bool GetMovies(this List<Movie> ls, string Dir, List<Movie.ReplaceMent> Replaces)
        {
            try
            {
                if (!string.IsNullOrEmpty(Dir))
                {
                    string[] fileNames = Directory.GetFiles(Dir, "*.avi*", SearchOption.AllDirectories);
                    if (fileNames != null && fileNames.Length > 0)
                    {
                        foreach (string fileName in fileNames)
                        {
                            Movie movie = new Movie(fileName);
                            movie.SavedName = movie.GetSavedName(Dir, Replaces);
                            ls.Add(movie);
                        }
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
    [Serializable]
    public class Movie
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public string SavedName { get; set; }

        #region Contructors
        public Movie()
        {
            this.FullPath = "";
            this.SavedName = "";
            this.FileName = "";
        }
        public Movie(string fullPath)
            : this()
        {
            this.FullPath = fullPath;
            this.FileName = GetFileName(fullPath);
        }
        #endregion

        #region Child Classes
            #region ReplaceMent
        [Serializable]
        public class ReplaceMent
        {
            public string Find { get; set; }
            public string Replace { get; set; }
            public bool UseRegeEx { get; set; }
            public ReplaceMent()
            {
                this.Find = "";
                this.Replace = "";
                this.UseRegeEx = true;
            }
            public ReplaceMent(string find, string replace)
                : this()
            {
                this.Find = find;
                this.Replace = replace;
            }
            public ReplaceMent(string find, string replace, bool useRegeEx)
                : this()
            {
                this.Find = find;
                this.Replace = replace;
                this.UseRegeEx = useRegeEx;
            }
        }
            #endregion
        #endregion
        
        internal  string GetFileName(string fullPath)
        {
            if (!string.IsNullOrEmpty(fullPath))
            {
                FileInfo f = new FileInfo(fullPath);
                if (f.Exists)
                    return f.Name;
            }
            return "";
        }
        internal string GetSavedName(string rootDirectory, List<ReplaceMent> replaces)
        {
            return replaces.ReplaceAll(this.FileName);
        }

    }
}

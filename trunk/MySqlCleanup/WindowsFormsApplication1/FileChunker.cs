using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsFormsApplication1
{
    class FileChunker
    {
        public delegate void ChunkedFile(string chunkedData, int position);
        public event ChunkedFile TextChunked;
        public int FileSize { get; set; }
        public string FileLocation { get; set; }
        public FileChunker()
        {
            TextChunked = new ChunkedFile(FileChunker_TextChunked);
        }
        public FileChunker(int fileSize, string fileLocation)
        {
            TextChunked = new ChunkedFile(FileChunker_TextChunked);
            this.FileSize = fileSize;
            this.FileLocation = fileLocation;
        }

        private void FileChunker_TextChunked(string chunkedData, int position)
        {
        }
        /// <summary>
        /// make sure to set FileSize, TmpDir, and FileLocation before this is ran
        /// </summary>
        public void ChunkTextFiles()
        {
            this.ChunkTextFiles(this.FileSize, this.FileLocation);
        }
        /// <summary>
        /// Divides a large text file into more managable bits.
        /// </summary>
        /// <param name="fileSize"></param>
        /// <param name="tmpDir"></param>
        /// <param name="fileLocation"></param>
        public void ChunkTextFiles(int fileSize, string fileLocation)
        {
            Stream str = null;
            StreamReader r = null;
            char[] buffer = new char[fileSize];
            try
            {
                str = File.Open(fileLocation, FileMode.Open);
                r = new StreamReader(str);
                int pos = 0;
                int i = 0;
                while (!r.EndOfStream)
                {

                    try
                    {
                        r.Read(buffer, 0, fileSize);
                        //need an event to fire here so you can do other munipulations.
                        TextChunked(new string(buffer), i);
                    }
                    catch (Exception exc)
                    { }
                    finally
                    {
                        //clear buffer but leave reader open of course
                        buffer = new char[fileSize];
                    }
                    i++;
                    pos += fileSize;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (str != null)
                    str.Close();
                if (r != null)
                    r.Close();
                buffer = null;
            }
        }

    }
}

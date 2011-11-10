using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace CreateTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\littleapps\VS_Templates\Sitefinity.Module_ContentBased_new\";
            string tmpPath = path + "Template\\";
            Directory.CreateDirectory(tmpPath);
            foreach(string fileName in Directory.GetFiles(path ,"*",SearchOption.AllDirectories))
            {
                FileInfo fi = new FileInfo(fileName);
                string name = fi.Name.Replace("Locations", "").Replace("Location", "");
                File.Copy(fi.FullName, tmpPath + name);

                string a = fileName;
            }
        }
    }
}

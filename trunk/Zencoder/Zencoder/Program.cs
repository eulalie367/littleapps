using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Zencoder
{
    class Program
    {
        public static string URL = "https://app.zencoder.com/api/jobs";
        static void Main(string[] args)
        {
            string location, fileName, baseName;
            bool isTest;
            SetVars(out isTest, out location, out fileName, out baseName);
            string postData = BuildRequest(isTest, location, fileName, baseName);


            Console.WriteLine("Posting Data:");
            Console.WriteLine(postData);

            HttpWebRequest req = (HttpWebRequest) HttpWebRequest.CreateDefault(new Uri(URL));
            req.Method = "POST";
            req.ContentType = "application/json";
            using (Stream strm = req.GetRequestStream())
            {
                using (StreamWriter writer = new StreamWriter(strm))
                {
                    writer.Write(postData);
                }
            }

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                using (Stream strm = resp.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(strm))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }
            }
        }

        private static void SetVars(out bool isTest, out string location, out string fileName, out string baseName)
        {
            Console.WriteLine("Is this a test? (y/n)");
            isTest = Console.ReadLine().ToBool() ?? true;

            Console.WriteLine("What is the location of the source video; this can be ftp, http, or sm?");
            location = Console.ReadLine();
            location = string.IsNullOrEmpty(location) ? "ftp://brmethod:hilldog@brmethod.net/Zencoder/" : location;

            Console.WriteLine("What is the name of the source video?");
            fileName = Console.ReadLine();
            fileName = string.IsNullOrEmpty(fileName) ? "Manifesto_60_FINAL.wmv" : fileName;

            Console.WriteLine("What would you like to name the encoded files?");
            baseName = Console.ReadLine();
            baseName = string.IsNullOrEmpty(baseName) ? fileName.Split('.')[0] : baseName;
        }

        private static string BuildRequest(bool isTest, string location, string fileName, string baseName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"{{
              ""test"": {0},
              ""api_key"": ""278c7feb047beed12562cad627b5592c"",
              ""input"": ""{1}"",
              ""region"": ""us"",
              ""output"": [
                {{
                  ""thumbnails"": 
                    {{
                        ""base_url"": ""{2}"",
                        ""prefix"": ""{3}"",
                        ""number"" : 10,
                        ""label"": ""thumb""
                    }}
                }},
                {{
                  ""base_url"": ""{2}"",
                  ""filename"": ""{3}.mp4"",
                  ""label"": ""mpeg4"",
                  ""video_codec"": ""mpeg4"",
                  ""quality"": 5,
                  ""width"": 320,
                  ""aspect_mode"": ""preserve""
                }},
                {{
                  ""base_url"": ""{2}"",
                  ""filename"": ""{3}.webm"",
                  ""label"": ""vp8"",
                  ""video_codec"": ""vp8"",
                  ""quality"": 5,
                  ""width"": 320,
                  ""aspect_mode"": ""preserve""
                }},
                {{
                  ""base_url"": ""{2}"",
                  ""filename"": ""{3}.ogv"",
                  ""label"": ""theora"",
                  ""video_codec"": ""theora"",
                  ""quality"": 5,
                  ""width"": 320,
                  ""aspect_mode"": ""preserve""
                }}
              ]
            }}", isTest ? 1 : 0, location + fileName, location, baseName);

            return sb.ToString();
        }
    }
}

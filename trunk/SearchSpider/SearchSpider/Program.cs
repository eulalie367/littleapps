using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCrawler;
using NCrawler.HtmlProcessor;
using NCrawler.Interfaces;
using NCrawler.Services;
using System.Text.RegularExpressions;


namespace SearchSpider
{
    class Program
    {
        const string indexDir = "C:\\Index";
        const int threads = 1;
        const string url = "http://dev.bv.com";
        const string fromUrl = "http://dev.bv.com/projects";
        const string tagName = "";
        static void Main(string[] args)
        {
            //SpiderThread();


            SearchProjects("westar");
        }
        public static void SpiderThread()
        {
            //state the file location of the index
            Lucene.Net.Store.Directory dir = Lucene.Net.Store.FSDirectory.GetDirectory(indexDir, true);

            //create an analyzer to process the text
            Lucene.Net.Analysis.Analyzer analyzer = new
            Lucene.Net.Analysis.Standard.StandardAnalyzer();

            //create the index writer with the directory and analyzer defined.
            Lucene.Net.Index.IndexWriter indexWriter = new Lucene.Net.Index.IndexWriter(dir, analyzer, true);

            using 
            (
                Crawler c = new Crawler
                (
                    new Uri(url),
                    new HtmlDocumentProcessor
                    (
                        new Dictionary<string, string>
						{
							{"<!--BeginTextFiler-->", "<!--EndTextFiler-->"}
						},
                        new Dictionary<string, string>
						{
							{"<head", "</head>"}//skip any links in the head
						}
                    ), 
                    new DumperStep(indexWriter)
                )
                {
                    // Custom step to visualize crawl
                    MaximumThreadCount = threads,
                    MaximumCrawlDepth = 10,
                    ExcludeFilter = new[] 
                    { 
                        new RegexFilter(
			                new Regex(@"(\.jpg|\.css|\.js|\.gif|\.jpeg|\.png|\.ico|\.axd)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)
                            ) 
                    },
                }
            )
            {
                // Begin crawl
                c.Crawl();
            }


            //optimize and close the writer
            indexWriter.Optimize();
            indexWriter.Close();

        }
        internal class DumperStep : IPipelineStep
        {
            private Lucene.Net.Index.IndexWriter indexWriter;

            public DumperStep(Lucene.Net.Index.IndexWriter iWriter)
            {
                indexWriter = iWriter;
            }


            #region IPipelineStep Members

            /// <summary>
            /// </summary>
            /// <param name="crawler">
            /// The crawler.
            /// </param>
            /// <param name="propertyBag">
            /// The property bag.
            /// </param>
            int i = 0;
            public void Process(Crawler crawler, PropertyBag propertyBag)
            {
                if (propertyBag.Step.Uri.PathAndQuery.ToLower().Contains("project"))
                {
                    string a = "";
                }
                if (!string.IsNullOrEmpty(propertyBag.Text) && !PreviouslyIndexed(propertyBag.Step.Uri.ToString()))
                {

                    Lucene.Net.Documents.Document doc = new
                    Lucene.Net.Documents.Document();

                    //add string properties
                    Lucene.Net.Documents.Field fldURL = new Lucene.Net.Documents.Field("url", propertyBag.Step.Uri.ToString(), Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES);
                    doc.Add(fldURL);
                    Lucene.Net.Documents.Field fldContent = new Lucene.Net.Documents.Field("content", propertyBag.Text, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES);
                    doc.Add(fldContent);
                    Lucene.Net.Documents.Field fldTitle = new Lucene.Net.Documents.Field("title", propertyBag.Title, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.YES);
                    doc.Add(fldTitle);

                    //write the document to the index
                    indexWriter.AddDocument(doc);
                }
            }

            #endregion
        }

        public static bool PreviouslyIndexed(string url)
        {

            string indexFileLocation = indexDir;
            Lucene.Net.Store.Directory dir = Lucene.Net.Store.FSDirectory.GetDirectory(indexFileLocation, false);
            Lucene.Net.Search.IndexSearcher searcher = new Lucene.Net.Search.IndexSearcher(dir);
            Lucene.Net.Search.Hits hits = null;
            try
            {
                Lucene.Net.Search.Query query = new Lucene.Net.Search.TermQuery(new Lucene.Net.Index.Term("url", url));

                hits = searcher.Search(query);

            }
            catch { }
            finally
            {
                searcher.Close();
            }
            return hits.Length() > 0;
        }

        public static List<IndexedItem> SearchProjects(string s)
        {

            List<IndexedItem> retVal = new List<IndexedItem>();

            string indexFileLocation = indexDir;
            Lucene.Net.Store.Directory dir = Lucene.Net.Store.FSDirectory.GetDirectory(indexFileLocation, false);
            Lucene.Net.Search.IndexSearcher searcher = new Lucene.Net.Search.IndexSearcher(dir);

            try
            {
                Lucene.Net.Search.Query query = new Lucene.Net.Search.TermQuery(new Lucene.Net.Index.Term("content", s));
                query = query.Combine(new Lucene.Net.Search.Query[] { query, new Lucene.Net.Search.TermQuery(new Lucene.Net.Index.Term("url", fromUrl)) });
                query = query.Combine(new Lucene.Net.Search.Query[] { query, new Lucene.Net.Search.TermQuery(new Lucene.Net.Index.Term("title", s)) });


                //execute the query
                Lucene.Net.Search.Hits hits = searcher.Search(query);

                //iterate over the results.
                for (int i = 0; i < hits.Length(); i++)
                {
                    Lucene.Net.Documents.Document doc = hits.Doc(i);
                    string article = doc.Get("content");
                    string title = doc.Get("title");
                    string url = doc.Get("url");
                    retVal.Add(new IndexedItem { Article = article, Href = url, Title = title });
                }
                foreach (IndexedItem ind in retVal)
                {
                    Console.WriteLine(ind.Href);
                }

                retVal = retVal.Distinct().ToList();
            }
            catch { }
            finally
            {
                searcher.Close();
            }
            return retVal;
        }
        public class IndexedItem
        {
            public string Title { get; set; }
            public string Article { get; set; }
            public string Href { get; set; }
        }
    }
}

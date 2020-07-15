using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuceneNetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Analyzer analyzer = new PanGuAnalyzer();
            //IndexWriter writer = new IndexWriter("IndexDirectory", analyzer, true);
            DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory("IndexDirectory");
            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(dirInfo);
            IndexWriter writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);
            AddDocument(writer, "SQL Server 2008 的发布", "SQL Server 2008 的新特性");
            AddDocument(writer, "ASP.Net MVC框架配置与分析", "而今，微软推出了新的MVC开发框架，也就是Microsoft ASP.NET 3.5 Extensions");
            writer.Optimize();
            writer.Dispose();
        }
        static void AddDocument(IndexWriter writer, string title, string content)
        {
            Document document = new Document();
            document.Add(new Field("title", title, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("content", content, Field.Store.YES, Field.Index.ANALYZED));
            writer.AddDocument(document);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory("IndexDirectory");
            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(dirInfo);
            IndexSearcher searcher = new IndexSearcher(directory, true);
            // IndexSearcher searcher = new IndexSearcher("IndexDirectory");
            MultiFieldQueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new string[] { "title", "content" }, analyzer);
            Query query = parser.Parse(richTextBox1.Text);
            TopDocs docs = searcher.Search(query, (Filter)null, 10);

            if (docs == null || docs.TotalHits == 0)
            {
                Console.WriteLine("没有结果!");
            }
            else
            {
                int counter = 1;
                foreach (ScoreDoc sd in docs.ScoreDocs)
                {
                    try
                    {
                        Document doc = searcher.Doc(sd.Doc);
                        string title = doc.Get("title");
                        string content = doc.Get("content");
                        string job = doc.Get("job");
                        string createdate = doc.Get("createdate");
                        string result = string.Format("这是第{0}个搜索结果,标题为{1},内容{2}", counter, title, content);
                        Console.WriteLine(result);
                    }
                    catch (Exception ex)
                    {


                    }
                    counter++;
                }
            }

        }

    }
}

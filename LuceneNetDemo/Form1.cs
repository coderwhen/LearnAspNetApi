using Lucene.Net.Analysis;
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
            string ret = "", keyword = richTextBox1.Text.Trim();
            ret = PanGu(keyword);
            Console.WriteLine(ret);
        }

        private string PanGu(string keyword)
        {
            string ret = "";
            Analyzer analyzer = new PanGuAnalyzer();
            System.IO.StringReader reader = new StringReader(keyword);
            Lucene.Net.Analysis.TokenStream ts = analyzer.TokenStream(keyword, reader);
            bool hasNext = ts.IncrementToken();
            Lucene.Net.Analysis.Tokenattributes.ITermAttribute ita;

            while (hasNext)
            {
                ita = ts.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();
                ret += ita.Term + "|";
                hasNext = ts.IncrementToken();
            }
            ts.CloneAttributes();
            reader.Close();
            analyzer.Close();
            return ret;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var IndexDic = @"C:\SearchIndex";
            var isCreate = true;
            try
            {
                //创建索引目录
                if (!System.IO.Directory.Exists(IndexDic))
                {
                    System.IO.Directory.CreateDirectory(IndexDic);
                }
                FSDirectory directory = FSDirectory.Open(new DirectoryInfo(IndexDic), new NativeFSLockFactory());
                //IndexReader:对索引库进行读取的类
                bool isExist = IndexReader.IndexExists(directory);
                //是否存在索引库文件夹以及索引库特征文件
                if (isExist)
                {
                    //如果索引目录被锁定（比如索引过程中程序异常退出或另一进程在操作索引库），则解锁
                    //Q:存在问题 如果一个用户正在对索引库写操作 此时是上锁的 而另一个用户过来操作时 将锁解开了 于是产生冲突 --解决方法后续
                    if (IndexWriter.IsLocked(directory))
                    {
                        IndexWriter.Unlock(directory);
                    }
                }
                //IndexWriter第三个参数:true指重新创建索引,false指从当前索引追加....此处为新建索引所以为true
                IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), isCreate, Lucene.Net.Index.IndexWriter.MaxFieldLength.LIMITED);

                for (int i = 1; i < 150; i++)
                {
                    //这里是测试数据
                    AddIndex(writer, "我的标题" + i, i + "标题内容是飞大师傅是地方十大飞啊的飞是 安抚爱上地方 爱上地方" + i, DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    AddIndex(writer, "射雕英雄传作者金庸" + i, i + "我是欧阳锋" + i, DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    AddIndex(writer, "天龙八部2" + i, i + "慕容废墟,上官静儿,打撒飞艾丝凡爱上,虚竹" + i, DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    AddIndex(writer, "倚天屠龙记2" + i, i + "张无忌机" + i, DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    AddIndex(writer, "三国演义" + i, i + "刘备,张飞,关羽" + i, DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                }
                writer.Optimize();
                writer.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var IndexDic = @"C:\SearchIndex";
            var res = SearchIndex(richTextBox1.Text.Trim(), IndexDic);
            if (res == null || res.Count == 0)
            {
                MessageBox.Show("Test");
                return;
            }
            listBox1.Items.Clear();
            foreach (dynamic item in res)
            {
                listBox1.Items.Add(item.Content);
            }
        }
        private static void AddIndex(IndexWriter writer, string title, string content, string date)
        {
            try
            {
                Document doc = new Document();
                doc.Add(new Field("Uid", title, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                doc.Add(new Field("UName", content, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                doc.Add(new Field("UPwd", date, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                writer.AddDocument(doc);
            }
            catch (FileNotFoundException fnfe)
            {
                throw fnfe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> SearchIndex(string content, string IndexDic)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                FSDirectory directory = FSDirectory.Open(new DirectoryInfo(IndexDic), new NoLockFactory());
                IndexReader reader = IndexReader.Open(directory, true);
                IndexSearcher search = new IndexSearcher(reader);
                string[] fields = { "UName", "UPwd" };
                ////创建查询
                PerFieldAnalyzerWrapper wrapper = new PerFieldAnalyzerWrapper(new PanGuAnalyzer());
                wrapper.AddAnalyzer("UName", new PanGuAnalyzer());
                wrapper.AddAnalyzer("UPwd", new PanGuAnalyzer());
                QueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, fields, wrapper);
                //Query query = parser.Parse(content);
                PhraseQuery query = new PhraseQuery();
                //query.Add(parser.Parse(""));
                //query.Slop = 100;
                //query.Add();
                var sts = PanGu(content).Split('|');
                //foreach (string item in sts)
                //{
                //    //query.Add(new Term("Uid", item));
                //    query.Add(new Term("UName", item));
                //    //query.Add(new Term("UPwd", item));
                //}
                TopScoreDocCollector collector = TopScoreDocCollector.Create(10, true);//10--查询条数

                search.Search(query, null, collector);
                var hits = collector.TopDocs().ScoreDocs;

                int numTotalHits = collector.TotalHits;
                List<dynamic> list = new List<dynamic>();
                for (int i = 0; i < hits.Length; i++)
                {
                    var hit = hits[i];
                    Document doc = search.Doc(hit.Doc);
                    var model = new
                    {
                        Title = doc.Get("Uid").ToString(),
                        Content = doc.Get("UName").ToString(),
                        AddTime = doc.Get("UPwd").ToString()
                    };
                    list.Add(model);
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
    }
}

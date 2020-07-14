using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using TestOA.Model;

namespace TestOA.WebApp.Models
{
    /// <summary>
    /// Lucene搜索索引管理
    /// </summary>
    public sealed class IndexManager
    {
        /// <summary>
        /// 实现单例模式私有字段
        /// </summary>
        private static readonly IndexManager indexManager = new IndexManager();
        /// <summary>
        /// 公有属于
        /// </summary>
        public static IndexManager GetInstance()
        {
            return indexManager;
        }
        /// <summary>
        /// 队列
        /// </summary>
        private Queue<UserInfo> queue = new Queue<UserInfo>();
        /// <summary>
        /// 向队列中添加数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public void AddQueue(UserInfo userInfo)
        {
            queue.Enqueue(userInfo);
        }
        /// <summary>
        /// 要删除的数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteQueue(UserInfo userInfo)
        {
            queue.Enqueue(userInfo);
        }
        /// <summary>
        /// 开始一个线程
        /// </summary>
        public void StartThread()
        {
            Thread thread = new Thread(WriteIndexContent);
            thread.IsBackground = true;
            thread.Start();
        }
        /// <summary>
        /// 写入索引数据
        /// </summary>
        private void WriteIndexContent()
        {
            while (true)
            {
                if (queue.Count > 0)
                {
                    CreateIndexContent();
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }
        /// <summary>
        /// 创建索引的方法
        /// </summary>
        private void CreateIndexContent()
        {
            var IndexDic = @"C:\SerachIndex\";
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
                IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), isCreate, IndexWriter.MaxFieldLength.LIMITED);
                Document doc = null;
                UserInfo user = null;
                while (queue.Count > 0)
                {
                    user = queue.Dequeue();
                    doc = new Document();
                    doc.Add(new Field("Uid", user.Uid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));//存储且索引
                    doc.Add(new Field("UName", user.UName, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                    doc.Add(new Field("UPwd", user.UPwd, Field.Store.YES, Field.Index.NOT_ANALYZED));//存储且索引
                    writer.AddDocument(doc);
                }
                writer.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
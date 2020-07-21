using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace TestOA.WebApp.Controllers
{
    [RoutePrefix("api/File")]
    public class FileController : ApiController
    {
        [HttpGet]
        [Route("DownLoad")]
        public void DownLoad()
        {
            string fileName = "down.txt";
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/"), fileName);
            if (File.Exists(filePath))
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearHeaders();
                response.ClearContent();
                response.Buffer = true;
                response.AddHeader("content-disposition", string.Format("attachment; FileName={0}", fileName));
                response.Charset = "GB2312";
                response.ContentEncoding = Encoding.UTF8;
                response.ContentType = MimeMapping.GetMimeMapping(fileName);
                response.WriteFile(filePath);
                response.Flush();
                response.Close();
            }
        }
        [HttpPost]
        [Route("UpLoad")]
        public object UpLoad()
        {
            var formData = HttpContext.Current.Request.Form;
            var data = formData["FirstName2"];//获取携带的key的值
            var files = HttpContext.Current.Request.Files.GetMultiple("list");//获取标识的文件可为多个
            foreach (var item in files)
            {
                //保存图片
                item.SaveAs(HttpContext.Current.Server.MapPath("~/App_Data/") + item.FileName);
            }
            return Json(formData);
        }
    }
}

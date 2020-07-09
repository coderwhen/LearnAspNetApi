using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace TestOA.WebApp.Models
{
    public class ExceptionAPIFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            Exception ex = actionExecutedContext.Exception;
            MyExceptionAttribute.ExceptionQueue.Enqueue(ex);
        }
    }
}
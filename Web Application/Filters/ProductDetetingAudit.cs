using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_Application.Filters
{
    public class ProductDetetingAudit:ActionFilterAttribute
    {
        private readonly string PATH = Directory.GetCurrentDirectory() + "/Logs/" + DateTime.Now.ToShortDateString() + ".txt";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Before
            File.AppendAllText(PATH, context.HttpContext.Request.Path);
            base.OnActionExecuting(context);
        }

    }
}

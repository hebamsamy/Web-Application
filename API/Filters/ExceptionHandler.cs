using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mail;
using System.Net;

namespace API.Filters
{
    public class ExceptionHandler :ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            //Send Mail With Code
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("6ecff26c8406b7", "83304a61e8f702"),
                EnableSsl = true
            };
            client.Send("from@example.com", "to@example.com", "Error", 
                $"at {DateTime.Now}" + Environment.NewLine +
                $"{context.Exception.Message}" + Environment.NewLine +
                $"{context.Exception.StackTrace}"
                );

            context.Result = new ViewResult()
            {
                ViewName = "Error",
            };
            
            base.OnException(context);
        }
    }
}

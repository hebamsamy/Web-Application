using Models;

namespace Web_Application
{
    public class ProductMiddleWare
    {
        public RequestDelegate next;
        public ProductMiddleWare(RequestDelegate _next)
        {
            this.next = _next;
        }
        public async Task InvokeAsync (HttpContext context)
        {
                MyDBContext myDB = new MyDBContext();
            if (context.Request.Path == "/product")
            {
                await context.Response.WriteAsJsonAsync(myDB.Products.ToList());
            }
            else if (context.Request.Path == "/productByID")
            {
                int ID = int.Parse(context.Request.Query["ID"]);
                var myProduct = myDB.Products.FirstOrDefault(x => x.ID == ID);
                await context.Response.WriteAsync(myProduct.Name); 
            }



            await next(context);
        }
    }
}

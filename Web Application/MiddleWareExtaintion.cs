namespace Web_Application
{
    public static class MiddleWareExtaintion
    {
        public static IApplicationBuilder HandelProductRequest(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ProductMiddleWare>();
        }
    }
}

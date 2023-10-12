namespace Web_Application.Models
{
    public class AddProductViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
    }
}

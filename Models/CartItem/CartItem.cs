using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CartItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public double SupPrice { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public string UserID { get; set; }
        public virtual User User { get; set; }

    }
}

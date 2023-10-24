using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ViewModel
{
    public class ApiResultVewModel<T>
    {
        public int status { get; set; }
        public string massage { get; set; }
        public bool success { get; set; }
        public T data { get; set; }
    }
}

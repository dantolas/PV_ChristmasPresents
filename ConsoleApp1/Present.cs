using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Present
    {
        public string name{ get; private set; }
        public double price { get; private set; }
        public string size { get; private set; }

        public Present(string name, double price, string size)
        {
            this.name = name;
            this.price = price;
            this.size = size;
        }

        public override string ToString()
        {
            return name+", price:"+price+",-kc size:"+size;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework
{
    public class Product
    {
        public Product()
        { }
        public Product(string office, string name, string brand, DateTime purchase_date, int price, string model, string currency, double local_Price)
        {
            this.name = name;
            this.purchase_date = purchase_date;
            this.price = price;
            this.model = model;
            this.currency = currency;
            this.local_Price = local_Price;
            this.brand = brand;
            this.office = office;
        }
        public int Id { get; set; }
        public string name { get; set; }
        public DateTime purchase_date { get; set; }
        public int price { get; set; }
        public string model { get; set; }
        public string office { get; set; }
        public string currency { get; set; }
        public double local_Price { get; set; }
        public string brand { get; set; }
    }
}

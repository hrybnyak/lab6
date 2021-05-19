using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }
        public Supplier Supplier { get; set; }

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product.Id == this.Id
                && product.IsDiscontinued == this.IsDiscontinued
                && product.SupplierId == this.SupplierId
                && product.UnitPrice == this.UnitPrice
                && product.ProductName == this.ProductName
                && product.Package == this.Package;
        }
    }
}

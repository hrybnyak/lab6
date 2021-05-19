using lab6.Context;
using lab6.Entities;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6.MigrationTests
{
    public class MigrationTest
    {
        private class QueryResult1
        {
            public string CompanyName { get; set; }
            public int NumberOfSuppliedProducts { get; set; }

            public override bool Equals(object obj)
            {
                var qr = obj as QueryResult1;
                return qr.CompanyName == this.CompanyName
                    && qr.NumberOfSuppliedProducts == this.NumberOfSuppliedProducts;
            }
        }

        public void MigrationTest1()
        {
            using (var dbContext = new NorthwindDbContext())
            {
                var query = from product in dbContext.Product
                            join supplier in dbContext.Supplier
                            on product.SupplierId equals supplier.Id
                            group product by supplier.CompanyName into productGroup
                            orderby productGroup.Key
                            select new QueryResult1
                            {
                                CompanyName = productGroup.Key,
                                NumberOfSuppliedProducts = productGroup.Count()
                            };
                var actual = query.ToList();
                using (var sw = new StreamWriter("Actual1.csv"))
                {
                    CsvSerializer.SerializeToStream(actual, sw.BaseStream);
                }

                var deserializedExpected = File.ReadAllText("Expected1.csv").FromCsv<List<QueryResult1>>();

                using (var sw = new StreamWriter("Difference1.csv"))
                {
                    var differences = new List<QueryResult1>();
                    for (int i = 0; i < deserializedExpected.Count; i++)
                    {
                        if (!deserializedExpected[i].Equals(actual[i]))
                        {
                            differences.Add(actual[i]);
                        }
                    }
                    sw.Write(CsvSerializer.SerializeToString(differences));
                }
            }
        }

        public void MigrationTest2()
        {
            using (var dbContext = new NorthwindDbContext())
            {
                var query = from product in dbContext.Product
                            where product.UnitPrice >= 50.0M
                            orderby product.UnitPrice
                            select product;
                var actual = query.ToList();
                using (var sw = new StreamWriter("Actual2.csv"))
                {
                    CsvSerializer.SerializeToStream(actual, sw.BaseStream);
                }

                var deserializedExpected = File.ReadAllText("Expected2.csv").FromCsv<List<Product>>();

                using (var sw = new StreamWriter("Difference2.csv"))
                {
                    var differences = new List<Product>();
                    for (int i = 0; i < deserializedExpected.Count; i++)
                    {
                        if (!deserializedExpected[i].Equals(actual[i]))
                        {
                            differences.Add(actual[i]);
                        }
                    }
                    sw.Write(CsvSerializer.SerializeToString(differences));
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ2inClass
{
    class Sample
    {
        static void Samples(string[] args)
        {
            NorthwindDataContext nw = new NorthwindDataContext();

            //Int array
            int[] numbers = { 5, 4, 1, 3, 9, 8, 7, 2, 0 };
            //String array
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var shortDigits = digits.Where((digit, index) => digit.Length < index);

            Console.WriteLine("Short Digits");

            foreach (var d in shortDigits)
            {
                Console.WriteLine("The word {0} is shorter than it's value", d);
            }

            Console.WriteLine();
            Console.WriteLine();

            var textNums = from n in numbers
                           select digits[n];

            foreach (var t in textNums)
            {
                Console.WriteLine(t);
            }

            Console.WriteLine();
            Console.WriteLine();



            var products = from p in nw.Products
                           select new
                           {
                               p.ProductName,
                               p.CategoryID,
                               p.UnitPrice
                           };

            foreach (var w in products)
            {
                Console.WriteLine("{0} is in the category {1} and costs {2}", w.ProductName, w.CategoryID, w.UnitPrice);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Query with the where clause.");
            //Querys with the where clause.
            var orders = from c in nw.Customers
                         from o in c.Orders
                         where o.OrderDate >= new DateTime(1998, 1, 1)
                         select new { c.CustomerID, o.OrderID, o.OrderDate };

            foreach (var order in orders)
            {
                Console.WriteLine("Order with an id of {0}, is for the customer with an id of {1} was placed on {2}",
                    order.OrderID, order.CustomerID, order.OrderDate);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Query with the where clause.");

            Product prod = (from p in nw.Products
                            where p.ProductID == 12
                            select p).FirstOrDefault();

            Console.WriteLine(prod.ProductName);

            Console.WriteLine();
            Console.WriteLine();

            string[] words = { "belief", "relief", "reciept", "field" };

            var iAfterE = words.Any(w => w.Contains("ie"));

            Console.WriteLine("The list coantains a word or words that contain 'ie' is {0}", iAfterE);

            Console.WriteLine();
            Console.WriteLine();

            ////This is broken:
            //var q = from c in nw.Categories
            //        join p in nw.Products on c.CategoryID equals p.CategoryID into ps
            //        from x in ps.DefaultIfEmpty()
            //        select new
            //        {
            //            CategID = c.CategoryID, ProductName = p.CategoryID == null
            //        }

            var cats = from c in nw.Categories
                       select c;

            foreach (var cat in cats)
            {
                Console.WriteLine("The category description is {0}", cat.Description);
            }




            Console.ReadLine();
        }
    }
}

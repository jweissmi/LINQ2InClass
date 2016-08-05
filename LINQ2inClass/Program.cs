using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ2inClass
{
    class Program
    {
        static void Main(string[] args)
        {
            NorthwindDataContext nw = new NorthwindDataContext();

            //The following queries examples are from Microsoft.
            //The examples from MS did not include the foreach loop or the WriteLine.  I added those.
            //See Sample.cs for samples I did while in class.
            //
            // Query #1.
            List<int> numbers = new List<int>() { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // The query variable can also be implicitly typed by using var
            Console.WriteLine("Filtering Query");
            IEnumerable<int> filteringQuery =
                from num in numbers
                where num < 3 || num > 7
                select num;

            foreach (var num in filteringQuery)
            {
                Console.WriteLine(num);
            }

            Console.WriteLine();
            Console.WriteLine();

            // Query #2.
            Console.WriteLine("Ordering Query");
            IEnumerable<int> orderingQuery =
                from num in numbers
                where num < 3 || num > 7
                orderby num ascending
                select num;

            foreach (var num in orderingQuery)
            {
                Console.WriteLine(num);
            }

            Console.WriteLine();
            Console.WriteLine();

            // Query #3.
            Console.WriteLine("Groupping Query");
            string[] groupingQuery = { "carrots", "cabbage", "broccoli", "beans", "barley" };
            IEnumerable<IGrouping<char, string>> queryFoodGroups =
                from item in groupingQuery
                group item by item[0];

            foreach (var item in groupingQuery)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine();

            List<int> numbers1 = new List<int>() { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            List<int> numbers2 = new List<int>() { 15, 14, 11, 13, 19, 18, 16, 17, 12, 10 };

            Console.WriteLine("Average");
            // Query #4.
            var average = numbers1.Average();

            Console.WriteLine(average);

            Console.WriteLine();
            Console.WriteLine();

            // Query #5.
            Console.WriteLine("Concatenation Query");

            var concatenationQuery = numbers1.Concat(numbers2);

            foreach (var item in concatenationQuery)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine();

            // Query #6.
            Console.WriteLine("Large Number Query");

            var largeNumbersQuery = numbers2.Where(c => c > 15);

            foreach (var item in largeNumbersQuery)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine();


            // Query #7.
            Console.WriteLine("Numbers Query");
            // Using a query expression with method syntax
            int numCount1 =
                (from num in numbers1
                 where num < 3 || num > 7
                 select num).Count();

            // Better: Create a new variable to store
            // the method call result
            IEnumerable<int> numbersQuery =
                from num in numbers1
                where num < 3 || num > 7
                select num;

            int numCount2 = numbersQuery.Count();

            foreach (var item in numbersQuery)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            Console.WriteLine("Numbers Query Using var");
            //The previous query can be written by using implicit typing with var, as follows:
            var numCount = from num in numbers1
                           where num < 3 || num > 7
                           select num;

            int numCount3 = numCount.Count();

            foreach (var item in numCount)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Group Join Query");
            //This query creates a group join, and then sorts the groups based on the category element, 
            //which is still in scope. Inside the anonymous type initializer, a sub-query orders all the matching 
            //elements from the products sequence.
            var groupJoinQuery2 =
                 from category in nw.Categories
                 join prod in nw.Products on category.CategoryID equals prod.CategoryID into prodGroup
                 orderby category.CategoryName
                 select new
                 {
                     Category = category.CategoryName,
                     Products = from prod2 in prodGroup
                                orderby prod2.ProductName
                                select prod2
                 };

            foreach (var productGroup in groupJoinQuery2)
            {
                Console.WriteLine(productGroup.Category);
                foreach (var prodItem in productGroup.Products)
                {
                    Console.WriteLine("  {0,-10} {1}", prodItem.ProductName, prodItem.CategoryID);
                }
            }

            Console.ReadLine();
        }
    }
}

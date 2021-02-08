using Business.Concrete;
using DataAccess.Concrete.EntityFrameWork;
using System;

namespace ConsoleUı
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Transformation Objects :DTOs

            


            ProductTest();
            //CategoryTest();
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            foreach (var x in productManager.GetProductDetails())
            {
                Console.WriteLine(x.ProductName + "/" +x.CategoryName);
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var x in categoryManager.GetAll())
            {
                Console.WriteLine(x.CategoryName);
            }
        }
    }
}

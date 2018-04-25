using ModelLayer;
using System;
using System.Linq;

namespace DataLayer
{
    /*
     * This class is used to create and populate the database by default
    */
    public static class DbInitializer
    {
        public static void Initialize(NetCoreProductManagerContext context)
        {
            context.Database.EnsureCreated();

            //Check if there is any products
            if (context.Product.Any())
            {
                //If any products exist than return
                return;   
            }

            //Create array with the categories, add to context and save into the table
            var categories = new Category[]
            {
                new Category(){ Name = "Livros", Created = DateTime.Now },
                new Category(){ Name = "Games", Created = DateTime.Now },
                new Category(){ Name = "Periféricos", Created = DateTime.Now }
            };

            foreach (var category in categories)
            {
                context.Category.Add(category);
            }
            context.SaveChanges();

            /*
             * Create array with the categories, add to context and save into the table
            */
            var products = new Product[]
            {
                new Product(){ Name = "Livro Game of Thrones", IdCategory = 1, Price = 55, Created = DateTime.Now },
                new Product(){ Name = "The Witcher 3: Wild Hunt", IdCategory = 2, Price = 99.90m, Created = DateTime.Now },
                new Product(){ Name = "Fone de Ouvido JBL", IdCategory = 3, Price = 159.90m, Created = DateTime.Now }
            };

            foreach (var product in products)
            {
                context.Product.Add(product);
            }
            context.SaveChanges();
        }
    }
}

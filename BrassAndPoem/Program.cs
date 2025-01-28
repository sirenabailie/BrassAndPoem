using System;
using System.Collections.Generic;

namespace BrassAndPoem
{
    public class Program
    {
        static void Main(string[] args)
        {
            string greeting = @"
            Welcome to Brass & Poem!";

            Console.WriteLine(greeting);

            // Create a list of ProductTypes
            List<ProductType> productTypes = new List<ProductType>
            {
                new ProductType { Id = 1, Title = "Brass" },
                new ProductType { Id = 2, Title = "Poem" }
            };

            // Create a list of Products
            List<Product> products = new List<Product>
            {
                new Product { Name = "Tuba", Price = 1900.99m, ProductTypeId = 1 },
                new Product { Name = "French Horn", Price = 750.00m, ProductTypeId = 1 },
                new Product { Name = "Saxophone", Price = 700.00m, ProductTypeId = 1 },
                new Product { Name = "The Bush Gatden", Price = 15.00m, ProductTypeId = 2 },
                new Product { Name = "Why Did You Vanish?", Price = 10.00m, ProductTypeId = 2 }
            };

            string choice;
            do
            {
                Console.WriteLine(@"
                Choose an option:
                1. View All Products
                2. Add a Product
                3. Delete a Product
                4. Update a Product
                0. Exit");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        DisplayAllProducts(products, productTypes);
                        break;
                    case "2":
                        Console.Clear();
                        AddProduct(products, productTypes);
                        break;
                    case "3":
                        Console.Clear();
                        DeleteProduct(products, productTypes);
                        break;
                    case "4":
                        Console.Clear();
                        UpdateProduct(products, productTypes);
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Goodbye!✨");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                // Pause before returning to the menu
                if (choice != "0")
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            while (choice != "0");
        }

        static void DisplayAllProducts(List<Product> products, List<ProductType> productTypes)
        {
            Console.WriteLine("\nAll Products:");
            foreach (var product in products)
            {
                var productType = productTypes.Find(pt => pt.Id == product.ProductTypeId);
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price:C}, Type: {productType?.Title}");
            }
        }

        static void AddProduct(List<Product> products, List<ProductType> productTypes)
        {
            Console.Write("\nEnter the name of the new product: ");
            string newName = Console.ReadLine();

            Console.Write("Enter the price of the new product: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
            {
                Console.Write("Enter the product type ID: ");
                if (int.TryParse(Console.ReadLine(), out int typeId) && productTypes.Exists(pt => pt.Id == typeId))
                {
                    products.Add(new Product { Name = newName, Price = newPrice, ProductTypeId = typeId });
                    Console.WriteLine("New product added successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid product type ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid price.");
            }
        }

        static void DeleteProduct(List<Product> products, List<ProductType> productTypes)
        {
            Console.Write("\nEnter the name of the product to delete: ");
            string nameToDelete = Console.ReadLine();

            var productToDelete = products.Find(p => p.Name.Equals(nameToDelete, StringComparison.OrdinalIgnoreCase));

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
                Console.WriteLine($"Product '{nameToDelete}' has been deleted.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void UpdateProduct(List<Product> products, List<ProductType> productTypes)
        {
            Console.Write("\nEnter the name of the product to update: ");
            string nameToUpdate = Console.ReadLine();

            var productToUpdate = products.Find(p => p.Name.Equals(nameToUpdate, StringComparison.OrdinalIgnoreCase));

            if (productToUpdate != null)
            {
                Console.Write("Enter the new name (or press Enter to keep the current name): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName)) productToUpdate.Name = newName;

                Console.Write("Enter the new price (or press Enter to keep the current price): ");
                string newPriceInput = Console.ReadLine();
                if (decimal.TryParse(newPriceInput, out decimal newPrice)) productToUpdate.Price = newPrice;

                Console.Write("Enter the new product type ID (or press Enter to keep the current type): ");
                string newTypeIdInput = Console.ReadLine();
                if (int.TryParse(newTypeIdInput, out int newTypeId) && productTypes.Exists(pt => pt.Id == newTypeId))
                    productToUpdate.ProductTypeId = newTypeId;

                Console.WriteLine("Product updated successfully!");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
    }
}

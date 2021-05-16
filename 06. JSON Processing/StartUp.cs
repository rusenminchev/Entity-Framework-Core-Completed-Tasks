using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.Dto;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<ProductShopProfile>());

            using (var db = new ProductShopContext())
            {
                Console.WriteLine(GetUsersWithProducts(db));

                File.WriteAllText("./../../../UsersWithSoldProducts.json", GetUsersWithProducts(db));
            }
        }

        //01.
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            {
                var users = JsonConvert.DeserializeObject<User[]>(inputJson);

                context.Users.AddRange(users);

                context.SaveChanges();

                return $"Successfully imported {users.Length}";
            }
        }

        //02.
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            {
                var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

                context.Products.AddRange(products);

                context.SaveChanges();

                return $"Successfully imported {products.Length}";
            }
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            {
                var categories = JsonConvert.DeserializeObject<Category[]>(inputJson);

                context.Categories.AddRange(categories);

                context.SaveChanges();

                return $"Successfully imported {categories.Length}";
            }
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            {
                var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

                context.CategoryProducts.AddRange(categoryProducts);

                context.SaveChanges();

                return $"Successfully imported {categoryProducts.Length}";
            }
        }

        //05.
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .OrderBy(p => p.Price)
                .ToList();

            var resolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var productsToJson = JsonConvert.SerializeObject(products, new JsonSerializerSettings
            {
                ContractResolver = resolver,
                Formatting = Formatting.Indented
            });

            return productsToJson;
        }

        //06.
        public static string GetSoldProducts(ProductShopContext context)
        {
            var userWithSoldProducts = context
                .Users
                .Where(u => u.ProductsSold.Any())
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    soldProducts = u.ProductsSold.Select(p => new
                    {
                        p.Name,
                        p.Price,
                        BuyerFirstName = p.Buyer.FirstName,
                        BuyerLastName = p.Buyer.LastName
                    })
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToList();

            var resolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(userWithSoldProducts, new JsonSerializerSettings()
            {
                ContractResolver = resolver,
                Formatting = Formatting.Indented
            });

            return json;
        }

        //07.
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    averagePrice = c.CategoryProducts.Average(cp => cp.Product.Price).ToString("F2"),
                    totalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price).ToString("F2")
                })
                .ToList();

            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return json;
        }

        //08.
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            //var users = context
            //    .Users
            //    .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
            //    .OrderByDescending(u => u.ProductsSold.Count)
            //    .Select(u => new
            //    {
            //        lastName = u.LastName,
            //        age = u.Age,
            //        soldProducts = new
            //        {
            //            count = u.ProductsSold.Count,
            //            products = u.ProductsSold
            //            .Where(p=>p.Buyer != null)
            //            .Select(p => new
            //            {
            //                name = p.Name,
            //                price = p.Price
            //            })
            //        }
            //    })
            //    .ToList();

            //Solved with AutoMapping

            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                .ProjectTo<UserDetailsDto>()
                .OrderByDescending(u => u.SoldProducts.Count)
                .ToArray();

            var usersOutput = Mapper.Map<UserInfoDto>(users);

            var resolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var json = JsonConvert.SerializeObject(usersOutput, new JsonSerializerSettings()
            {
                ContractResolver = resolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            return json;
        }
    }
}
namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            //using (var db = new BookShopContext())
            //{
            //    DbInitializer.ResetDatabase(db);
            //}

            var context = new BookShopContext();

            string input = Console.ReadLine();

            Console.WriteLine(GetBooksByCategory(context,input));
        }

        //01.
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().TrimEnd();
        }
        
        //02.
        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var goldenBooks = context
                .Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            foreach (var book in goldenBooks)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().TrimEnd();
        }

        //03.
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price}");
            }

            return sb.ToString().TrimEnd();
        }

        //04.
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
               .Books
               .Where(b => b.ReleaseDate.Value.Year != year)
               .OrderBy(b => b.BookId)
               .Select(b => b.Title)
               .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().TrimEnd();
        }

        //05.
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var books = context
               .Books
               .Where(b => b.BookCategories.Any(bc => input.Contains(bc.Category.Name.ToLower())))
               .Select(b => b.Title)
               .OrderBy(b => b)
               .ToList();

            return string.Join(Environment.NewLine, books);
        }

        //06.
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
               .Books
               .Where(b => b.ReleaseDate < DateTime.Parse(date))
               .OrderByDescending(b => b.ReleaseDate)
               .Select(b => new
               { 
                    b.Title,
                    b.EditionType,
                    b.Price            
               })
               .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - {book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //07.
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var authors = context
               .Authors
               .Where(a => a.FirstName.EndsWith(input))
               .Select(a => new
               {
                   fullName = a.FirstName + ' ' + a.LastName
               })
               .OrderBy(a => a.fullName)
               .ToList();

            foreach (var author in authors)
            {
                sb.AppendLine(author.fullName);
            }

            return sb.ToString().TrimEnd();
        }

        //08.
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
               .Books
               .Where(b => b.Title.ToLower().Contains(input.ToLower()))
               .OrderBy(b => b.Title)
               .Select(b => b.Title) 
               .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().TrimEnd();
        }

        //09.
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
               .Books
               .Where(a => a.Author.LastName.ToLower().StartsWith(input.ToLower()))
               .OrderBy(b => b.BookId)
               .Select(b => new
               {
                   bookTitle = b.Title,
                   authorNames = b.Author.FirstName + " " + b.Author.LastName
               })
               .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.bookTitle} ({book.authorNames})");
            }

            return sb.ToString().TrimEnd();
        }

        //10.
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var numberOfBooks = context
                  .Books
                  .Where(b => b.Title.Length > lengthCheck)
                  .Count();

            return numberOfBooks;
        }

        //11.
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var authors = context
               .Authors
               .Select(a => new
               {
                   authorNames = a.FirstName + " " + a.LastName,
                   totalCopies = a.Books.Sum(b=>b.Copies)
               })
               .OrderByDescending(b=>b.totalCopies)
               .ToList();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.authorNames} - {author.totalCopies}");
            }

            return sb.ToString().TrimEnd();
        }

        //12.
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context
                .Categories
                .Select(c => new
                {
                    categorieName = c.Name,
                    totalProfit = c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price)
                })
                .OrderByDescending(c=>c.totalProfit)
                .ThenBy(c=>c.categorieName)
                .ToList();

            foreach (var category in categories)
            {
                sb.AppendLine($"{category.categorieName} - {category.totalProfit}");
            }

            return sb.ToString().TrimEnd();
        }

        //13.
        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context
                .Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    books = c.CategoryBooks.Select(cb => new
                    {
                        cb.Book.Title,
                        cb.Book.ReleaseDate.Value.Year
                    } )
                    .OrderByDescending(b => b.Year)
                    .Take(3)
                    .ToList()
                })    
                .ToList();

            foreach (var category in categories)
            {   
                sb.AppendLine($"--{category.Name}");

                foreach (var book in category.books)
                {
                    sb.AppendLine($"{book.Title} ({book.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //14.

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.
                Books
                .Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        //15.
        public static int RemoveBooks(BookShopContext context)
        {
            var booksToRemove = context
                .Books
                .Where(b => b.Copies < 4200);

            int count = booksToRemove.Count();

            context.Books.RemoveRange(booksToRemove);

            context.SaveChanges();

            return count;   
        }
    }
}

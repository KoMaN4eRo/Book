using BookTests.Model;
using BookTests.Model.BookModel;
using SimpleInjector;
using System;
using System.Linq;

namespace BookTests
{
    public class Program
    {
        static readonly Container container;

        static Program()
        {
            container = new Container();
            container.Register<IBookRepository, BookRepository>();
            container.Register<BookRepository>();

            container.Verify();
        }

        static void Main(string[] args)
        {
            var service = container.GetInstance<IBookRepository>();
            using (ApplicationContext db = new ApplicationContext())
            {
                var books = db.Books.ToList();
                Console.WriteLine("Список объектов:");
                foreach (Book b in books)
                {
                    Console.WriteLine($"{b.BookiId}.{b.Author} - {b.Title} - {b.EditionDate}");
                }
            }

            Console.ReadKey();
        }
    }
}

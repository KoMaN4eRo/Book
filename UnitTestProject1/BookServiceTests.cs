using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookTests.Model;
using System.Data.Entity;
using BookTests.Model.BookModel;
using BookTests;
using BookTests.Infrastructure;
using System.Linq;

namespace BookTests.Tests
{
    [TestClass]
    public class BookServiceTests
    {
        ApplicationContext context { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationContext>());
            context = new ApplicationContext();
            context.Database.Initialize(true);
        }

        [TestMethod]
        public void CreateBook_Test()
        {
            Book book1 = new Book { Author = "Author", Title = "Title1", EditionDate = DateTimeOffset.Parse("05/01/2008")};
            Book book2 = new Book { Author = "Author", Title = "Title2", EditionDate = DateTimeOffset.Parse("06/02/2008")};
            Book book3 = new Book { Author = "Author", Title = "Title3", EditionDate = DateTimeOffset.Parse("07/03/2008")};

            BookService service = new BookService(new BookRepository(context), context, new IsValid(context));

            service.CreateBook("Author", "Title1", "05", "01", "2008");
            service.CreateBook("Author", "Title2", "06", "02", "2008");
            service.CreateBook("Author", "Title3", "07", "03", "2008");

            var targetBook1 = context.Books.Where(b => b.Title.Equals("Title1")).First();
            var targetBook2 = context.Books.Where(b => b.Title.Equals("Title2")).First();
            var targetBook3 = context.Books.Where(b => b.Title.Equals("Title3")).First();

            Assert.AreEqual(3, context.Books.ToList().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateBook_Author_Failed_Test()
        {
            Book book1 = new Book { Author = "Author", Title = "Title1", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book2 = new Book { Author = "Author", Title = "Title2", EditionDate = DateTimeOffset.Parse("06/02/2008") };
            Book book3 = new Book { Author = "Author", Title = "Title3", EditionDate = DateTimeOffset.Parse("07/03/2008") };

            BookService service = new BookService(new BookRepository(context), context, new IsValid(context));

            service.CreateBook("", "Title1", "05", "01", "2008");
            service.CreateBook("", "Title2", "06", "02", "2008");
            service.CreateBook("", "Title3", "07", "03", "2008");

            Assert.AreEqual(3, context.Books.ToList().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(NonNumericSequenceException))]
        public void CreateBook_EdditionDate_Failed_Test()
        {
            Book book1 = new Book { Author = "Author", Title = "Title1", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book2 = new Book { Author = "Author", Title = "Title2", EditionDate = DateTimeOffset.Parse("06/02/2008") };
            Book book3 = new Book { Author = "Author", Title = "Title3", EditionDate = DateTimeOffset.Parse("07/03/2008") };

            BookService service = new BookService(new BookRepository(context), context, new IsValid(context));

            service.CreateBook("Author", "Title1", "05", "01", "2008/**asdfsdf");
            service.CreateBook("Author", "Title2", "06", "", "2008");
            service.CreateBook("Author", "Title3", "", "03", "2008");

            Assert.AreEqual(3, context.Books.ToList().Count());
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookTests.Model;
using System.Data.Entity;
using Moq;
using BookTests.Model.BookModel;
using System.Linq;
using BookTests;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class BookRepositoryTests
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
        public void CreateBookTest_Verify_Add_And_Save()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(m => m.Books).Returns(mockSet.Object);

            var bookRepositry = new BookRepository(mockContext.Object);
            bookRepositry.Create(new Book { Author = "Author500", Title = "Title5", EditionDate = DateTimeOffset.Parse("05/01/2008") });

            mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void CreateBookTest_Test()
        {
            Book book1 = new Book { Author = "Author", Title = "Title1", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book2 = new Book { Author = "Author", Title = "Title2", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book3 = new Book { Author = "Author", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") };

            var bookRepository = new BookRepository(context);
            bookRepository.Create(book1);
            bookRepository.Create(book2);
            bookRepository.Create(book3);

            var targetBook1 = context.Books.Where(b => b.Title.Equals("Title1")).First();
            var targetBook2 = context.Books.Where(b => b.Title.Equals("Title2")).First();
            var targetBook3 = context.Books.Where(b => b.Title.Equals("Title3")).First();

            Assert.AreEqual(book1, targetBook1);
            Assert.AreEqual(book2, targetBook2);
            Assert.AreEqual(book3, targetBook3);
            Assert.AreEqual(3, context.Books.ToList().Count());
        }

        [TestMethod]
        public void SearchBookByAuthor_Test()
        {
            Book book1 = new Book { Author = "Author", Title = "Title1", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book2 = new Book { Author = "Author", Title = "Title2", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book3 = new Book { Author = "Author", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book4 = new Book { Author = "Author", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book5 = new Book { Author = "Author1", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book6 = new Book { Author = "Author1", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book7 = new Book { Author = "Author1", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book8 = new Book { Author = "Author1", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") };

            List<Book> books1 = new List<Book>();
            books1.Add(book1);
            books1.Add(book2);
            books1.Add(book3);
            books1.Add(book4);

            List<Book> books2 = new List<Book>();
            books2.Add(book5);
            books2.Add(book6);
            books2.Add(book7);
            books2.Add(book8);

            var bookRepository = new BookRepository(context);
            bookRepository.Create(book1);
            bookRepository.Create(book2);
            bookRepository.Create(book3);
            bookRepository.Create(book4);
            bookRepository.Create(book5);
            bookRepository.Create(book6);
            bookRepository.Create(book7);
            bookRepository.Create(book8);

            var targetList1 = bookRepository.SearchByAuthor("Author");
            var targetList2 = bookRepository.SearchByAuthor("Author1");

            CollectionAssert.AreEqual(books1, targetList1);
            CollectionAssert.AreEqual(books2, targetList2);
        }

        [TestMethod]
        public void SearchByTitle_Test()
        {
            Book book1 = new Book { Author = "Author", Title = "Title1", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book2 = new Book { Author = "Author", Title = "Title2", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book3 = new Book { Author = "Author", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book4 = new Book { Author = "Author", Title = "Title4", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book5 = new Book { Author = "Author1", Title = "Title5", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book6 = new Book { Author = "Author1", Title = "Title6", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book7 = new Book { Author = "Author1", Title = "Title7", EditionDate = DateTimeOffset.Parse("05/01/2008") };
            Book book8 = new Book { Author = "Author1", Title = "Title8", EditionDate = DateTimeOffset.Parse("05/01/2008") };

            var bookRepository = new BookRepository(context);
            bookRepository.Create(book1);
            bookRepository.Create(book2);
            bookRepository.Create(book3);
            bookRepository.Create(book4);
            bookRepository.Create(book5);
            bookRepository.Create(book6);
            bookRepository.Create(book7);
            bookRepository.Create(book8);

            var targetBook1 = bookRepository.SearchByTitle("Title1");
            var targetBook3 = bookRepository.SearchByTitle("Title3");
            var targetBook5 = bookRepository.SearchByTitle("Title5");
            var targetBook7 = bookRepository.SearchByTitle("Title7");

            Assert.AreEqual(book1, targetBook1);
            Assert.AreEqual(book3, targetBook3);
            Assert.AreEqual(book5, targetBook5);
            Assert.AreEqual(book7, targetBook7);
        }

        [TestMethod]
        public void SearchByEditionDate_Test()
        {
            Book book1 = new Book { Author = "Author",  Title = "Title1", EditionDate = DateTimeOffset.Parse("06/01/2008") };
            Book book2 = new Book { Author = "Author",  Title = "Title2", EditionDate = DateTimeOffset.Parse("06/01/2008") };
            Book book3 = new Book { Author = "Author",  Title = "Title3", EditionDate = DateTimeOffset.Parse("06/01/2008") };
            Book book4 = new Book { Author = "Author",  Title = "Title4", EditionDate = DateTimeOffset.Parse("06/02/2008") };
            Book book5 = new Book { Author = "Author1", Title = "Title5", EditionDate = DateTimeOffset.Parse("06/02/2008") };
            Book book6 = new Book { Author = "Author1", Title = "Title6", EditionDate = DateTimeOffset.Parse("06/02/2008") };
            Book book7 = new Book { Author = "Author1", Title = "Title7", EditionDate = DateTimeOffset.Parse("06/02/2016") };
            Book book8 = new Book { Author = "Author1", Title = "Title8", EditionDate = DateTimeOffset.Parse("06/02/2016") };

            List<Book> list1 = new List<Book>();
            list1.Add(book1);
            list1.Add(book2);
            list1.Add(book3);

            List<Book> list2 = new List<Book>();
            list2.Add(book4);
            list2.Add(book5);
            list2.Add(book6);

            List<Book> list3 = new List<Book>();
            list3.Add(book7);
            list3.Add(book8);

            var bookRepository = new BookRepository(context);
            bookRepository.Create(book1);
            bookRepository.Create(book2);
            bookRepository.Create(book3);
            bookRepository.Create(book4);
            bookRepository.Create(book5);
            bookRepository.Create(book6);
            bookRepository.Create(book7);
            bookRepository.Create(book8);

            var targetList1 = bookRepository.SearchByEditionDate("06", "01", "2008");
            var targetList2 = bookRepository.SearchByEditionDate("06", "02", "2008");
            var targetList3 = bookRepository.SearchByEditionDate("06", "02", "2016");

            CollectionAssert.AreEqual(list1, targetList1);
            CollectionAssert.AreEqual(list2, targetList2);
            CollectionAssert.AreEqual(list3, targetList3);
        }
    }
}

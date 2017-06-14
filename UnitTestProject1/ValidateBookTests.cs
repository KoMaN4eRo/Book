using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookTests;
using BookTests.Model.BookModel;
using BookTests.Model;
using System.Data.Entity;
using BookTests.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class ValidateBookTests
    {
        private IQueryable<Book> data { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            data = new List<Book>
            {
                new Book { Author = "Author",  Title = "Title1", EditionDate = DateTimeOffset.Parse("05/01/2008") },
                new Book { Author = "Author",  Title = "Title2", EditionDate = DateTimeOffset.Parse("05/01/2008") },
                new Book { Author = "Author",  Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") },
                new Book { Author = "Author",  Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") },
                new Book { Author = "Author1", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") },
                new Book { Author = "Author1", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") },
                new Book { Author = "Author1", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") },
                new Book { Author = "Author1", Title = "Title3", EditionDate = DateTimeOffset.Parse("05/01/2008") }
            }.AsQueryable();

        }

        [TestMethod]
        public void ValidateCorrectBook()
        {

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(()=> data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var validateBook = new IsValid(mockContext.Object);
            validateBook.ValidateBook("Author", "Title", "01", "12", "1996");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_Empty_Author()
        {

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var validateBook = new IsValid(mockContext.Object);
            validateBook.ValidateBook("", "Title", "01", "12", "1996");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_Empty_Title()
        {

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var validateBook = new IsValid(mockContext.Object);
            validateBook.ValidateBook("Author", "", "01", "12", "1996");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_Empty_Day()
        {

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var validateBook = new IsValid(mockContext.Object);
            validateBook.ValidateBook("Author", "Title", "", "12", "1996");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_Empty_Mounth()
        {

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var validateBook = new IsValid(mockContext.Object);
            validateBook.ValidateBook("Author", "Title", "01", "", "1996");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_Empty_Year()
        {

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var validateBook = new IsValid(mockContext.Object);
            validateBook.ValidateBook("Author", "Title", "01", "12", "");
        }

        [TestMethod]
        [ExpectedException(typeof(NonNumericSequenceException))]
        public void Validate_NonNumeric_Day()//Field day contain non-numeric elements
        {

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var validateBook = new IsValid(mockContext.Object);
            validateBook.ValidateBook("Author", "Title", "01* ///", "12", "1996");
        }

        [TestMethod]
        [ExpectedException(typeof(NonNumericSequenceException))]
        public void Validate_NonNumeric_Mounths()//Field mounths contain non-numeric elements
        {

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var validateBook = new IsValid(mockContext.Object);
            validateBook.ValidateBook("Author", "Title", "01", "12* ///", "1996");
        }

        [TestMethod]
        [ExpectedException(typeof(NonNumericSequenceException))]
        public void Validate_NonNumeric_Year()//Field mounths contain non-numeric elements
        {

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var validateBook = new IsValid(mockContext.Object);
            validateBook.ValidateBook("Author", "Title", "01", "12", "1996* ///");
        }
    }
}

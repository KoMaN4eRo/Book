using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTests.Model.BookModel
{
    public class BookRepository : IBookRepository
    {
        public ApplicationContext db;

        public BookRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public void Create(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        public List<Book> SearchByAuthor(string author)
        {
            return db.Books.Where(b => b.Author.Equals(author)).ToList();
        }

        public Book SearchByTitle(string title)
        {
            return db.Books.Where(b => b.Title.Equals(title)).FirstOrDefault();
        }

        public List<Book> SearchByEditionDate(string day, string mounth, string year)
        {
            var EditionDate = DateTimeOffset.Parse($"{day}/{mounth}/{year}");
            return db.Books.Where(b => b.EditionDate == EditionDate).ToList();
        }
    }
}

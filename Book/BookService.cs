using BookTests.Infrastructure;
using BookTests.Model;
using BookTests.Model.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTests
{
    public class BookService
    {
        public IBookRepository repo;
        public ApplicationContext context;
        public IsValid valid;
        public BookService(IBookRepository bookRepo, ApplicationContext context, IsValid valid)
        {
            this.context = context;
            this.valid = valid;
            repo = bookRepo; 
        }

        public void CreateBook(string author, string title, string day, string mounts, string year)
        {
            valid.ValidateBook(author, title, day, mounts, year);
            repo.Create(new Book { Author = author, Title = title, EditionDate = DateTimeOffset.Parse($"{day}/{mounts}/{year}")});
        }

        public List<Book> SearchByAuthor(string author)
        {
            valid.ValidateAuthor(author);
            return repo.SearchByAuthor(author);
        }

        public Book SearchByTitle(string title)
        {
            valid.ValidateTitle(title);
            return repo.SearchByTitle(title);
        }

        public List<Book> SearchByEditionDate(string day, string mounth, string year)
        {
            valid.ValidateEditionDate(day, mounth, year);
            return repo.SearchByEditionDate(day, mounth, year);
        }
    }
}

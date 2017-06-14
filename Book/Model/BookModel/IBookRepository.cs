using System.Collections.Generic;

namespace BookTests.Model.BookModel
{
    public interface IBookRepository
    {
        void Create(Book book);
        List<Book> SearchByAuthor(string author);
        Book SearchByTitle(string title);
        List<Book> SearchByEditionDate(string day, string mounth, string year);
    }
}

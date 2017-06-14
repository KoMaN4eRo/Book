using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookTests.Model.BookModel;

namespace BookTests.Model
{
    public class ApplicationContext: DbContext
    {
            public ApplicationContext()
                : base("DbConnection")
            { }

            public virtual DbSet<Book> Books { get; set; }
    }
}

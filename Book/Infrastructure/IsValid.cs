using BookTests.Model;
using System;
using System.Linq;

namespace BookTests.Infrastructure
{
    public class IsValid
    {
        private ApplicationContext context;

        public IsValid(ApplicationContext context)
        {
            this.context = context;
        }

        public void ValidateAuthor(string author)
        {
            if(String.IsNullOrEmpty(author))
                throw new ValidationException("Field author is empty", "Author");
        }

        public void ValidateTitle(string title)
        {
            if (String.IsNullOrEmpty(title))
                throw new ValidationException("Field title is empty", "Title");

            if (context.Books.Where(b => b.Title == title).Any())
            {
                throw new ValidationException($"Book with field {title} is already contains", "TitleDuplicated");
            }
        }

        public void ValidateEditionDate(string day, string mounths, string year)
        {
            if (String.IsNullOrEmpty(day))
            {
                throw new ValidationException("Field day is empty", "Day");
            }

            if (String.IsNullOrEmpty(mounths))
            {
                throw new ValidationException("Field mounths is empty", "Mounths");
            }

            if (String.IsNullOrEmpty(year))
            {
                throw new ValidationException("Field year is empty", "Year");
            }



            if (!day.All(char.IsDigit))
            {
                throw new NonNumericSequenceException($"Field day contain non-numeric characters", "Non-Numeric characters");
            }

            if (!mounths.All(char.IsDigit))
            {
                throw new NonNumericSequenceException($"Field mounths contain non-numeric characters", "Non-Numeric characters");
            }

            if (!year.All(char.IsDigit))
            {
                throw new NonNumericSequenceException($"Field year contain non-numeric characters", "Non-Numeric characters");
            }
        }

        public void ValidateBook(string author, string title, string day, string mounths, string year)
        {
            ValidateAuthor(author);
            ValidateTitle(title);
            ValidateEditionDate(day, mounths, year);
        }
    }
}

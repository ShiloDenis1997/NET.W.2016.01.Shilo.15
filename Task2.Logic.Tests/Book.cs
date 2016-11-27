using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic.Tests
{
    /// <summary>
    /// Represents behavior of a book
    /// </summary>
    public class Book : IEquatable<Book>, IComparable, IComparable<Book>
    {
        private decimal price;

        /// <summary>
        /// Books name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Author of the book
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Year, when book was published
        /// </summary>
        public int PublishedYear { get; }

        /// <summary>
        /// Price of the book
        /// </summary>
        /// <exception cref="ArgumentException">Throws if value is 
        /// less or equal to zero</exception>
        public decimal Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException
                        ($"value for {nameof(Price)}" +
                         "is less or equal to zero");
                price = value;
            }
        }

        /// <summary>
        /// Constructs a book
        /// </summary>
        /// <exception cref="ArgumentException">Throws if one of the parameters is invalid</exception>
        public Book(string name, string author, int publishedYear, decimal price)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(
                    $"{nameof(name)} is empty, whitespace or null");
            if (string.IsNullOrEmpty(author))
                throw new ArgumentException(
                    $"{nameof(author)} is empty, whitespace or null");
            if (publishedYear <= 0)
                throw new ArgumentException(
                    $"{nameof(publishedYear)} is less or equal to zero");
            Name = name;
            Author = author;
            PublishedYear = publishedYear;
            Price = price;
        }


        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(other, this))
                return true;
            if (!(other.GetType() == GetType()))
                return false;
            return Name.Equals(other.Name) && Author.Equals(other.Author)
                   && PublishedYear == other.PublishedYear && Price == other.Price;
        }

        /// <summary>
        /// Compare books by <see cref="Name"/>, if equals then by 
        /// <see cref="Author"/>, if equals then by <see cref="PublishedYear"/>,
        /// if equals then by <see cref="Price"/>.
        /// </summary>
        public int CompareTo(Book other)
        {
            if (other == null)
                return 1;
            int ret = string.Compare
                (Name, other.Name);
            if (ret != 0)
                return ret;
            ret = string.Compare
                (Author, other.Author);
            if (ret != 0)
                return ret;
            ret = PublishedYear.CompareTo(other.PublishedYear);
            if (ret != 0)
                return ret;
            return Price.CompareTo(other.Price);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
                return true;
            if (ReferenceEquals(obj, null))
                return false;
            Book book = obj as Book;
            if (book == null)
                return false;
            return Equals(book);
        }

        public override int GetHashCode()
        {
            return ((Name.GetHashCode() << 5) ^ Author.GetHashCode() << 7)
                ^ PublishedYear;
        }

        public override string ToString()
        {
            return $"{nameof(Name)} = {Name}\n" +
                   $"{nameof(Author)} = {Author}\n" +
                   $"{nameof(Price)} = {Price}\n" +
                   $"{nameof(PublishedYear)} = {PublishedYear}";
        }

        /// <summary>
        /// Uses <see cref="CompareTo"/>
        /// </summary>
        /// <exception cref="ArgumentException">Throws if 
        /// <paramref name="obj"/> cannot be casted to <see cref="Book"/></exception>
        /// <exception cref="ArgumentException">Throws if <paramref name="obj"/> is
        /// not a type of <see cref="Book"/></exception>
        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            Book book = obj as Book;
            if (book == null)
            {
                throw new ArgumentException($"{nameof(obj)} has type" +
                                            $"{obj.GetType()}. It cannot be compared" +
                                            $"with {GetType()}");
            }
            return CompareTo(book);
        }
    }
}
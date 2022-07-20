using System.Collections.Generic;
using System.Linq;
using BookInvoicing.Domain.Book;
using BookInvoicing.Domain.Country;

namespace BookInvoicing.Tests.Builders
{
    public interface BookBuilder<T> where T:IBook
    {
        public T Build();
    }

    public static class BookBuilder
    {
        public static EducationalBookBuilder AnEducationalBook => new EducationalBookBuilder(
            "An educational book", 20, AuthorBuilder.AnAuthor, Language.English, Category.Business
        );

        public static EducationalBookBuilder CleanCode => new EducationalBookBuilder(
            "Clean Code", 20, AuthorBuilder.UncleBob, Language.English, Category.Computer
        );

        public static NovelBookBuilder ANovel => new NovelBookBuilder("An educational book", 20, AuthorBuilder.AnAuthor, Language.English, Genre.Mystery, Genre.DarkFantasy);
    }

    public class NovelBookBuilder
    {
        private string _name;
        private double _price;
        private AuthorBuilder _author;
        private Language _language;
        private List<Genre> _genre;

        internal NovelBookBuilder(string name, double price, AuthorBuilder author, Language language, params Genre[] genre)
        {
            _name = name;
            _price = price;
            _author = author;
            _language = language;
            _genre = genre.ToList();
        }

        public NovelBookBuilder Named(string name)
        {
            _name = name;
            return this;
        }

        public NovelBookBuilder Costing(double price)
        {
            _price = price;
            return this;
        }

        public NovelBookBuilder WrittenBy(AuthorBuilder author)
        {
            _author = author;
            return this;
        }

        public NovelBookBuilder WrittenIn(Language language)
        {
            _language = language;
            return this;
        }

        public NovelBookBuilder InGenres(params Genre[] genres)
        {
            _genre = genres.ToList();
            return this;
        }

        public Novel Build()
        {
            return new Novel(_name, _price, _author.Build(), _language, _genre);
        }
    }

    public class EducationalBookBuilder : BookBuilder<EducationalBook>
    {
        private string _name;
        private double _price;
        private AuthorBuilder _author;
        private Language _language;
        private Category _category;

        public EducationalBookBuilder(string name, double price, AuthorBuilder author, Language language, Category category)
        {
            _name = name;
            _price = price;
            _author = author;
            _language = language;
            _category = category;
        }

        public EducationalBookBuilder Named(string name)
        {
            _name = name;
            return this;
        }

        public EducationalBookBuilder Costing(double price)
        {
            _price = price;
            return this;
        }

        public EducationalBookBuilder WrittenBy(AuthorBuilder author)
        {
            _author = author;
            return this;
        }

        public EducationalBookBuilder WrittenIn(Language language)
        {
            _language = language;
            return this;
        }

        public EducationalBookBuilder InCategory(Category category)
        {
            _category = category;
            return this;
        }


        public EducationalBook Build()
        {
            return new EducationalBook(_name, _price, _author.Build(), _language, _category);
        }
    }
}

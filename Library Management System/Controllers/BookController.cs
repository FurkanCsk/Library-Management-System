using Library_Management_System.Entities;
using Library_Management_System.Models;
using Library_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;
using static System.Net.WebRequestMethods;

namespace Library_Management_System.Controllers
{
    public class BookController : Controller
    {
        public void AuthorViewBag()
        {
            var authors = AuthorController._authors;

            // If there are no authors, create an empty list for the ViewBag.
            if (authors == null || !authors.Any())
            {
                ViewBag.Authors = new List<SelectListItem>();
            }
            else
            {
                // Populate the ViewBag with authors as SelectListItems.
                ViewBag.Authors = authors.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.FirstName + " " + x.LastName
                }).ToList();
            }
        }

        // Static list of books containing example book data.
        static List<BookEntity> _books = new List<BookEntity>()
        {
    new BookEntity
    {
        Id = 1,
        Title = "Cirque Du Freak",
        AuthorId = 1,
        Genre = "Fiction",
        PublishDate = new DateTime(2000, 1, 20),
        ISBN = "9780743273565",
        CopiesAvailable = 2268,
        ImageUrl = "https://www.mbebooks.com/wp-content/uploads/products/9780006754169_Cirque-du-Freak_large.jpg",
        Summary ="Darren Shan was an ordinary student until he won a ticket to visit the Cirque Du Freak... until he met Madam Octa... until he came face to face with a creature emerging from the darkness of the night...\nShortly after, he and his friend Steve would fall into a deadly trap. Darren was forced to make a deal with the only person who could save Steve. But this person wasn't human and was only interested in blood...\n'A book you can't put down, read in one breath... Full of spine-chilling touches... It teases the reader just enough.' - J.K. Rowling\n'Cleverly written, a terrifying forest.' - Guardian\n'I read Cirque Du Freak last week. I loved it. I admired how it blends humor with unsettling events, compassion with cruelty. A magnificent storyteller.' - Roddy Doyle"
    },
    new BookEntity
    {
        Id = 2,
        Title = "The Little Prince",
        AuthorId = 2,
        Genre = "Fiction",
        PublishDate = new DateTime(1943, 5, 6),
        ISBN = "9780061120084",
        CopiesAvailable = 1907,
        ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1367545443i/157993.jpg",
        Summary = "A pilot stranded in the desert awakes one morning to see, standing before him, the most extraordinary little fellow. \"Please,\" asks the stranger, \"draw me a sheep.\" And the pilot realizes that when life's events are too difficult to understand, there is no choice but to succumb to their mysteries. He pulls out pencil and paper... And thus begins this wise and enchanting fable that, in teaching the secret of what is really important in life, has changed forever the world for its readers.\r\n\r\n\n“‘The Little Prince’ is a book that touches our hearts because it deals with the essence of life. It speaks to all ages.” – Werner Herzog\r\n\r\n“The Little Prince is a book about loneliness, friendship, and the meaning of life—written in a way that anyone, of any age, can understand.” – Katherine Rundell\r\n\r\n“It’s a story that stays with you, no matter how old you are. Every time you read it, you learn something new about yourself and the world.” – Jean Reno"
    },
    new BookEntity
    {
        Id = 3,
        Title = "A Game of Thrones",
        AuthorId = 3,
        Genre = "Fiction",
        PublishDate = new DateTime(1996, 8, 1),
        ISBN = "9780451524935",
        CopiesAvailable = 2358,
        ImageUrl = "https://img.kitapyurdu.com/v1/getImage/fn:2728787/wh:true/wi:220",
        Summary = "Summers span decades. Winter can last a lifetine. And the struggle fort he Iron Throne has begun.As Warden of the North, Lord Eddard Stark counts it a curse when King Robert bestows on him the Office of the Hand. His honour weighs him down at court where a true man does what he will, not what he must… and a dead enemy is a thing of beauty.\n\"The scale and ambition of this production is absolutely extraordinary. There's nothing like it on TV.\" – Elijah Wood\r\n\r\n\"Game of Thrones is a masterclass in storytelling, filled with complex characters and unexpected twists that keep you hooked.\" – Steven Spielberg\r\n\r\n\"The world-building is unlike anything I've seen. It draws you in and doesn't let go.\" – Margaret Atwood"
    }
};
        // Displays a list of books, excluding those marked as deleted.
        public IActionResult List()
        {
            var books = _books.Where(x => !x.IsDeleted).ToList();

            return View(books);
        }

        // Displays the form for creating a new book.
        [HttpGet]
        public IActionResult Create()
        {
            AuthorViewBag();

            return View();
        }

        // Handles the creation of a new book with POST request.
        [HttpPost]
        public IActionResult Create(BookCreateViewModel formData)
        {
            // Validates the model; if invalid, re-displays the form.
            if (!ModelState.IsValid)
            {
                // If the form validation fails, we need to fill ViewBag.Authors again
                // so the dropdown options are not lost when the form reloads.
                AuthorViewBag();
                return View();
            }

            // Finds the maximum ID and creates a new ID for the new book.
            int maxId = _books.Max(x => x.Id);

            var newBook = new BookEntity()
            {
                Id = maxId + 1,
                Title = formData.Title,
                AuthorId = formData.AuthorId,
                Genre = formData.Genre,
                PublishDate = formData.PublishDate,
                ISBN= formData.ISBN,
                CopiesAvailable = formData.CopiesAvailable,
                ImageUrl = formData.ImageUrl,
                Summary = formData.Summary,
            };

            // Adds the new book to the list.
            _books.Add(newBook);


            return RedirectToAction("List");

        }

        // Displays the details of a specific book. Retrieves data based on book ID and creates a ViewModel.
        public IActionResult Details(int Id)
        {
            var bookDetails = (from book in _books
                               join author in AuthorController._authors on book.AuthorId equals author.Id
                               where book.Id == Id
                               select new
                               {
                                   Book = book,
                                   AuthorName = author.FirstName + " " + author.LastName,
                               }).SingleOrDefault();

            var viewModel = new BookDetailsViewModel
            {
                Title = bookDetails.Book.Title,
                ISBN = bookDetails.Book.ISBN,
                Genre = bookDetails.Book.Genre,
                PublishDate = bookDetails.Book.PublishDate,
                CopiesAvailable = bookDetails.Book.CopiesAvailable,
                ImageUrl = bookDetails.Book.ImageUrl,
                Summary = bookDetails.Book.Summary,
                AuthorName = bookDetails.AuthorName
            };

            return View(viewModel);
        }

        // Handles the deletion of a book with POST request. Marks the book as deleted.
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var book = _books.Find(x => x.Id == Id);

            book.IsDeleted = true;

            return RedirectToAction("List");
        }

        // Displays the form for editing an existing book.
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            AuthorViewBag();

            var book = _books.Find(x => x.Id == Id);

            var bookViewModel = new BookCreateViewModel
            {
                Title = book.Title,
                AuthorId = book.AuthorId,
                Genre = book.Genre,
                PublishDate = book.PublishDate,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable,
                ImageUrl = book.ImageUrl,
                Summary = book.Summary,
            };

            return View(bookViewModel);
        }

        // Handles the editing of an existing book with POST request. Updates the book details.
        [HttpPost]
        public IActionResult Edit(int Id,BookCreateViewModel formData)
        {
            // Validates the model; if invalid, re-displays the form.
            if (!ModelState.IsValid)
            {
                // If the form validation fails, we need to fill ViewBag.Authors again
                // so the dropdown options are not lost when the form reloads.
                AuthorViewBag();
                return View(formData);
            }

            var book = _books.Find(x => x.Id == Id);

            book.Title = formData.Title;
            book.AuthorId = formData.AuthorId;
            book.Genre = formData.Genre;
            book.PublishDate = formData.PublishDate;
            book.ISBN = formData.ISBN;
            book.CopiesAvailable = formData.CopiesAvailable;
            book.ImageUrl = formData.ImageUrl;
            book.Summary = formData.Summary;

            return RedirectToAction("List");
        }
    }
   
}

using Library_Management_System.Entities;
using Library_Management_System.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class AuthorController : Controller
    {
        // Static list of authors containing example author data.
        public static List<AuthorEntity> _authors = new List<AuthorEntity>()
        {
            new AuthorEntity
            {
                Id = 1,
                FirstName = "Darren",
                LastName = "Shan",
                DateOfBirth = new DateTime(1972, 8, 2),
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/ca/Darren_Shan_%282016%29.jpg/220px-Darren_Shan_%282016%29.jpg",
                Bio = "Darren Shan is the pen name of Irish author Darren O'Shaughnessy, born on July 2, 1972, in London. He is best known for his \"Darren Shan Saga\" (Cirque du Freak), a series of young adult novels filled with vampires, supernatural powers, and adventures. Shan has written extensively in the genres of dark fantasy, horror, and adventure. He is also known for series aimed at adults, such as \"The Demonata\" and \"Zom-B.\" Shan's writing style has garnered a large following, particularly among young readers."
            },

            new AuthorEntity
            {
                Id = 2,
                FirstName = "Antoine",
                LastName = "Saint Exupery",
                DateOfBirth = new DateTime(1900, 6, 29),
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7f/11exupery-inline1-500.jpg/220px-11exupery-inline1-500.jpg",
                Bio = "Antoine de Saint-Exupéry (1900–1944) was a French writer and pilot, best known for his novella The Little Prince (Le Petit Prince). As a pioneer in civil aviation, Saint-Exupéry's writings often explore themes of humanity, friendship, and responsibility. He disappeared during a reconnaissance flight in World War II."
            },

            new AuthorEntity
            {
                Id = 3,
                FirstName = "George R. R.",
                LastName = "Martin",
                DateOfBirth = new DateTime(1948, 9, 20),
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ed/Portrait_photoshoot_at_Worldcon_75%2C_Helsinki%2C_before_the_Hugo_Awards_%E2%80%93_George_R._R._Martin.jpg/220px-Portrait_photoshoot_at_Worldcon_75%2C_Helsinki%2C_before_the_Hugo_Awards_%E2%80%93_George_R._R._Martin.jpg",
                Bio = "George R. R. Martin, born on September 20, 1948, is an American author and screenwriter, best known for his A Song of Ice and Fire fantasy novel series, which inspired HBO's famous TV show Game of Thrones. Martin's works are known for their complex characters, unexpected plot twists, and political intrigue. He is considered one of the most influential writers in the world of fantasy fiction."
            },
        };

        // Displays a list of authors, excluding those marked as deleted.
        public IActionResult List()
        {
            var authors = _authors.Where(x => !x.IsDeleted).ToList();
            return View(authors);
        }

        // Displays the form for adding a new author.
        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        // Handles the creation of a new author with POST request.
        [HttpPost]
        public IActionResult AddAuthor(AuthorListViewModel formData)
        {
            // Validates the model; if invalid, re-displays the form.
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Finds the maximum ID and creates a new ID for the new author.
            var maxId = _authors.Max(x => x.Id);

            var newAuthor = new AuthorEntity()
            {
                Id = maxId + 1,
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                DateOfBirth = formData.DateOfBirth,
                ImageUrl = formData.ImageUrl,
                Bio = formData.Bio
            };

            // Adds the new author to the list.
            _authors.Add(newAuthor);

            return RedirectToAction("List");
        }

        // Displays the details of a specific author based on author ID.
        public IActionResult Details(int Id)
        {
            var authorDetails = _authors.Find(x => x.Id == Id);
            return View(authorDetails);
        }

        // Handles the deletion of an author. Marks the author as deleted.
        public IActionResult Delete(int Id)
        {
            var author = _authors.Find(x => x.Id == Id);

            if (author != null)
            {
                author.IsDeleted = true;
            }

            return RedirectToAction("List");
        }

        // Displays the form for editing an existing author.
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var author = _authors.Find(x => x.Id == Id);

            var authorViewModel = new AuthorListViewModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                ImageUrl = author.ImageUrl,
                Bio = author.Bio
            };

            return View(authorViewModel);
        }

        // Handles the editing of an existing author with POST request. Updates the author details.
        [HttpPost]
        public IActionResult Edit(int Id, AuthorListViewModel formData)
        {
            // Validates the model; if invalid, re-displays the form.
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            var author = _authors.Find(x => x.Id == Id);

            if (author != null)
            {
                author.FirstName = formData.FirstName;
                author.LastName = formData.LastName;
                author.DateOfBirth = formData.DateOfBirth;
                author.ImageUrl = formData.ImageUrl;
                author.Bio = formData.Bio;
            }

            return RedirectToAction("List");
        }
    }
}

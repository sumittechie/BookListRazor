using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Book Book { get; set; }

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();

            if (id == null)
            {
                return Page();
            }

            Book = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                    //var book = await _db.Book.FindAsync(Book.Id);
                    //book.Name = Book.Name;
                    //book.Author = Book.Author;
                    //book.ISBN = Book.ISBN;
                }

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();

        }
    }
}
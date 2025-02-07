﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return Json(new { success = false, message = " Error while deleting" });
            }

            _db.Book.Remove(book);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });

        }
    }
}
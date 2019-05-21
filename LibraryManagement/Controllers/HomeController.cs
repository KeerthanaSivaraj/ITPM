using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using LibraryManagement.Models.AccountViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        

        private readonly UserManager<ApplicationUser> _userManager;
        int count = 1;
        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public IActionResult Index()
        {
            //var userid = _userManager.GetUserId(HttpContext.User);
            //if (userid == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //else
            //{


            //    ApplicationUser user = _userManager.FindByIdAsync(userid).Result;
            //    return View(user);
            //}
            return View();
        }
       

 
        public async Task<IActionResult> BookIndex(string Title, string searchString)
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in _context.BookManageViewModel
                           orderby d.Title
                           select d.Title;
            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.Title = new SelectList(GenreLst);

            var movies = from m in  _context.BookManageViewModel
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(Title))
            {
                movies = movies.Where(x => x.Title == Title);
            }
            return View(movies.ToList());
            //return View(await _context.BookManageViewModel.ToListAsync());
        }

        public IActionResult Contact()
        {
            return View();
        }



        [Authorize]

        public IActionResult Create(int Id)
        {
            Card c = new Card();
            var item = _context.BookManageViewModel.FirstOrDefault(a => a.BookID == Id);
            c.Book_Name = item.Title;

            c.des = item.Description;
            c.BookId = item.BookID;
           // c.Datetime = null;
            return View(c);
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id,Card b)
        {
            if (ModelState.IsValid)
            {
                Card c = new Card();
                var item = _context.BookManageViewModel.FirstOrDefault(a => a.BookID == Id);
                c.Book_Name = item.Title;

                c.des = item.Description;
                c.BookId = item.BookID;
                c.Date = System.DateTime.Now;
                c.Datetime = b.Datetime;
                c.email = b.email;


                MailMessage mm = new MailMessage("karthiteststudies@gmail.com", b.email);
                mm.Subject = c.Book_Name;
                mm.Body = "Please return the book with " +b.Datetime;
                mm.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential("karthiteststudies@gmail.com", "Karthitest1");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(mm);




                _context.Add(c);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AddIndex));
            }
            return View(b);
        }

        [Authorize]
        public async Task<IActionResult> AddIndex()
        {
            return View(await _context.Card.ToListAsync());
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

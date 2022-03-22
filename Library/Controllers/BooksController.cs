using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
  //[Authorize]
  public class BooksController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager; //new line


    public BooksController(UserManager<ApplicationUser> userManager,LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    // public ActionResult Index()
    // {
    //   List<Book> model = _db.Books.ToList();
    //   return View(model);
    // }
    public async Task<ActionResult> Index()
{
    var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var currentUser = await _userManager.FindByIdAsync(userId);
    var userBooks = _db.Books.Where(entry => entry.User.Id == currentUser.Id).ToList();
    return View(userBooks);
}

    public ActionResult Create()
    {
      ViewBag.Authors = _db.Authors.ToList();
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View();
    }

    [HttpPost]
    // public ActionResult Create(Book book)
    // {
    //   _db.Books.Add(book);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
    public async Task<ActionResult> Create(Book book)
{
    var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var currentUser = await _userManager.FindByIdAsync(userId);
    book.User = currentUser;
    _db.Books.Add(book);
    _db.SaveChanges();
    return RedirectToAction("Index");
}

    public ActionResult Details(int id)
    {
      ViewBag.Authors = _db.Authors.ToList();
      var thisBook = _db.Books
          .Include(b => b.JoinEntities)
          .ThenInclude(join => join.Author)
          .FirstOrDefault(b => b.BookId == id);
      return View(thisBook);
    }
    public ActionResult Edit(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(b => b.BookId == id);
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult Edit(Book book)
    {
      _db.Entry(book).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(b => b.BookId == id);
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(b => b.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
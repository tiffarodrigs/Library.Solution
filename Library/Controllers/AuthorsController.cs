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
  [Authorize]
  public class AuthorsController : Controller
  {
    private readonly LibraryContext _db;
    public AuthorsController(LibraryContext db)
    {
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Author> model = _db.Authors.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Author author)
    {
      _db.Authors.Add(author);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id= author.AuthorId});
    }
    
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      ViewBag.Books = _db.Books.ToList();
      var thisAuthor = _db.Authors
        .Include(author => author.JoinEntities)
        .ThenInclude(join => join.Book)
        .FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    public ActionResult Edit(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost]
    public ActionResult Edit(Author author)
    {
      _db.Entry(author).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = author.AuthorId});
    }

    public ActionResult Delete(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      _db.Authors.Remove(thisAuthor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBook(int id)
    {
      ViewBag.Books = _db.Books.ToList();
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      var thisAuthor = _db.Authors.FirstOrDefault(author => author.AuthorId == id);
      return View(thisAuthor); 
    }

    [HttpPost]
    public ActionResult AddBook(int AuthorId, int BookId)
    {
      _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = BookId});
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = AuthorId});
    }

    public ActionResult RemoveBook(int id)
    {
      var thisJoin = _db.AuthorBook.FirstOrDefault(join => join.AuthorBookId == id);
      var redirectId = thisJoin.AuthorId;
      _db.AuthorBook.Remove(thisJoin);
      _db.SaveChanges();
      return RedirectToAction("Index", new {id = redirectId});
    }
  }
}
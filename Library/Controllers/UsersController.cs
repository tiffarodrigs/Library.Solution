// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using Library.Models;
// using System.Collections.Generic;
// using System.Linq;
// using System;


// namespace Library.Controllers
// {
//   [Authorize] //new line
//   public class ItemsController : Controller
//   {
//     private readonly LibraryContext _db;
//     private readonly UserManager<ApplicationUser> _userManager; //new line

//     //updated constructor
//     public ItemsController(UserManager<ApplicationUser> userManager, LibraryContext db)
//     {
//       _userManager = userManager;
//       _db = db;
//     }
//     public ActionResult Index()
//     {
//       //goes to the books index route
//       // page where you choose whether to sign in or create a new account

//     }

//     public ActionResult Create()
//     {
//       // create a new account form
//       ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
//       return View();
//     }

//     [HttpPost]
//     public ActionResult Create(Item item, int CategoryId)
//     // taking the information from the create() view and returning it to the database
//     {
//       _db.Items.Add(item);
//       _db.SaveChanges();
//       if (CategoryId != 0)
//       {
//         _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
//         _db.SaveChanges();
//       }
//       return RedirectToAction("Index");
//     }

//     public ActionResult Details(int id)
//     {
//       // user details, like email, password, etc.
//       var thisItem = _db.Items
//           .Include(item => item.JoinEntities)
//           .ThenInclude(join => join.Category)
//           .FirstOrDefault(item => item.ItemId == id);
//       return View(thisItem);
//     }

//     public ActionResult Edit(int id)
//     {
//       // changing email
//       var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
//       ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
//       return View(thisItem);
//     }

//     [HttpPost]
//     public ActionResult Edit(Item item, int CategoryId)
//     {
//       if (CategoryId != 0)
//       {
//         _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
//       }
//       _db.Entry(item).State = EntityState.Modified;
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult AddCategory(int id)
//     {
//       var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
//       ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
//       return View(thisItem);
//     }

//     [HttpPost]
//     public ActionResult AddCategory(Item item, int CategoryId)
//     {
//       if (CategoryId != 0)
//       {
//         _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
//         _db.SaveChanges();
//       }
//       return RedirectToAction("Index");
//     }
    
//     public ActionResult Delete(int id)
//     {
//       var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
//       return View(thisItem);
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
//       _db.Items.Remove(thisItem);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     [HttpPost]
//     public ActionResult DeleteCategory(int joinId)
//     {
//         var joinEntry = _db.CategoryItem.FirstOrDefault(entry => entry.CategoryItemId == joinId);
//         _db.CategoryItem.Remove(joinEntry);
//         _db.SaveChanges();
//         return RedirectToAction("Index");
//     }


//   }  

// }  
using KatooniStoreWeb.Data;
using KatooniStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace KatooniStoreWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> ObjCategoryList = _db.Categories;
            return View(ObjCategoryList);
        }

        [HttpPost]
        public IActionResult Index(String SearchText)
        {
            if (SearchText != "" && SearchText != null)
            {
                var SearchItem = _db.Categories.Where(i => i.Name == SearchText);
                return View(SearchItem);
            }

            return View();
        }

        //GET
        public IActionResult Create()
        {
            
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("CustomError", "The DisplayOrder can not exactly as the same field Name ");
                ModelState.AddModelError("name", "The DisplayOrder can not exactly as the same field Name ");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfuly";
                return RedirectToAction("Index");

            }
           return View(obj);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDb = _db.Categories.Find(id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
                return View(CategoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("CustomError", "The DisplayOrder can not exactly as the same field Name ");
                ModelState.AddModelError("name", "The DisplayOrder can not exactly as the same field Name ");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfuly";
                return RedirectToAction("Index");

            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDb = _db.Categories.Find(id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }
        //POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category deleted successfuly";
            return RedirectToAction("Index");

            
           
        }
    }
}

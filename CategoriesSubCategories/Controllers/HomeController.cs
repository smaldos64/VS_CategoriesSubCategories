using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CategoriesSubCategories.Models;
using CategoriesSubCategories.ViewModels;
using System.Web.Script.Serialization;

namespace CategoriesSubCategories.Controllers
{
    public class HomeController : Controller
    {
        private Test db = new Test();

        public PartialViewResult _Navigo()
        {
            return PartialView(db.Categories.Where(c => c.ParentCategoryID == null).OrderBy(c => c.CategoryID).ToList());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProductsInMainCategory(int id)
        {
            List<Product> ProductList = db.Products.Where(m => m.CategoryID == id).OrderBy(m => m.CategoryID).ToList();
            ViewBag.SubCategoryList = db.Categories.Where(c => c.ParentCategoryID == id).ToList();
            ViewBag.Category = db.Categories.Find(id);

            List<VM_CategoryLevel> VM_CategoryLevel_List = RecursiveFunction.GetAllCategoriesUnderMainCategoryWithLevel(db.Categories.Find(id));
            ViewBag.SubCategoryLevelList = VM_CategoryLevel_List;

            return View(ProductList);
        }

        //public ActionResult ProductsInSubCategory(int id)
        //{
        //    List<Category> CategoryNamesList = db.Categories.Where(c => c.ParentCategoryID == id).ToList();
        //    List<Category> CategoryList = db.Categories.SelectMany(c => c.ChildCategories).ToList();
        //    List<Product> ProductList = db.Products.Where(m => m.Category.ParentCategoryID == id).OrderBy(m => m.CategoryID).ToList();
        //    ViewBag.CategoryName = db.Categories.Find(id).CategoryName;

        //    return (View(ProductList));
        //}

        //public ActionResult ProductsInSubSubCategory(int id)
        //{
        //    List<Product> ProductList = db.Products.Where(m => m.CategoryID == id).OrderBy(m => m.ProductID).ToList();

        //    return (View(ProductList));
        //}

        public ActionResult AddProduct()
        {
            List<Category> CategoryList = db.Categories.Where(c => c.ParentCategoryID == null).OrderBy(c => c.CategoryID).ToList();
            
            return View(CategoryList);
        }

        [HttpPost]
        public ActionResult AddProduct(Product Product_Object)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(Product_Object);
                db.SaveChanges();
                return (RedirectToAction("Index", "Home"));
            }
            else
            {
                return (RedirectToAction("AddProduct", "Home"));
            } 
        }

        public ActionResult AddCategory()
        {
            List<Category> CategoryList = db.Categories.Where(c => c.ParentCategoryID == null).OrderBy(c => c.CategoryID).ToList();

            return View(CategoryList);
        }

        [HttpPost]
        public ActionResult AddCategory(Category Category_Object)
        {
            if (ModelState.IsValid)
            {
                if (0 == Category_Object.ParentCategoryID)
                {
                    Category_Object.ParentCategoryID = null;
                }
                db.Categories.Add(Category_Object);
                db.SaveChanges();
                return (RedirectToAction("Index", "Home"));
            }
            else
            {
                return (RedirectToAction("AddCategory", "Home"));
            }
        }

        public JsonResult GetSubCategoriesUnderMainCategory(int ParentCategoryID)
        {
            List<VM_CategoryLevel> VM_CategoryLevel_List = RecursiveFunction.GetAllCategoriesUnderMainCategoryWithLevel(db.Categories.Find(ParentCategoryID));

            List<object> JsonList = new List<object>();

            foreach (VM_CategoryLevel item in VM_CategoryLevel_List)
            {
                var VM_CategoryLevelItem = new
                {
                    CategoryName = item.Category.CategoryName,
                    CategoryID = item.Category.CategoryID,
                    CategoryLevel = item.CategoryLevel
                };
                JsonList.Add(VM_CategoryLevelItem);
            }
            
            return Json(JsonList, JsonRequestBehavior.AllowGet);
        }
    }
}
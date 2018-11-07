using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CategoriesSubCategories.ViewModels;

namespace CategoriesSubCategories.Models
{
    public class RecursiveFunction
    {
        private static List<Category> CategoryList = new List<Category>();

        public static List<Category> GetAllCategoriesUnderMainCategory(Category Category_Object)
        {
            CategoryList.Clear();
            Category Category_ObjectWork = Category_Object;

            while (null != Category_ObjectWork.ParentCategoryID)
            {
                Category_ObjectWork = Category_ObjectWork.ParentCategory;
                CategoryList.Add(Category_ObjectWork);
            }
            CategoryList.Reverse();
            CategoryList.Add(Category_Object);
            
            if (Category_Object.ChildCategories.Count > 0)
            {
                GetAllCategories(Category_Object);
            }
            
            return (CategoryList);
        }

        private static void GetAllCategories(Category Category_Object)
        {
            foreach (Category item in Category_Object.ChildCategories)
            {
                CategoryList.Add(item);
                if (item.ChildCategories.Count > 0)
                {
                    GetAllCategories(item);
                }
            }
        }

        private static List<Category> CategoryList1 = new List<Category>();
        private static List<VM_CategoryLevel> VM_CategoryLevel_List = new List<VM_CategoryLevel>();

        public static List<VM_CategoryLevel> GetAllCategoriesUnderMainCategoryWithLevel(Category Category_Object)
        {
            CategoryList1.Clear();
            VM_CategoryLevel_List.Clear();
            Category Category_ObjectWork = Category_Object;
            VM_CategoryLevel VM_CategoryLevel_Object = new VM_CategoryLevel();
          
            while (null != Category_ObjectWork.ParentCategoryID)
            {
                Category_ObjectWork = Category_ObjectWork.ParentCategory;
                CategoryList1.Add(Category_ObjectWork);
            }
            CategoryList1.Reverse();

            int CategoryLevel = 1;
            foreach (Category item in CategoryList1)
            {
                VM_CategoryLevel This_VM_CategoryLevel_Object = new VM_CategoryLevel();
                This_VM_CategoryLevel_Object.Category = item;
                This_VM_CategoryLevel_Object.CategoryLevel = CategoryLevel;
                VM_CategoryLevel_List.Add(This_VM_CategoryLevel_Object);
                CategoryLevel++;
            }

            CategoryList1.Add(Category_Object);
            VM_CategoryLevel_Object = new VM_CategoryLevel();
            VM_CategoryLevel_Object.Category = Category_Object;
            VM_CategoryLevel_Object.CategoryLevel = CategoryLevel;
            VM_CategoryLevel_List.Add(VM_CategoryLevel_Object);
            CategoryLevel++;

            if (Category_Object.ChildCategories.Count > 0)
            {
                GetAllCategoriesWithLevel(Category_Object, CategoryLevel);
            }

            return (VM_CategoryLevel_List);
        }

        private static void GetAllCategoriesWithLevel(Category Category_Object, int CategoryLevel)
        {
            int CurrentCategoryLevel = 0;
            foreach (Category item in Category_Object.ChildCategories)
            {
                CategoryList1.Add(item);

                VM_CategoryLevel VM_CategoryLevel_Object = new VM_CategoryLevel();
                VM_CategoryLevel_Object.Category = item;
                VM_CategoryLevel_Object.CategoryLevel = CategoryLevel;
                VM_CategoryLevel_List.Add(VM_CategoryLevel_Object);

                if (item.ChildCategories.Count > 0)
                {
                    CurrentCategoryLevel = CategoryLevel + 1;
                    GetAllCategoriesWithLevel(item, CurrentCategoryLevel);
                }
            }
        }

    }
}
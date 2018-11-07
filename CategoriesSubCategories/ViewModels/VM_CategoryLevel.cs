using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CategoriesSubCategories.Models;

namespace CategoriesSubCategories.ViewModels
{
    public class VM_CategoryLevelCollection
    {
        public virtual Category Category { get; set; }

        public virtual int CategoryLevel { get; set; }
    }

    public class VM_CategoryLevel
    {
        //public virtual ICollection<VM_CategoryLevelCollection> VM_CategoryLevelCollections { get; set; }

        public virtual Category Category { get; set; }

        public virtual int CategoryLevel { get; set; }
    }
}
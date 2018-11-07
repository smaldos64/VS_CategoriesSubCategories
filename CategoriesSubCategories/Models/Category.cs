using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CategoriesSubCategories.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        // Erklæringer herunder er med til at sikre, at vi kan relatere
        // produkter til kategorier - under kategorier og så videre
        // som vi selv ønsker det.

        //[Required]
            public int ?ParentCategoryID { get; set; }
            [ForeignKey("ParentCategoryID")]
            public virtual Category ParentCategory { get; set; }

            public virtual ICollection<Category> ChildCategories { get; set; }
    }
}
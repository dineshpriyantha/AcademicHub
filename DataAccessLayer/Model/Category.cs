using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Category
    {
        [Key]
        public int CId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int DisplayArea { get; set; }
        public int CRank { get; set; }

        // Navigation property for posts
        //public virtual ICollection<Post> Posts { get; set; }
    }

    public class Subcategory
    {
        [Key]
        public int SId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        // Foreign key for the parent category
        public int CategoryId { get; set; }
        public int SRank { get; set; }

        // Navigation property for the parent category
        public virtual Category Category { get; set; }

        // Navigation property for posts
        public virtual ICollection<Post> Posts { get; set; }
    }

    public class Post
    {
        [Key]
        public int PId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        // Foreign key for the category
        public int CategoryId { get; set; }

        // Foreign key for the subcategory
        public int SubcategoryId { get; set; }

        // Navigation property for the category
        public virtual Category Category { get; set; }

        // Navigation property for the subcategory
        public virtual Subcategory Subcategory { get; set; }
    }

    public enum DisplayArea : int
    {
        LeftBar = 2, RightBar = 3, NavBar = 4
    }
}

using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModel
{
    public class CategorySubcategoryViewModel
    {
        public List<Category> Category { get; set; }
        public List<Subcategory> Subcategory { get; set; }
        public List<Post> Posts { get; set; }
    }

}

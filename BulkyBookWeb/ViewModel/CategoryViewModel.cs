using Bulky.Models;

namespace BulkyBookWeb.ViewModel
{
    public class CategoryViewModel
    {
        public IQueryable<Category> Categories { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string Term { get; set; }
    }
}

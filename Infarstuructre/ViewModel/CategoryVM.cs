using Domain.Entity;

namespace Infarstuructre.ViewModel
{
    public class CategoryVM
    {
        public List<Category>? Categories { get; set; }
        public List<CategoryLog>? LogCategories { get; set; }
        public Category NewCategory { get; set; }
    }
}
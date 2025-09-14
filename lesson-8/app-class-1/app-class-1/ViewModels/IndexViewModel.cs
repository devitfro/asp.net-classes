using app_class_1.Models;

namespace app_class_1.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }

        public IEnumerable<CompanyViewModel> Companies { get; set; }

        public int? SelectedCompanyId { get; set; }
    }
}

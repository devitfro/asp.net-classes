using app_class_1.Models;

namespace app_class_1.Services
{
    public class PhoneService
    {
        List<Phone> phones;
        List<Company> companies;

        public PhoneService()
        {
            companies = new List<Company>()
            {
                new Company { Id = 1, Name = "Apple", Country = "США" },
                new Company { Id = 2, Name = "Samsung", Country = "Республика Корея" },
                new Company { Id = 3, Name = "Google", Country = "США" }

            };

            phones = new List<Phone>
            {
                new Phone { Id=1, Manufacturer= companies[0], Name="iPhone X", Price=56000 },
                new Phone { Id=2, Manufacturer= companies[0], Name="iPhone XZ", Price=41000 },
                new Phone { Id=3, Manufacturer= companies[1], Name="Galaxy 9", Price=9000 },
                new Phone { Id=4, Manufacturer= companies[1], Name="Galaxy 10", Price=40000 },
                new Phone { Id=5, Manufacturer= companies[2], Name="Pixel 2", Price=30000 },
                new Phone { Id=6, Manufacturer= companies[2], Name="Pixel XL", Price=50000 }
            };
        }

        public IEnumerable<Phone> GetPhones() => phones;

        public IEnumerable<Company> GetCompanies() => companies;

        public Company GetCompany(int companyId)
        {
            return companies.FirstOrDefault(e => e.Id == companyId);
        }

        public void AddPhone(Phone phone)
        {
            phones.Add(phone);
        }
    }
}

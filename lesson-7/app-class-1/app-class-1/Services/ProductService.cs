using app_class_1.Models;

namespace app_class_1.Services
{
    public class ProductService
    {
        public string[] GetProducts()
        {
            return new string[]
            {
                "Apple", "Banana", "Orange", "Kiwi"
            };
        }

        public List<Person> GetCustomers()
        {
            return new List<Person>
            {
                new Person{ Name = "Alex", Age = 30 },
                new Person{ Name = "Alice", Age = 32 }
            };
        }
    }
}

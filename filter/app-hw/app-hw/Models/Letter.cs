using System.ComponentModel.DataAnnotations;

namespace app_hw.Models
{
    public class Letter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Введите сообщение")]
        public string Message { get; set; } = "";

        [Required(ErrorMessage = "Выберите дату отправки")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; } = DateTime.Now.AddDays(1);
    }
}

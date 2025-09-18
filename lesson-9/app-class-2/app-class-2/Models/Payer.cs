namespace app_class_2.Models
{
    public record class Payer
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }

        public float LightBill { get; set; }
        public float WaterBill { get; set; }
        public float GasBill { get; set; }
    }
}

﻿namespace app_class_1.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company Manufacturer { get; set; }
        public decimal Price { get; set; }
    }
}

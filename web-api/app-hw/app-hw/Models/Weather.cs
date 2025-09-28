namespace app_hw.Models
{
    public class Weather
    {
        public string Name { get; set; } // Название города
        public MainInfo Main { get; set; }
        public List<WeatherInfo> w { get; set; }
    }

    public class MainInfo
    {
        public double Temp { get; set; } 
        public double Humidity { get; set; }
    }

    public class WeatherInfo
    {
        public string Main { get; set; } 
        public string Description { get; set; } 
    }
}

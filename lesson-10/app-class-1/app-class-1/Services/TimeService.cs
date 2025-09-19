namespace app_class_1.Services
{
    public interface ITimeService
    {
        string GetTime();
    }

    public class TimeService : ITimeService
    {
        public string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}

namespace INVEST.BUSINESSLOGIC.Settings
{
    public class AppSettings
    {
        public Configuration? Configuration { get; set; }

        public string? GetAppBaseApiUrl()
        {
            return Configuration?.AppBase?.ApiUrl;
        }
    }
}

namespace INVEST.SITE.Service.Interface
{
    public interface IRestService
    {
        Task<string> ExecuteGetAsync(string url, string accept = "application/json");

        Task<string> ExecutePostAsync(string url, object payload, string accept = "application/json");
    }
}

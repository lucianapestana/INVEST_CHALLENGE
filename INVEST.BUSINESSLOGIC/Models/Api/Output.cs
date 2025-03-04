namespace INVEST.BUSINESSLOGIC.Models.Api
{
    public class Output
    {
        public string Status { get; set; }

        public string Code { get; set; }

        public string MessageCode { get; set; }

        public List<Error> Errors { get; set; }
    }
}

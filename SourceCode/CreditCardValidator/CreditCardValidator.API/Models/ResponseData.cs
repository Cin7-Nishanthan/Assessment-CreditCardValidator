namespace CreditCardValidator.API.Models
{
    public class ResponseData
    {
        public int Status { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }

    }
}

namespace UserApi.Models
{
    public enum ResponseStatus
    {
        Error,
        Success
    }

    public class ResponseModel
    {
        public ResponseStatus Status { get; set; }

        public string Message { get; set; }
    }
}
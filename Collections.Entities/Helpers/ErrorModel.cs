namespace Collections.Entities.Helpers
{
    public class ErrorModel
    {
        public ErrorModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}

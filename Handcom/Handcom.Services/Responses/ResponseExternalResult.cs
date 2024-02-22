namespace Handcom.Services.Responses
{
    public class ResponseExternalResult
    {
        public ResponseExternalResult() =>
            Errors = new ResponseErrorMessages();

        public string? Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }
    }

    public class ResponseErrorMessages
    {
        public ResponseErrorMessages() =>
            Messages = new List<string>();

        public List<string> Messages { get; set; }
    }
}
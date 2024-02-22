namespace Handcom.Web.Model.Extensions
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
    }
}

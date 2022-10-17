using System.Net;

namespace StartasLamstvk.Shared.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public ICollection<Error> Errors { get; set; }

        public ErrorResponse()
        {
            Errors = new List<Error>();
        }

        public void SetInfo(HttpStatusCode status, List<Error> errors)
        {
            Errors = errors;
            StatusCode = (int)status;
        }

        public void SetInfo(HttpStatusCode status, Error error)
        {
            SetInfo(status, new List<Error> { error });
        }
    }
}
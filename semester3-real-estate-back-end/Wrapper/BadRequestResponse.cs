using System.Net;

namespace semester4.Wrapper;

public class BadRequestResponse
{
    public BadRequestResponse(string message = "")
    {
        Message = message;
    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    public string Message { get; set; }
}
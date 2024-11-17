using System.Net;

namespace semester4.Wrapper;

public class ConflictResponse
{
    public ConflictResponse(string message = "")
    {
        Message = message;
    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Conflict;
    public string Message { get; set; }
}
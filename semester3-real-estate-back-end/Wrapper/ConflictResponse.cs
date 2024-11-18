using System.Net;

namespace semester3_real_estate_back_end.Wrapper;

public class ConflictResponse
{
    public ConflictResponse(string message = "")
    {
        Message = message;
    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Conflict;
    public string Message { get; set; }
}
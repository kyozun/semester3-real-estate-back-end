using System.Net;

namespace semester3_real_estate_back_end.Wrapper;

public class BadRequestResponse
{
    public BadRequestResponse(string message = "")
    {
        Message = message;
    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    public string Message { get; set; }
}
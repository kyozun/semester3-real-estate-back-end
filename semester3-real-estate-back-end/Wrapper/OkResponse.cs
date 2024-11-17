using System.Net;

namespace semester4.Wrapper;

public class OkResponse
{
    public OkResponse(string message = "OK")
    {
        Message = message;
    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public string Message { get; set; }
}
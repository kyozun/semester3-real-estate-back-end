using System.Net;

namespace semester3_real_estate_back_end.Wrapper;

public class OkResponse
{
    public OkResponse(string message = "OK")
    {
        Message = message;
    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public string Message { get; set; }
}
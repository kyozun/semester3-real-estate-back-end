using System.Net;

namespace semester3_real_estate_back_end.Wrapper;

/*data, status, message*/
public class Response<T>
{
    // Constructor with Total
    public Response(List<T> data, int total, int limit, int offset, string message = "OK")
    {
        Data = data;
        Total = total;
        Limit = limit;
        Offset = offset;
        Message = message;
    }

    // Constructor without Total
    public Response(T data, string message = "")
    {
        Data = [data];
        Total = null;
        Message = message;
    }

    public int? Total { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public List<T> Data { get; set; }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public string Message { get; set; }
}
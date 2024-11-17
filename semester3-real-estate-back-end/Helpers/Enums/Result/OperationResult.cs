namespace semester4.Helpers.Enums.Result;

public class OperationResult
{
    public OperationResult(OperationStatus status)
    {
        Status = status;
    }

    public OperationStatus Status { get; set; }

    // Helper methods for convenience
    public static OperationResult SuccessResult()
    {
        return new OperationResult(OperationStatus.Success);
    }

    public static OperationResult Unauthorized()
    {
        return new OperationResult(OperationStatus.Unauthorized);
    }

    public static OperationResult BadRequest()
    {
        return new OperationResult(OperationStatus.BadRequest);
    }

    public static OperationResult Failure()
    {
        return new OperationResult(OperationStatus.Failure);
    }
}

public enum OperationStatus
{
    Success,
    Unauthorized,
    BadRequest,
    Failure // Represents generic failure like NotFound or Forbidden
}
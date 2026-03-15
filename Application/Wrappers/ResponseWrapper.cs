namespace Application.Wrappers;
public class ResponseWrapper : IResponseWrapper
{
    public List<string> Messages { get; set; } = [];
    public bool IsSuccessful { get; set; }

    #region Failures
    public static IResponseWrapper Fail()
    {
        return new ResponseWrapper { IsSuccessful = false };
    }

    public static IResponseWrapper Fail(string message)
    {
        return new ResponseWrapper { IsSuccessful = false, Messages = [message] };
    }

    public static IResponseWrapper Fail(List<string> messages)
    {
        return new ResponseWrapper { IsSuccessful = false, Messages = messages };
    }

    #endregion

    #region Success
    public static IResponseWrapper Success()
    {
        return new ResponseWrapper { IsSuccessful = true };
    }
    
    public static IResponseWrapper Success(string message)
    {
        return new ResponseWrapper { IsSuccessful = true, Messages = [message] };
    }

    public static IResponseWrapper Success(List<string> messages)
    {
        return new ResponseWrapper { IsSuccessful = true, Messages = messages };
    }
    #endregion
}

public class ResponseWrapper<T> : ResponseWrapper, IResponseWrapper<T>
{
    public T Data { get; private set; }

    #region Failures
    public new static IResponseWrapper<T> Fail()
    {
        return new ResponseWrapper<T>() { IsSuccessful = false };
    }

    public new static IResponseWrapper<T> Fail(string message)
    {
        return new ResponseWrapper<T>() { IsSuccessful = false, Messages = [message] };
    }

    public new static IResponseWrapper<T> Fail(List<string> messages)
    {
        return new ResponseWrapper<T>() { IsSuccessful = false, Messages = messages };
    }
    #endregion

    #region Success
    public new static IResponseWrapper<T> Success()
    {
        return new ResponseWrapper<T>() { IsSuccessful = true };
    }
    
    public new static IResponseWrapper<T> Success(string message)
    {
        return new ResponseWrapper<T>() { IsSuccessful = true, Messages = [message] };
    }

    public new static IResponseWrapper<T> Success(List<string> messages)
    {
        return new ResponseWrapper<T>() { IsSuccessful = true, Messages = messages };
    }

    public static IResponseWrapper<T> Success(T data)
    {
        return new ResponseWrapper<T>() { IsSuccessful = true, Data = data };
    }

    public static IResponseWrapper<T> Success(T data, string message)
    {
        return new ResponseWrapper<T>() { IsSuccessful = true, Data = data, Messages = [message] };
    }

    public static IResponseWrapper<T> Success(T data, List<string> messages)
    {
        return new ResponseWrapper<T>() { IsSuccessful = true, Data = data, Messages = messages };
    }
    #endregion
}

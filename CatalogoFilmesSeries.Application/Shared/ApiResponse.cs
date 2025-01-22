namespace CatalogoFilmesSeries.Application.Shared;

public sealed class ApiResult<T>
{
    private ApiResult(bool success, string message, int statusCode, T? data = default, IEnumerable<string>? errors = null)
    {
        Success = success;
        Data = data;
        Message = message;
        StatusCode = statusCode;

        Errors = errors ?? Enumerable.Empty<String>();
    }

    public bool Success { get; private set; }
    public T? Data { get; private set; }
    public string Message { get; private set; }
    public IEnumerable<string>? Errors { get; private set; }
    public int StatusCode { get; private set; }


    public static ApiResult<T> Ok(T data, string message = "OK") =>
        new(success: true, statusCode: 200, message: message, data: data);

    public static ApiResult<T> Created(T data, string message = "Sucesso na inclusão") =>
        new(success: true, statusCode: 201, message: message, data: data);
    
    
    public static ApiResult<T> BadRequest(string message = "Erro na requisição", IEnumerable<string>? errors = null) =>
        new(success: false, statusCode: 400, message: message, errors: errors);
    
    public static ApiResult<T> NotFound(string message = "Não encontrado") =>
        new(success: false, statusCode: 404, message: message);
    
    public static ApiResult<T> InternalServerError(string message = "Erro interno") =>
        new(success: false, statusCode: 500, message: message);
}
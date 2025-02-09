using System.Net;

namespace CatalogoFilmesSeries.Application.Shared;

public sealed class ApiResult<T>
{
    private ApiResult(bool success, string message, HttpStatusCode statusCode, T? data = default, IEnumerable<string>? errors = null)
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
    public HttpStatusCode StatusCode { get; private set; }


    public static ApiResult<T> Ok(T data, string message = "OK") =>
        new(success: true, statusCode: HttpStatusCode.OK, message: message, data: data);

    public static ApiResult<T> Created(T data, string message = "Sucesso na inclusão") =>
        new(success: true, statusCode: HttpStatusCode.Created, message: message, data: data);
    
    
    public static ApiResult<T> BadRequest(string message = "Erro na requisição", IEnumerable<string>? errors = null) =>
        new(success: false, statusCode: HttpStatusCode.BadRequest, message: message, errors: errors);
    
    public static ApiResult<T> NotFound(string message = "Não encontrado") =>
        new(success: false, statusCode: HttpStatusCode.NotFound, message: message);
    
    public static ApiResult<T> InternalServerError(string message = "Erro interno") =>
        new(success: false, statusCode: HttpStatusCode.InternalServerError, message: message);
}
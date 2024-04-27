using System.Net;

namespace WebApp.DTO;

public class RestApiErrorResponse
{
    public HttpStatusCode Status { get; set; } = default!;
    public string Error { get; set; } = default!;
}
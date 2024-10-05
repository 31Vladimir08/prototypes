using Grpc.Core;
using Grpc.Core.Interceptors;

namespace QuartzService.Interceptors;

public class LogInterceptor : Interceptor
{
    private readonly ILogger<LogInterceptor> _logger;

    public LogInterceptor(
        ILogger<LogInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Message: {Message}", ex.Message);
            throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
        }
    }
}

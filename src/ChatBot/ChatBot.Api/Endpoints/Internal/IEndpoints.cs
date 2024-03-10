namespace ChatBot.Api.Endpoints;

public interface IEndpoints
{
    public static abstract void DefineEndpoints(IEndpointRouteBuilder app);
    public static abstract void AddService(IServiceCollection services, IConfiguration configuration);
}
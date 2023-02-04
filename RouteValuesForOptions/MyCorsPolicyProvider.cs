using Microsoft.AspNetCore.Cors.Infrastructure;

namespace RouteValuesForOptions;

internal class MyCorsPolicyProvider :
    ICorsPolicyProvider
{
    public Task<CorsPolicy?> GetPolicyAsync(
        HttpContext context,
        string? policyName)
    {
        var id = (string?)context.GetRouteValue("id");

        var policyBuilder = new CorsPolicyBuilder()
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();

        if (id is not null)
        {
            policyBuilder.WithExposedHeaders("RouteValueObtained");
        }

        var policy = policyBuilder.Build();

        return Task.FromResult<CorsPolicy?>(policy);
    }
}

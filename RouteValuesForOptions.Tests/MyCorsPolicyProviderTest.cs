using Microsoft.AspNetCore.Mvc.Testing;

namespace RouteValuesForOptions.Tests;

public class MyCorsPolicyProviderTest
{
    [Fact]
    public async Task GetRequest()
    {
        await using var factory = new WebApplicationFactory<Program>();

        using var client = factory.CreateDefaultClient();

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/1");

        request.Headers.Add("Origin", "http://example.com");

        using var response = await client.SendAsync(request).ConfigureAwait(false);

        var hasExposeHeaders = response.Headers.TryGetValues("Access-Control-Expose-Headers", out var exposeHeaders);

        Assert.True(hasExposeHeaders);
        Assert.Contains("RouteValueObtained", exposeHeaders!);
    }

    [Fact]
    public async Task PreflightRequest()
    {
        await using var factory = new WebApplicationFactory<Program>();

        using var client = factory.CreateDefaultClient();

        using var request = new HttpRequestMessage(
            HttpMethod.Options,
            "/api/1");

        request.Headers.Add("Origin", "http://example.com");
        request.Headers.Add("Access-Control-Request-Method", "GET");

        using var response = await client.SendAsync(request).ConfigureAwait(false);

        var hasExposeHeaders = response.Headers.TryGetValues("Access-Control-Expose-Headers", out var exposeHeaders);

        // This test will pass if the MyCorsPolicyProvider was able to
        // obtain route values from OPTIONS requests.
        Assert.True(hasExposeHeaders);
        Assert.Contains("RouteValueObtained", exposeHeaders!);
    }
}

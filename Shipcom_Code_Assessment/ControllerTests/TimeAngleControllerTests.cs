using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Shipcom_Code_Assessment.ControllerTests;

public class TimeAngleControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TimeAngleControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData(3, 0, 90)]
    [InlineData(9, 30, 465)]
    [InlineData(12, 0, 0)]
    [InlineData(4, 3, 139.5)]
    [InlineData(14, 12, 138)]
    public async Task GetTimeAngleCalculation_ReturnsCorrectAngles(int hour, int  minute, double expectedAngle)
    {
        var response = await _client.GetAsync($"/calculateTimeAngle/{hour}/{minute}");
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadAsStringAsync();
        Assert.Equal(expectedAngle.ToString(CultureInfo.InvariantCulture), result);
    }

    [Theory]
    [InlineData(-5, 0)]
    [InlineData(24, 10)]
    [InlineData(23, 60)]
    public async Task GetTimeAngleCalculation_ReturnsBadRequest(int hour, int minute)
    {
        var response = await _client.GetAsync($"/calculateTimeAngle/{hour}/{minute}");
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetTimeAngleCalculationFromDateTime_ReturnsSuccess()
    {
        var response = await _client.GetAsync($"/calculateTimeAngleFromDateTime?time={DateTime.Now}");
        response.EnsureSuccessStatusCode();
    }
}
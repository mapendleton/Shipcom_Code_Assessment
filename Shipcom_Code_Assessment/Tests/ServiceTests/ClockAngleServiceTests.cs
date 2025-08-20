using System.Globalization;
using Shipcom_Code_Assessment.Services;
using Xunit;

namespace Shipcom_Code_Assessment.Tests.ServiceTests;

public class ClockAngleServiceTests
{
    private readonly IClockAngleService _clockAngleService = new ClockAngleService();

    [Theory]
    [InlineData(3, 0, 90)]
    [InlineData(9, 30, 465)]
    [InlineData(12, 0, 0)]
    [InlineData(4, 3, 139.5)]
    [InlineData(14, 12, 138)]
    [InlineData(19, 54, 561)]
    public void CalculateTimeAngle_ReturnsCorrectAngle(int hour, int  minute, double expectedAngle)
    {
        var result = _clockAngleService.CalculateTimeAngle(hour, minute);
        Assert.Equal(expectedAngle, result);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(2, 70)]
    [InlineData(24, 0)]
    [InlineData(23, -30)]
    public void CalculateTimeAngle_ThrowsExceptionOnInvalidArguments(int hour, int minute)
    {
        var exception = Assert.Throws<ArgumentException>(() => _clockAngleService.CalculateTimeAngle(hour, minute));
        Assert.Equal("Invalid hour/minute, hour must be between 0 and 23 and minute must be between 0 and 59", exception.Message);
        
    }
}
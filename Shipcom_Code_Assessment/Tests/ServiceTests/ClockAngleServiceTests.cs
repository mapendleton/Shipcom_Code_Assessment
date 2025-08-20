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
    public void GetTimeAngleCalculation_ReturnsCorrectAngles(int hour, int  minute, double expectedAngle)
    {
        var result = _clockAngleService.CalculateTimeAngle(hour, minute);
        Assert.Equal(expectedAngle, result);
    }
}
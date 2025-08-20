namespace Shipcom_Code_Assessment.Services;

public interface IClockAngleService
{
    double CalculateTimeAngle(int hour, int minute);
}

public class ClockAngleService : IClockAngleService
{
    public double CalculateTimeAngle(int hour, int minute)
    {
        if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
        {
            throw new ArgumentException("Invalid hour/minute, hour must be between 0 and 23 and minute must be between 0 and 59");
        }
        
        var hourHand = hour % 12; // Get hour in 12-hour clock format
              
        // 360 divided by 12 (hours) = 30 so each hour is a multiple of 30
        double hourHandAngle = hourHand * 30;
        // The degrees between each Hour represented by the minute hand so we need to add 30 / 60 (minutes) = .5
        hourHandAngle += minute * .5;
              
        // 360 / 60 (minutes) = 6 so each minute is a multiple of 6
        var minuteHandAngle = minute * 6; 

        return hourHandAngle + minuteHandAngle;
    }
}
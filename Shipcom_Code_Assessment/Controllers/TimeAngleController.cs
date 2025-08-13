using Microsoft.AspNetCore.Mvc;

namespace Shipcom_Code_Assessment.Controllers;

[ApiController]
public class TimeAngleController : ControllerBase
{
       [HttpGet("/calculateTimeAngleFromDateTime")]
       public ActionResult<int> GetTimeAngleCalculationFromDateTime([FromQuery] DateTime time)
       {
              var hourHand = time.Hour % 12; // Get hour in 12-hour clock format
              var minuteHand = time.Minute;

              return CalculateTimeAngle(hourHand, minuteHand);
       }

       [HttpGet("/calculateTimeAngle/{hour}/{minute}")]
       public ActionResult<int> GetTimeAngleCalculation(int hour, int minute)
       {
              if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
              {
                     return BadRequest("Invalid hour/minute, hour must be between 0 and 23 and minute must be between 0 and 59");
              }
              
              return CalculateTimeAngle(hour, minute);
       }

       private int CalculateTimeAngle(int hourHand, int minuteHand)
       {
              // 360 divided by 12 (hours) = 30 so each hour is a multiple of 30
              // so to get the degrees by hour hand we'll multiply the number by 30
              // also if either is 12, then set to 0 degrees
              var hourHandAngle = hourHand == 12 ? 0 : hourHand * 30;
              
              // 360 / 60 (minutes) = 6 so each minute is a multiple of 6
              var minuteHandAngle = minuteHand * 6; 

              return hourHandAngle + minuteHandAngle;
       }
}
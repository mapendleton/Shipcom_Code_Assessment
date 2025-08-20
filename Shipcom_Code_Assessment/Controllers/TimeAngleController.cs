using Microsoft.AspNetCore.Mvc;

namespace Shipcom_Code_Assessment.Controllers;

[ApiController]
public class TimeAngleController : ControllerBase
{
       [HttpGet("/calculateTimeAngleFromDateTime")]
       public ActionResult<double> GetTimeAngleCalculationFromDateTime([FromQuery] DateTime time)
       {
              var hourHand = time.Hour % 12; // Get hour in 12-hour clock format
              var minuteHand = time.Minute;

              return CalculateTimeAngle(hourHand, minuteHand);
       }

       [HttpGet("/calculateTimeAngle/{hour}/{minute}")]
       public ActionResult<double> GetTimeAngleCalculation(int hour, int minute)
       {
              if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
              {
                     return BadRequest("Invalid hour/minute, hour must be between 0 and 23 and minute must be between 0 and 59");
              }
              
              return CalculateTimeAngle(hour, minute);
       }

       private double CalculateTimeAngle(int hourHand, int minuteHand)
       {
              // 360 divided by 12 (hours) = 30 so each hour is a multiple of 30
              // so to get the degrees by hour hand we'll multiply the number by 30
              // also if either is 12, then set to 0 degrees
              double hourHandAngle = hourHand == 12 ? 0 : hourHand * 30;
              
              // Think I forgot to include degrees between the hour hands like 12 to 3 is 0 - 90 not just 0 then 30 when it hits "1" and 60 when it hits "2" there's an in between
              // so 30 / 60 (min hand) is .5
              hourHandAngle += (minuteHand * .5);
              
              // 360 / 60 (minutes) = 6 so each minute is a multiple of 6
              var minuteHandAngle = minuteHand * 6; 

              return hourHandAngle + minuteHandAngle;
       }
}
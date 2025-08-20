using Microsoft.AspNetCore.Mvc;

namespace Shipcom_Code_Assessment.Controllers;

[ApiController]
public class TimeAngleController : ControllerBase
{
       /// <summary>
       /// Calculates the angle of the clock hands based on DateTime.
       /// </summary>
       /// <param name="time">DateTime to get angle from. Ex : 2025-08-19 21:14:00</param>
       /// <returns>The sum of the hour hand and minute hand angles.</returns>
       [HttpGet("/calculateTimeAngleFromDateTime")]
       [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
       [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public ActionResult<double> GetTimeAngleCalculationFromDateTime([FromQuery] DateTime time) => 
              CalculateTimeAngle(time.Hour, time.Minute);

       /// <summary>
       /// Calculates the angle of the clock hands based on the hour and minute passed. 
       /// </summary>
       /// <param name="hour">24 hours in a day, 0 representing 12 AM (Midnight) and 23 representing 11 PM. A number outside that range will return a 400</param>
       /// <param name="minute">60 minutes in an hour, any number outside 0 - 59 will return a 400</param>
       /// <returns>The sum of the hour hand and minute hand angles.</returns>
       [HttpGet("/calculateTimeAngle/{hour}/{minute}")]
       [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
       [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public ActionResult<double> GetTimeAngleCalculation(int hour, int minute)
       {
              if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
              {
                     return BadRequest("Invalid hour/minute, hour must be between 0 and 23 and minute must be between 0 and 59");
              }
              
              return CalculateTimeAngle(hour, minute);
       }

       private double CalculateTimeAngle(int hour, int minute)
       {
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
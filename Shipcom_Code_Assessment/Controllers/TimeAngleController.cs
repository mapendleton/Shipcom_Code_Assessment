using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Shipcom_Code_Assessment.Models;
using Shipcom_Code_Assessment.Services;

namespace Shipcom_Code_Assessment.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class TimeAngleController : ControllerBase
{
       private readonly IClockAngleService _clockAngleService;
       
       public TimeAngleController(IClockAngleService  clockAngleService)
       {
              _clockAngleService = clockAngleService;
       }
       
       /// <summary>
       /// Calculates the angle of the clock hands based on DateTime.
       /// </summary>
       /// <param name="time">DateTime to get angle from. Ex : 2025-08-19 21:14:00</param>
       /// <returns>The sum of the hour hand and minute hand angles.</returns>
       [HttpGet("calculateTimeAngleFromDateTime")]
       [MapToApiVersion("1.0")]
       [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
       [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public ActionResult<double> GetTimeAngleCalculationFromDateTime([FromQuery] DateTime time) => 
              _clockAngleService.CalculateTimeAngle(time.Hour, time.Minute);

       /// <summary>
       /// Calculates the angle of the clock hands based on the hour and minute passed. 
       /// </summary>
       /// <param name="hour">24 hours in a day, 0 representing 12 AM (Midnight) and 23 representing 11 PM. A number outside that range will return a 400</param>
       /// <param name="minute">60 minutes in an hour, any number outside 0 - 59 will return a 400</param>
       /// <returns>The sum of the hour hand and minute hand angles.</returns>
       /// <response code="400">If hour or minute is out of range</response>
       [HttpGet("calculateTimeAngle/{hour}/{minute}")]
       [MapToApiVersion("1.0")]
       [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
       [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
       public ActionResult<double> GetTimeAngleCalculation(int hour, int minute)
       {
              if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
              {
                     return BadRequest(new ErrorResponse("Invalid hour/minute, hour must be between 0 and 23 and minute must be between 0 and 59"));
              }
              
              return _clockAngleService.CalculateTimeAngle(hour, minute);
       }
}
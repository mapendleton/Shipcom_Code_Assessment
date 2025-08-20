using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Shipcom_Code_Assessment.Models;
using Shipcom_Code_Assessment.Services;

namespace Shipcom_Code_Assessment.Controllers;

/// <summary>
/// Controller with endpoints that calculate the angle of the clock hands based on time passed in.
/// </summary>
/// <remarks>
/// Currently using try/catch to grab validation error from service. Would probably want to set up some middleware to handle the exceptions instead
/// but not sure how deep I need to go for this.
/// </remarks>
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
       [HttpGet("CalculateTimeAngleFromDateTime")]
       [MapToApiVersion("1.0")]
       [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
       [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public ActionResult<double> GetTimeAngleCalculationFromDateTime([FromQuery] DateTime time)
       {
              try
              {
                     return _clockAngleService.CalculateTimeAngle(time.Hour, time.Minute);
              }
              catch (ArgumentException e)
              {
                     return BadRequest(new ErrorResponse(e.Message));
              }
       }

       /// <summary>
       /// Calculates the angle of the clock hands based on the hour and minute passed. 
       /// </summary>
       /// <param name="hour">24 hours in a day, 0 representing 12 AM (Midnight) and 23 representing 11 PM. A number outside that range will return a 400</param>
       /// <param name="minute">60 minutes in an hour, any number outside 0 - 59 will return a 400</param>
       /// <returns>The sum of the hour hand and minute hand angles.</returns>
       /// <response code="400">If hour or minute is out of range</response>
       [HttpGet("CalculateTimeAngle/{hour}/{minute}")]
       [MapToApiVersion("1.0")]
       [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
       [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
       public ActionResult<double> GetTimeAngleCalculation(int hour, int minute)
       {
              try
              {
                     return _clockAngleService.CalculateTimeAngle(hour, minute);
              }
              catch (ArgumentException e)
              {
                     return BadRequest(new ErrorResponse(e.Message));
              }
       }
}
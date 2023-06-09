using CarListApi.Models;
using CarListApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Google.Apis.Auth;
using Google.Apis.CloudIdentity.v1;
using Microsoft.AspNetCore.Authorization;

namespace CarListApi.Controllers
{
    [ApiController, Authorize]
    //[Route("[controller]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public class CarsController : ControllerBase
    {

        private readonly ILogger<CarsController> _logger;
        private readonly CarListDbContext _carListDbContext;

        public CarsController(ILogger<CarsController> logger,
                              CarListDbContext carListDbContext)
        {
            _logger = logger;
            _carListDbContext = carListDbContext;
        }

        [HttpGet("cars")]
        public async Task<IActionResult> GetAll()
        {

            var baseClientInitializer = new Google.Apis.Services.BaseClientService.Initializer
            {
                ApiKey = Environment.GetEnvironmentVariable("CarListApiKey"),
                ApplicationName = "CarListApi"
            };
            
            //var service = new Google.Apis.CloudIdentity.v1.CloudIdentityService(baseClientInitializer);
            //service.
            try
            {
                var cars = await _carListDbContext.Cars.ToListAsync<Car>();
                if(cars is null)
                {
                    // Could not find required dataset
                    return StatusCode(404);
                }
                return Ok(cars);
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to retrieve data");
            }
        }

        [HttpGet("cars/{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            try
            {
                var car = await _carListDbContext.Cars.FirstOrDefaultAsync(i => i.Id == id);
                if(car is null)
                {
                    // Could not find this specific car in database
                    return StatusCode(404, "Could not find the required Car, provided id does not exist in store.");
                }
                return Ok(car);
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to retrieve data");
            }
        }

        [HttpPut("cars")]
        public async Task<IActionResult> Put([FromBody] Car car)
        {
            bool isUpdateOp = false;
            try
            {
                var record = await _carListDbContext.Cars.FirstOrDefaultAsync(i => i.Id == car.Id);
                if (record is null)
                {
                    // Insert new car in database
                    var updatedRecord = await _carListDbContext.AddAsync(car);
                    if (updatedRecord is null) 
                    {
                        throw new Exception("Uh oh ! Could not create new entry in DB !");
                    }
                }
                else
                {
                    // Update old record with new data
                    isUpdateOp = true;
                    record.CopyFrom(car);
                }

                await _carListDbContext.SaveChangesAsync();
                var opKindTxt = isUpdateOp ? "updated" : "created";
                return Ok($"Successfully {opKindTxt} car record in database");
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to retrieve data");
            }
        }

        [HttpDelete("cars/{id}")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var record = await _carListDbContext.Cars.FirstOrDefaultAsync(i => i.Id == id);
                if (record is null)
                {
                    return Ok("Nothing to do, no entry for this id.");
                }
                else
                {
                    _carListDbContext.Remove(record);
                }

                await _carListDbContext.SaveChangesAsync();
                return Ok($"Successfully deleted car record from database");
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to delete data");
            }
        }

    }
}
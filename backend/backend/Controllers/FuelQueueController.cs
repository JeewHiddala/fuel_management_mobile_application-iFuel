using System;
using Microsoft.AspNetCore.Mvc;
using Ifuel.Services;
using Ifuel.Models;

namespace Ifuel.Controllers;

[Controller]
[Route("api/[controller]")]
public class FuelQueueController : Controller
{
    // Declearing the fuel station service instance
    private readonly FuelQueueService _fuelQueueService;
    private readonly FuelStationService _fuelStationService;
    public FuelQueueController(FuelQueueService fuelQueueService, FuelStationService fuelStationService) {
        _fuelQueueService = fuelQueueService;
        _fuelStationService = fuelStationService;
    }
    
    // create a fuel queue
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FuelQueue fuelQueue)
    {
            await _fuelQueueService.CreateAsync(fuelQueue);
            return CreatedAtAction(nameof(GetFuelStations), new {id = fuelQueue.Id}, fuelQueue);
    }

    // get all fuel queues
    [HttpGet]
    public async Task<List<FuelQueue>> GetFuelStations()
    {
        return await _fuelQueueService.GetAsync();
    }

    // get fuel queue point by id
    [HttpGet("{id}")]
    public async Task<FuelQueue> GetFuelStationById(string id)
    {
        return await _fuelQueueService.GetByIdAsync(id);
    }

    // get fuel queue length
    [HttpGet]
    public async Task<Array> GetQueueLengthByVehicalType(string userVehicalType){
        return await _fuelQueueService.GetQueueLengthAsync(userVehicalType);
    }

    // get fuel queue time 
    [HttpGet]
    [Route("GetQueueTime")]
    public async Task<Array> GetQueueTime(string id){
        var station = await _fuelStationService.GetFuelStationById(id);
        var res = await _fuelQueueService.GetQueueTimeAsync(station.Queue[0]);
        return res;
    }

    // update departure time in fuel queue
    [HttpPut("{id}")]
    // [Route("UpdateFuelStatus")]
    public async Task<IActionResult> UpdateDepartureTime(string id, [FromBody] string departureTime, [FromBody] bool fuelPumpStatus)
    {
        await _fuelQueueService.UpdateFuelQueueAsync(id, departureTime, fuelPumpStatus);
        return NoContent();
    }

    // delete past queue data
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id){
        await _fuelQueueService.DeleteAsync(id);
        return NoContent();
    }

}
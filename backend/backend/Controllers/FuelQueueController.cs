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
    public FuelQueueController(FuelQueueService fuelQueueService) {
        _fuelQueueService = fuelQueueService;
    }
    
    // This is required to create a fuel queue
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FuelQueue fuelQueue)
    {
            await _fuelQueueService.CreateAsync(fuelQueue);
            return CreatedAtAction(nameof(GetFuelStations), new {id = fuelQueue.Id}, fuelQueue);
    }

    // This is required to get all fuel queues
    [HttpGet]
    public async Task<List<FuelQueue>> GetFuelStations()
    {
        return await _fuelQueueService.GetAsync();
    }

    // This is required to get fuel queue point by id
    [HttpGet("{id}")]
    public async Task<FuelQueue> GetFuelStationById(string id)
    {
        return await _fuelQueueService.GetByIdAsync(id);
    }

    // update departure time in fuel queue
    [HttpPut("{id}")]
    // [Route("UpdateFuelStatus")]
    public async Task<IActionResult> UpdateDepartureTime(string id, [FromBody] string departureTime, [FromBody] bool fuelPumpStatus)
    {
        await _fuelQueueService.UpdateFuelQueueAsync(id, departureTime, fuelPumpStatus);
        return NoContent();
    }

    // delete queue data
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id){
        await _fuelQueueService.DeleteAsync(id);
        return NoContent();
    }


    // // update fuel status in fuel station
    // [HttpPut("{id}")]
    // // [Route("UpdateFuelStatus")]
    // public async Task<IActionResult> UpdateFuelPumpStatus(string id, [FromBody] bool fuelPumpStatus)
    // {
    //     await _fuelQueueService.UpdateFuelPumpStatusAsync(id, fuelPumpStatus);
    //     return NoContent();
    // }

    // // This is required to update petrol status
    // [HttpPut]
    // [Route("UpdatePetrolStatus")]
    // public async Task<FuelStationModel> UpdatePetrolStatus(bool status, string id)
    // {
    //     try
    //     {
    //         var res = await _fuelStationService.UpdatePetrolStatus(status, id);
    //         return res;
    //     }
    //     catch (Exception ex)
    //     {
    //         return null;
    //     }
    // }

    // [HttpPut]
    // [Route("UpdateFuelAmount")]
    // // This is required to update the total fuel amount
    // public async void UpdateTotalFuelAmount(string stationId, int amount, string type)
    // {
    //     try
    //     {
    //         var currentAmount = await _fuelStationService.getCurrentFuelAmount(stationId, type);
    //         _fuelStationService.UpdateTotalFuelAmount(stationId, amount, type, currentAmount);
    //     }
    //     catch(Exception ex)
    //     {

    //     }
    // }
}
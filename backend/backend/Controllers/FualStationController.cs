using System;
using Microsoft.AspNetCore.Mvc;
using Ifuel.Services;
using Ifuel.Models;

namespace Ifuel.Controllers;

[Controller]
[Route("api/[controller]")]
public class FuelStationController : Controller
{
    // Declearing the fuel station service instance
    private readonly FuelStationService _fuelStationService;
    public FuelStationController(FuelStationService fuelStationService) {
        _fuelStationService = fuelStationService;
    }
    
    // This is required to create a fuel station
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FuelStation fuelStation)
    {
            await _fuelStationService.CreateAsync(fuelStation);
            return CreatedAtAction(nameof(GetFuelStations), new {id = fuelStation.Id}, fuelStation);
    }

    // This is required to get all fuel stations
    [HttpGet]
    public async Task<List<FuelStation>> GetFuelStations()
    {
        return await _fuelStationService.GetAsync();
    }

    // This is required to get fuel station by id
    [HttpGet("{id}")]
    public async Task<FuelStation> GetFuelStationById(string id)
    {
        return await _fuelStationService.GetByIdAsync(id);
    }

    // // update fuel status in fuel station
    // [HttpPut("{id}")]
    // [Route("UpdateFuelStatus")]
    // public async Task<IActionResult> UpdateFuelStatus(string id, [FromBody] object fuelStatus)
    // {
    //     await _fuelStationService.UpdateFuelStatus(id, fuelStatus);
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
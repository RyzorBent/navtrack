﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Navtrack.Web.Models;
using Navtrack.Web.Models.Locations;
using Navtrack.Web.Services;

namespace Navtrack.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService locationService;

        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet("history")]
        public async Task<List<LocationModel>> GetHistory([FromQuery] LocationHistoryRequestModel model)
        {
            List<LocationModel> locations = await locationService.GetLocations(model);

            return locations;
        }

        [HttpGet("{assetId}/latest")]
        public async Task<LocationModel> GetLatest(int assetId)
        {
            LocationModel location = await locationService.GetLatestLocation(assetId);

            return location;
        }
    }
}
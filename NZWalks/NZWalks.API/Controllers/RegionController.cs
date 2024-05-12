using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilers;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;
using System.Collections.Generic;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionController> logger;

        public RegionController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        // [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetAll()  // Async   : Should make all long running tasks async (Db calls : API calls)
        {
            var regionsDto = new List<RegionDto>();

            try
            {
                logger.LogInformation("Info GetAllRegions has been Invoked");  // if minimun level is warning, can't use Info
                //logger.LogWarning("Warning GetAllRegions has been Invoked");

                var regions = await regionRepository.GetAllAsync();

                regionsDto = mapper.Map<List<RegionDto>>(regions);

                logger.LogInformation($"GetAllRegions has Completed with data: {JsonSerializer.Serialize(regionsDto)}"); // Return data as Json to logger
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return Ok(regionsDto); // GET : OK
        }

        [HttpGet]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);   // GET : OK 200 response
        }

        [HttpPost]
        [ValidateModelAttributes]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> Create([FromBody] AddRegionDto request)
        {
            //Map DTO to Domain model so we can insert in the DB

            var newRegion = await regionRepository.CreateAsync(request);

            var regionDto = new RegionDto()
            {
                Id = newRegion.Id,
                Code = newRegion.Code,
                Name = newRegion.Name,
                RegionImageUrl = newRegion.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = newRegion.Id }, newRegion); //POST : 201 response....return created Item
                                                                                           // return CreatedAtAction("GetById", new { id = region.Id}, regionDto); //POST : same as above
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModelAttributes]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto request)
        {
            var regionDomainModel = await regionRepository.UpdateAsync(id, request);

            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto); // PUT : 200 response....return success and updated Item
        }

        [HttpDelete]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Map DTO to Domain model so we can insert in the DB

            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto); // DELETE : 200 response....return success and updated Item
            //return Ok(); This also works  
        }
    }
}


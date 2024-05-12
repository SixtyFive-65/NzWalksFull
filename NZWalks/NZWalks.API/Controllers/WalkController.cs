using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilers;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalkController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        public IWalkRepository WalkRepository { get; }

        [HttpPost]
        [ValidateModelAttributes]

        public async Task<IActionResult> Create([FromBody]AddWalkRequestDto request)
        {
            var requestDto = mapper.Map<Walk>(request);

            var addWalk = walkRepository.CreateAsync(requestDto);

            mapper.Map<WalkDto>(requestDto);

            return Ok(requestDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy = null, 
            [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, int pageSize = 1000)
        {
            throw new Exception("middleware exception");  

            var walks = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize); 

            return Ok(mapper.Map<List<WalkDto>>(walks));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById( Guid id)
        {
            var walks = await walkRepository.GetByIdAsync(id);

            if (walks == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walks));
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModelAttributes]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto request)
        {
            var addWalk = await walkRepository.UpdateAsync(id, mapper.Map<Walk>(request));

            if (addWalk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(addWalk));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var walks = await walkRepository.RemoveAsync(id);

            if (walks == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walks));
        }
    }
}

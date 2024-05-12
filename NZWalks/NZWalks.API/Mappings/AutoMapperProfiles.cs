using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionDto, Region>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();
            
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
                      
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }


        public class UserDto
        {
            public string FullName { get; set; }
        }

        public class UserDomain
        {
            public string Name { get; set; }
        }
        //public AutoMapperProfiles()
        //{
        //    CreateMap<UserDto, UserDomain>()   // CreateMap<source, destination>
        //        .ForMember(_ => _.Name, opt => opt.MapFrom(_ => _.FullName)) // Define which property differentley named properties should link from
        //        .ReverseMap();

        //    CreateMap<Region, RegionDto>().ReverseMap();
        //    CreateMap<Region, AddRegionDto>().ReverseMap();
        //    CreateMap<Region, UpdateRegionDto>().ReverseMap();
        //}
        //public class UserDto
        //{
        //    public string FullName { get; set; }
        //}

        //public class UserDomain
        //{
        //    public string Name { get; set; }
        //}
    }
}

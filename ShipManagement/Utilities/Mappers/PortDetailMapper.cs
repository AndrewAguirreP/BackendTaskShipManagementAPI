using AutoMapper;

using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Common.Mappers
{
    public class PortDetailMapper : Profile
    {
        public PortDetailMapper()
        {
            CreateMap<Port, PortDetail>()
                .ForMember(destination => destination.GeoLocation,
                        options => options.MapFrom((source, destination) => new GeoLocation(source.Latitude, source.Longitude)))
                .ForMember(destination => destination.Name,
                        options => options.MapFrom((source, destination) => source.Name))
                .ForMember(destination => destination.Id,
                        options => options.MapFrom((source, destination) => source.Id));
        }
    }
}
using AutoMapper;

using ShipManagement.Common;
using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Utilities.Mappers
{
    public class ShipDetailMapper : Profile
    {
        public ShipDetailMapper()
        {
            CreateMap<Ship, ShipDetail>()
                .ForMember(destination => destination.CurrentGeoLocation,
                        options => options.MapFrom((source, destination) => { 
                            var latestStatus = source.GetLatestStatus();

                            return new GeoLocation(latestStatus?.Latitude ?? 0, latestStatus?.Longitude ?? 0); 
                        }))
                .ForMember(destination => destination.Name,
                        options => options.MapFrom((source, destination) => source.Name))
                .ForMember(destination => destination.UniqueShipId,
                        options => options.MapFrom((source, destination) => source.UniqueShipId))
                .ForMember(destination => destination.Velocity,
                        options => options.MapFrom((source, destination) => source.GetLatestStatus()?.Velocity ?? 0));
        }
    }
}

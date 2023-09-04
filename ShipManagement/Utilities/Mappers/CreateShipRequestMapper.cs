using AutoMapper;

using ShipManagement.DTOs;
using ShipManagement.Repositories.Models;

namespace ShipManagement.Utilities.Mappers
{
    public class CreateShipRequestMapper : Profile
    {
        public CreateShipRequestMapper()
        {
            CreateMap<CreateShipRequest, Ship>()
                .ForMember(destination => destination.UniqueShipId,
                        options => options.MapFrom((source, destination) => source.UniqueShipId))
                .ForMember(destination => destination.Name,
                        options => options.MapFrom((source, destination) => source.Name))
                .ForMember(destination => destination.ShipStatuses,
                        options => options.MapFrom((source, destination) => BuildShipStatusRequest(source)));
        }

        private IEnumerable<ShipStatus> BuildShipStatusRequest(CreateShipRequest source)
        {
            var result = new List<ShipStatus>() {
                new ShipStatus()
                {
                    Velocity = source.Velocity,
                    Latitude = source.Latitude,
                    Longitude = source.Longitude,
                    LastCheckedIn = DateTime.Now
                }
            };

            return result;
        }
    }
}
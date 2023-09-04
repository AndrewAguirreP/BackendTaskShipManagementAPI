using AutoMapper;

using ShipManagement.Common;
using ShipManagement.DTOs;
using ShipManagement.Repositories.Interfaces;
using ShipManagement.Repositories.Models;
using ShipManagement.Services.Interfaces;

namespace ShipManagement.Services
{
    public class ShipService : IShipService
    {
        private readonly IShipRepository _shipRepository;
        private readonly IPortRepository _portRepository;
        private IMapper _mapper;

        public ShipService(IShipRepository shipRepository, IPortRepository portRepository, IMapper mapper) =>
            (_shipRepository, _portRepository, _mapper) = (shipRepository, portRepository, mapper);

        #region READ

        public async Task<ShipDetail?> GetByIdAsync(int id)
        {
            var shipDbResult = await _shipRepository.GetByIdAsync(id);

            if (shipDbResult == null)
                return null;

            return _mapper.Map<Ship, ShipDetail>(shipDbResult);
        }

        public async Task<IEnumerable<ShipDetail>> GetShipsAsync()
        {
            var shipDbResults = await _shipRepository.GetShipsAsync();

            if (shipDbResults.IsNullOrEmpty())
                return new List<ShipDetail>();

            return _mapper.Map<IEnumerable<Ship>, IEnumerable<ShipDetail>>(shipDbResults);
        }

        public async Task<GetClosestPortResponse?> GetClosestPortAsync(int shipId)
        {
            var result = new GetClosestPortResponse();
            var ship = await _shipRepository.GetByIdAsync(shipId);
            var ports = await _portRepository.GetPortsAsync();

            if (ship == null || ports.IsNullOrEmpty())
                return result;

            var closestPort = ship.ToClosestPort(ports);

            if (closestPort == null)
                return result;

            var shipDetail = _mapper.Map<Ship, ShipDetail>(ship);
            var closestPortDetail = _mapper.Map<Port, PortDetail>(closestPort);

            if (shipDetail.CurrentGeoLocation == null)
                return result;

            return new GetClosestPortResponse() 
            {
                ShipDetail = shipDetail,
                ClosestPort = closestPortDetail,
                TravelInformation = shipDetail.CurrentGeoLocation.GetTravelInformation(new GeoLocation(closestPort.Latitude, closestPort.Longitude), shipDetail.Velocity)
            };
        }

        #endregion READ

        #region WRITE

        public async Task<bool> CreateShipAsync(CreateShipRequest request)
        {
            var ship = _mapper.Map<CreateShipRequest, Ship>(request);

            return await _shipRepository.CreateShipAsync(ship);
        }

        public async Task<bool> PatchShipsVelocityAsync(int id, PatchShipsVelocityRequest request)
        {
            var ship = await _shipRepository.GetByIdAsync(id);
            if (ship == null)
            {
                return false;
            }

            var newShipStatus = new ShipStatus()
            {
                ShipId = ship.Id,
                Velocity = request.Velocity,
                LastCheckedIn = DateTime.Now
            };

            var latestStatus = ship.GetLatestStatus();

            if (latestStatus != null)
            {
                newShipStatus.Latitude = latestStatus.Latitude;
                newShipStatus.Longitude = latestStatus.Longitude;
            }

            return await _shipRepository.AddShipStatus(newShipStatus);
        }

        #endregion WRITE
    }
}
using AutoMapper;

using ShipManagement.Common;
using ShipManagement.DTOs;
using ShipManagement.Repositories.Interfaces;
using ShipManagement.Repositories.Models;
using ShipManagement.Services.Interfaces;

namespace ShipManagement.Services
{
    public class PortService : IPortService
    {
        private readonly IPortRepository _portRepository;
        private IMapper _mapper;

        public PortService(IShipRepository shipRepository, IPortRepository portRepository, IMapper mapper) => (_portRepository, _mapper) = (portRepository, mapper);

        #region READ

        public async Task<IEnumerable<PortDetail>> GetPortsAsync()
        {
            var portDbResults = await _portRepository.GetPortsAsync();

            if (portDbResults.IsNullOrEmpty())
                return new List<PortDetail>();

            return _mapper.Map<IEnumerable<Port>, IEnumerable<PortDetail>>(portDbResults);
        }

        #endregion READ
    }
}
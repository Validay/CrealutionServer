using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrealutionServer.Domain.Entities;
using CrealutionServer.Infrastructure.Database;
using CrealutionServer.Infrastructure.Repositories.Interfaces;
using CrealutionServer.Models.Dtos.StatisticTypes;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CrealutionServer.Infrastructure.Repositories
{
    public class StatisticTypeRepository : IStatisticTypeRepository
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly CrealutionDb _context;

        public StatisticTypeRepository(
            ILogger logger, 
            IMapper mapper,
            CrealutionDb context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }   

        public async Task<StatisticTypeGetAllDto> GetAll()
        {
            _logger.Information("GetAll");

            var entities = await _context.StatisticTypes
                .ToListAsync();

            return _mapper.Map<StatisticTypeGetAllDto>(entities);
        }

        public async Task<StatisticTypeDto> Create(StatisticTypeCreateDto createDto)
        {
            _logger.Information("Create");

            var entity = _mapper.Map<StatisticType>(createDto);
            var entryEntity = await _context.StatisticTypes.AddAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<StatisticTypeDto>(entryEntity.Entity);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class EventTypeData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EventTypeData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<EventType>> GetAllAsync()
        {
            return await _context.Set<EventType>().ToListAsync();
        }

        public async Task<EventType?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<EventType>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el EventType con id {EventTypeId}", id);
                throw;
            }
        }

        public async Task<EventType> CreateAsync(EventType eventType)
        {
            try
            {
                await _context.Set<EventType>().AddAsync(eventType);
                await _context.SaveChangesAsync();
                return eventType;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el EventType: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(EventType eventType)
        {
            try
            {
                _context.Set<EventType>().Update(eventType);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al actualizar el EventType: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var eventType = await _context.Set<EventType>().FindAsync(id);
                if (eventType == null)
                    return false;
                _context.Set<EventType>().Remove(eventType);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al eliminar el EventType: {ex.Message}");
                return false;
            }
        }
    }
}

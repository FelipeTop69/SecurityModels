using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class EventData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EventData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            String Query = @"
                SELECT 
                    ev.Id,
                    ev.Name,
                    ev.Description,
                    ev.Date,
                    ev.EventType,
                    evt.Name
                FROM Event as ev
                INNER JOIN EventType as evt
                ON ev.EventType = evt.Id";

            return (IEnumerable<Event>) await _context.QueryAsync<IEnumerable<Event>>(Query);
            //return await _context.Set<Event>().ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            try
            {
                String Query = @"
                        SELECT 
                        ev.Id,
                        ev.Name,
                        ev.Description,
                        ev.Date,
                        ev.EventType,
                        evt.Name
                    FROM Event as ev
                    INNER JOIN EventType as evt
                    ON ev.EventType = evt.Id
                    WHERE ev.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<Event>(Query, new {Id = id});
                //return await _context.Set<Event>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Eventt con id {EventtId}", id);
                throw;
            }
        }

        public async Task<Event> CreateAsync(Event eventP)
        {
            try
            {
                await _context.Set<Event>().AddAsync(eventP);
                await _context.SaveChangesAsync();
                return eventP;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el Eventt: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Event eventP)
        {
            try
            {
                _context.Set<Event>().Update(eventP);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el Eventt: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var eventt = await _context.Set<Event>().FindAsync(id);
                if (eventt == null)
                    return false;
                _context.Set<Event>().Remove(eventt);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al eliminar el Eventt: {ex.Message}");
                return false;
            }
        }
    }
}

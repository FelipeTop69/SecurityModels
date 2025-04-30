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
    public class EventSessionData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EventSessionData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<EventSession>> GetAllAsync()
        {
            String Query = @"
                SELECT 
                    evs.Id,
                    evs.Name,
                    evs.StartDate,
                    evs.EndDate,
                    evs.EventId,
                    ev.Name
                FROM EventSession as evs
                INNER JOIN Event as ev
                ON evs.EventId = ev.Id
                ";

            return (IEnumerable<EventSession>) await _context.QueryAsync<IEnumerable<EventSession>>(Query);
            //return await _context.Set<EventSession>().ToListAsync();
        }

        public async Task<EventSession?> GetByIdAsync(int id)
        {
            try
            {
                String Query = @"
                SELECT 
                    evs.Id,
                    evs.Name,
                    evs.StartDate,
                    evs.EndDate,
                    evs.EventId,
                    ev.Name
                FROM EventSession as evs
                INNER JOIN Event as ev
                ON evs.EventId = ev.Id
                WHERE ev.id = @Id";

                return await _context.QueryFirstOrDefaultAsync<EventSession>(Query, new { Id = id });
                //return await _context.Set<EventSession>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el EventSession con id {EventSessionId}", id);
                throw;
            }
        }

        public async Task<EventSession> CreateAsync(EventSession eventSession)
        {
            try
            {
                await _context.Set<EventSession>().AddAsync(eventSession);
                await _context.SaveChangesAsync();
                return eventSession;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el EventSession: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(EventSession eventSession)
        {
            try
            {
                _context.Set<EventSession>().Update(eventSession);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al actualizar el EventSession: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var eventSession = await _context.Set<EventSession>().FindAsync(id);
                if (eventSession == null)
                    return false;
                _context.Set<EventSession>().Remove(eventSession);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al eliminar el EventSession: {ex.Message}");
                return false;
            }
        }
    }
}

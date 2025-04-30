using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class AttendanceData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AttendanceData(ApplicationDbContext context, ILogger logger)
        {
            _context = context; 
            _logger = logger;
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            string query = @"
                SELECT 
                    SELECT 
                        at.Id, 
                        at.CardId, 
                        ca.Name as CardName, 
                        at.EventSessionId, 
                        es.Name as EventSessionName
                    FROM Attendance at
                    INNER JOIN Card ca 
                    ON at.CardId = ca.Id
                    INNER JOIN EventSession es 
                    ON at.EventSessionId = es.Id";

            return (IEnumerable<Attendance>)await _context.QueryAsync<IEnumerable<Attendance>>(query);
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
            try
            {
                string query = @"
                    SELECT 
                        at.Id, 
                        at.CardId, 
                        ca.Name AS CardName, 
                        at.EventSessionId, 
                        es.Name AS EventSessionName
                    FROM Attendance at
                    INNER JOIN Card ca 
                    ON at.CardId = ca.Id
                    INNER JOIN EventSession es 
                    ON at.EventSessionId = es.Id
                    WHERE at.Id = @Id";
                    return await _context.QueryFirstOrDefaultAsync<Attendance>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Attendace con id {AttendanceId}", id);
                throw;
            }
        }

        public async Task<Attendance> CreateAsync(Attendance attendance)
        {
            try
            {
                await _context.Set<Attendance>().AddAsync(attendance);
                await _context.SaveChangesAsync();
                return attendance;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el Assignament: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Attendance attendance)
        {
            try
            {
                _context.Set<Attendance>().Update(attendance);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al actualizar el Assignament: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var attendance = await _context.Set<Attendance>().FindAsync(id);
                if (attendance == null)
                    return false;
                _context.Set<Attendance>().Remove(attendance);
                await _context.SaveChangesAsync ();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al eliminar el Assignament: {ex.Message}");
                return false;
            }
        }
    }
}

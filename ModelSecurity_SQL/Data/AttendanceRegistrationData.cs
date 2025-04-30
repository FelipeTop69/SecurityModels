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
    public class AttendanceRegistrationData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AttendanceRegistrationData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<AttendanceRegistration>> GetAllAsync()
        {
            string query = @"
                SELECT 
                    atr.Id, 
                    atr.Hour, 
                    atr.IsEntrance, 
                    atr.AttendanceId, 
                    at.Name as AttendanceName, 
                    atr.AccessPointId, 
                    ap.Name as AccessPoint
                FROM AttendanceRegistration atr
                INNER JOIN Attendance at
                ON atr.AttendanceId = at.Id
                INNER JOIN AccessPoint ap
                ON atr.AccessPoint = ap.Id";

            return (IEnumerable<AttendanceRegistration>) await _context.QueryAsync<IEnumerable<AttendanceRegistration>>(query);
        }

        public async Task<AttendanceRegistration?> GetByIdAsync(int id)
        {
            try
            {
                string query = @"
                    SELECT 
                        atr.Id, 
                        atr.Hour, 
                        atr.IsEntrance, 
                        atr.AttendanceId, 
                        at.Name as AttendanceName, 
                        atr.AccessPointId, 
                        ap.Name as AccessPoint
                    FROM AttendanceRegistration atr
                    INNER JOIN Attendance at
                    ON atr.AttendanceId = at.Id
                    INNER JOIN AccessPoint ap
                    ON atr.AccessPointId = ap.Id
                    WHERE at.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<AttendanceRegistration>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Attendace con id {AttendanceRegistrationId}", id);
                throw;
            }
        }

        public async Task<AttendanceRegistration> CreateAsync(AttendanceRegistration attendanceRegistration)
        {
            try
            {
                await _context.Set<AttendanceRegistration>().AddAsync(attendanceRegistration);
                await _context.SaveChangesAsync();
                return attendanceRegistration;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el Assignament: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AttendanceRegistration attendanceRegistration)
        {
            try
            {
                _context.Set<AttendanceRegistration>().Update(attendanceRegistration);
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
                var attendanceRegistration = await _context.Set<AttendanceRegistration>().FindAsync(id);
                if (attendanceRegistration == null)
                    return false;
                _context.Set<AttendanceRegistration>().Remove(attendanceRegistration);
                await _context.SaveChangesAsync();
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

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
    public class AccessPointData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AccessPointData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los AccessPoint almacenados en la base de datos
        /// </summary>
        public async Task<IEnumerable<AccessPoint>> GetAllAsync()
        {

            string query = @"
                SELECT 
                    ap.Id, 
                    ap.Name, 
                    ap.Ubication, 
                    ap.EventId as EventId, 
                    ev.Name as EventName
                FROM AccessPoint ap
                INNER JOIN Event ev
                ON ap.EventId = ev.Id";

            return (IEnumerable<AccessPoint>) await _context.QueryAsync<IEnumerable<AccessPoint>>(query);

        }

        /// <summary>
        /// Obtiene un AccessPoint especifico por su identificacion
        /// </summary>
        public async Task<AccessPoint?> GetByIdAsync(int id)
        {
            try
            {
                string query = @"
                SELECT
                    ap.Id, 
                    ap.Name, 
                    ap.Ubication, 
                    ap.EventId as EventId, 
                    ev.Name as EventName
                FROM AccessPoint ap
                INNER JOIN Event ev
                ON ap.EventId = ev.Id
                WHERE ap.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<AccessPoint>(query, new {Id = id});
 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el AccessPoint con Id {FormId}", id);
                throw;
            }
        }
        /// <summary>
        /// Crear un nuevo AccessPoint en la base de datos
        /// </summary>
        public async Task<AccessPoint> CreateAsync(AccessPoint accessPoint)
        {
            try
            {
                await _context.Set<AccessPoint>().AddAsync(accessPoint);
                await _context.SaveChangesAsync();
                return accessPoint;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el AccessPoint: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza un rol existente en la base de datos.
        /// </summary>
        public async Task<bool> UpdateAsync(AccessPoint accessPoint)
        {
            try
            {
                _context.Set<AccessPoint>().Update(accessPoint);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el AccessPoint: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un AccessPoint de la base de datos.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var accessPoint = await _context.Set<AccessPoint>().FindAsync(id);
                if (accessPoint == null)
                    return false;
                _context.Set<AccessPoint>().Remove(accessPoint);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el AccessPoint: {ex.Message}");
                return false;
            }
        }
    }
}

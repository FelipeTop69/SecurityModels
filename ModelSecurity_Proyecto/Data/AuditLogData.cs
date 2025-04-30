using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Data
{
    public class AuditLogData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AuditLogData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los AuditLog almacenados en la base de datos SQL
        /// </summary>
        public async Task<IEnumerable<AuditLog>> GetAllAsync()
        {
            String query = @"SELECT * FROM AuditLog";
            return (IEnumerable<AuditLog>)await _context.QueryAsync<IEnumerable<AuditLog>>(query);


            //return await _context.Set<AuditLogData>().ToListAsync();
        }

        /// <summary>
        /// Obtiene un AuditLog especifico por su identificacion SQL
        /// </summary
        public async Task<AuditLog?> GetByIdAsync(int id)
        {
            try
            {
                String query = @"SELECT * FROM AuditLog WHERE Id = @Id";
                return await _context.QueryFirstOrDefaultAsync<AuditLog>(query, new { Id = id });
                //return await _context.Set<AuditLogData>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la auditoria con ID {AuditLogId}", id);
                throw;
            }

        }

        /// <summary>
        /// Crear un nuevo AuditLog en la base de datos SQL
        /// </summary>
        public async Task<AuditLog> CreateAsync(AuditLog auditLog)
        {
            try
            {
                await _context.Set<AuditLog>().AddAsync(auditLog);
                await _context.SaveChangesAsync();
                return auditLog;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear la auditoria: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza un AuditLog existente en la base de datos SQL
        /// </summary>
        public async Task<bool> UpdateAsync(AuditLog auditLog)
        {
            try
            {
                _context.Set<AuditLog>().Update(auditLog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la auditoria : {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un AuditLog de la base de datos SQL
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var auditLog = await _context.Set<AuditLog>().FindAsync(id);
                if (auditLog == null)
                    return false;
                _context.Set<AuditLog>().Remove(auditLog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la auditoria {ex.Message}");
                return false;
            }
        }
    }
}

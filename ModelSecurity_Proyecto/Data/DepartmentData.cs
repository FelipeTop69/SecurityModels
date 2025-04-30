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
    public class DepartmentData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DepartmentData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Set<Department>().ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Department>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Department con id {DepartmentId}", id);
                throw;
            }
        }

        public async Task<Department> CreateAsync(Department department)
        {
            try
            {
                await _context.Set<Department>().AddAsync(department);
                await _context.SaveChangesAsync();
                return department;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el Department: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            try
            {
                _context.Set<Department>().Update(department);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al actualizar el Department: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var department = await _context.Set<Department>().FindAsync(id);
                if (department == null)
                    return false;
                _context.Set<Department>().Remove(department);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al eliminar el Department: {ex.Message}");
                return false;
            }
        }
    }
}

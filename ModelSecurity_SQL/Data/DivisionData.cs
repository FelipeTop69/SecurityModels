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
    public class DivisionData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DivisionData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Division>> GetAllAsync()
        {
            return await _context.Set<Division>().ToListAsync();
        }

        public async Task<Division?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Division>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Division con id {DivisionId}", id);
                throw;
            }
        }

        public async Task<Division> CreateAsync(Division division)
        {
            try
            {
                await _context.Set<Division>().AddAsync(division);
                await _context.SaveChangesAsync();
                return division;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el Division: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Division division)
        {
            try
            {
                _context.Set<Division>().Update(division);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al actualizar el Division: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var division = await _context.Set<Division>().FindAsync(id);
                if (division == null)
                    return false;
                _context.Set<Division>().Remove(division);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al eliminar el Division: {ex.Message}");
                return false;
            }
        }
    }
}

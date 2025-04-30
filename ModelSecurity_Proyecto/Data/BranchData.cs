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
    public class BranchData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public BranchData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            string query = @"
                SELECT 
                    b.Id, 
                    b.Name, 
                    b.Location, 
                    b.OrganizationId, 
                    o.Name as OrganizationName
                FROM Branch b
                INNER JOIN Organization o 
                ON b.OrganizationId = o.Id";

            return (IEnumerable<Branch>)await _context.QueryAsync<IEnumerable<Branch>>(query);
            //return await _context.Set<Branch>().ToListAsync();
        }

        public async Task<Branch?> GetByIdAsync(int id)
        {
            try
            {
                string query = @"
                    SELECT 
                        b.Id, 
                        b.Name, 
                        b.Location, 
                        b.OrganizationId, 
                        o.Name as OrganizationName
                    FROM Branch b
                    INNER JOIN Organization o 
                    ON b.OrganizationId = o.Id
                    WHERE b.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<Branch>(query, new { Id = id });
                //return await _context.Set<Branch>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Branch con id {BranchId}", id);
                throw;
            }
        }

        public async Task<Branch> CreateAsync(Branch branch)
        {
            try
            {
                await _context.Set<Branch>().AddAsync(branch);
                await _context.SaveChangesAsync();
                return branch;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el Branch: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Branch branch)
        {
            try
            {
                _context.Set<Branch>().Update(branch);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al actualizar el Branch: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var branch = await _context.Set<Branch>().FindAsync(id);
                if (branch == null)
                    return false;
                _context.Set<Branch>().Remove(branch);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al eliminar el Branch: {ex.Message}");
                return false;
            }
        }
    }
}

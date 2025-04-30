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
    public class DivisionBranchData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DivisionBranchData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<DivisionBranch>> GetAllAsync()
        {

            string Query = @"
                SELECT 
                    db.Id,
                    db.DivisionId as DivisionId,
                    d.Name as DivisionName,
                    db.BranchId as BranchId,
                    b.Name as BranchName
                FROM DivisionBranch as db
                INNER JOIN Division as d
                ON db.DivisionId = d.Id
                INNER JOIN Branch as b
                ON db.BranchId  = b.id";

            return (IEnumerable<DivisionBranch>) await _context.QueryAsync<IEnumerable<DivisionBranch>>(Query);
            //return await _context.Set<DivisionBranch>().ToListAsync();
        }

        public async Task<DivisionBranch?> GetByIdAsync(int id)
        {
            try
            {
                string Query = @"
                SELECT 
                    db.Id,
                    db.DivisionId as DivisionId,
                    d.Name as DivisionName,
                    db.BranchId as BranchId,
                    b.Name as BranchName
                FROM DivisionBranch as db
                INNER JOIN Division as d
                ON db.DivisionId = d.Id
                INNER JOIN Branch as b
                ON db.BranchId  = b.id
                WHERE db.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<DivisionBranch>(Query, new {Id = id});
                //return await _context.Set<DivisionBranch>().FindAsync(id)
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el DivisionBranch con id {DivisionBranchId}", id);
                throw;
            }
        }

        public async Task<DivisionBranch> CreateAsync(DivisionBranch divisionBranch)
        {
            try
            {
                await _context.Set<DivisionBranch>().AddAsync(divisionBranch);
                await _context.SaveChangesAsync();
                return divisionBranch;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el DivisionBranch: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(DivisionBranch divisionBranch)
        {
            try
            {
                _context.Set<DivisionBranch>().Update(divisionBranch);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al actualizar el DivisionBranch: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var divisionBranch = await _context.Set<DivisionBranch>().FindAsync(id);
                if (divisionBranch == null)
                    return false;
                _context.Set<DivisionBranch>().Remove(divisionBranch);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al eliminar el DivisionBranch: {ex.Message}");
                return false;
            }
        }
    }
}

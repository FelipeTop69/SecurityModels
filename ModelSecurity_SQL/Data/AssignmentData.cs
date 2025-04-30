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
    public class AssignmentData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AssignmentData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Assignment>> GetAllAsync()
        {
            string query = @"
                SELECT 
                    asg.Id, 
                    asg.Name, 
                    asg.DivisionId as DivisionId, 
                    di.Name as DivisionName
                FROM AccessPoint asg
                INNER JOIN Division di
                ON as.DivisionId = di.Id";

            return (IEnumerable<Assignment>)await _context.QueryAsync<IEnumerable<Assignment>>(query);
        }



        public async Task<Assignment?> GetByIdAsync(int id)
        {
            try
            {
                string query = @"
                     SELECT 
                        asg.Id, 
                        asg.Name 
                        asg.DivisionId as DivisionId, 
                        di.Name as DivisionName
                     FROM AccessPoint asg
                     INNER JOIN Division di
                     ON as.DivisionId = di.Id
                     WHERE as.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<Assignment>(query, new { Id = id });
            }
            catch(Exception ex)   
            {
                    _logger.LogError(ex,"Error al obtener el Assignament con id {AssignamentId}" ,id);
                    throw;
            }
        }

        public async Task<Assignment> CreateAsync(Assignment assignment)
        {
            try
            {
                await _context.Set<Assignment>().AddAsync(assignment);
                await _context.SaveChangesAsync();
                return assignment;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el Assignament: {ex.Message}");
                throw;
            }  
        }
        
        public async Task<bool> UpdateAsync(Assignment assignment)
        {
            try
            {
                _context.Set<Assignment>().Update(assignment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el Assignament: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var assiganament = await _context.Set<Assignment>().FindAsync(id);
                if (assiganament == null)
                    return false;
                _context.Set<Assignment>().Remove(assiganament);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al crear el Assignament: {ex.Message}");
                throw;
            }
        }
    }
}

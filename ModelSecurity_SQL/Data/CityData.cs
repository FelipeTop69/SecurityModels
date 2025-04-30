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
    public class CityData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CityData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            string query = @"
                SELECT 
                    c.Id, 
                    c.Name, 
                    c.DepartmentId, 
                    d.Name as DepartmentName
                FROM City c
                INNER JOIN Department d 
                ON c.DepartmentId = d.Id";

            return (IEnumerable<City>) await _context.QueryAsync<IEnumerable<City>>(query);
            //return await _context.Set<City>().ToListAsync();
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            try
            {
                string query = @"
                    SELECT 
                        c.Id, 
                        c.Name, 
                        c.DepartmentId, 
                        d.Name AS DepartmentName
                    FROM City c
                    INNER JOIN Department d ON c.DepartmentId = d.Id
                    WHERE c.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<City>(query, new { Id = id });
                //return await _context.Set<City>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el City con id {CityId}", id);
                throw;
            }
        }

        public async Task<City> CreateAsync(City city)
        {
            try
            {
                await _context.Set<City>().AddAsync(city);
                await _context.SaveChangesAsync();
                return city;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el City: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(City city)
        {
            try
            {
                _context.Set<City>().Update(city);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al actualizar el City: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var city = await _context.Set<City>().FindAsync(id);
                if (city == null)
                    return false;
                _context.Set<City>().Remove(city);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al eliminar el City: {ex.Message}");
                return false;
            }
        }
    }
}

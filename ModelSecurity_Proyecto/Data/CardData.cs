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
    public class CardData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CardData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            string query = @"
                SELECT 
                    c.Id, 
                    c.QR, 
                    c.Active AS Status, 
                    c.CreationDate, 
                    c.ExpirationDate, 
                    c.PersonId, 
                    p.Name as PersonName
                FROM Card c
                INNER JOIN Person p 
                ON c.PersonId = p.Id";

            return (IEnumerable<Card>) await _context.QueryAsync<IEnumerable<Card>>(query);
            //return await _context.Set<Card>().ToListAsync();
        }

        public async Task<Card?> GetByIdAsync(int id)
        {
            try
            {
                string query = @"
                    SELECT 
                        c.Id, 
                        c.QR, 
                        c.Active AS Status, 
                        c.CreationDate, 
                        c.ExpirationDate, 
                        c.PersonId, 
                        p.Name as PersonName
                    FROM Card c
                    INNER JOIN Person p 
                    ON c.PersonId = p.Id
                    WHERE c.Id = @Id";

                return await _context.QueryFirstOrDefaultAsync<Card>(query, new { Id = id });
                //return await _context.Set<Card>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Card con id {CardId}", id);
                throw;
            }
        }

        public async Task<Card> CreateAsync(Card card)
        {
            try
            {
                await _context.Set<Card>().AddAsync(card);
                await _context.SaveChangesAsync();
                return card;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el Card: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Card card)
        {
            try
            {
                _context.Set<Card>().Update(card);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al actualizar el Card: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var card = await _context.Set<Card>().FindAsync(id);
                if (card == null)
                    return false;
                _context.Set<Card>().Remove(card);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error al eliminar el Card: {ex.Message}");
                return false;
            }
        }
    }
}

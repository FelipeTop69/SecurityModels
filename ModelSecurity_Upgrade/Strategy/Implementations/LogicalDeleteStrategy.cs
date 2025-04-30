using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Strategy.Interfaces;

namespace Strategy.Implementations
{
    public class LogicalDeleteStrategy : IDeleteStrategy
    {
        private readonly FormData _formData;

        public LogicalDeleteStrategy(FormData formData)
        {
            _formData = formData;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _formData.DeleteLogicAsync(id);
        }
    }
}

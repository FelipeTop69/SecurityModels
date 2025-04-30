using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Strategy.Interfaces
{
    public interface IDeleteStrategy<TEntity>
    {
        Task<bool> DeleteAsync(int id, IData<TEntity> data);
    }
}

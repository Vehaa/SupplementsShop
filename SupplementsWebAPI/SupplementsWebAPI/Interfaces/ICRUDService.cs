using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementsWebAPI.Interfaces
{
    public interface ICRUDService<T,TSearch,TInsert,TUpdate>
    {
        T Insert(TInsert request);

        T Update(int id, TUpdate request);

        List<T> Get(TSearch search);

        T GetById(int id);

        void Delete(int id);
    }
}

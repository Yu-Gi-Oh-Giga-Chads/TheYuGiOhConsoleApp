using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IDB<T,K>
    {
        void Create(T entity);
        T Read(K key, bool useNavigational = false, bool isReadonly = false);
        List<T> ReadAll(bool useNavigational = false, bool isReadonly = false);
        void Update(T entity, bool useNavigational = false, bool isReadonly = false);
        void Delete(K key, bool useNavigational = false, bool isReadonly = false);
    }
}

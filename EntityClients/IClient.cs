using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace D365Entities.EntityClients
{
    public interface IClient<T>
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<IEnumerable<T>> GetList(params object[] arguments);

        public Task<T> Get(params object[] arguments);

        public Task<T> Put(T entity);

        public Task<T> Post(T entity);


    }
}

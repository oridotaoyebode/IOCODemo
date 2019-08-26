using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IOCO.Demo.Services.Base
{
    public interface IBaseService<T> where T: class
    {
        Task<List<T>> Get(string url);
        Task<T> Get(string url, int id);
        Task<T> Create(string url, T data);
        Task<bool> Delete(string url, int id);
        Task<T> Update(string url, int id, T data);
    }
}

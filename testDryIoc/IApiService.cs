using System;
using System.Threading.Tasks;

namespace testDryIoc
{
    public interface IApiService
    {
        Task<bool> GetAsync();
    }
}

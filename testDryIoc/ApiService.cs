using System;
using System.Threading.Tasks;

namespace testDryIoc
{
    public class ApiService : IApiService
    {
        public async Task<bool> GetAsync()
        {
            return await Task.FromResult(true);
        }
    }
}

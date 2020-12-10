using System;
namespace testDryIoc.ViewModels
{
    public class MainViewModel
    {
        private readonly IApiService apiService;

        public MainViewModel(IApiService apiService)
        {
            this.apiService = apiService;
        }
    }
}

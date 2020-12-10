using System;
using DryIoc;
using testDryIoc.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testDryIoc
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainView();
        }

        
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

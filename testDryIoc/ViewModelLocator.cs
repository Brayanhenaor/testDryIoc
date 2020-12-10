using System;
using System.Globalization;
using System.Reflection;
using DryIoc;
using testDryIoc.ViewModels;
using Xamarin.Forms;

namespace testDryIoc
{
    public static class ViewModelLocator
    {
        public static Container Container { get; private set; }

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        static ViewModelLocator()
        {
            Container = new Container();
            RegisterInstances();
        }

        private static void RegisterInstances()
        {
            Register<MainViewModel>();
            Register<IApiService, ApiService>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
                return;

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
                return;

            try
            {
                var viewModel = Resolve(viewModelType);
                view.BindingContext = viewModel;
            }
            catch
            {
                return;
            }
        }

        public static void RegisterSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            Container.Register<TInterface, TImplementation>(Reuse.Singleton);
        }

        public static void Register<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            Container.Register<TInterface, TImplementation>();
        }

        public static void Register<TImplementation>() where TImplementation : class
        {
            Container.Register<TImplementation>();
        }

        public static T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            return Container.Resolve(type);
        }
    }
}

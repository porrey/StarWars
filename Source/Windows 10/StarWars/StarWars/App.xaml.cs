using System;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using StarWars.Interfaces;
using StarWars.Models;
using StarWars.Repositories;
using StarWars.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StarWars
{
	sealed partial class App : Application
    {
		private IUnityContainer _container = null;

		public App()
        {
            this.InitializeComponent();
			
			// ***
			// *** Create the container
			// ***
			_container = new UnityContainer();

			// ***
			// *** Register the unity container
			// ***
			_container.RegisterInstance<IUnityContainer>(_container);

			// ***
			// *** Set up the global locator service
			// ***
			ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(_container));

			// ***
			// *** Add the OnSuspending handler
			// ***
			this.Suspending += OnSuspending;
			this.UnhandledException += OnUnhandledException;
        }

		public IUnityContainer Container
		{
			get
			{
				return _container;
			}
		}

		private void OnContainerRegistration(IUnityContainer container)
		{
			// ***
			// *** Register objects
			// ***
			container.RegisterInstance<IEventAggregator>(new EventAggregator(), new ContainerControlledLifetimeManager());
			container.RegisterType<IApplicationSettingsRepository, ApplicationSettingsRepository>(new ContainerControlledLifetimeManager());
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
			// ***
			// *** Initialize the Unity container
			// ***
			this.OnContainerRegistration(this.Container);

			Frame rootFrame = Window.Current.Content as Frame;

			if (rootFrame == null)
			{
				rootFrame = new Frame();

				rootFrame.NavigationFailed += OnNavigationFailed;

				if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
				{
					//TODO: Load state from previously suspended application
				}

				Window.Current.Content = rootFrame;
			}

			if (rootFrame.Content == null)
			{
				rootFrame.Navigate(typeof(MainPage), e.Arguments);
			}

			Window.Current.Activate();
		}

		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			deferral.Complete();
		}

		private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			IEventAggregator eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
			eventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(e.Exception));
			e.Handled = true;
		}
	}
}

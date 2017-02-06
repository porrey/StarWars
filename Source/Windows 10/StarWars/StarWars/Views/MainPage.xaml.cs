using System;
using Porrey.Uwp.IoT.Devices.Photon;
using StarWars.Common;
using StarWars.Models;
using Windows.UI.Xaml;

namespace StarWars.Views
{
	public sealed partial class MainPage : BindablePage
	{
		public MainPage()
		{
			this.InitializeComponent();
		}

		#region Bindings
		private bool _isPaneOpen = false;
		public bool IsPaneOpen
		{
			get
			{
				return _isPaneOpen;
			}
			set
			{
				this.SetProperty(ref _isPaneOpen, value);
			}
		}
		#endregion

		#region Event Handlers
		private void ControlPanel_Click(object sender, RoutedEventArgs e)
		{
			this.IsPaneOpen = false;
			this.Frame.Navigate(typeof(MainPage), null);
		}

		private void MenuButton_Click(object sender, RoutedEventArgs e)
		{
			this.IsPaneOpen = !this.IsPaneOpen;
		}

		private void CloudSetup_Click(object sender, RoutedEventArgs e)
		{
			this.IsPaneOpen = false;
			this.Frame.Navigate(typeof(CloudSetupPage), null);
		}

		private void Settings_Click(object sender, RoutedEventArgs e)
		{
			this.IsPaneOpen = false;
			this.Frame.Navigate(typeof(SettingsPage), null);
		}

		private void ResetSettings_Click(object sender, RoutedEventArgs e)
		{
			this.IsPaneOpen = false;
			this.Frame.Navigate(typeof(CloudSetupPage), null);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
		}

		private async void LightSaberOn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				int result = await cloudFunction.Go("controller", "lightSaberOn");

				if (result != 1)
				{
					this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs("Remote Command", "The remote command failed."));
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}
		}

		private async void BreatheButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				int result = await cloudFunction.Go("controller", "voice=breathe");

				if (result != 1)
				{
					this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs("Remote Command", "The remote command failed."));
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}
		}

		private async void DarkSideButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				int result = await cloudFunction.Go("controller", "voice=darkside");

				if (result != 1)
				{
					this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs("Remote Command", "The remote command failed."));
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}
		}

		private async void TaughtButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				int result = await cloudFunction.Go("controller", "voice=taught");

				if (result != 1)
				{
					this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs("Remote Command", "The remote command failed."));
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}
		}

		private async void LastTimeButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				int result = await cloudFunction.Go("controller", "voice=lasttime");

				if (result != 1)
				{
					this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs("Remote Command", "The remote command failed."));
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}
		}

		private async void GiveInButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				int result = await cloudFunction.Go("controller", "voice=givein");

				if (result != 1)
				{
					this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs("Remote Command", "The remote command failed."));
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}
		}

		private async void RespectButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				int result = await cloudFunction.Go("controller", "voice=breathe");

				if (result != 1)
				{
					this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs("Remote Command", "The remote command failed."));
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}
		}

		private async void DestroyButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				int result = await cloudFunction.Go("controller", "voice=destroy");

				if (result != 1)
				{
					this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs("Remote Command", "The remote command failed."));
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}
		}

		private async void ResistButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				int result = await cloudFunction.Go("controller", "voice=resist");

				if (result != 1)
				{
					this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs("Remote Command", "The remote command failed."));
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}
		}
		#endregion
	}
}

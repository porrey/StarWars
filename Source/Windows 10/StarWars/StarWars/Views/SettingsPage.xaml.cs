// Copyright © 2016 Daniel Porrey
//
// This file is part of the Star Wars 3D Art Solution.
// 
// Star Wars 3D Art Solution is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Star Wars 3D Art Solution is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Star Wars 3D Art Solution. If not, see http://www.gnu.org/licenses/.
//
using System;
using System.Threading.Tasks;
using Porrey.Uwp.IoT.Devices.Photon;
using StarWars.Common;
using StarWars.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace StarWars.Views
{
	public partial class SettingsPage : BindablePage
	{
		DispatcherTimer _timer = null;

		public SettingsPage()
		{
			this.InitializeComponent();
		}

		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			_volume = await this.GetParameterValue(MagicValue.RemoteDevice.Parameter.Volume);
			this.Distance = await this.GetParameterValue(MagicValue.RemoteDevice.Parameter.DistanceThreshod);
			this.LightLevel = await this.GetParameterValue(MagicValue.RemoteDevice.Parameter.LightLevel);

			_timer = new DispatcherTimer();
			_timer.Interval = TimeSpan.FromMilliseconds(750);
			_timer.Tick += Timer_Tick;
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			_timer.Stop();
			_timer = null;
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

		private int _volume = 0;
		public int Volume
		{
			get
			{
				_volume = this.GetParameterValue(MagicValue.RemoteDevice.Parameter.Volume).WaitForResult();
				return _volume;
			}
			set
			{
				_timer.Stop();
				_timer.Start();
				this._volume = value;
			}
		}

		private async void Timer_Tick(object sender, object e)
		{
			await this.SetParameterValue<int>(MagicValue.RemoteDevice.Parameter.Volume, _volume);
		}

		public int Distance
		{
			get
			{
				return this.GetParameterValue(MagicValue.RemoteDevice.Parameter.DistanceThreshod).WaitForResult();
			}
			set
			{
				this.SetParameterValue<int>(MagicValue.RemoteDevice.Parameter.DistanceThreshod, value).Wait();
			}
		}

		public int LightLevel
		{
			get
			{
				return this.GetParameterValue(MagicValue.RemoteDevice.Parameter.LightLevel).WaitForResult();
			}
			set
			{
				this.SetParameterValue<int>(MagicValue.RemoteDevice.Parameter.LightLevel, value).Wait();
			}
		}
		#endregion

		#region Event Handlers
		private void ControlPanel_Click(object sender, RoutedEventArgs e)
		{
			this.IsPaneOpen = false;
			this.Frame.Navigate(typeof(MainPage), null);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
		}

		private void LightSaber_On_Click(object sender, RoutedEventArgs e)
		{
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
		#endregion

		private async Task<int> GetParameterValue(string parameterName)
		{
			int returnValue = 0;

			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				returnValue = await cloudFunction.Go(MagicValue.RemoteDevice.Function.Parameter, string.Format(MagicValue.RemoteDevice.Function.GetParameterFormat, parameterName));
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}

			return returnValue;
		}

		private async Task<bool> SetParameterValue<T>(string parameterName, T value)
		{
			bool returnValue = false;

			try
			{
				ICloudIdentity identity = new CloudIdentityV1(this.ApplicationSettings.ApiKey, this.ApplicationSettings.DeviceId);

				CloudFunction cloudFunction = new CloudFunction(identity);
				int result = await cloudFunction.Go(MagicValue.RemoteDevice.Function.Parameter, string.Format(MagicValue.RemoteDevice.Function.SetParameterFormat, parameterName, value.ToString()));

				if (result == 1)
				{
					returnValue = true;
				}
				else
				{
					this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(parameterName, string.Format("Failed to set parameter '{0}' on remote device.", parameterName)));
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}

			return returnValue;
		}
	}
}

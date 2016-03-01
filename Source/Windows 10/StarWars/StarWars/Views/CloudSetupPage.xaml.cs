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
using StarWars.Common;
using Windows.UI.Xaml;

namespace StarWars.Views
{
	public partial class CloudSetupPage : BindablePage
	{
		public CloudSetupPage()
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
	}
}

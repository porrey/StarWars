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
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.Storage;
using System;
using StarWars.Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.PubSubEvents;
using StarWars.Common;
using StarWars.Models;

namespace StarWars.Repositories
{
	public class ApplicationSettingsRepository : BindableModel, IApplicationSettingsRepository
	{
		[Dependency]
		protected IEventAggregator EventAggregator { get; set; }

		#region Settings
		public string ApiKey
		{
			get
			{
				return this.GetSetting<string>(MagicValue.Property.Name.ApiKey, MagicValue.Property.Default.ApiKey);
			}
			set
			{
				if (value != this.ApiKey)
				{
					this.SaveSetting<string>(MagicValue.Property.Name.ApiKey, value);
				}
			}
		}

		public string DeviceId
		{
			get
			{
				return this.GetSetting<string>(MagicValue.Property.Name.DeviceId, MagicValue.Property.Default.DeviceId);
			}
			set
			{
				if (value != this.DeviceId)
				{
					this.SaveSetting<string>(MagicValue.Property.Name.DeviceId, value);
				}
			}
		}

		public bool ShowApiKey
		{
			get
			{
				return this.GetSetting<bool>(MagicValue.Property.Name.ShowApiKey, MagicValue.Property.Default.ShowApiKey);
			}
			set
			{
				if (value != this.ShowApiKey)
				{
					this.SaveSetting<bool>(MagicValue.Property.Name.ShowApiKey, value);
				}
			}
		}
		#endregion

		public T GetSetting<T>(string name, T defaultValue)
		{
			T returnValue = default(T);

			try
			{
				if (ApplicationData.Current.RoamingSettings.Values.ContainsKey(name))
				{
					// ***
					// *** WintRT will not serialize all objects, so use Newtonsoft.Json
					// ***
					string json = (string)ApplicationData.Current.RoamingSettings.Values[name];
					returnValue = JsonConvert.DeserializeObject<T>(json);
				}
				else
				{
					returnValue = defaultValue;
				}
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}

			return returnValue;
		}

		public void SaveSetting<T>(string name, T value)
		{
			try
			{
				// ***
				// *** Not all objects will serialize so use Newtonsoft.Json for everything
				// ***
				string json = JsonConvert.SerializeObject(value);
				ApplicationData.Current.RoamingSettings.Values[name] = json;

				this.OnPropertyChanged(name);
				this.EventAggregator.GetEvent<Events.ApplicationSettingChangedEvent>().Publish(new ApplicationSettingChangedEventArgs(name, value));
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}
		}

		public Task ResetToDefaults()
		{
			try
			{
				// ***
				// *** This will fire the events for any poperty
				// *** that had a different value thn its default.
				// ***
				this.ApiKey = MagicValue.Property.Default.ApiKey;
				this.DeviceId = MagicValue.Property.Default.DeviceId;
				this.ShowApiKey = MagicValue.Property.Default.ShowApiKey;
			}
			catch (Exception ex)
			{
				this.EventAggregator.GetEvent<Events.ExceptionEvent>().Publish(new ExceptionEventArgs(ex));
			}

			return Task.FromResult(0);
		}
	}
}

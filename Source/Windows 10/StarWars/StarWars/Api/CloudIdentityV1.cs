// Copyright © 2016 Daniel Porrey. All Rights Reserved.
//
// This file is part of the StarWars project.
// 
// StarWars is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// StarWars is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with StarWars. If not, 
// see http://www.gnu.org/licenses/.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Porrey.Uwp.IoT.Devices.Photon
{
	public class CloudIdentityV1 : ICloudIdentity
	{
		private string _token = string.Empty;
		private AuthenticationHeaderValue _authentication = null;
		private readonly string _baseUri = "https://api.particle.io/v1/devices/{0}/";
		private string _device = string.Empty;

		public CloudIdentityV1(string token, string device)
		{
			this.Token = token;
			this.Device = device;
		}

		public string Token
		{
			get
			{
				return _token;
			}
			set
			{
				_token = value;
				this.Authentication = new AuthenticationHeaderValue("Bearer", this.Token);
			}
		}

		public string Device
		{
			get
			{
				return _device;
			}
			set
			{
				_device = value;
			}
		}

		public AuthenticationHeaderValue Authentication
		{
			get
			{
				return _authentication;
			}
			set
			{
				_authentication = value;
			}
		}

		public Uri BaseUri
		{
			get
			{
				return new Uri(string.Format(_baseUri, this.Device));
			}
		}
	}
}

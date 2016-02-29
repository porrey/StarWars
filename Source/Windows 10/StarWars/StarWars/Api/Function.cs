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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Porrey.Uwp.IoT.Devices.Photon
{
	public class Function : ApiMethod
	{
		public Function(ICloudIdentity identity)
			: base(identity, "{0}", HttpMethod.Post)
		{
		}

		public async Task<int> Go(string function, string command)
		{
			return await this.Execute(function, command);
		}

		protected async virtual Task<int> Execute(string functionName, string command)
		{
			int returnValue = 0;

			HttpClient httpClient = new HttpClient();
			HttpRequestMessage request = new HttpRequestMessage();

			var values = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("args", command)
			};

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			request.Headers.Authorization = this.Identity.Authentication;
			request.Method = this.Method;
			request.RequestUri = new Uri(this.Identity.BaseUri, string.Format(this.Resource, functionName));
			request.Content = content;
			HttpResponseMessage response = await httpClient.SendAsync(request);

			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();
				dynamic obj = JsonConvert.DeserializeObject(json);
				string value = obj.return_value;

				returnValue = Convert.ToInt32(value);
			}
			else
			{
				throw new HttpRequestException(response.ReasonPhrase);
			}

			return returnValue;
		}
	}
}

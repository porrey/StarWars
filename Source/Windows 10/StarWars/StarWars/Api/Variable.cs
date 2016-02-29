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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Porrey.Uwp.IoT.Devices.Photon
{
	public class Variable<T> : ApiMethod<T>
	{
		public Variable(ICloudIdentity identity)
			: base(identity, "{0}")
		{
		}

		public async Task<T> Get(string command)
		{	
			return await this.Execute(command);
		}

		protected async override Task<T> Execute(params string[] args)
		{
			T returnValue = default(T);

			HttpClient httpClient = new HttpClient();
			HttpRequestMessage request = new HttpRequestMessage();

			request.Headers.Authorization = this.Identity.Authentication;
			request.Method = HttpMethod.Get;
			request.RequestUri = new Uri(this.Identity.BaseUri, string.Format(this.Resource, args));

			HttpResponseMessage response = await httpClient.SendAsync(request);

			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();
				dynamic obj = JsonConvert.DeserializeObject(json);
				string value = obj.result;

				returnValue = (T)Convert.ChangeType(value, typeof(T));
			}
			else
			{
				await this.OnFailed(response);
			}

			return returnValue;
		}
	}
}

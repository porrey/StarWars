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
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Porrey.Uwp.IoT.Devices.Photon
{
	public abstract class ApiMethod : IApi
	{
		private string _resource = string.Empty;
		private ICloudIdentity _identity = null;
		private HttpMethod _method = HttpMethod.Get;

		protected ApiMethod(ICloudIdentity identity, string resource, HttpMethod method)
		{
			this.Identity = identity;
			this.Resource = resource;
			this.Method = method;
		}

		protected virtual string Resource
		{
			get
			{
				return _resource;
			}
			set
			{
				_resource = value;
			}
		}

		protected ICloudIdentity Identity
		{
			get
			{
				return _identity;
			}

			set
			{
				_identity = value;
			}
		}

		protected HttpMethod Method
		{
			get
			{
				return _method;
			}
			set
			{
				this._method = value;
			}
		}
	}

	public abstract class ApiMethod<TResult> : ApiMethod
	{
		protected ApiMethod(ICloudIdentity identity, string resource)
			: base(identity, resource, HttpMethod.Get)
		{
		}

		protected async virtual Task<TResult> Execute(params string[] args)
		{
			TResult returnValue = default(TResult);

			HttpClient httpClient = new HttpClient();
			HttpRequestMessage request = new HttpRequestMessage();

			request.Headers.Authorization = this.Identity.Authentication;
			request.Method = HttpMethod.Get;
			request.RequestUri = new Uri(this.Identity.BaseUri, string.Format(this.Resource, args));

			HttpResponseMessage response = await httpClient.SendAsync(request);

			if (response.IsSuccessStatusCode)
			{
				returnValue = await this.OnExecuted(response);
			}
			else
			{
				await this.OnFailed(response);
			}

			return returnValue;
		}

		protected virtual Task<TResult> OnExecuted(HttpResponseMessage response)
		{
			return Task<TResult>.FromResult(default(TResult));
		}

		protected virtual Task OnFailed(HttpResponseMessage response)
		{
			throw new HttpRequestException(response.ReasonPhrase);
		}
	}
}

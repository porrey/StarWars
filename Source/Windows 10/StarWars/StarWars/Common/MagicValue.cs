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
namespace StarWars.Common
{
	public static class MagicValue
	{
		public static class Property
		{
			public static class Default
			{
				public const string ApiKey = null;
				public const bool ShowApiKey = false;
				public const string DeviceId = null;
			}

			public static class Name
			{
				public const string ApiKey = "ApiKey";
				public const string ShowApiKey = "ShowApiKey";
				public const string DeviceId = "DeviceId";
			}
		}

		public static class RemoteDevice
		{
			public static class Function
			{
				public const string Parameter = "parameter";
				public const string SetParameterFormat = "{0}={1}";
				public const string GetParameterFormat = "{0}";
			}

			public static class Parameter
			{
				public const string Volume = "volume";
				public const string DistanceThreshod = "distance";
				public const string LightLevel = "lightLevel";
			}
		}

		// ***
		// *** These items must match a key in the string resource file
		// ***
		public static class ResourceItem
		{

		}
	}
}

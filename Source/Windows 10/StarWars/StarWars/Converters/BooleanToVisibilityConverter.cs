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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace StarWars.Converters
{
	public sealed class BooleanToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language) =>
			(value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;

		public object ConvertBack(object value, Type targetType, object parameter, string language) =>
			value is Visibility && (Visibility)value == Visibility.Visible;
	}
}
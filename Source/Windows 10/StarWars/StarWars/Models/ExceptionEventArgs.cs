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
using System.Runtime.CompilerServices;

namespace StarWars.Models
{
	/// <summary>
	/// Debug event arguments passed to the DebugEvent
	/// </summary>
	public class ExceptionEventArgs : EventArgs
	{
		/// <summary>
		/// Creates an instance of ExceptionEventArgs from the given title and message.
		/// </summary>
		/// <param name="title">The title of this instance.</param>
		/// <param name="description">The details of this instance.</param>
		public ExceptionEventArgs(string title, string description)
		{
			this.Title = title;
			this.Description = description;
			this.TimestampUtc = DateTimeOffset.Now.UtcDateTime;
		}

		/// <summary>
		/// Creates an instance of ExceptionEventArgs from the given title and message.
		/// </summary>
		/// <param name="title">The title of this instance.</param>
		/// <param name="format">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		public ExceptionEventArgs(string title, string format, params object[] args)
			: this(title, string.Format(format, args))
		{
		}

		/// <summary>
		/// Creates an instance of ExceptionEventArgs from an exception.
		/// </summary>
		/// <param name="ex">The exception used to create the instance.</param>
		/// <param name="callerName">The method or property name of the caller to the method.</param>
		public ExceptionEventArgs(Exception ex, [CallerMemberName]string callerName = null)
		{
			this.Title = string.Format("Exception in '{0}'", callerName);
			this.Description = ex.Message;
			this.TimestampUtc = DateTimeOffset.Now.UtcDateTime;
		}

		/// <summary>
		/// Gets the title of this instance.
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// Gets the details of this instance.
		/// </summary>
		public string Description { get; }

		/// <summary>
		/// Gets the UTC date and time this instance was created.
		/// </summary>
		public DateTimeOffset TimestampUtc { get; set; }

		/// <summary>
		/// Gets the Timestamp value in local time.
		/// </summary>
		public DateTimeOffset TimestampLocal => this.TimestampUtc.ToLocalTime();
	}
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Music.Infrastructure.Mvvm
{
	/// <summary>
	/// Implementation of <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> to simplify models.
	/// </summary>
	public abstract class BindableBase : INotifyPropertyChanged
	{
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Checks if a property already matches a desired value. Sets the property and
		/// notifies listeners only when necessary.
		/// </summary>
		/// <typeparam name="T">Type of the property.</typeparam>
		/// <param name="storage">Reference to a property with both getter and setter.</param>
		/// <param name="value">Desired value for the property.</param>
		/// <param name="propertyName">Name of the property used to notify listeners. This
		/// value is optional and can be provided automatically when invoked from compilers that
		/// support CallerMemberName.</param>
		/// <returns>True if the value was changed, false if the existing value matched the
		/// desired value.</returns>
		protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value))
			{
				return false;
			}
			storage = value;
			RaisePropertyChanged(propertyName);
			return true;
		}

		/// <summary>
		/// Checks if a property already matches a desired value. Sets the property and
		/// notifies listeners only when necessary.
		/// </summary>
		/// <typeparam name="T">Type of the property.</typeparam>
		/// <param name="storage">Reference to a property with both getter and setter.</param>
		/// <param name="value">Desired value for the property.</param>
		/// <param name="propertyName">Name of the property used to notify listeners. This
		/// value is optional and can be provided automatically when invoked from compilers that
		/// support CallerMemberName.</param>
		/// <param name="onChanged">Action that is called after the property value has been changed.</param>
		/// <returns>True if the value was changed, false if the existing value matched the
		/// desired value.</returns>
		protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value))
			{
				return false;
			}
			storage = value;
			onChanged?.Invoke();
			RaisePropertyChanged(propertyName);
			return true;
		}

		/// <summary>
		/// Raises this object's PropertyChanged event.
		/// </summary>
		/// <param name="propertyName">Name of the property used to notify listeners. This
		/// value is optional and can be provided automatically when invoked from compilers
		/// that support <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" />.</param>
		protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			OnPropertyChanged(propertyName);
		}

		/// <summary>
		/// Notifies listeners that a property value has changed.
		/// </summary>
		/// <param name="propertyName">Name of the property used to notify listeners. This
		/// value is optional and can be provided automatically when invoked from compilers
		/// that support <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" />.</param>
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Raises this object's PropertyChanged event.
		/// </summary>
		/// <param name="args">The PropertyChangedEventArgs</param>
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			this.PropertyChanged?.Invoke(this, args);
		}
	}

}

using Freedom.Controls.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freed.Controls.Foundation
{
	public class ObservableObject : INotifyPropertyChanged, IDisposable
	{
		protected ObservableObject()
		{
			this.Compose();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void RaisePropertyChanged(string propertyName)
		{
			this.VerifyPropertyName(propertyName);

			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null)
			{
				var e = new PropertyChangedEventArgs(propertyName);
				handler(this, e);
			}
		}

		#region Debugging Aides

		/// <summary>
		/// Warns the developer if this object does not have
		/// a public property with the specified name. This 
		/// method does not exist in a Release build.
		/// </summary>
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public void VerifyPropertyName(string propertyName)
		{
			// If you raise PropertyChanged and do not specify a property name,
			// all properties on the object are considered to be changed by the binding system.
			if (String.IsNullOrEmpty(propertyName))
				return;

			// Verify that the property name matches a real,  
			// public, instance property on this object.
			if (TypeDescriptor.GetProperties(this)[propertyName] == null)
			{
				string msg = "Invalid property name: " + propertyName;

				if (this.ThrowOnInvalidPropertyName)
					throw new ArgumentException(msg);
				else
					Debug.Fail(msg);
			}
		}

		/// <summary>
		/// Returns whether an exception is thrown, or if a Debug.Fail() is used
		/// when an invalid property name is passed to the VerifyPropertyName method.
		/// The default value is false, but subclasses used by unit tests might 
		/// override this property's getter to return true.
		/// </summary>
		protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

		#endregion // Debugging Aides

		#region IDisposable接口

		/// <summary>
		/// Invoked when this object is being removed from the application
		/// and will be subject to garbage collection.
		/// </summary>
		public void Dispose()
		{
			this.OnDispose();
		}

		/// <summary>
		/// Child classes can override this method to perform 
		/// clean-up logic, such as removing event handlers.
		/// </summary>
		protected virtual void OnDispose()
		{

		}

#if DEBUG
		/// <summary>
		/// Useful for ensuring that ViewModel objects are properly garbage collected.
		/// </summary>
		~ObservableObject()
		{
			string msg = string.Format("{0} ({1}) Finalized", this.GetType().Name, this.GetHashCode());
			System.Diagnostics.Debug.WriteLine(msg);
		}
#endif

		#endregion

		public void Compose()
		{
			var catalog = new DirectoryCatalog(@".\");
			var container = new CompositionContainer(catalog);
			container.ComposeParts(this);
		}

		#region 公用
		private ICommand _DoInit;
		private ICommand _DoNext;
		private ICommand _DoBack;
		private ICommand _DoExit;

		public ICommand DoInit
		{
			get
			{
				if (this._DoInit == null)
				{
					this._DoInit = new RelayCommand<object>(
					  (obj) => this.DoInitFunction(obj));
				}
				return _DoInit;
			}
		}

		public ICommand DoNext
		{
			get
			{
				if (this._DoNext == null)
				{
					this._DoNext = new RelayCommand<object>(
					  (obj) => this.DoNextFunction(obj));
				}
				return _DoNext;
			}
		}

		public ICommand DoBack
		{
			get
			{
				if (this._DoBack == null)
				{
					this._DoBack = new RelayCommand<object>(
					  (obj) => this.DoBackFunction(obj));
				}
				return _DoBack;
			}
		}

		public ICommand DoExit
		{
			get
			{
				if (this._DoExit == null)
				{
					this._DoExit = new RelayCommand<object>(
					  (obj) => this.DoExitFunction(obj));
				}
				return _DoExit;
			}
		}

		public virtual void DoInitFunction(object obj)
		{

		}

		public virtual void DoNextFunction(object obj)
		{

		}

		public virtual void DoBackFunction(object obj)
		{

		}

		public virtual void DoExitFunction(object obj)
		{

		}
		#endregion
	}
}

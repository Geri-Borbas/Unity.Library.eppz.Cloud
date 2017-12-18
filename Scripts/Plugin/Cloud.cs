//
// Copyright (c) 2017 Geri Borbás http://www.twitter.com/_eppz
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EPPZ.Cloud.Plugin
{


	public class Cloud
	{

		
		protected ICloud cloudObject;


	#region Native setup

		public static Cloud NativePluginInstance(ICloud cloudObject)
		{
			Cloud plugin;

			// Get plugin class for the actual platform.
			#if UNITY_IPHONE
			plugin = (Application.isEditor)
				? (Cloud)new Cloud_Editor()
				: (Cloud)new Cloud_iOS();
			#elif UNITY_ANDROID
			plugin = (Application.isEditor)
				? (Cloud)new Cloud_Editor()
				: (Cloud)new Cloud_Android();
			#else
			plugin = (Cloud)new Cloud_Editor();
			#endif

			plugin.cloudObject = cloudObject;

			return plugin;
		}

	#endregion


	#region Features

		public virtual void InitializeWithGameObjectName(string gameObjectName) { }
		public virtual void Synchronize() { }

		public virtual string StringForKey(string key) { return ""; }
		public virtual void SetStringForKey(string value, string key) { }

		public virtual float FloatForKey(string key) { return 0.0f; }
		public virtual void SetFloatForKey(float value, string key) { }

		public virtual int IntForKey(string key) { return 0; }
		public virtual void SetIntForKey(int value, string key) { }

		public virtual bool BoolForKey(string key) { return false; }
		public virtual void SetBoolForKey(bool value, string key) { }

		public virtual void CloudDidChange(string message) { }

		protected void Log(string message)
		{ EPPZ.Cloud.Cloud.Log(message); }

	#endregion
	

	}
}

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
using UnityEngine.Events;


namespace EPPZ.Cloud
{


	public class Cloud : MonoBehaviour, Plugin.ICloud
	{


		public Model.KeyValuePair[] keyValuePairs;
		public bool initializeOnStart = true;

		Plugin.Cloud plugin;


		void Start()
		{
			// Create plugin instance (of whichever platform).
			plugin = Plugin.Cloud.NativePluginInstance(this);
			if (initializeOnStart) { Initialize(); }
		}

		public void Initialize()
		{ plugin.InitializeWithGameObjectName(this.name); }


	#region Features

		void Synchrnonize()
		{ plugin.Synchronize(); }

		public void CloudDidChange(string message)
		{ plugin.CloudDidChange(message); }

		public void SetBoolForKey(bool value, string key)
		{ plugin.SetBoolForKey(value, key); }

		public bool BoolForKey(string key)
		{ return plugin.BoolForKey(key); }

	#endregion
	

	#region Plugin callbacks (from whichever platform)

		[ContextMenu("SimulateCloudDidChange()")]
		public void SimulateCloudDidChange()
		{ plugin.CloudDidChange("Simulate"); }

		public void CloudDidChange(string[] changedKeys)
		{
			// Enumerate changed keys.
			foreach (string eachChangedKey in changedKeys)
			{
				// Lookup corresponding key value pair models.
				foreach (Model.KeyValuePair eachKeyValuePair in keyValuePairs)
				{
					// Invoke event (if any).
					if (eachKeyValuePair.key == eachChangedKey)
					{ eachKeyValuePair.InvokeOnValueChangedEventWithCloud(this); }
				}
			}
		}

	#endregion

	
	}
}

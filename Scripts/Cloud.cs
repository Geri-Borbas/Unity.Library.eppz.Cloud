//
// Copyright (c) 2017 Geri Borbás http://www.twitter.com/_eppz
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace EPPZ.Cloud
{


	public class Cloud : MonoBehaviour, Plugin.ICloud
	{


		static Cloud _instance;
		void Awake() { _instance = this; }

		
		public Model.Settings settings;
		public Model.Simulation.KeyValueStore simulationKeyValueStore;
		Plugin.Cloud plugin;


		void Start()
		{
			// Create plugin instance (of whichever platform).
			plugin = Plugin.Cloud.NativePluginInstance(this);
			if (settings.initializeOnStart) { Initialize(); }
		}

		public void Initialize()
		{ plugin.InitializeWithGameObjectName(this.name); }


	#region Features

		public static void Synchrnonize()
		{ _instance.plugin.Synchronize(); }

		public static void OnKeyChange(string key, Action<string> action)
		{ _instance._OnKeyChange(key, action); }

		public static void OnKeyChange(string key, Action<float> action)
		{ _instance._OnKeyChange(key, action); }

		public static void OnKeyChange(string key, Action<int> action)
		{ _instance._OnKeyChange(key, action); }

		public static void OnKeyChange(string key, Action<bool> action)
		{ _instance._OnKeyChange(key, action); }

		public static void RemoveOnKeyChangeAction(string key, Action<string> action)
		{ _instance._RemoveOnKeyChangeAction(key, action); }

		public static void RemoveOnKeyChangeAction(string key, Action<float> action)
		{ _instance._RemoveOnKeyChangeAction(key, action); }

		public static void RemoveOnKeyChangeAction(string key, Action<int> action)
		{ _instance._RemoveOnKeyChangeAction(key, action); }

		public static void RemoveOnKeyChangeAction(string key, Action<bool> action)
		{ _instance._RemoveOnKeyChangeAction(key, action); }

		public static void SetStringForKey(string value, string key)
		{ _instance.plugin.SetStringForKey(value, key); }

		public static string StringForKey(string key)
		{ return _instance.plugin.StringForKey(key); }

		public static void SetFloatForKey(float value, string key)
		{ _instance.plugin.SetFloatForKey(value, key); }

		public static float FloatForKey(string key)
		{ return _instance.plugin.FloatForKey(key); }

		public static void SetIntForKey(int value, string key)
		{ _instance.plugin.SetIntForKey(value, key); }

		public static int IntForKey(string key)
		{ return _instance.plugin.IntForKey(key); }

		public static void SetBoolForKey(bool value, string key)
		{ _instance.plugin.SetBoolForKey(value, key); }

		public static bool BoolForKey(string key)
		{ return _instance.plugin.BoolForKey(key); }

	#endregion


	#region Simulation

		public static Model.Simulation.KeyValueStore SimulationKeyValueStore()
		{ return _instance.simulationKeyValueStore; }

		public static void InvokeOnKeysChanged(string[] changedKeys, Plugin.Cloud.ChangeReason changeReason)
		{
			Debug.Log("InvokeOnKeysChanged(`"+changedKeys+"`, `"+changeReason+"`)");
			_instance.OnKeysChanged(changedKeys, changeReason);
		}

	#endregion
	
	
	#region Registering actions

		void _RemoveOnKeyChangeAction(string key, object action)
		{
			Model.KeyValuePair keyValuePair = settings.KeyValuePairForKey(key);
			if (keyValuePair != null) { keyValuePair.RemoveOnChangeAction(action); }
		}

		void _OnKeyChange(string key, object action)
		{
			Model.KeyValuePair keyValuePair = settings.KeyValuePairForKey(key);
			if (keyValuePair != null) { keyValuePair.AddOnChangeAction(action); }
		}

	#endregion


	#region Plugin callbacks (from whichever platform)

		[ContextMenu("SimulateCloudDidChange()")]
		public void SimulateCloudDidChange()
		{ plugin.CloudDidChange("Simulate"); }

		public void CloudDidChange(string message)
		{ plugin.CloudDidChange(message); }

		public void OnKeysChanged(string[] changedKeys, Plugin.Cloud.ChangeReason changeReason)
		{
			Debug.Log("OnKeysChanged(`"+changedKeys+"`, `"+changeReason+"`)");

			// Enumerate changed keys.
			foreach (string eachChangedKey in changedKeys)
			{
				// Lookup corresponding key value pair models.
				foreach (Model.KeyValuePair eachKeyValuePair in settings.keyValuePairs)
				{
					// Invoke event (if any).
					if (eachKeyValuePair.key == eachChangedKey)
					{ eachKeyValuePair.InvokeOnValueChangedAction(); }
				}
			}
		}

	#endregion

	
	}
}

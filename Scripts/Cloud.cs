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


	public enum ChangeReason
    {
	    ServerChange,
     	InitialSyncChange,
       	QuotaViolationChange,
       	AccountChange
    }


	public class Cloud : MonoBehaviour, Plugin.ICloud
	{


		// Singleton.
		static Cloud _instance;
		void Awake() { _instance = this; }

		// Inspector hooks.
		public Model.Settings settings;
		public Model.Simulation.KeyValueStore simulationKeyValueStore;
		
		// Delegate.
		public enum Should { UpdateKeys, StopUpdateKeys }
		public delegate Should OnCloudChange(string[] changedKeys, ChangeReason changeReason);
		public static OnCloudChange onCloudChange;

		// Internal.
		ChangeReason latestChangeReason;
		Plugin.Cloud _plugin;
		Plugin.Cloud plugin
		{
			get
			{
				// Create plugin instance (of whichever platform) lazy.
				if (_plugin == null)
				{ _plugin = Plugin.Cloud.NativePluginInstance(this); }
				return _plugin;
			}
		}


		void Start()
		{ if (settings.initializeOnStart) { _Initialize(); } }

		void _Initialize()
		{ plugin.InitializeWithGameObjectName(this.name); }

		void OnDestroy()
		{ _RemoveOnKeyChangeActions(); }


	#region Features

		public static void Initialize()
		{ _instance._Initialize(); }

		public static void Synchrnonize()
		{ _instance.plugin.Synchronize(); }

		public static void OnKeyChange(string key, Action<string> action, int priority = 0)
		{ _instance._OnKeyChange(key, action, priority); }

		public static void OnKeyChange(string key, Action<float> action, int priority = 0)
		{ _instance._OnKeyChange(key, action, priority); }

		public static void OnKeyChange(string key, Action<int> action, int priority = 0)
		{ _instance._OnKeyChange(key, action, priority); }

		public static void OnKeyChange(string key, Action<bool> action, int priority = 0)
		{ _instance._OnKeyChange(key, action, priority); }

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

		public static ChangeReason LatestChangeReason()
		{ return _instance.latestChangeReason; }

		public static void Log(string message)
		{
			if (_instance.settings.log)
			{ Debug.Log(message); }
		}

	#endregion


	#region Simulation

		public static Model.Simulation.KeyValueStore SimulationKeyValueStore()
		{ return _instance.simulationKeyValueStore; }

		public static void InvokeOnKeysChanged(string[] changedKeys, ChangeReason changeReason)
		{
			Log("Cloud.InvokeOnKeysChanged(`"+changedKeys+"`, `"+changeReason+"`)");
			_instance._OnCloudChange(changedKeys, changeReason);
		}

	#endregion
	
	
	#region Registering actions

		void _RemoveOnKeyChangeAction(string key, object action)
		{
			Model.KeyValuePair keyValuePair = settings.KeyValuePairForKey(key);
			if (keyValuePair != null) { keyValuePair.RemoveOnChangeAction(action); }
		}

		void _OnKeyChange(string key, object action, int priority)
		{
			Model.KeyValuePair keyValuePair = settings.KeyValuePairForKey(key);
			if (keyValuePair != null) { keyValuePair.AddOnChangeAction(action, priority); }
		}

		void _RemoveOnKeyChangeActions()
		{
			foreach (Model.KeyValuePair eachKeyValuePair in settings.keyValuePairs)
			{ eachKeyValuePair.RemoveOnChangeActions(); }
		}

	#endregion


	#region Plugin callbacks (from whichever platform)

		public void _CloudDidChange(string message)
		{ plugin.CloudDidChange(message); }

		public void _OnCloudChange(string[] changedKeys, ChangeReason changeReason)
		{
			Log("Cloud._OnCloudChange(`"+changedKeys+"`, `"+changeReason+"`)");

			// Populate latest change reason.
			latestChangeReason = changeReason;

			// Lookup delegates if any.
			if (onCloudChange != null)
			{
				Should should = onCloudChange(changedKeys, changeReason);
				if (should == Should.StopUpdateKeys) { return; }
			}

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

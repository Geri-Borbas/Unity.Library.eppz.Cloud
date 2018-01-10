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


	public class Cloud_Editor : Cloud
	{


		Model.Simulation.KeyValueStore keyValueStore { get { return EPPZ.Cloud.Cloud.SimulationKeyValueStore(); } }
		

	#region Features

		public override void Synchronize()
		{ keyValueStore.SimulateCloudDidChange(); }

		public override string StringForKey(string key)
		{ return keyValueStore.KeyValuePairForKey(key).stringValue; }

		public override void SetStringForKey(string value, string key)
		{
			Log("Cloud_Editor.SetStringForKey(`"+value+"`, `"+key+"`)");
			keyValueStore.KeyValuePairForKey(key).stringValue = value;
		}

		public override float FloatForKey(string key)
		{ return keyValueStore.KeyValuePairForKey(key).floatValue; }

		public override void SetFloatForKey(float value, string key)
		{
			Log("Cloud_Editor.SetFloatForKey(`"+value+"`, `"+key+"`)");
			keyValueStore.KeyValuePairForKey(key).floatValue = value;
		}

		public override int IntForKey(string key)
		{ return keyValueStore.KeyValuePairForKey(key).intValue; }

		public override void SetIntForKey(int value, string key)
		{
			Log("Cloud_Editor.SetIntForKey(`"+value+"`, `"+key+"`)");
			keyValueStore.KeyValuePairForKey(key).intValue = value;
		}

		public override bool BoolForKey(string key)
		{ return keyValueStore.KeyValuePairForKey(key).boolValue; }

		public override void SetBoolForKey(bool value, string key)
		{
			Log("Cloud_Editor.SetBoolForKey(`"+value+"`, `"+key+"`)");
			keyValueStore.KeyValuePairForKey(key).boolValue = value;
		}

	#endregion

	
	}
}

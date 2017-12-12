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


namespace EPPZ.Cloud.Model
{


	[System.Serializable]
	public class KeyValuePair
    {


		public string key;
		public enum Type { Bool }
		public Type type;


		/// <summary>
		/// Event invoked when the value has changed remotely.
		/// Hook up this event to a class that orchestrates
		/// merging new values to from the cloud to the local
		/// application states.
		/// </summary>
		/// <param>Key.</param>
		/// <param>Updated value in the cloud.</param>
		[System.Serializable]
		public class BoolKeyValuePairEvent : UnityEvent<string, bool> {}  
		public BoolKeyValuePairEvent onBoolValueChangedForKey;


		public void InvokeOnValueChangedEventWithCloud(Cloud cloud)
		{
			if (onBoolValueChangedForKey == null) return; // Only if any
			onBoolValueChangedForKey.Invoke(key, cloud.BoolForKey(key));
		}
	}
}

		
		
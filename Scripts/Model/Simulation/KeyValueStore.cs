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


namespace EPPZ.Cloud.Model.Simulation
{


    using Plugin;


	[CreateAssetMenu(fileName = "Key-value store simulation", menuName = "eppz!/Cloud/Key-value store simulation")]
	public class KeyValueStore : ScriptableObject
    {


        public ChangeReason changeReason;
		public KeyValuePair[] keyValuePairs;
        public Cloud_Editor plugin;


        public virtual void EnumerateKeyValuePairs(Action<Simulation.KeyValuePair> action)
        {
            foreach (KeyValuePair eachKeyValuePair in keyValuePairs)
            { action(eachKeyValuePair); }
        }

        public virtual Simulation.KeyValuePair KeyValuePairForKey(string key)
		{
			foreach (KeyValuePair eachKeyValuePair in keyValuePairs)
			{
				if (eachKeyValuePair.key == key)
				{ return eachKeyValuePair; }
			}
            Debug.LogWarning("eppz! Cloud: No Key-value pair defined for key `"+key+"`");
			return null;
		}

        [ContextMenu("Simulate `CloudDidChange`")]
        public virtual void SimulateCloudDidChange()
        {
            Debug.Log("SimulateCloudDidChange()");

            // Collect changed keys (from simulation store).
            List<string> changedKeys = new List<string>();
            EnumerateKeyValuePairs((KeyValuePair eachKeyValuePair) =>
            {
                if (eachKeyValuePair.isChanged)
                {
                    changedKeys.Add(eachKeyValuePair.key);

                    // Reset.
                    eachKeyValuePair.isChanged = false;
                }
            });

            Debug.Log("changedKeys: `"+changedKeys.ToArray()+"`");

            // Invoke `OnKeyChanged` (simulating plugin call).
            EPPZ.Cloud.Cloud.InvokeOnKeysChanged(changedKeys.ToArray(), changeReason);
        }   
	}
}

		
		
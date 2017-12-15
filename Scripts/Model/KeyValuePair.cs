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


namespace EPPZ.Cloud.Model
{


	[System.Serializable]
	public class KeyValuePair
    {


		public string key;
		public enum Type { String, Float, Int, Bool }
		public Type type;
		

		Dictionary<Type, System.Type> actionTypesForTypes = new Dictionary<Type, System.Type>
		{
			{ Type.String, typeof(Action<string>) },
			{ Type.Float, typeof(Action<float>) },
			{ Type.Int, typeof(Action<int>) },
			{ Type.Bool, typeof(Action<bool>) }
		};
		System.Type actionType { get { return actionTypesForTypes[type]; } }


		List<object> onChangeActions;
		Dictionary<Type, Action<object>> invokersForTypes;
		Action<object> invoker { get { return invokersForTypes[type]; } }


		public KeyValuePair()
		{
			invokersForTypes = new Dictionary<Type, Action<object>>{
				{
					Type.String, (object eachAction) =>
					{ ((Action<string>)eachAction)(stringValue); }
				},
				{
					Type.Float, (object eachAction) =>
					{ ((Action<float>)eachAction)(floatValue); }
				},
				{
					Type.Int, (object eachAction) =>
					{ ((Action<int>)eachAction)(intValue); }
				},
				{
					Type.Bool, (object eachAction) =>
					{ ((Action<bool>)eachAction)(boolValue); }
				},
			};
		}

		public void InvokeOnValueChangedAction()
		{
			if (onChangeActions.Count == 0) return; // Only if any

			// Invoke each registered change listener actions.
			foreach (object eachOnChangeAction in onChangeActions)
			{ invoker(eachOnChangeAction); }
		}

		public void AddOnChangeAction(object action)
		{
			if (action.GetType() != actionType)
			{
				Debug.LogWarning("eppz! Cloud: Cannot add on change action for key `"+key+"` with type `"+type+"`. Types mismatched.");
				return;
			}
			
			onChangeActions.Add(action);
		}

		public void RemoveOnChangeAction(object action)
		{
			if (onChangeActions.Contains(action))
			{ onChangeActions.Remove(action); }
		}

		public string stringValue
		{ get { return Cloud.StringForKey(key); } }

		public float floatValue
		{ get { return Cloud.FloatForKey(key); } }

		public int intValue
		{ get { return Cloud.IntForKey(key); } }

		public bool boolValue
		{ get { return Cloud.BoolForKey(key); } }
	}
}

		
		
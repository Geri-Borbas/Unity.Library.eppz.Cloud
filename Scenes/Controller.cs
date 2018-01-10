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
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace EPPZ.Cloud.Scenes
{


	public class Controller : MonoBehaviour
	{


		[System.Serializable]
		public class Elements
		{
			public InputField nameLabel;
			public Animation nameLabelAnimation;
			[Space]
			public Toggle soundToggle;
			public Animation soundToggleAnimation;
			[Space]
			public Slider volumeSlider;
			public Animation volumeSliderAnimation;
			[Space]
			public Dropdown levelDropdown;
			public Animation levelDropdownAnimation;
			[Space]
			public Toggle firstTrophyToggle;
			public Animation firstTrophyToggleAnimation;
			[Space]
			public Toggle secondTrophyToggle;
			public Animation secondTrophyToggleAnimation;
			[Space]
			public Toggle thirdTrophyToggle;
			public Animation thirdTrophyToggleAnimation;
		}
		public Elements elements;


		void Start()
		{
			Cloud.onCloudChange += OnCloudChange;
			AddElementUpdatingActions();
			PopulateElementsFromCloud();
		}

		void OnDestroy()
		{
			Cloud.onCloudChange -= OnCloudChange;
		}

		void PopulateElementsFromCloud()
		{
			elements.nameLabel.text = Cloud.StringForKey("name");
			elements.soundToggle.isOn = Cloud.BoolForKey("sound");
			elements.volumeSlider.value = Cloud.FloatForKey("volume");
			elements.levelDropdown.value = Cloud.IntForKey("level");
			elements.firstTrophyToggle.isOn = Cloud.BoolForKey("firstTrophy");
			elements.secondTrophyToggle.isOn = Cloud.BoolForKey("secondTrophy");
			elements.thirdTrophyToggle.isOn = Cloud.BoolForKey("thirdTrophy");
		}

		Cloud.Should OnCloudChange(string[] changedKeys, ChangeReason changeReason)
		{
			if (changeReason == ChangeReason.InitialSyncChange)
			{
				PopulateElementsFromCloud();
				return Cloud.Should.StopUpdateKeys;
			}

			if (changeReason == ChangeReason.QuotaViolationChange)
			{
				// May display error.
				return Cloud.Should.StopUpdateKeys;
			}

			if (changeReason == ChangeReason.AccountChange)
			{
				PopulateElementsFromCloud();
				return Cloud.Should.StopUpdateKeys;
			}

			else
			{
				// Let local states to be updated (with conflict resolution).
				return Cloud.Should.UpdateKeys;
			}
		}

	#region UI Events

		public void OnNameInputFieldEndEdit(string text)
		{
			Cloud.SetStringForKey(text, "name");
			Cloud.Synchrnonize();
		}

		public void OnSoundToggleValueChanged(bool isOn)
		{
			Cloud.SetBoolForKey(isOn, "sound");
			Cloud.Synchrnonize();
		}

		public void OnVolumeSliderEndDrag(BaseEventData eventData)
		{
			Cloud.SetFloatForKey(elements.volumeSlider.value, "volume");
			Cloud.Synchrnonize();
		}

		public void OnLevelDropDownValueChanged(int value)
		{
			Cloud.SetIntForKey(value, "level");
			Cloud.Synchrnonize();
		}

		public void OnFirstTrophyToggleValueChanged(bool isOn)
		{
			Cloud.SetBoolForKey(isOn, "firstTrophy");
			Cloud.Synchrnonize();
		}

		public void OnSecondTrophyToggleValueChanged(bool isOn)
		{
			Cloud.SetBoolForKey(isOn, "secondTrophy");
			Cloud.Synchrnonize();
		}

		public void OnThirdTrophyToggleValueChanged(bool isOn)
		{
			Cloud.SetBoolForKey(isOn, "thirdTrophy");
			Cloud.Synchrnonize();
		}

		public void OnConflictResolutionToggleValueChanged(bool isOn)
		{
			if (isOn)
			{ AddConflictResolvingActions(); }
			else
			{ RemoveConflictResolvingActions(); }
		}

		void AddElementUpdatingActions()
		{
			Cloud.OnKeyChange("name", (string value) =>
			{
				elements.nameLabel.text = value;
				elements.nameLabelAnimation.Play("Blink");
			}, 2);

			Cloud.OnKeyChange("sound", (bool value) =>
			{
				elements.soundToggle.isOn = value;
				elements.soundToggleAnimation.Play("Blink");
			}, 2);

			Cloud.OnKeyChange("volume", (float value) =>
			{
				elements.volumeSlider.value = value;
				elements.volumeSliderAnimation.Play("Blink");
			}, 2);

			Cloud.OnKeyChange("level", (int value) =>
			{
				elements.levelDropdown.value = value;
				elements.levelDropdownAnimation.Play("Blink");
			}, 2);

			Cloud.OnKeyChange("firstTrophy", (bool value) =>
			{
				elements.firstTrophyToggle.isOn = value;
				elements.firstTrophyToggleAnimation.Play("Blink");
			}, 2);

			Cloud.OnKeyChange("secondTrophy", (bool value) =>
			{
				elements.secondTrophyToggle.isOn = value;
				elements.secondTrophyToggleAnimation.Play("Blink");
			}, 2);

			Cloud.OnKeyChange("thirdTrophy", (bool value) =>
			{
				elements.thirdTrophyToggle.isOn = value;
				elements.thirdTrophyToggleAnimation.Play("Blink");
			}, 2);
		}

	#endregion


	#region Conflict resolving

		void AddConflictResolvingActions()
		{
			Cloud.OnKeyChange("level", ResolveConflictForLevel, 1);
			Cloud.OnKeyChange("firstTrophy", ResolveConflictForFirstTrophy, 1);
			Cloud.OnKeyChange("secondTrophy", ResolveConflictForSecondTrophy, 1);
			Cloud.OnKeyChange("thirdTrophy", ResolveConflictForThirdTrophy, 1);
		}

		void RemoveConflictResolvingActions()
		{
			Cloud.RemoveOnKeyChangeAction("level", ResolveConflictForLevel);
			Cloud.RemoveOnKeyChangeAction("firstTrophy", ResolveConflictForFirstTrophy);
			Cloud.RemoveOnKeyChangeAction("secondTrophy", ResolveConflictForSecondTrophy);
			Cloud.RemoveOnKeyChangeAction("thirdTrophy", ResolveConflictForThirdTrophy);
		}

		void ResolveConflictForLevel(int value)
		{
			Debug.Log("ResolveConflictForLevel("+value+")");

			bool isConflict = (elements.levelDropdown.value != value);
    		if (isConflict)
    		{
    			// Resolve strategy.
    			elements.levelDropdown.value = Math.Max(elements.levelDropdown.value, value);
    			OnLevelDropDownValueChanged(elements.levelDropdown.value);
			}
		}

		void ResolveConflictForFirstTrophy(bool value)
		{
			Debug.Log("ResolveConflictForFirstTrophy("+value+")");

			bool isConflict = (elements.firstTrophyToggle.isOn != value);
    		if (isConflict)
    		{
    			// Resolve strategy.
    			elements.firstTrophyToggle.isOn = elements.firstTrophyToggle.isOn || value;
				// Update UI and Sync.
    			OnFirstTrophyToggleValueChanged(elements.firstTrophyToggle.isOn);
			};
		}

		void ResolveConflictForSecondTrophy(bool value)
		{
			Debug.Log("ResolveConflictForSecondTrophy("+value+")");

			bool isConflict = (elements.secondTrophyToggle.isOn != value);
    		if (isConflict)
    		{
    			// Resolve strategy.
    			elements.secondTrophyToggle.isOn = elements.secondTrophyToggle.isOn || value;
				// Update UI and Sync.
    			OnSecondTrophyToggleValueChanged(elements.secondTrophyToggle.isOn);
			};
		}

		void ResolveConflictForThirdTrophy(bool value)
		{
			Debug.Log("ResolveConflictForThirdTrophy("+value+")");

			bool isConflict = (elements.thirdTrophyToggle.isOn != value);
    		if (isConflict)
    		{
    			// Resolve strategy.
    			elements.thirdTrophyToggle.isOn = elements.thirdTrophyToggle.isOn || value;
				// Update UI and Sync.
    			OnThirdTrophyToggleValueChanged(elements.thirdTrophyToggle.isOn); 
			};
		}

	#endregion


	}
}

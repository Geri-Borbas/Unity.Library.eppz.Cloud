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
		{ AddElementUpdatingActions(); }


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
			});

			Cloud.OnKeyChange("sound", (bool value) =>
			{
				elements.soundToggle.isOn = value;
				elements.nameLabelAnimation.Play("Blink");
			});

			Cloud.OnKeyChange("volume", (float value) =>
			{
				elements.volumeSlider.value = value;
				elements.nameLabelAnimation.Play("Blink");
			});

			Cloud.OnKeyChange("level", (int value) =>
			{
				elements.levelDropdown.value = value;
				elements.nameLabelAnimation.Play("Blink");
			});

			Cloud.OnKeyChange("firstTrophy", (bool value) =>
			{
				elements.firstTrophyToggle.isOn = value;
				elements.nameLabelAnimation.Play("Blink");
			});

			Cloud.OnKeyChange("secondTrophy", (bool value) =>
			{
				elements.secondTrophyToggle.isOn = value;
				elements.nameLabelAnimation.Play("Blink");
			});

			Cloud.OnKeyChange("thirdTrophy", (bool value) =>
			{
				elements.thirdTrophyToggle.isOn = value;
				elements.nameLabelAnimation.Play("Blink");
			});
		}

	#endregion


	#region Conflict resolving

		void AddConflictResolvingActions()
		{
			Cloud.OnKeyChange("level", ResolveConflictForLevel);
			Cloud.OnKeyChange("firstTrophy", ResolveConflictForFirstTrophy);
			Cloud.OnKeyChange("secondTrophy", ResolveConflictForSecondTrophy);
			Cloud.OnKeyChange("thirdTrophy", ResolveConflictForThirdTrophy);
		}

		void RemoveConflictResolvingActions()
		{
			Cloud.RemoveOnKeyChangeAction("level", ResolveConflictForLevel);
			Cloud.RemoveOnKeyChangeAction("firstTrophy", ResolveConflictForFirstTrophy);
			Cloud.RemoveOnKeyChangeAction("secondTrophy", ResolveConflictForSecondTrophy);
			Cloud.RemoveOnKeyChangeAction("thirdTrophy", ResolveConflictForThirdTrophy);
		}

		void ResolveConflictForLevel(int value)
		{ Debug.Log("Resolving confict for `level`."); }

		void ResolveConflictForFirstTrophy(bool value)
		{ Debug.Log("Resolving confict for `firstTrophy`."); }

		void ResolveConflictForSecondTrophy(bool value)
		{ Debug.Log("Resolving confict for `secondTrophy`."); }

		void ResolveConflictForThirdTrophy(bool value)
		{ Debug.Log("Resolving confict for `thirdTrophy`."); }

	#endregion


	}
}

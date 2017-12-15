using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace  EPPZ.Cloud.Scenes.Helpers
{

			
	[ExecuteInEditMode]
	public class CanvasScaleToScreenWidthConstraint : MonoBehaviour
	{


		public AnimationCurve canvasScaleToScreenWidth;
		
		
		void Update()
		{
			GetComponent<CanvasScaler>().scaleFactor =
				canvasScaleToScreenWidth.Evaluate(Screen.width);
		}
	}
}

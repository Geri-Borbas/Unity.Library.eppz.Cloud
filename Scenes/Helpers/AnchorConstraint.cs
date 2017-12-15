using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace  EPPZ.Cloud.Scenes.Helpers
{

	
	[ExecuteInEditMode]
	public class AnchorConstraint : MonoBehaviour
	{


		RectTransform _parentRectTransform;
		RectTransform _rectTransform;


		void OnEnable()
		{
			_rectTransform = GetComponent<RectTransform>();
			_parentRectTransform = transform.parent.GetComponent<RectTransform>();
		}
		
		void Update()
		{
			if (enabled == false) return;

			// Get parent aspect ratio.
			float parentAspect = _parentRectTransform.rect.width / _parentRectTransform.rect.height;

			// Match vertical anchors to horizontal.
			_rectTransform.anchorMin = new Vector2(
				_rectTransform.anchorMin.x,
				_rectTransform.anchorMin.x * parentAspect
			);
			_rectTransform.anchorMax = new Vector2(
				_rectTransform.anchorMax.x,
				1.0f - (1.0f - _rectTransform.anchorMax.x) * parentAspect
			);

			// Align rect.
			_rectTransform.offsetMin =
			_rectTransform.offsetMax =
			Vector2.zero;
		}
	}
}
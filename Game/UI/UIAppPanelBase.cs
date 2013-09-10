using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum AppViewerUIPanelContext {
	DIRECT,
	BACK,
	NEXT
}

public class UIAppPanelBase : UIAppPanel {
	
	public GameObject listGridRoot;
    public UIGrid listGrid;
	
	public UIPanel panelClipped;
	
	[NonSerialized]
	public int increment = 0;
		
	public GameObject panelLeftObject;
	public GameObject panelLeftBottomObject;
	public GameObject panelLeftTopObject;
	public GameObject panelRightObject;
	public GameObject panelRightBottomObject;
	public GameObject panelRightTopObject;
	public GameObject panelTopObject;
	public GameObject panelBottomObject;
	public GameObject panelCenterObject;
	
	public GameObject panelContainer;	

	[NonSerialized]
	public float durationShow = .33f;
	[NonSerialized]
	public float durationHide = .33f;
	[NonSerialized]
	public float durationDelayShow = 0f;
	[NonSerialized]
	public float durationDelayHide = .05f;
	[NonSerialized]
	public float leftOpenX = 0f;
	[NonSerialized]
	public float leftClosedX = -2000f;
	[NonSerialized]
	public float rightOpenX = 0f;
	[NonSerialized]
	public float rightClosedX = 2000f;
	[NonSerialized]
	public float bottomOpenY = 0f;
	[NonSerialized]
	public float bottomClosedY = -2000f;
	[NonSerialized]
	public float topOpenY = 0f;
	[NonSerialized]
	public float topClosedY = 2000f;
			
	public override void Start() {
		base.Start();		
	}
	
	public override void Init() {
		base.Init();
		
		// hide all panels
		
		HideAllPanelsNow();
	}
			
	public void HideAllPanels() {
		foreach(UIAppPanelBase baseItem in Resources.FindObjectsOfTypeAll(typeof(UIAppPanelBase))) {
			baseItem.AnimateOut();
		}
	}
	
	public void HideAllPanelsNow() {
		foreach(UIAppPanelBase baseItem in Resources.FindObjectsOfTypeAll(typeof(UIAppPanelBase))) {
			baseItem.AnimateOutNow();
		}
	}
	
	public virtual void AnimateInCenter(float time, float delay) {
		if(panelCenterObject != null) {
        	UITweenerUtil.MoveTo(panelCenterObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithY(bottomOpenY));			
		}
	}
	
	public virtual void AnimateOutCenter(float time, float delay) {
		if(panelCenterObject != null) {
        	UITweenerUtil.MoveTo(panelCenterObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithY(bottomClosedY));			
		}
	}
	
	public virtual void AnimateInLeft(float time, float delay) {
		if(panelLeftObject != null) {
        	UITweenerUtil.MoveTo(panelLeftObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(0));			
		}
	}
	
	public virtual void AnimateOutLeft(float time, float delay) {
		if(panelLeftObject != null) {
        	UITweenerUtil.MoveTo(panelLeftObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(leftClosedX));			
		}
	}
	
	public virtual void AnimateInLeftBottom(float time, float delay) {
		if(panelLeftBottomObject != null) {
        	UITweenerUtil.MoveTo(panelLeftBottomObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(0));			
		}
	}
	
	public virtual void AnimateOutLeftBottom(float time, float delay) {
		if(panelLeftBottomObject != null) {
        	UITweenerUtil.MoveTo(panelLeftBottomObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(leftClosedX));			
		}
	}	
	
	public virtual void AnimateInLeftTop(float time, float delay) {
		if(panelLeftTopObject != null) {
        	UITweenerUtil.MoveTo(panelLeftTopObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(0));			
		}
	}
	
	public virtual void AnimateOutLeftTop(float time, float delay) {
		if(panelLeftTopObject != null) {
        	UITweenerUtil.MoveTo(panelLeftTopObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay,  Vector3.zero.WithX(leftClosedX));			
		}
	}
	
	public virtual void AnimateInRight(float time, float delay) {
		if(panelRightObject != null) {
        	UITweenerUtil.MoveTo(panelRightObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(0));			
		}
	}
	
	public virtual void AnimateOutRight(float time, float delay) {
		if(panelRightObject != null) {
        	UITweenerUtil.MoveTo(panelRightObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(rightClosedX));			
		}
	}	
	
	
	public virtual void AnimateInRightBottom(float time, float delay) {
		if(panelRightBottomObject != null) {
        	UITweenerUtil.MoveTo(panelRightBottomObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(0));			
		}
	}
	
	public virtual void AnimateOutRightBottom(float time, float delay) {
		if(panelRightBottomObject != null) {
        	UITweenerUtil.MoveTo(panelRightBottomObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(rightClosedX));			
		}
	}	
	
	public virtual void AnimateInRightTop(float time, float delay) {
		if(panelRightTopObject != null) {
        	UITweenerUtil.MoveTo(panelRightTopObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(0));			
		}
	}
	
	public virtual void AnimateOutRightTop(float time, float delay) {
		if(panelRightTopObject != null) {
        	UITweenerUtil.MoveTo(panelRightTopObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithX(rightClosedX));			
		}
	}	
	
	public virtual void AnimateInTop(float time, float delay) {
		if(panelTopObject != null) {
        	UITweenerUtil.MoveTo(panelTopObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithY(0));			
		}
	}
	
	public virtual void AnimateOutTop(float time, float delay) {
		if(panelTopObject != null) {
        	UITweenerUtil.MoveTo(panelTopObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithY(topClosedY));			
		}
	}
	
	public virtual void AnimateInBottom(float time, float delay) {
		if(panelBottomObject != null) {
        	UITweenerUtil.MoveTo(panelBottomObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithY(0));			
		}
	}
	
	public virtual void AnimateOutBottom(float time, float delay) {
		if(panelBottomObject != null) {
        	UITweenerUtil.MoveTo(panelBottomObject, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, time, delay, Vector3.zero.WithY(bottomClosedY));			
		}
	}
	
	public virtual void AnimateIn() {
		
		//AnimateOut(0f, 0f);
		
		float time = durationShow;
		float delay = durationDelayShow;
		
		AnimateIn(time, delay);
	}
	
	public virtual void AnimateIn(float time, float delay) {
				
		CancelInvoke("HidePanel");
		ShowPanel();
				
		StartCoroutine(AnimateInCo(time, delay));
	}
	
	public IEnumerator AnimateInCo(float time, float delay) {
		
		yield return new WaitForEndOfFrame();
		
		AnimateInCenter(time, delay);
		AnimateInLeft(time, delay);
		AnimateInLeftBottom(time, delay);
		AnimateInLeftTop(time, delay);
		AnimateInRight(time, delay);
		AnimateInRightBottom(time, delay);
		AnimateInRightTop(time, delay);
		AnimateInTop(time, delay);
		AnimateInBottom(time, delay);
	}
	
	public virtual void AnimateOut() {
		AnimateOut(true);
	}
	
	public virtual void AnimateOut(bool hideObject) {
		
		float time = durationHide;
		float delay = durationDelayHide;
		
		AnimateOut(time, delay, hideObject);
	}
	
	public virtual void AnimateOutNow() {
		
		float time = 0f;
		float delay = 0f;
		
		AnimateOut(time, delay);
	}
	
	public virtual void AnimateOut(float time, float delay) {
		
		AnimateOut(time, delay, true);
	}
	
	public virtual void AnimateOut(float time, float delay, bool hideObject) {
		
		AnimateOutCenter(time, delay);
		AnimateOutLeft(time, delay);
		AnimateOutLeftBottom(time, delay);
		AnimateOutLeftTop(time, delay);
		AnimateOutRight(time, delay);
		AnimateOutRightBottom(time, delay);
		AnimateOutRightTop(time, delay);
		AnimateOutTop(time, delay);
		AnimateOutBottom(time, delay);	
				
		if(hideObject) {
			Invoke("HidePanel", delay);
		}
	}
	
	public virtual void HidePanel() {
		/*
		if(panelCenterObject != null) {
        	panelCenterObject.Hide();		
		}
		if(panelLeftObject != null) {
        	panelLeftObject.Hide();		
		}
		if(panelLeftBottomObject != null) {
        	panelLeftBottomObject.Hide();		
		}
		if(panelLeftTopObject != null) {
        	panelLeftTopObject.Hide();		
		}
		if(panelRightObject != null) {
        	panelRightObject.Hide();		
		}
		if(panelRightBottomObject != null) {
        	panelRightBottomObject.Hide();		
		}
		if(panelRightTopObject != null) {
        	panelRightTopObject.Hide();		
		}
		if(panelTopObject != null) {
        	panelTopObject.Hide();		
		}
		if(panelBottomObject != null) {
        	panelBottomObject.Hide();		
		}
		*/
		
		if(panelContainer != null) {
        	panelContainer.Hide();		
		}
		
		isVisible = false;
	}
	
	public virtual void ShowPanel() {
		
		/*
		if(panelCenterObject != null) {
        	panelCenterObject.Show();		
		}	
		if(panelLeftObject != null) {
        	panelLeftObject.Show();		
		}
		if(panelLeftBottomObject != null) {
        	panelLeftBottomObject.Show();		
		}
		if(panelLeftTopObject != null) {
        	panelLeftTopObject.Show();		
		}
		if(panelRightObject != null) {
        	panelRightObject.Show();		
		}
		if(panelRightBottomObject != null) {
        	panelRightBottomObject.Show();		
		}
		if(panelRightTopObject != null) {
        	panelRightTopObject.Show();		
		}
		if(panelTopObject != null) {
        	panelTopObject.Show();		
		}
		if(panelBottomObject != null) {
        	panelBottomObject.Show();		
		}
		*/
		
		if(panelContainer != null) {
        	panelContainer.Show();		
		}	
		
		isVisible = true;
	}
	
	// top
	
	public virtual void ShowPanelTop(GameObject panel) {
		ShowPanelTop(panel, true);
	}
	
	public virtual void ShowPanelTop(GameObject panel, bool fade) {
		if(panel != null) {
			
			//UITweenerUtil.MoveTo(panel, 
			////	UITweener.Method.Linear, UITweener.Style.Once, 0f, 0f, 
			//	Vector3.zero.WithX(leftOpenX).WithY(topClosedY));	
			
			if(fade) {
				UITweenerUtil.FadeTo(panel, 
					UITweener.Method.Linear, UITweener.Style.Once, durationShow, durationDelayShow, 1f);
			}
			
			UITweenerUtil.MoveTo(panel, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, durationShow, durationDelayShow, 
				Vector3.zero.WithY(topOpenY));	
			
		}
	}
	
	public virtual void HidePanelTop(GameObject panel) {
		HidePanelTop(panel, true);
	}
	
	public virtual void HidePanelTop(GameObject panel, bool fade) {
		if(panel != null) {
			
			if(fade) {
				UITweenerUtil.FadeTo(panel, 
					UITweener.Method.Linear, UITweener.Style.Once, durationHide, durationDelayHide, 0f);
			}
			
			UITweenerUtil.MoveTo(panel, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, durationHide, durationDelayHide, 
				Vector3.zero.WithY(topClosedY));	
			
			//UITweenerUtil.MoveTo(panel, 
			//	UITweener.Method.Linear, UITweener.Style.Once, durationHide, durationHide * 2, 
			//	Vector3.zero.WithX(leftClosedX).WithY(topClosedY));	
		}
	}
		
	// bottom
	
	public virtual void ShowPanelBottom(GameObject panel) {
		ShowPanelBottom(panel, true);
	}
		
	public virtual void ShowPanelBottom(GameObject panel, bool fade) {
		if(panel != null) {
			
			UITweenerUtil.MoveTo(panel, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, 0f, 0f, 
				Vector3.zero.WithY(bottomOpenY));	
			
			if(fade) {
				UITweenerUtil.FadeTo(panel, 
					UITweener.Method.Linear, UITweener.Style.Once, durationShow, durationDelayShow, 1f);
			}
			
			// TODO look for -alpha-[number] to handle nested items to only fade to a certain amount.
			//foreach(Transform t in panel.transform) {
				
			//}
			
			//UITweenerUtil.MoveTo(panel, 
			//	UITweener.Method.EaseInOut, UITweener.Style.Once, durationShow, durationDelayShow, 
			//	Vector3.zero.WithY(bottomOpenY));	
			
		}
	}
	
	public virtual void HidePanelBottom(GameObject panel, bool fade) {
		if(panel != null) {
			
			if(fade) {
				UITweenerUtil.FadeTo(panel, 
					UITweener.Method.Linear, UITweener.Style.Once, durationHide, durationDelayHide, 0f);
			}
			
			UITweenerUtil.MoveTo(panel, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, durationHide, durationDelayHide, 
				Vector3.zero.WithY(bottomClosedY));	
			
			//UITweenerUtil.MoveTo(panel, 
			//	UITweener.Method.Linear, UITweener.Style.Once, durationHide, durationHide * 2, 
			//	Vector3.zero.WithX(leftClosedX).WithY(bottomClosedY));	
		}
	}
			
	public virtual void HidePanelBottom(GameObject panel) {
		HidePanelBottom(panel, true);
	}
	
	// left
	
	public virtual void ShowPanelLeft(GameObject panel) {
		ShowPanelLeft(panel, true);
	}
	
	
	public virtual void ShowPanelLeft(GameObject panel, bool fade) {
		if(panel != null) {
			
			//UITweenerUtil.MoveTo(panel, 
			//	UITweener.Method.Linear, UITweener.Style.Once, 0f, 0f, 
			//	Vector3.zero.WithX(leftClosedX));	
			
			if(fade) {
				UITweenerUtil.FadeTo(panel, 
					UITweener.Method.Linear, UITweener.Style.Once, durationShow * 2, durationShow / 2, 1f);
			}
			
			UITweenerUtil.MoveTo(panel, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, durationShow * 2, durationShow / 2, 
				Vector3.zero.WithX(leftOpenX));	
			
		}
	}
	
	public virtual void HidePanelLeft(GameObject panel) {
		HidePanelLeft(panel, true);
	}
	
	public virtual void HidePanelLeft(GameObject panel, bool fade) {
		if(panel != null) {
			
			if(fade) {
				UITweenerUtil.FadeTo(panel, 
					UITweener.Method.Linear, UITweener.Style.Once, durationHide * 2, durationHide * 2, 0f);
			}
			
			UITweenerUtil.MoveTo(panel, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, durationHide * 2, 0f, 
				Vector3.zero.WithX(leftClosedX));
							
			//UITweenerUtil.MoveTo(panel, 
			//	UITweener.Method.Linear, UITweener.Style.Once, durationHide * 4, durationHide * 8, 
			//	Vector3.zero.WithY(topClosedY));	
		}
	}
	
	// right
	
	public virtual void ShowPanelRight(GameObject panel) {
		ShowPanelRight(panel, true);
	}
	
	public virtual void ShowPanelRight(GameObject panel, bool fade) {
		if(panel != null) {
			
			//UITweenerUtil.MoveTo(panel, 
			//	UITweener.Method.Linear, UITweener.Style.Once, 0f, 0f, 
			//	Vector3.zero.WithX(rightClosedX));	
			
			if(fade) {
				UITweenerUtil.FadeTo(panel, 
					UITweener.Method.Linear, UITweener.Style.Once, durationShow * 2, durationShow / 2, 1f);
			}
			
			UITweenerUtil.MoveTo(panel, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, durationShow * 2, durationShow / 2, 
				Vector3.zero.WithX(rightOpenX));	
			
		}
	}
	
	public virtual void HidePanelRight(GameObject panel) {
		HidePanelRight(panel, true);
	}
	
	public virtual void HidePanelRight(GameObject panel, bool fade) {
		if(panel != null) {
									
			if(fade) {
				UITweenerUtil.FadeTo(panel, 
					UITweener.Method.Linear, UITweener.Style.Once, durationHide * 2, durationHide * 2, 0f);
			}
			
			UITweenerUtil.MoveTo(panel, 
				UITweener.Method.EaseInOut, UITweener.Style.Once, durationHide * 2, 0f, 
				Vector3.zero.WithX(rightClosedX));	
			
			//UITweenerUtil.MoveTo(panel, 
			//	UITweener.Method.Linear, UITweener.Style.Once, durationHide * 4, durationHide * 8, 
			//	Vector3.zero.WithX(rightClosedX).WithY(topClosedY));	
		}
	}
	
	
	public virtual GameObject LoadObject(GameObject prefabObject, string itemName) {
		return LoadObject(listGridRoot, prefabObject, itemName);
	}
	
	public virtual GameObject LoadObject(GameObject prefabObject, string itemName, 
		string title, string description, string note, string type) {
		return LoadObject(listGridRoot, prefabObject, itemName, title, description, note, type);
	}
	
	public virtual GameObject LoadObject(GameObject listObject, GameObject prefabObject, string itemName) {
		if(listObject == null) {
			return null;
		}
		if(prefabObject == null) {
			return null;
		}
		GameObject item = NGUITools.AddChild(listObject, prefabObject);
		item.name = "_" + increment++ + "_" + itemName;
		return item;
	}
	
	public virtual GameObject LoadObject(GameObject listObject, GameObject prefabObject, string itemName, 
		string title, string description, string note, string type) {
		
		if(listObject == null) {
			return null;
		}
		
		if(prefabObject == null) {
			return null;
		}
		
		GameObject item = LoadObject(listObject, prefabObject, itemName);
		SetItemLabel(item, "LabelName", title);
		SetItemLabel(item, "LabelDescription", description);
		SetItemLabel(item, "LabelNote", note);
		
		// show type icon
		
		Transform typeObjects = item.transform.FindChild("types");
		
		if(typeObjects != null) {
			foreach(Transform t in typeObjects.gameObject.transform) {
				t.gameObject.Hide(); // hide all 
			}
			
			Transform typeObject = typeObjects.FindChild(type);
			if(typeObject != null) {
				// show current
				typeObject.gameObject.Show();
			}
		}
		
		return item;
	}
	
	public void SetItemLabel(GameObject item, string labelName, string val) {
		if(item == null) {
			return;
		}
		
		UILabel label = GetItemLabel(item, labelName);
		if(label != null) {
			label.text = val;
		}
	}
	
	public UILabel GetItemLabel(GameObject item, string labelName) {
		if(item == null) {
			return null;
		}
		
		Transform t = item.transform.FindChild(labelName);
		if(t != null) {
			UILabel label = t.GetComponent<UILabel>();
			if(label != null) {
				return label;
			}
		}
		return null;
	}
	
}

using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009F RID: 159
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Event System (UICamera)")]
[RequireComponent(typeof(Camera))]
public class UICamera : MonoBehaviour
{
	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000700 RID: 1792 RVA: 0x0002291C File Offset: 0x00020B1C
	[Obsolete("Use new OnDragStart / OnDragOver / OnDragOut / OnDragEnd events instead")]
	public bool stickyPress
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000701 RID: 1793 RVA: 0x000398BB File Offset: 0x00037ABB
	// (set) Token: 0x06000702 RID: 1794 RVA: 0x000398CE File Offset: 0x00037ACE
	public static bool disableController
	{
		get
		{
			return UICamera.mDisableController && !UIPopupList.isOpen;
		}
		set
		{
			UICamera.mDisableController = value;
		}
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000703 RID: 1795 RVA: 0x000398D6 File Offset: 0x00037AD6
	// (set) Token: 0x06000704 RID: 1796 RVA: 0x000398DD File Offset: 0x00037ADD
	[Obsolete("Use lastEventPosition instead. It handles controller input properly.")]
	public static Vector2 lastTouchPosition
	{
		get
		{
			return UICamera.mLastPos;
		}
		set
		{
			UICamera.mLastPos = value;
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000705 RID: 1797 RVA: 0x000398E8 File Offset: 0x00037AE8
	// (set) Token: 0x06000706 RID: 1798 RVA: 0x000398DD File Offset: 0x00037ADD
	public static Vector2 lastEventPosition
	{
		get
		{
			if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
			{
				GameObject hoveredObject = UICamera.hoveredObject;
				if (hoveredObject != null)
				{
					Bounds bounds = NGUIMath.CalculateAbsoluteWidgetBounds(hoveredObject.transform);
					return NGUITools.FindCameraForLayer(hoveredObject.layer).WorldToScreenPoint(bounds.center);
				}
			}
			return UICamera.mLastPos;
		}
		set
		{
			UICamera.mLastPos = value;
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06000707 RID: 1799 RVA: 0x0003993A File Offset: 0x00037B3A
	public static UICamera first
	{
		get
		{
			if (UICamera.list == null || UICamera.list.size == 0)
			{
				return null;
			}
			return UICamera.list.buffer[0];
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06000708 RID: 1800 RVA: 0x00039960 File Offset: 0x00037B60
	// (set) Token: 0x06000709 RID: 1801 RVA: 0x000399DC File Offset: 0x00037BDC
	public static UICamera.ControlScheme currentScheme
	{
		get
		{
			if (UICamera.mCurrentKey == KeyCode.None)
			{
				return UICamera.ControlScheme.Touch;
			}
			if (UICamera.mCurrentKey >= KeyCode.JoystickButton0)
			{
				return UICamera.ControlScheme.Controller;
			}
			if (!(UICamera.current != null))
			{
				return UICamera.ControlScheme.Mouse;
			}
			if (UICamera.mLastScheme == UICamera.ControlScheme.Controller && (UICamera.mCurrentKey == UICamera.current.submitKey0 || UICamera.mCurrentKey == UICamera.current.submitKey1))
			{
				return UICamera.ControlScheme.Controller;
			}
			if (UICamera.current.useMouse)
			{
				return UICamera.ControlScheme.Mouse;
			}
			if (UICamera.current.useTouch)
			{
				return UICamera.ControlScheme.Touch;
			}
			return UICamera.ControlScheme.Controller;
		}
		set
		{
			if (UICamera.mLastScheme != value)
			{
				if (value == UICamera.ControlScheme.Mouse)
				{
					UICamera.currentKey = KeyCode.Mouse0;
				}
				else if (value == UICamera.ControlScheme.Controller)
				{
					UICamera.currentKey = KeyCode.JoystickButton0;
				}
				else if (value == UICamera.ControlScheme.Touch)
				{
					UICamera.currentKey = KeyCode.None;
				}
				else
				{
					UICamera.currentKey = KeyCode.Alpha0;
				}
				UICamera.mLastScheme = value;
			}
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x0600070A RID: 1802 RVA: 0x00039A29 File Offset: 0x00037C29
	// (set) Token: 0x0600070B RID: 1803 RVA: 0x00039A30 File Offset: 0x00037C30
	public static KeyCode currentKey
	{
		get
		{
			return UICamera.mCurrentKey;
		}
		set
		{
			if (UICamera.mCurrentKey != value)
			{
				UICamera.ControlScheme controlScheme = UICamera.mLastScheme;
				UICamera.mCurrentKey = value;
				UICamera.mLastScheme = UICamera.currentScheme;
				if (controlScheme != UICamera.mLastScheme)
				{
					UICamera.HideTooltip();
					if (UICamera.mLastScheme == UICamera.ControlScheme.Mouse)
					{
						Cursor.lockState = CursorLockMode.None;
						Cursor.visible = true;
					}
					else if (UICamera.current != null && UICamera.current.autoHideCursor)
					{
						Cursor.visible = false;
						Cursor.lockState = CursorLockMode.Locked;
						UICamera.mMouse[0].ignoreDelta = 2;
					}
					if (UICamera.onSchemeChange != null)
					{
						UICamera.onSchemeChange();
					}
				}
			}
		}
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x0600070C RID: 1804 RVA: 0x00039AC0 File Offset: 0x00037CC0
	public static Ray currentRay
	{
		get
		{
			if (!(UICamera.currentCamera != null) || UICamera.currentTouch == null)
			{
				return default(Ray);
			}
			return UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x0600070D RID: 1805 RVA: 0x00039B04 File Offset: 0x00037D04
	public static bool inputHasFocus
	{
		get
		{
			return UICamera.mInputFocus && UICamera.mSelected && UICamera.mSelected.activeInHierarchy;
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x0600070E RID: 1806 RVA: 0x00039B28 File Offset: 0x00037D28
	// (set) Token: 0x0600070F RID: 1807 RVA: 0x00039B2F File Offset: 0x00037D2F
	[Obsolete("Use delegates instead such as UICamera.onClick, UICamera.onHover, etc.")]
	public static GameObject genericEventHandler
	{
		get
		{
			return UICamera.mGenericHandler;
		}
		set
		{
			UICamera.mGenericHandler = value;
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06000710 RID: 1808 RVA: 0x00039B37 File Offset: 0x00037D37
	public static UICamera.MouseOrTouch mouse0
	{
		get
		{
			return UICamera.mMouse[0];
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06000711 RID: 1809 RVA: 0x00039B40 File Offset: 0x00037D40
	public static UICamera.MouseOrTouch mouse1
	{
		get
		{
			return UICamera.mMouse[1];
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000712 RID: 1810 RVA: 0x00039B49 File Offset: 0x00037D49
	public static UICamera.MouseOrTouch mouse2
	{
		get
		{
			return UICamera.mMouse[2];
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000713 RID: 1811 RVA: 0x00039B52 File Offset: 0x00037D52
	private bool handlesEvents
	{
		get
		{
			return UICamera.eventHandler == this;
		}
	}

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06000714 RID: 1812 RVA: 0x00039B5F File Offset: 0x00037D5F
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.GetComponent<Camera>();
			}
			return this.mCam;
		}
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x06000715 RID: 1813 RVA: 0x00039B81 File Offset: 0x00037D81
	// (set) Token: 0x06000716 RID: 1814 RVA: 0x00039B88 File Offset: 0x00037D88
	public static GameObject tooltipObject
	{
		get
		{
			return UICamera.mTooltip;
		}
		set
		{
			UICamera.ShowTooltip(value);
		}
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00039B91 File Offset: 0x00037D91
	public static bool IsPartOfUI(GameObject go)
	{
		return !(go == null) && !(go == UICamera.fallThrough) && NGUITools.FindInParents<UIRoot>(go) != null;
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06000718 RID: 1816 RVA: 0x00039BB8 File Offset: 0x00037DB8
	public static bool isOverUI
	{
		get
		{
			int frameCount = Time.frameCount;
			if (UICamera.mLastOverCheck != frameCount)
			{
				UICamera.mLastOverCheck = frameCount;
				if (UICamera.currentTouch != null)
				{
					if (UICamera.currentTouch.pressed != null)
					{
						UICamera.mLastOverResult = UICamera.IsPartOfUI(UICamera.currentTouch.pressed);
						return UICamera.mLastOverResult;
					}
					UICamera.mLastOverResult = UICamera.IsPartOfUI(UICamera.currentTouch.current);
					return UICamera.mLastOverResult;
				}
				else
				{
					int i = 0;
					int count = UICamera.activeTouches.Count;
					while (i < count)
					{
						if (UICamera.IsPartOfUI(UICamera.activeTouches[i].pressed))
						{
							UICamera.mLastOverResult = true;
							return UICamera.mLastOverResult;
						}
						i++;
					}
					for (int j = 0; j < 3; j++)
					{
						UICamera.MouseOrTouch mouseOrTouch = UICamera.mMouse[j];
						if (UICamera.IsPartOfUI((mouseOrTouch.pressed != null) ? mouseOrTouch.pressed : ((j == 0) ? mouseOrTouch.current : null)))
						{
							UICamera.mLastOverResult = true;
							return UICamera.mLastOverResult;
						}
					}
					UICamera.mLastOverResult = UICamera.IsPartOfUI(UICamera.controller.pressed);
				}
			}
			return UICamera.mLastOverResult;
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06000719 RID: 1817 RVA: 0x00039CCC File Offset: 0x00037ECC
	public static bool uiHasFocus
	{
		get
		{
			int frameCount = Time.frameCount;
			if (UICamera.mLastFocusCheck != frameCount)
			{
				UICamera.mLastFocusCheck = frameCount;
				if (UICamera.inputHasFocus)
				{
					UICamera.mLastFocusResult = true;
					return UICamera.mLastFocusResult;
				}
				if (UICamera.currentTouch != null)
				{
					UICamera.mLastFocusResult = UICamera.currentTouch.isOverUI;
					return UICamera.mLastFocusResult;
				}
				int i = 0;
				int count = UICamera.activeTouches.Count;
				while (i < count)
				{
					if (UICamera.IsPartOfUI(UICamera.activeTouches[i].pressed))
					{
						UICamera.mLastFocusResult = true;
						return UICamera.mLastFocusResult;
					}
					i++;
				}
				UICamera.MouseOrTouch mouseOrTouch = UICamera.mMouse[0];
				if (UICamera.IsPartOfUI(mouseOrTouch.pressed) || UICamera.IsPartOfUI(mouseOrTouch.current))
				{
					UICamera.mLastFocusResult = true;
					return UICamera.mLastFocusResult;
				}
				for (int j = 1; j < 3; j++)
				{
					mouseOrTouch = UICamera.mMouse[j];
					if (UICamera.IsPartOfUI(mouseOrTouch.pressed))
					{
						UICamera.mLastFocusResult = true;
						return UICamera.mLastFocusResult;
					}
				}
				UICamera.mLastFocusResult = UICamera.IsPartOfUI(UICamera.controller.pressed);
			}
			return UICamera.mLastFocusResult;
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x0600071A RID: 1818 RVA: 0x00039DD4 File Offset: 0x00037FD4
	public static bool interactingWithUI
	{
		get
		{
			int frameCount = Time.frameCount;
			if (UICamera.mLastInteractionCheck != frameCount)
			{
				UICamera.mLastInteractionCheck = frameCount;
				if (UICamera.inputHasFocus)
				{
					UICamera.mLastInteractionResult = true;
					return UICamera.mLastInteractionResult;
				}
				int i = 0;
				int count = UICamera.activeTouches.Count;
				while (i < count)
				{
					if (UICamera.IsPartOfUI(UICamera.activeTouches[i].pressed))
					{
						UICamera.mLastInteractionResult = true;
						return UICamera.mLastInteractionResult;
					}
					i++;
				}
				for (int j = 0; j < 3; j++)
				{
					if (UICamera.IsPartOfUI(UICamera.mMouse[j].pressed))
					{
						UICamera.mLastInteractionResult = true;
						return UICamera.mLastInteractionResult;
					}
				}
				UICamera.mLastInteractionResult = UICamera.IsPartOfUI(UICamera.controller.pressed);
			}
			return UICamera.mLastInteractionResult;
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x0600071B RID: 1819 RVA: 0x00039E8C File Offset: 0x0003808C
	// (set) Token: 0x0600071C RID: 1820 RVA: 0x00039EE4 File Offset: 0x000380E4
	public static GameObject hoveredObject
	{
		get
		{
			if (UICamera.currentTouch != null && (UICamera.currentScheme != UICamera.ControlScheme.Mouse || UICamera.currentTouch.dragStarted))
			{
				return UICamera.currentTouch.current;
			}
			if (UICamera.mHover && UICamera.mHover.activeInHierarchy)
			{
				return UICamera.mHover;
			}
			UICamera.mHover = null;
			return null;
		}
		set
		{
			if (UICamera.mHover == value)
			{
				return;
			}
			bool flag = false;
			UICamera uicamera = UICamera.current;
			if (UICamera.currentTouch == null)
			{
				flag = true;
				UICamera.currentTouchID = -100;
				UICamera.currentTouch = UICamera.controller;
			}
			UICamera.ShowTooltip(null);
			if (UICamera.mSelected && UICamera.currentScheme == UICamera.ControlScheme.Controller)
			{
				UICamera.Notify(UICamera.mSelected, "OnSelect", false);
				if (UICamera.onSelect != null)
				{
					UICamera.onSelect(UICamera.mSelected, false);
				}
				UICamera.mSelected = null;
			}
			if (UICamera.mHover)
			{
				UICamera.Notify(UICamera.mHover, "OnHover", false);
				if (UICamera.onHover != null)
				{
					UICamera.onHover(UICamera.mHover, false);
				}
			}
			UICamera.mHover = value;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
			if (UICamera.mHover)
			{
				if (UICamera.mHover != UICamera.controller.current && UICamera.mHover.GetComponent<UIKeyNavigation>() != null)
				{
					UICamera.controller.current = UICamera.mHover;
				}
				if (flag)
				{
					UICamera uicamera2 = (UICamera.mHover != null) ? UICamera.FindCameraForLayer(UICamera.mHover.layer) : UICamera.list.buffer[0];
					if (uicamera2 != null)
					{
						UICamera.current = uicamera2;
						UICamera.currentCamera = uicamera2.cachedCamera;
					}
				}
				if (UICamera.onHover != null)
				{
					UICamera.onHover(UICamera.mHover, true);
				}
				UICamera.Notify(UICamera.mHover, "OnHover", true);
			}
			if (flag)
			{
				UICamera.current = uicamera;
				UICamera.currentCamera = ((uicamera != null) ? uicamera.cachedCamera : null);
				UICamera.currentTouch = null;
				UICamera.currentTouchID = -100;
			}
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x0600071D RID: 1821 RVA: 0x0003A0A0 File Offset: 0x000382A0
	// (set) Token: 0x0600071E RID: 1822 RVA: 0x0003A1E8 File Offset: 0x000383E8
	public static GameObject controllerNavigationObject
	{
		get
		{
			if (UICamera.controller.current && UICamera.controller.current.activeInHierarchy)
			{
				return UICamera.controller.current;
			}
			if (UICamera.currentScheme == UICamera.ControlScheme.Controller && UICamera.current != null && UICamera.current.useController && !UICamera.ignoreControllerInput && UIKeyNavigation.list.size > 0)
			{
				for (int i = 0; i < UIKeyNavigation.list.size; i++)
				{
					UIKeyNavigation uikeyNavigation = UIKeyNavigation.list.buffer[i];
					if (uikeyNavigation && uikeyNavigation.constraint != UIKeyNavigation.Constraint.Explicit && uikeyNavigation.startsSelected)
					{
						UICamera.hoveredObject = uikeyNavigation.gameObject;
						UICamera.controller.current = UICamera.mHover;
						return UICamera.mHover;
					}
				}
				if (UICamera.mHover == null)
				{
					for (int j = 0; j < UIKeyNavigation.list.size; j++)
					{
						UIKeyNavigation uikeyNavigation2 = UIKeyNavigation.list.buffer[j];
						if (uikeyNavigation2 && uikeyNavigation2.constraint != UIKeyNavigation.Constraint.Explicit)
						{
							UICamera.hoveredObject = uikeyNavigation2.gameObject;
							UICamera.controller.current = UICamera.mHover;
							return UICamera.mHover;
						}
					}
				}
			}
			UICamera.controller.current = null;
			return null;
		}
		set
		{
			if (UICamera.controller.current != value && UICamera.controller.current)
			{
				UICamera.Notify(UICamera.controller.current, "OnHover", false);
				if (UICamera.onHover != null)
				{
					UICamera.onHover(UICamera.controller.current, false);
				}
				UICamera.controller.current = null;
			}
			UICamera.hoveredObject = value;
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x0600071F RID: 1823 RVA: 0x0003A25F File Offset: 0x0003845F
	// (set) Token: 0x06000720 RID: 1824 RVA: 0x0003A288 File Offset: 0x00038488
	public static GameObject selectedObject
	{
		get
		{
			if (UICamera.mSelected && UICamera.mSelected.activeInHierarchy)
			{
				return UICamera.mSelected;
			}
			UICamera.mSelected = null;
			return null;
		}
		set
		{
			if (UICamera.mSelected == value)
			{
				UICamera.hoveredObject = value;
				UICamera.controller.current = value;
				return;
			}
			UICamera.ShowTooltip(null);
			bool flag = false;
			UICamera uicamera = UICamera.current;
			if (UICamera.currentTouch == null)
			{
				flag = true;
				UICamera.currentTouchID = -100;
				UICamera.currentTouch = UICamera.controller;
			}
			UICamera.mInputFocus = false;
			if (UICamera.mSelected)
			{
				UICamera.Notify(UICamera.mSelected, "OnSelect", false);
				if (UICamera.onSelect != null)
				{
					UICamera.onSelect(UICamera.mSelected, false);
				}
			}
			UICamera.mSelected = value;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
			if (value != null && value.GetComponent<UIKeyNavigation>() != null)
			{
				UICamera.controller.current = value;
			}
			if (UICamera.mSelected && flag)
			{
				UICamera uicamera2 = (UICamera.mSelected != null) ? UICamera.FindCameraForLayer(UICamera.mSelected.layer) : UICamera.list.buffer[0];
				if (uicamera2 != null)
				{
					UICamera.current = uicamera2;
					UICamera.currentCamera = uicamera2.cachedCamera;
				}
			}
			if (UICamera.mSelected)
			{
				UICamera.mInputFocus = (UICamera.mSelected.activeInHierarchy && UICamera.mSelected.GetComponent<UIInput>() != null);
				if (UICamera.onSelect != null)
				{
					UICamera.onSelect(UICamera.mSelected, true);
				}
				UICamera.Notify(UICamera.mSelected, "OnSelect", true);
			}
			if (flag)
			{
				UICamera.current = uicamera;
				UICamera.currentCamera = ((uicamera != null) ? uicamera.cachedCamera : null);
				UICamera.currentTouch = null;
				UICamera.currentTouchID = -100;
			}
		}
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0003A42C File Offset: 0x0003862C
	public static bool IsPressed(GameObject go)
	{
		for (int i = 0; i < 3; i++)
		{
			if (UICamera.mMouse[i].pressed == go)
			{
				return true;
			}
		}
		int j = 0;
		int count = UICamera.activeTouches.Count;
		while (j < count)
		{
			if (UICamera.activeTouches[j].pressed == go)
			{
				return true;
			}
			j++;
		}
		return UICamera.controller.pressed == go;
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06000722 RID: 1826 RVA: 0x0003A4A1 File Offset: 0x000386A1
	[Obsolete("Use either 'CountInputSources()' or 'activeTouches.Count'")]
	public static int touchCount
	{
		get
		{
			return UICamera.CountInputSources();
		}
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0003A4A8 File Offset: 0x000386A8
	public static int CountInputSources()
	{
		int num = 0;
		int i = 0;
		int count = UICamera.activeTouches.Count;
		while (i < count)
		{
			if (UICamera.activeTouches[i].pressed != null)
			{
				num++;
			}
			i++;
		}
		for (int j = 0; j < UICamera.mMouse.Length; j++)
		{
			if (UICamera.mMouse[j].pressed != null)
			{
				num++;
			}
		}
		if (UICamera.controller.pressed != null)
		{
			num++;
		}
		return num;
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06000724 RID: 1828 RVA: 0x0003A52C File Offset: 0x0003872C
	public static int dragCount
	{
		get
		{
			int num = 0;
			int i = 0;
			int count = UICamera.activeTouches.Count;
			while (i < count)
			{
				if (UICamera.activeTouches[i].dragged != null)
				{
					num++;
				}
				i++;
			}
			for (int j = 0; j < UICamera.mMouse.Length; j++)
			{
				if (UICamera.mMouse[j].dragged != null)
				{
					num++;
				}
			}
			if (UICamera.controller.dragged != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06000725 RID: 1829 RVA: 0x0003A5B0 File Offset: 0x000387B0
	public static Camera mainCamera
	{
		get
		{
			UICamera eventHandler = UICamera.eventHandler;
			if (!(eventHandler != null))
			{
				return null;
			}
			return eventHandler.cachedCamera;
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06000726 RID: 1830 RVA: 0x0003A5D4 File Offset: 0x000387D4
	public static UICamera eventHandler
	{
		get
		{
			for (int i = 0; i < UICamera.list.size; i++)
			{
				UICamera uicamera = UICamera.list.buffer[i];
				if (!(uicamera == null) && uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
				{
					return uicamera;
				}
			}
			return null;
		}
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0003A624 File Offset: 0x00038824
	private static int CompareFunc(UICamera a, UICamera b)
	{
		if (a.cachedCamera.depth < b.cachedCamera.depth)
		{
			return 1;
		}
		if (a.cachedCamera.depth > b.cachedCamera.depth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x0003A65C File Offset: 0x0003885C
	private static Rigidbody FindRootRigidbody(Transform trans)
	{
		while (trans != null && !(trans.GetComponent<UIPanel>() != null))
		{
			Rigidbody component = trans.GetComponent<Rigidbody>();
			if (component != null)
			{
				return component;
			}
			trans = trans.parent;
		}
		return null;
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0003A6A0 File Offset: 0x000388A0
	private static Rigidbody2D FindRootRigidbody2D(Transform trans)
	{
		while (trans != null && !(trans.GetComponent<UIPanel>() != null))
		{
			Rigidbody2D component = trans.GetComponent<Rigidbody2D>();
			if (component != null)
			{
				return component;
			}
			trans = trans.parent;
		}
		return null;
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x0003A6E4 File Offset: 0x000388E4
	public static void Raycast(UICamera.MouseOrTouch touch)
	{
		if (!UICamera.Raycast(touch.pos))
		{
			UICamera.mRayHitObject = UICamera.fallThrough;
		}
		if (UICamera.mRayHitObject == null)
		{
			UICamera.mRayHitObject = UICamera.mGenericHandler;
		}
		touch.last = touch.current;
		touch.current = UICamera.mRayHitObject;
		UICamera.mLastPos = touch.pos;
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x0003A748 File Offset: 0x00038948
	public static bool Raycast(Vector3 inPos)
	{
		for (int i = 0; i < UICamera.list.size; i++)
		{
			UICamera uicamera = UICamera.list.buffer[i];
			if (uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
			{
				UICamera.currentCamera = uicamera.cachedCamera;
				if (UICamera.currentCamera.targetDisplay == 0)
				{
					Vector3 vector = UICamera.currentCamera.ScreenToViewportPoint(inPos);
					if (!float.IsNaN(vector.x) && !float.IsNaN(vector.y) && vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f)
					{
						Ray ray = UICamera.currentCamera.ScreenPointToRay(inPos);
						int layerMask = UICamera.currentCamera.cullingMask & uicamera.eventReceiverMask;
						float num = (uicamera.rangeDistance > 0f) ? uicamera.rangeDistance : (UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane);
						if (uicamera.eventType == UICamera.EventType.World_3D)
						{
							UICamera.lastWorldRay = ray;
							if (Physics.Raycast(ray, out UICamera.lastHit, num, layerMask, QueryTriggerInteraction.Ignore))
							{
								UICamera.lastWorldPosition = UICamera.lastHit.point;
								UICamera.mRayHitObject = UICamera.lastHit.collider.gameObject;
								if (!uicamera.eventsGoToColliders)
								{
									Rigidbody componentInParent = UICamera.mRayHitObject.gameObject.GetComponentInParent<Rigidbody>();
									if (componentInParent != null)
									{
										UICamera.mRayHitObject = componentInParent.gameObject;
									}
								}
								return true;
							}
						}
						else if (uicamera.eventType == UICamera.EventType.UI_3D)
						{
							if (UICamera.mRayHits == null)
							{
								UICamera.mRayHits = new RaycastHit[50];
							}
							int num2 = Physics.RaycastNonAlloc(ray, UICamera.mRayHits, num, layerMask, QueryTriggerInteraction.Collide);
							if (num2 > 1)
							{
								int j = 0;
								while (j < num2)
								{
									GameObject gameObject = UICamera.mRayHits[j].collider.gameObject;
									UIWidget component = gameObject.GetComponent<UIWidget>();
									if (component != null)
									{
										if (component.isVisible && (!(component is UISpriteCollection) || (component as UISpriteCollection).GetCurrentSprite() != null))
										{
											if (component.hitCheck == null || component.hitCheck(UICamera.mRayHits[j].point))
											{
												goto IL_268;
											}
										}
									}
									else
									{
										UIRect uirect = NGUITools.FindInParents<UIRect>(gameObject);
										if (!(uirect != null) || uirect.finalAlpha >= 0.001f)
										{
											goto IL_268;
										}
									}
									IL_2EA:
									j++;
									continue;
									IL_268:
									UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject);
									if (UICamera.mHit.depth != 2147483647)
									{
										UICamera.mHit.hit = UICamera.mRayHits[j];
										UICamera.mHit.point = UICamera.mRayHits[j].point;
										UICamera.mHit.go = UICamera.mRayHits[j].collider.gameObject;
										UICamera.mHits.Add(UICamera.mHit);
										goto IL_2EA;
									}
									goto IL_2EA;
								}
								UICamera.mHits.Sort((UICamera.DepthEntry r1, UICamera.DepthEntry r2) => r2.depth.CompareTo(r1.depth));
								for (int k = 0; k < UICamera.mHits.size; k++)
								{
									if (UICamera.IsVisible(ref UICamera.mHits.buffer[k]))
									{
										UICamera.lastHit = UICamera.mHits.buffer[k].hit;
										UICamera.mRayHitObject = UICamera.mHits.buffer[k].go;
										UICamera.lastWorldRay = ray;
										UICamera.lastWorldPosition = UICamera.mHits.buffer[k].point;
										UICamera.mHits.Clear();
										return true;
									}
								}
								UICamera.mHits.Clear();
							}
							else if (num2 == 1)
							{
								GameObject gameObject2 = UICamera.mRayHits[0].collider.gameObject;
								UIWidget component2 = gameObject2.GetComponent<UIWidget>();
								if (component2 != null)
								{
									if (!component2.isVisible)
									{
										goto IL_796;
									}
									if (component2.hitCheck != null && !component2.hitCheck(UICamera.mRayHits[0].point))
									{
										goto IL_796;
									}
								}
								else
								{
									UIRect uirect2 = NGUITools.FindInParents<UIRect>(gameObject2);
									if (uirect2 != null && uirect2.finalAlpha < 0.001f)
									{
										goto IL_796;
									}
								}
								if (UICamera.IsVisible(UICamera.mRayHits[0].point, UICamera.mRayHits[0].collider.gameObject))
								{
									UICamera.lastHit = UICamera.mRayHits[0];
									UICamera.lastWorldRay = ray;
									UICamera.lastWorldPosition = UICamera.mRayHits[0].point;
									UICamera.mRayHitObject = UICamera.lastHit.collider.gameObject;
									return true;
								}
							}
						}
						else if (uicamera.eventType == UICamera.EventType.World_2D)
						{
							if (UICamera.m2DPlane.Raycast(ray, out num))
							{
								Vector3 point = ray.GetPoint(num);
								Collider2D collider2D = Physics2D.OverlapPoint(point, layerMask);
								if (collider2D)
								{
									UICamera.lastWorldPosition = point;
									UICamera.mRayHitObject = collider2D.gameObject;
									if (!uicamera.eventsGoToColliders)
									{
										Rigidbody2D rigidbody2D = UICamera.FindRootRigidbody2D(UICamera.mRayHitObject.transform);
										if (rigidbody2D != null)
										{
											UICamera.mRayHitObject = rigidbody2D.gameObject;
										}
									}
									return true;
								}
							}
						}
						else if (uicamera.eventType == UICamera.EventType.UI_2D && UICamera.m2DPlane.Raycast(ray, out num))
						{
							UICamera.lastWorldPosition = ray.GetPoint(num);
							if (UICamera.mOverlap == null)
							{
								UICamera.mOverlap = new Collider2D[50];
							}
							int num3 = Physics2D.OverlapPointNonAlloc(UICamera.lastWorldPosition, UICamera.mOverlap, layerMask);
							if (num3 > 1)
							{
								int l = 0;
								while (l < num3)
								{
									GameObject gameObject3 = UICamera.mOverlap[l].gameObject;
									UIWidget component3 = gameObject3.GetComponent<UIWidget>();
									if (component3 != null)
									{
										if (component3.isVisible)
										{
											if (component3.hitCheck == null || component3.hitCheck(UICamera.lastWorldPosition))
											{
												goto IL_623;
											}
										}
									}
									else
									{
										UIRect uirect3 = NGUITools.FindInParents<UIRect>(gameObject3);
										if (!(uirect3 != null) || uirect3.finalAlpha >= 0.001f)
										{
											goto IL_623;
										}
									}
									IL_66F:
									l++;
									continue;
									IL_623:
									UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject3);
									if (UICamera.mHit.depth != 2147483647)
									{
										UICamera.mHit.go = gameObject3;
										UICamera.mHit.point = UICamera.lastWorldPosition;
										UICamera.mHits.Add(UICamera.mHit);
										goto IL_66F;
									}
									goto IL_66F;
								}
								UICamera.mHits.Sort((UICamera.DepthEntry r1, UICamera.DepthEntry r2) => r2.depth.CompareTo(r1.depth));
								for (int m = 0; m < UICamera.mHits.size; m++)
								{
									if (UICamera.IsVisible(ref UICamera.mHits.buffer[m]))
									{
										UICamera.mRayHitObject = UICamera.mHits.buffer[m].go;
										UICamera.mHits.Clear();
										return true;
									}
								}
								UICamera.mHits.Clear();
							}
							else if (num3 == 1)
							{
								GameObject gameObject4 = UICamera.mOverlap[0].gameObject;
								UIWidget component4 = gameObject4.GetComponent<UIWidget>();
								if (component4 != null)
								{
									if (!component4.isVisible)
									{
										goto IL_796;
									}
									if (component4.hitCheck != null && !component4.hitCheck(UICamera.lastWorldPosition))
									{
										goto IL_796;
									}
								}
								else
								{
									UIRect uirect4 = NGUITools.FindInParents<UIRect>(gameObject4);
									if (uirect4 != null && uirect4.finalAlpha < 0.001f)
									{
										goto IL_796;
									}
								}
								if (UICamera.IsVisible(UICamera.lastWorldPosition, gameObject4))
								{
									UICamera.mRayHitObject = gameObject4;
									return true;
								}
							}
						}
					}
				}
			}
			IL_796:;
		}
		return false;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0003AF00 File Offset: 0x00039100
	private static bool IsVisible(Vector3 worldPoint, GameObject go)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(go);
		while (uipanel != null)
		{
			if (!uipanel.IsVisible(worldPoint))
			{
				return false;
			}
			uipanel = uipanel.parentPanel;
		}
		return true;
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0003AF34 File Offset: 0x00039134
	private static bool IsVisible(ref UICamera.DepthEntry de)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(de.go);
		while (uipanel != null)
		{
			if (!uipanel.IsVisible(de.point))
			{
				return false;
			}
			uipanel = uipanel.parentPanel;
		}
		return true;
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0003AF70 File Offset: 0x00039170
	public static bool IsHighlighted(GameObject go)
	{
		return UICamera.hoveredObject == go;
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x0003AF80 File Offset: 0x00039180
	public static UICamera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		for (int i = 0; i < UICamera.list.size; i++)
		{
			UICamera uicamera = UICamera.list.buffer[i];
			Camera cachedCamera = uicamera.cachedCamera;
			if (cachedCamera != null && (cachedCamera.cullingMask & num) != 0)
			{
				return uicamera;
			}
		}
		return null;
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x0003AFD3 File Offset: 0x000391D3
	private static int GetDirection(KeyCode up, KeyCode down)
	{
		if (UICamera.GetKeyDown(up))
		{
			UICamera.currentKey = up;
			return 1;
		}
		if (UICamera.GetKeyDown(down))
		{
			UICamera.currentKey = down;
			return -1;
		}
		return 0;
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0003B000 File Offset: 0x00039200
	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
		if (UICamera.GetKeyDown(up0))
		{
			UICamera.currentKey = up0;
			return 1;
		}
		if (UICamera.GetKeyDown(up1))
		{
			UICamera.currentKey = up1;
			return 1;
		}
		if (UICamera.GetKeyDown(down0))
		{
			UICamera.currentKey = down0;
			return -1;
		}
		if (UICamera.GetKeyDown(down1))
		{
			UICamera.currentKey = down1;
			return -1;
		}
		return 0;
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0003B064 File Offset: 0x00039264
	private static int GetDirection(string axis)
	{
		float time = RealTime.time;
		if (UICamera.mNextEvent < time && !string.IsNullOrEmpty(axis))
		{
			float num = UICamera.GetAxis(axis);
			if (num > 0.75f)
			{
				UICamera.currentKey = KeyCode.JoystickButton0;
				UICamera.mNextEvent = time + 0.25f;
				return 1;
			}
			if (num < -0.75f)
			{
				UICamera.currentKey = KeyCode.JoystickButton0;
				UICamera.mNextEvent = time + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0003B0D4 File Offset: 0x000392D4
	public static void Notify(GameObject go, string funcName, object obj)
	{
		if (UICamera.mNotifying > 10)
		{
			return;
		}
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller && UIPopupList.isOpen && UIPopupList.current.source == go && UIPopupList.isOpen)
		{
			go = UIPopupList.current.gameObject;
		}
		if (go && go.activeInHierarchy)
		{
			UICamera.mNotifying++;
			go.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
			if (UICamera.mGenericHandler != null && UICamera.mGenericHandler != go)
			{
				UICamera.mGenericHandler.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
			}
			UICamera.mNotifying--;
		}
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0003B178 File Offset: 0x00039378
	private void Awake()
	{
		UICamera.mWidth = Screen.width;
		UICamera.mHeight = Screen.height;
		if (Application.platform == RuntimePlatform.PS4 || Application.platform == RuntimePlatform.XboxOne)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
		}
		UICamera.mMouse[0].pos = Input.mousePosition;
		for (int i = 1; i < 3; i++)
		{
			UICamera.mMouse[i].pos = UICamera.mMouse[0].pos;
			UICamera.mMouse[i].lastPos = UICamera.mMouse[0].pos;
		}
		UICamera.mLastPos = UICamera.mMouse[0].pos;
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		if (commandLineArgs != null)
		{
			foreach (string a in commandLineArgs)
			{
				if (a == "-noMouse")
				{
					this.useMouse = false;
				}
				else if (a == "-noTouch")
				{
					this.useTouch = false;
				}
				else if (a == "-noController")
				{
					this.useController = false;
					UICamera.ignoreControllerInput = true;
				}
				else if (a == "-noJoystick")
				{
					this.useController = false;
					UICamera.ignoreControllerInput = true;
				}
				else if (a == "-useMouse")
				{
					this.useMouse = true;
				}
				else if (a == "-useTouch")
				{
					this.useTouch = true;
				}
				else if (a == "-useController")
				{
					this.useController = true;
				}
				else if (a == "-useJoystick")
				{
					this.useController = true;
				}
			}
		}
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0003B2F8 File Offset: 0x000394F8
	private void OnEnable()
	{
		UICamera.list.Add(this);
		UICamera.list.Sort(new BetterList<UICamera>.CompareFunc(UICamera.CompareFunc));
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0003B31B File Offset: 0x0003951B
	private void OnDisable()
	{
		UICamera.list.Remove(this);
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0003B32C File Offset: 0x0003952C
	private void Start()
	{
		UICamera.list.Sort(new BetterList<UICamera>.CompareFunc(UICamera.CompareFunc));
		if (this.eventType != UICamera.EventType.World_3D && this.cachedCamera.transparencySortMode != TransparencySortMode.Orthographic)
		{
			this.cachedCamera.transparencySortMode = TransparencySortMode.Orthographic;
		}
		if (Application.isPlaying)
		{
			if (UICamera.fallThrough == null)
			{
				UIRoot uiroot = NGUITools.FindInParents<UIRoot>(base.gameObject);
				UICamera.fallThrough = ((uiroot != null) ? uiroot.gameObject : base.gameObject);
			}
			this.cachedCamera.eventMask = 0;
			if (!UICamera.ignoreControllerInput && UICamera.disableControllerCheck && this.useController && this.handlesEvents)
			{
				UICamera.disableControllerCheck = false;
				if (!string.IsNullOrEmpty(this.horizontalAxisName) && Mathf.Abs(UICamera.GetAxis(this.horizontalAxisName)) > 0.1f)
				{
					UICamera.ignoreControllerInput = true;
					return;
				}
				if (!string.IsNullOrEmpty(this.verticalAxisName) && Mathf.Abs(UICamera.GetAxis(this.verticalAxisName)) > 0.1f)
				{
					UICamera.ignoreControllerInput = true;
					return;
				}
				if (!string.IsNullOrEmpty(this.horizontalPanAxisName) && Mathf.Abs(UICamera.GetAxis(this.horizontalPanAxisName)) > 0.1f)
				{
					UICamera.ignoreControllerInput = true;
					return;
				}
				if (!string.IsNullOrEmpty(this.verticalPanAxisName) && Mathf.Abs(UICamera.GetAxis(this.verticalPanAxisName)) > 0.1f)
				{
					UICamera.ignoreControllerInput = true;
				}
			}
		}
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0003B4AB File Offset: 0x000396AB
	[ContextMenu("Start ignoring events")]
	private void StartIgnoring()
	{
		UICamera.ignoreAllEvents = true;
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0003B4B3 File Offset: 0x000396B3
	[ContextMenu("Stop ignoring events")]
	private void StopIgnoring()
	{
		UICamera.ignoreAllEvents = false;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0003B4BB File Offset: 0x000396BB
	private void Update()
	{
		if (UICamera.ignoreAllEvents)
		{
			return;
		}
		if (!this.handlesEvents)
		{
			return;
		}
		if (this.processEventsIn == UICamera.ProcessEventsIn.Update)
		{
			this.ProcessEvents();
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0003B4DC File Offset: 0x000396DC
	private void LateUpdate()
	{
		if (!this.handlesEvents)
		{
			return;
		}
		if (this.processEventsIn == UICamera.ProcessEventsIn.LateUpdate)
		{
			this.ProcessEvents();
		}
		int width = Screen.width;
		int height = Screen.height;
		if (width != UICamera.mWidth || height != UICamera.mHeight)
		{
			UICamera.mWidth = width;
			UICamera.mHeight = height;
			UIRoot.Broadcast("UpdateAnchors");
			if (UICamera.onScreenResize != null)
			{
				UICamera.onScreenResize();
			}
		}
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0003B544 File Offset: 0x00039744
	private void ProcessEvents()
	{
		UICamera.current = this;
		NGUIDebug.debugRaycast = this.debug;
		if (this.useTouch)
		{
			this.ProcessTouches();
		}
		else if (this.useMouse)
		{
			this.ProcessMouse();
		}
		if (UICamera.onCustomInput != null)
		{
			UICamera.onCustomInput();
		}
		if ((this.useKeyboard || this.useController) && !UICamera.disableController && !UICamera.ignoreControllerInput)
		{
			this.ProcessOthers();
		}
		if (this.useMouse && UICamera.mHover != null)
		{
			float num = (!string.IsNullOrEmpty(this.scrollAxisName)) ? UICamera.GetAxis(this.scrollAxisName) : 0f;
			if (num != 0f)
			{
				if (UICamera.onScroll != null)
				{
					UICamera.onScroll(UICamera.mHover, num);
				}
				UICamera.Notify(UICamera.mHover, "OnScroll", num);
			}
			if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.showTooltips && UICamera.mTooltipTime != 0f && !UIPopupList.isOpen && UICamera.mMouse[0].dragged == null && (UICamera.mTooltipTime < Time.unscaledTime || UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift)))
			{
				UICamera.currentTouch = UICamera.mMouse[0];
				UICamera.currentTouchID = -1;
				UICamera.ShowTooltip(UICamera.mHover);
			}
		}
		if (UICamera.mTooltip != null && !NGUITools.GetActive(UICamera.mTooltip))
		{
			UICamera.ShowTooltip(null);
		}
		UICamera.current = null;
		UICamera.currentTouchID = -100;
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0003B6D8 File Offset: 0x000398D8
	public void ProcessMouse()
	{
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < 3; i++)
		{
			if (Input.GetMouseButtonDown(i))
			{
				UICamera.currentKey = KeyCode.Mouse0 + i;
				flag2 = true;
				flag = true;
			}
			else if (Input.GetMouseButton(i))
			{
				UICamera.currentKey = KeyCode.Mouse0 + i;
				flag = true;
			}
		}
		if (UICamera.currentScheme == UICamera.ControlScheme.Touch && UICamera.activeTouches.Count > 0)
		{
			return;
		}
		UICamera.currentTouch = UICamera.mMouse[0];
		Vector2 vector = Input.mousePosition;
		if (UICamera.currentTouch.ignoreDelta == 0)
		{
			UICamera.currentTouch.delta = vector - UICamera.currentTouch.pos;
		}
		else
		{
			UICamera.currentTouch.ignoreDelta--;
			UICamera.currentTouch.delta.x = 0f;
			UICamera.currentTouch.delta.y = 0f;
		}
		float sqrMagnitude = UICamera.currentTouch.delta.sqrMagnitude;
		UICamera.currentTouch.pos = vector;
		UICamera.mLastPos = vector;
		bool flag3 = false;
		if (UICamera.currentScheme != UICamera.ControlScheme.Mouse)
		{
			if (sqrMagnitude < 0.001f)
			{
				return;
			}
			UICamera.currentKey = KeyCode.Mouse0;
			flag3 = true;
		}
		else if (sqrMagnitude > 0.001f)
		{
			flag3 = true;
		}
		for (int j = 1; j < 3; j++)
		{
			UICamera.mMouse[j].pos = UICamera.currentTouch.pos;
			UICamera.mMouse[j].delta = UICamera.currentTouch.delta;
		}
		if (flag || flag3 || this.mNextRaycast < RealTime.time)
		{
			this.mNextRaycast = RealTime.time + 0.02f;
			UICamera.Raycast(UICamera.currentTouch);
			if (flag)
			{
				flag3 = true;
				for (int k = 1; k < 3; k++)
				{
					UICamera.mMouse[k].current = UICamera.currentTouch.current;
				}
			}
			else if (UICamera.mMouse[0].current != UICamera.currentTouch.current)
			{
				UICamera.currentKey = KeyCode.Mouse0;
				flag3 = true;
				for (int l = 1; l < 3; l++)
				{
					UICamera.mMouse[l].current = UICamera.currentTouch.current;
				}
			}
		}
		bool flag4 = UICamera.currentTouch.last != UICamera.currentTouch.current;
		bool flag5 = UICamera.currentTouch.pressed != null;
		if (!flag5 && flag3)
		{
			UICamera.hoveredObject = UICamera.currentTouch.current;
		}
		UICamera.currentTouchID = -1;
		if (flag4)
		{
			UICamera.currentKey = KeyCode.Mouse0;
		}
		if (!flag && flag3)
		{
			if (UICamera.mTooltipTime != 0f)
			{
				UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
			}
			else if (UICamera.mTooltip != null && (!this.stickyTooltip || flag4))
			{
				UICamera.ShowTooltip(null);
			}
		}
		if (flag3 && UICamera.onMouseMove != null)
		{
			UICamera.onMouseMove(UICamera.currentTouch.delta);
			UICamera.currentTouch = null;
		}
		if (flag4 && (flag2 || (flag5 && !flag)))
		{
			UICamera.hoveredObject = null;
		}
		for (int m = 0; m < 3; m++)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(m);
			bool mouseButtonUp = Input.GetMouseButtonUp(m);
			if (mouseButtonDown || mouseButtonUp)
			{
				UICamera.currentKey = KeyCode.Mouse0 + m;
			}
			UICamera.currentTouch = UICamera.mMouse[m];
			UICamera.currentTouchID = -1 - m;
			UICamera.currentKey = KeyCode.Mouse0 + m;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
				UICamera.currentTouch.pressTime = RealTime.time;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
		}
		if (!flag && flag4)
		{
			UICamera.currentTouch = UICamera.mMouse[0];
			UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
			UICamera.currentTouchID = -1;
			UICamera.currentKey = KeyCode.Mouse0;
			UICamera.hoveredObject = UICamera.currentTouch.current;
		}
		UICamera.currentTouch = null;
		UICamera.mMouse[0].last = UICamera.mMouse[0].current;
		for (int n = 1; n < 3; n++)
		{
			UICamera.mMouse[n].last = UICamera.mMouse[0].last;
		}
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x0003BB10 File Offset: 0x00039D10
	public void ProcessTouches()
	{
		int num = (UICamera.GetInputTouchCount == null) ? Input.touchCount : UICamera.GetInputTouchCount();
		for (int i = 0; i < num; i++)
		{
			TouchPhase phase;
			int fingerId;
			Vector2 position;
			int tapCount;
			if (UICamera.GetInputTouch == null)
			{
				UnityEngine.Touch touch = Input.GetTouch(i);
				phase = touch.phase;
				fingerId = touch.fingerId;
				position = touch.position;
				tapCount = touch.tapCount;
			}
			else
			{
				UICamera.Touch touch2 = UICamera.GetInputTouch(i);
				phase = touch2.phase;
				fingerId = touch2.fingerId;
				position = touch2.position;
				tapCount = touch2.tapCount;
			}
			UICamera.currentTouchID = (this.allowMultiTouch ? fingerId : 1);
			UICamera.currentTouch = UICamera.GetTouch(UICamera.currentTouchID, true);
			bool flag = phase == TouchPhase.Began || UICamera.currentTouch.touchBegan;
			bool flag2 = phase == TouchPhase.Canceled || phase == TouchPhase.Ended;
			UICamera.currentTouch.delta = position - UICamera.currentTouch.pos;
			UICamera.currentTouch.pos = position;
			UICamera.currentKey = KeyCode.None;
			UICamera.Raycast(UICamera.currentTouch);
			if (flag)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			if (tapCount > 1)
			{
				UICamera.currentTouch.clickTime = RealTime.time;
			}
			this.ProcessTouch(flag, flag2);
			if (flag2)
			{
				UICamera.RemoveTouch(UICamera.currentTouchID);
			}
			UICamera.currentTouch.touchBegan = false;
			UICamera.currentTouch.last = null;
			UICamera.currentTouch = null;
			if (!this.allowMultiTouch)
			{
				break;
			}
		}
		if (num == 0)
		{
			if (UICamera.mUsingTouchEvents)
			{
				UICamera.mUsingTouchEvents = false;
				return;
			}
			if (this.useMouse)
			{
				this.ProcessMouse();
				return;
			}
		}
		else
		{
			UICamera.mUsingTouchEvents = true;
		}
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x0003BCD0 File Offset: 0x00039ED0
	private void ProcessFakeTouches()
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		bool mouseButton = Input.GetMouseButton(0);
		if (mouseButtonDown || mouseButtonUp || mouseButton)
		{
			UICamera.currentTouchID = 1;
			UICamera.currentTouch = UICamera.mMouse[0];
			UICamera.currentTouch.touchBegan = mouseButtonDown;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressTime = RealTime.time;
				UICamera.activeTouches.Add(UICamera.currentTouch);
			}
			Vector2 vector = Input.mousePosition;
			UICamera.currentTouch.delta = vector - UICamera.currentTouch.pos;
			UICamera.currentTouch.pos = vector;
			UICamera.Raycast(UICamera.currentTouch);
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			UICamera.currentKey = KeyCode.None;
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
			if (mouseButtonUp)
			{
				UICamera.activeTouches.Remove(UICamera.currentTouch);
			}
			UICamera.currentTouch.last = null;
			UICamera.currentTouch = null;
		}
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0003BDDC File Offset: 0x00039FDC
	public void ProcessOthers()
	{
		UICamera.currentTouchID = -100;
		UICamera.currentTouch = UICamera.controller;
		bool flag = false;
		bool flag2 = false;
		if (this.submitKey0 != KeyCode.None && UICamera.GetKeyDown(this.submitKey0))
		{
			UICamera.currentKey = this.submitKey0;
			flag = true;
		}
		else if (this.submitKey1 != KeyCode.None && UICamera.GetKeyDown(this.submitKey1))
		{
			UICamera.currentKey = this.submitKey1;
			flag = true;
		}
		else if ((this.submitKey0 == KeyCode.Return || this.submitKey1 == KeyCode.Return) && UICamera.GetKeyDown(KeyCode.KeypadEnter))
		{
			UICamera.currentKey = this.submitKey0;
			flag = true;
		}
		if (this.submitKey0 != KeyCode.None && UICamera.GetKeyUp(this.submitKey0))
		{
			UICamera.currentKey = this.submitKey0;
			flag2 = true;
		}
		else if (this.submitKey1 != KeyCode.None && UICamera.GetKeyUp(this.submitKey1))
		{
			UICamera.currentKey = this.submitKey1;
			flag2 = true;
		}
		else if ((this.submitKey0 == KeyCode.Return || this.submitKey1 == KeyCode.Return) && UICamera.GetKeyUp(KeyCode.KeypadEnter))
		{
			UICamera.currentKey = this.submitKey0;
			flag2 = true;
		}
		if (flag)
		{
			UICamera.currentTouch.pressTime = RealTime.time;
		}
		if ((flag || flag2) && UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			UICamera.currentTouch.current = UICamera.controllerNavigationObject;
			this.ProcessTouch(flag, flag2);
			UICamera.currentTouch.last = UICamera.currentTouch.current;
		}
		KeyCode keyCode = KeyCode.None;
		if (this.useController && !UICamera.ignoreControllerInput)
		{
			if (!UICamera.disableController && UICamera.currentScheme == UICamera.ControlScheme.Controller && (UICamera.currentTouch.current == null || !UICamera.currentTouch.current.activeInHierarchy))
			{
				UICamera.currentTouch.current = UICamera.controllerNavigationObject;
			}
			if (!string.IsNullOrEmpty(this.verticalAxisName))
			{
				int direction = UICamera.GetDirection(this.verticalAxisName);
				if (direction != 0)
				{
					UICamera.ShowTooltip(null);
					UICamera.currentScheme = UICamera.ControlScheme.Controller;
					UICamera.currentTouch.current = UICamera.controllerNavigationObject;
					if (UICamera.currentTouch.current != null)
					{
						keyCode = ((direction > 0) ? KeyCode.UpArrow : KeyCode.DownArrow);
						if (UICamera.onNavigate != null)
						{
							UICamera.onNavigate(UICamera.currentTouch.current, keyCode);
						}
						UICamera.Notify(UICamera.currentTouch.current, "OnNavigate", keyCode);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.horizontalAxisName))
			{
				int direction2 = UICamera.GetDirection(this.horizontalAxisName);
				if (direction2 != 0)
				{
					UICamera.ShowTooltip(null);
					UICamera.currentScheme = UICamera.ControlScheme.Controller;
					UICamera.currentTouch.current = UICamera.controllerNavigationObject;
					if (UICamera.currentTouch.current != null)
					{
						keyCode = ((direction2 > 0) ? KeyCode.RightArrow : KeyCode.LeftArrow);
						if (UICamera.onNavigate != null)
						{
							UICamera.onNavigate(UICamera.currentTouch.current, keyCode);
						}
						UICamera.Notify(UICamera.currentTouch.current, "OnNavigate", keyCode);
					}
				}
			}
			float num = (!string.IsNullOrEmpty(this.horizontalPanAxisName)) ? UICamera.GetAxis(this.horizontalPanAxisName) : 0f;
			float num2 = (!string.IsNullOrEmpty(this.verticalPanAxisName)) ? UICamera.GetAxis(this.verticalPanAxisName) : 0f;
			if (num != 0f || num2 != 0f)
			{
				UICamera.ShowTooltip(null);
				UICamera.currentScheme = UICamera.ControlScheme.Controller;
				UICamera.currentTouch.current = UICamera.controllerNavigationObject;
				if (UICamera.currentTouch.current != null)
				{
					Vector2 vector = new Vector2(num, num2);
					vector *= Time.unscaledDeltaTime;
					if (UICamera.onPan != null)
					{
						UICamera.onPan(UICamera.currentTouch.current, vector);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnPan", vector);
				}
			}
		}
		if ((UICamera.GetAnyKeyDown != null) ? UICamera.GetAnyKeyDown() : Input.anyKeyDown)
		{
			int i = 0;
			int num3 = NGUITools.keys.Length;
			while (i < num3)
			{
				KeyCode keyCode2 = NGUITools.keys[i];
				if (keyCode != keyCode2 && UICamera.GetKeyDown(keyCode2) && (this.useKeyboard || keyCode2 >= KeyCode.Mouse0) && ((this.useController && !UICamera.ignoreControllerInput) || keyCode2 < KeyCode.JoystickButton0) && (this.useMouse || keyCode2 < KeyCode.Mouse0 || keyCode2 > KeyCode.Mouse6))
				{
					UICamera.currentKey = keyCode2;
					if (UICamera.onKey != null)
					{
						UICamera.onKey(UICamera.currentTouch.current, keyCode2);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnKey", keyCode2);
				}
				i++;
			}
		}
		UICamera.currentTouch = null;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0003C298 File Offset: 0x0003A498
	private void ProcessPress(bool pressed, float click, float drag)
	{
		if (pressed)
		{
			if (UICamera.mTooltip != null)
			{
				UICamera.ShowTooltip(null);
			}
			UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
			UICamera.currentTouch.pressStarted = true;
			if (UICamera.onPress != null && UICamera.currentTouch.pressed)
			{
				UICamera.onPress(UICamera.currentTouch.pressed, false);
			}
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
			if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == null && UICamera.currentTouch.current != null)
			{
				UICamera.hoveredObject = UICamera.currentTouch.current;
			}
			UICamera.currentTouch.pressed = UICamera.currentTouch.current;
			UICamera.currentTouch.dragged = UICamera.currentTouch.current;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			UICamera.currentTouch.totalDelta = Vector2.zero;
			UICamera.currentTouch.dragStarted = false;
			if (UICamera.onPress != null && UICamera.currentTouch.pressed)
			{
				UICamera.onPress(UICamera.currentTouch.pressed, true);
			}
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", true);
			if (UICamera.mSelected != UICamera.currentTouch.pressed)
			{
				UICamera.mInputFocus = false;
				if (UICamera.mSelected)
				{
					UICamera.Notify(UICamera.mSelected, "OnSelect", false);
					if (UICamera.onSelect != null)
					{
						UICamera.onSelect(UICamera.mSelected, false);
					}
				}
				UICamera.mSelected = UICamera.currentTouch.pressed;
				if (UICamera.currentTouch.pressed != null && UICamera.currentTouch.pressed.GetComponent<UIKeyNavigation>() != null)
				{
					UICamera.controller.current = UICamera.currentTouch.pressed;
				}
				if (UICamera.mSelected)
				{
					UICamera.mInputFocus = (UICamera.mSelected.activeInHierarchy && UICamera.mSelected.GetComponent<UIInput>() != null);
					if (UICamera.onSelect != null)
					{
						UICamera.onSelect(UICamera.mSelected, true);
					}
					UICamera.Notify(UICamera.mSelected, "OnSelect", true);
					return;
				}
			}
		}
		else if (UICamera.currentTouch.pressed != null && (UICamera.currentTouch.delta.sqrMagnitude != 0f || UICamera.currentTouch.current != UICamera.currentTouch.last))
		{
			UICamera.currentTouch.totalDelta += UICamera.currentTouch.delta;
			float sqrMagnitude = UICamera.currentTouch.totalDelta.sqrMagnitude;
			bool flag = false;
			if (!UICamera.currentTouch.dragStarted && UICamera.currentTouch.last != UICamera.currentTouch.current)
			{
				UICamera.currentTouch.dragStarted = true;
				UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
				UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				UICamera.isDragging = true;
				if (UICamera.onDragStart != null)
				{
					UICamera.onDragStart(UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", null);
				if (UICamera.onDragOver != null)
				{
					UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.last, "OnDragOver", UICamera.currentTouch.dragged);
				UICamera.isDragging = false;
			}
			else if (!UICamera.currentTouch.dragStarted && drag < sqrMagnitude)
			{
				flag = true;
				UICamera.currentTouch.dragStarted = true;
				UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
			}
			if (UICamera.currentTouch.dragStarted)
			{
				if (UICamera.mTooltip != null)
				{
					UICamera.ShowTooltip(null);
				}
				UICamera.isDragging = true;
				bool flag2 = UICamera.currentTouch.clickNotification == UICamera.ClickNotification.None;
				if (flag)
				{
					if (UICamera.onDragStart != null)
					{
						UICamera.onDragStart(UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", null);
					if (UICamera.onDragOver != null)
					{
						UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", UICamera.currentTouch.dragged);
				}
				else if (UICamera.currentTouch.last != UICamera.currentTouch.current)
				{
					if (UICamera.onDragOut != null)
					{
						UICamera.onDragOut(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", UICamera.currentTouch.dragged);
					if (UICamera.onDragOver != null)
					{
						UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", UICamera.currentTouch.dragged);
				}
				if (UICamera.onDrag != null)
				{
					UICamera.onDrag(UICamera.currentTouch.dragged, UICamera.currentTouch.delta);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDrag", UICamera.currentTouch.delta);
				UICamera.currentTouch.last = UICamera.currentTouch.current;
				UICamera.isDragging = false;
				if (flag2)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
					return;
				}
				if (UICamera.currentTouch.clickNotification == UICamera.ClickNotification.BasedOnDelta && click < sqrMagnitude)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				}
			}
		}
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0003C854 File Offset: 0x0003AA54
	private void ProcessRelease(bool isMouse, float drag)
	{
		if (UICamera.currentTouch == null)
		{
			return;
		}
		UICamera.currentTouch.pressStarted = false;
		if (UICamera.currentTouch.pressed != null)
		{
			if (UICamera.currentTouch.dragStarted)
			{
				if (UICamera.onDragOut != null)
				{
					UICamera.onDragOut(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", UICamera.currentTouch.dragged);
				if (UICamera.onDragEnd != null)
				{
					UICamera.onDragEnd(UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDragEnd", null);
			}
			if (UICamera.onPress != null)
			{
				UICamera.onPress(UICamera.currentTouch.pressed, false);
			}
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
			if (isMouse && this.HasCollider(UICamera.currentTouch.pressed))
			{
				if (UICamera.mHover == UICamera.currentTouch.current)
				{
					if (UICamera.onHover != null)
					{
						UICamera.onHover(UICamera.currentTouch.current, true);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnHover", true);
				}
				else
				{
					UICamera.hoveredObject = UICamera.currentTouch.current;
				}
			}
			if (UICamera.currentTouch.dragged == UICamera.currentTouch.current || (UICamera.currentScheme != UICamera.ControlScheme.Controller && UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.totalDelta.sqrMagnitude < drag))
			{
				if (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.pressed == UICamera.currentTouch.current)
				{
					UICamera.ShowTooltip(null);
					float time = RealTime.time;
					if (UICamera.onClick != null)
					{
						UICamera.onClick(UICamera.currentTouch.pressed);
					}
					UICamera.Notify(UICamera.currentTouch.pressed, "OnClick", null);
					if (UICamera.currentTouch.clickTime + 0.35f > time && UICamera.currentTouch.lastClickGO == UICamera.currentTouch.pressed)
					{
						if (UICamera.onDoubleClick != null)
						{
							UICamera.onDoubleClick(UICamera.currentTouch.pressed);
						}
						UICamera.Notify(UICamera.currentTouch.pressed, "OnDoubleClick", null);
					}
					UICamera.currentTouch.lastClickGO = UICamera.currentTouch.pressed;
					UICamera.currentTouch.clickTime = time;
				}
			}
			else if (UICamera.currentTouch.dragStarted)
			{
				if (UICamera.onDrop != null)
				{
					UICamera.onDrop(UICamera.currentTouch.current, UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.current, "OnDrop", UICamera.currentTouch.dragged);
			}
		}
		UICamera.currentTouch.dragStarted = false;
		UICamera.currentTouch.pressed = null;
		UICamera.currentTouch.dragged = null;
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0003CB54 File Offset: 0x0003AD54
	private bool HasCollider(GameObject go)
	{
		if (go == null)
		{
			return false;
		}
		Collider component = go.GetComponent<Collider>();
		if (component != null)
		{
			return component.enabled;
		}
		Collider2D component2 = go.GetComponent<Collider2D>();
		return component2 != null && component2.enabled;
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0003CB9C File Offset: 0x0003AD9C
	public void ProcessTouch(bool pressed, bool released)
	{
		if (released)
		{
			UICamera.mTooltipTime = 0f;
		}
		bool flag = UICamera.currentScheme == UICamera.ControlScheme.Mouse;
		float num = flag ? this.mouseDragThreshold : this.touchDragThreshold;
		float num2 = flag ? this.mouseClickThreshold : this.touchClickThreshold;
		num *= num;
		num2 *= num2;
		if (UICamera.currentTouch.pressed != null)
		{
			if (released)
			{
				this.ProcessRelease(flag, num);
			}
			this.ProcessPress(pressed, num2, num);
			if (this.tooltipDelay != 0f && UICamera.currentTouch.deltaTime > this.tooltipDelay && UICamera.currentTouch.pressed == UICamera.currentTouch.current && UICamera.mTooltipTime != 0f && !UICamera.currentTouch.dragStarted)
			{
				UICamera.mTooltipTime = 0f;
				UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				if (this.longPressTooltip)
				{
					UICamera.ShowTooltip(UICamera.currentTouch.pressed);
				}
				UICamera.Notify(UICamera.currentTouch.current, "OnLongPress", null);
				return;
			}
		}
		else if (flag || pressed || released)
		{
			this.ProcessPress(pressed, num2, num);
			if (released)
			{
				this.ProcessRelease(flag, num);
			}
		}
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0003CCCA File Offset: 0x0003AECA
	public static void CancelNextTooltip()
	{
		UICamera.mTooltipTime = 0f;
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0003CCD8 File Offset: 0x0003AED8
	public static bool ShowTooltip(GameObject go)
	{
		if (UICamera.mTooltip != go)
		{
			if (UICamera.mTooltip != null)
			{
				if (UICamera.onTooltip != null)
				{
					UICamera.onTooltip(UICamera.mTooltip, false);
				}
				UICamera.Notify(UICamera.mTooltip, "OnTooltip", false);
			}
			UICamera.mTooltip = go;
			UICamera.mTooltipTime = 0f;
			if (UICamera.mTooltip != null)
			{
				if (UICamera.onTooltip != null)
				{
					UICamera.onTooltip(UICamera.mTooltip, true);
				}
				UICamera.Notify(UICamera.mTooltip, "OnTooltip", true);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x0003CD7A File Offset: 0x0003AF7A
	public static bool HideTooltip()
	{
		return UICamera.ShowTooltip(null);
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x0003CD82 File Offset: 0x0003AF82
	public static void ResetTooltip(float delay = 0.5f)
	{
		UICamera.ShowTooltip(null);
		UICamera.mTooltipTime = Time.unscaledTime + delay;
	}

	// Token: 0x0400064B RID: 1611
	public static BetterList<UICamera> list = new BetterList<UICamera>();

	// Token: 0x0400064C RID: 1612
	public static UICamera.GetKeyStateFunc GetKeyDown = (KeyCode key) => (key < KeyCode.JoystickButton0 || !UICamera.ignoreControllerInput) && Input.GetKeyDown(key);

	// Token: 0x0400064D RID: 1613
	public static UICamera.GetKeyStateFunc GetKeyUp = (KeyCode key) => (key < KeyCode.JoystickButton0 || !UICamera.ignoreControllerInput) && Input.GetKeyUp(key);

	// Token: 0x0400064E RID: 1614
	public static UICamera.GetKeyStateFunc GetKey = (KeyCode key) => (key < KeyCode.JoystickButton0 || !UICamera.ignoreControllerInput) && Input.GetKey(key);

	// Token: 0x0400064F RID: 1615
	public static UICamera.GetAxisFunc GetAxis = delegate(string axis)
	{
		if (UICamera.ignoreControllerInput)
		{
			return 0f;
		}
		return Input.GetAxis(axis);
	};

	// Token: 0x04000650 RID: 1616
	public static UICamera.GetAnyKeyFunc GetAnyKeyDown;

	// Token: 0x04000651 RID: 1617
	public static UICamera.GetMouseDelegate GetMouse = (int button) => UICamera.mMouse[button];

	// Token: 0x04000652 RID: 1618
	public static UICamera.GetTouchDelegate GetTouch = delegate(int id, bool createIfMissing)
	{
		if (id < 0)
		{
			return UICamera.GetMouse(-id - 1);
		}
		int i = 0;
		int count = UICamera.mTouchIDs.Count;
		while (i < count)
		{
			if (UICamera.mTouchIDs[i] == id)
			{
				return UICamera.activeTouches[i];
			}
			i++;
		}
		if (createIfMissing)
		{
			UICamera.MouseOrTouch mouseOrTouch = new UICamera.MouseOrTouch();
			mouseOrTouch.pressTime = RealTime.time;
			mouseOrTouch.touchBegan = true;
			UICamera.activeTouches.Add(mouseOrTouch);
			UICamera.mTouchIDs.Add(id);
			return mouseOrTouch;
		}
		return null;
	};

	// Token: 0x04000653 RID: 1619
	public static UICamera.RemoveTouchDelegate RemoveTouch = delegate(int id)
	{
		int i = 0;
		int count = UICamera.mTouchIDs.Count;
		while (i < count)
		{
			if (UICamera.mTouchIDs[i] == id)
			{
				UICamera.mTouchIDs.RemoveAt(i);
				UICamera.activeTouches.RemoveAt(i);
				return;
			}
			i++;
		}
	};

	// Token: 0x04000654 RID: 1620
	public static UICamera.OnScreenResize onScreenResize;

	// Token: 0x04000655 RID: 1621
	public UICamera.EventType eventType = UICamera.EventType.UI_3D;

	// Token: 0x04000656 RID: 1622
	public bool eventsGoToColliders;

	// Token: 0x04000657 RID: 1623
	public LayerMask eventReceiverMask = -1;

	// Token: 0x04000658 RID: 1624
	public UICamera.ProcessEventsIn processEventsIn;

	// Token: 0x04000659 RID: 1625
	public bool debug;

	// Token: 0x0400065A RID: 1626
	public bool useMouse = true;

	// Token: 0x0400065B RID: 1627
	public bool useTouch = true;

	// Token: 0x0400065C RID: 1628
	public bool allowMultiTouch = true;

	// Token: 0x0400065D RID: 1629
	public bool useKeyboard = true;

	// Token: 0x0400065E RID: 1630
	public bool useController = true;

	// Token: 0x0400065F RID: 1631
	public bool stickyTooltip = true;

	// Token: 0x04000660 RID: 1632
	public float tooltipDelay = 1f;

	// Token: 0x04000661 RID: 1633
	public bool longPressTooltip;

	// Token: 0x04000662 RID: 1634
	public float mouseDragThreshold = 4f;

	// Token: 0x04000663 RID: 1635
	public float mouseClickThreshold = 10f;

	// Token: 0x04000664 RID: 1636
	public float touchDragThreshold = 40f;

	// Token: 0x04000665 RID: 1637
	public float touchClickThreshold = 40f;

	// Token: 0x04000666 RID: 1638
	public float rangeDistance = -1f;

	// Token: 0x04000667 RID: 1639
	public string horizontalAxisName = "Horizontal";

	// Token: 0x04000668 RID: 1640
	public string verticalAxisName = "Vertical";

	// Token: 0x04000669 RID: 1641
	public string horizontalPanAxisName;

	// Token: 0x0400066A RID: 1642
	public string verticalPanAxisName;

	// Token: 0x0400066B RID: 1643
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x0400066C RID: 1644
	[Tooltip("If enabled, command-click will result in a right-click event on OSX")]
	public bool commandClick = true;

	// Token: 0x0400066D RID: 1645
	public KeyCode submitKey0 = KeyCode.Return;

	// Token: 0x0400066E RID: 1646
	public KeyCode submitKey1 = KeyCode.JoystickButton0;

	// Token: 0x0400066F RID: 1647
	public KeyCode cancelKey0 = KeyCode.Escape;

	// Token: 0x04000670 RID: 1648
	public KeyCode cancelKey1 = KeyCode.JoystickButton1;

	// Token: 0x04000671 RID: 1649
	public bool autoHideCursor = true;

	// Token: 0x04000672 RID: 1650
	public static UICamera.OnCustomInput onCustomInput;

	// Token: 0x04000673 RID: 1651
	public static bool showTooltips = true;

	// Token: 0x04000674 RID: 1652
	public static bool ignoreAllEvents = false;

	// Token: 0x04000675 RID: 1653
	public static bool ignoreControllerInput = false;

	// Token: 0x04000676 RID: 1654
	private static bool mDisableController = false;

	// Token: 0x04000677 RID: 1655
	private static Vector2 mLastPos = Vector2.zero;

	// Token: 0x04000678 RID: 1656
	public static Vector3 lastWorldPosition = Vector3.zero;

	// Token: 0x04000679 RID: 1657
	public static Ray lastWorldRay = default(Ray);

	// Token: 0x0400067A RID: 1658
	public static RaycastHit lastHit;

	// Token: 0x0400067B RID: 1659
	public static UICamera current = null;

	// Token: 0x0400067C RID: 1660
	public static Camera currentCamera = null;

	// Token: 0x0400067D RID: 1661
	public static UICamera.OnSchemeChange onSchemeChange;

	// Token: 0x0400067E RID: 1662
	private static UICamera.ControlScheme mLastScheme = UICamera.ControlScheme.Mouse;

	// Token: 0x0400067F RID: 1663
	public static int currentTouchID = -100;

	// Token: 0x04000680 RID: 1664
	private static KeyCode mCurrentKey = KeyCode.Alpha0;

	// Token: 0x04000681 RID: 1665
	[NonSerialized]
	public static UICamera.MouseOrTouch currentTouch = null;

	// Token: 0x04000682 RID: 1666
	[NonSerialized]
	private static bool mInputFocus = false;

	// Token: 0x04000683 RID: 1667
	[NonSerialized]
	private static GameObject mGenericHandler;

	// Token: 0x04000684 RID: 1668
	[NonSerialized]
	public static GameObject fallThrough;

	// Token: 0x04000685 RID: 1669
	[NonSerialized]
	public static UICamera.VoidDelegate onClick;

	// Token: 0x04000686 RID: 1670
	[NonSerialized]
	public static UICamera.VoidDelegate onDoubleClick;

	// Token: 0x04000687 RID: 1671
	[NonSerialized]
	public static UICamera.BoolDelegate onHover;

	// Token: 0x04000688 RID: 1672
	[NonSerialized]
	public static UICamera.BoolDelegate onPress;

	// Token: 0x04000689 RID: 1673
	[NonSerialized]
	public static UICamera.BoolDelegate onSelect;

	// Token: 0x0400068A RID: 1674
	[NonSerialized]
	public static UICamera.FloatDelegate onScroll;

	// Token: 0x0400068B RID: 1675
	[NonSerialized]
	public static UICamera.VectorDelegate onDrag;

	// Token: 0x0400068C RID: 1676
	[NonSerialized]
	public static UICamera.VoidDelegate onDragStart;

	// Token: 0x0400068D RID: 1677
	[NonSerialized]
	public static UICamera.ObjectDelegate onDragOver;

	// Token: 0x0400068E RID: 1678
	[NonSerialized]
	public static UICamera.ObjectDelegate onDragOut;

	// Token: 0x0400068F RID: 1679
	[NonSerialized]
	public static UICamera.VoidDelegate onDragEnd;

	// Token: 0x04000690 RID: 1680
	[NonSerialized]
	public static UICamera.ObjectDelegate onDrop;

	// Token: 0x04000691 RID: 1681
	[NonSerialized]
	public static UICamera.KeyCodeDelegate onKey;

	// Token: 0x04000692 RID: 1682
	[NonSerialized]
	public static UICamera.KeyCodeDelegate onNavigate;

	// Token: 0x04000693 RID: 1683
	[NonSerialized]
	public static UICamera.VectorDelegate onPan;

	// Token: 0x04000694 RID: 1684
	[NonSerialized]
	public static UICamera.BoolDelegate onTooltip;

	// Token: 0x04000695 RID: 1685
	[NonSerialized]
	public static UICamera.MoveDelegate onMouseMove;

	// Token: 0x04000696 RID: 1686
	private static UICamera.MouseOrTouch[] mMouse = new UICamera.MouseOrTouch[]
	{
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch()
	};

	// Token: 0x04000697 RID: 1687
	[NonSerialized]
	public static UICamera.MouseOrTouch controller = new UICamera.MouseOrTouch();

	// Token: 0x04000698 RID: 1688
	[NonSerialized]
	public static List<UICamera.MouseOrTouch> activeTouches = new List<UICamera.MouseOrTouch>();

	// Token: 0x04000699 RID: 1689
	[NonSerialized]
	private static List<int> mTouchIDs = new List<int>();

	// Token: 0x0400069A RID: 1690
	[NonSerialized]
	private static int mWidth = 0;

	// Token: 0x0400069B RID: 1691
	[NonSerialized]
	private static int mHeight = 0;

	// Token: 0x0400069C RID: 1692
	[NonSerialized]
	private static GameObject mTooltip = null;

	// Token: 0x0400069D RID: 1693
	[NonSerialized]
	private Camera mCam;

	// Token: 0x0400069E RID: 1694
	[NonSerialized]
	private static float mTooltipTime = 0f;

	// Token: 0x0400069F RID: 1695
	[NonSerialized]
	private float mNextRaycast;

	// Token: 0x040006A0 RID: 1696
	[NonSerialized]
	public static bool isDragging = false;

	// Token: 0x040006A1 RID: 1697
	private static int mLastInteractionCheck = -1;

	// Token: 0x040006A2 RID: 1698
	private static bool mLastInteractionResult = false;

	// Token: 0x040006A3 RID: 1699
	private static int mLastFocusCheck = -1;

	// Token: 0x040006A4 RID: 1700
	private static bool mLastFocusResult = false;

	// Token: 0x040006A5 RID: 1701
	private static int mLastOverCheck = -1;

	// Token: 0x040006A6 RID: 1702
	private static bool mLastOverResult = false;

	// Token: 0x040006A7 RID: 1703
	private static GameObject mRayHitObject;

	// Token: 0x040006A8 RID: 1704
	private static GameObject mHover;

	// Token: 0x040006A9 RID: 1705
	private static GameObject mSelected;

	// Token: 0x040006AA RID: 1706
	private static UICamera.DepthEntry mHit = default(UICamera.DepthEntry);

	// Token: 0x040006AB RID: 1707
	private static BetterList<UICamera.DepthEntry> mHits = new BetterList<UICamera.DepthEntry>();

	// Token: 0x040006AC RID: 1708
	private static RaycastHit[] mRayHits;

	// Token: 0x040006AD RID: 1709
	private static Collider2D[] mOverlap;

	// Token: 0x040006AE RID: 1710
	private static Plane m2DPlane = new Plane(Vector3.back, 0f);

	// Token: 0x040006AF RID: 1711
	private static float mNextEvent = 0f;

	// Token: 0x040006B0 RID: 1712
	private static int mNotifying = 0;

	// Token: 0x040006B1 RID: 1713
	private static bool disableControllerCheck = true;

	// Token: 0x040006B2 RID: 1714
	private static bool mUsingTouchEvents = true;

	// Token: 0x040006B3 RID: 1715
	public static UICamera.GetTouchCountCallback GetInputTouchCount;

	// Token: 0x040006B4 RID: 1716
	public static UICamera.GetTouchCallback GetInputTouch;

	// Token: 0x0200065E RID: 1630
	[DoNotObfuscateNGUI]
	public enum ControlScheme
	{
		// Token: 0x0400457F RID: 17791
		Mouse,
		// Token: 0x04004580 RID: 17792
		Touch,
		// Token: 0x04004581 RID: 17793
		Controller
	}

	// Token: 0x0200065F RID: 1631
	[DoNotObfuscateNGUI]
	public enum ClickNotification
	{
		// Token: 0x04004583 RID: 17795
		None,
		// Token: 0x04004584 RID: 17796
		Always,
		// Token: 0x04004585 RID: 17797
		BasedOnDelta
	}

	// Token: 0x02000660 RID: 1632
	public class MouseOrTouch
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x001C44E0 File Offset: 0x001C26E0
		public float deltaTime
		{
			get
			{
				return RealTime.time - this.pressTime;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x001C44EE File Offset: 0x001C26EE
		public bool isOverUI
		{
			get
			{
				return this.current != null && this.current != UICamera.fallThrough && NGUITools.FindInParents<UIRoot>(this.current) != null;
			}
		}

		// Token: 0x04004586 RID: 17798
		public KeyCode key;

		// Token: 0x04004587 RID: 17799
		public Vector2 pos;

		// Token: 0x04004588 RID: 17800
		public Vector2 lastPos;

		// Token: 0x04004589 RID: 17801
		public Vector2 delta;

		// Token: 0x0400458A RID: 17802
		public Vector2 totalDelta;

		// Token: 0x0400458B RID: 17803
		public Camera pressedCam;

		// Token: 0x0400458C RID: 17804
		public GameObject last;

		// Token: 0x0400458D RID: 17805
		public GameObject current;

		// Token: 0x0400458E RID: 17806
		public GameObject pressed;

		// Token: 0x0400458F RID: 17807
		public GameObject dragged;

		// Token: 0x04004590 RID: 17808
		public GameObject lastClickGO;

		// Token: 0x04004591 RID: 17809
		public float pressTime;

		// Token: 0x04004592 RID: 17810
		public float clickTime;

		// Token: 0x04004593 RID: 17811
		public UICamera.ClickNotification clickNotification = UICamera.ClickNotification.Always;

		// Token: 0x04004594 RID: 17812
		public bool touchBegan = true;

		// Token: 0x04004595 RID: 17813
		public bool pressStarted;

		// Token: 0x04004596 RID: 17814
		public bool dragStarted;

		// Token: 0x04004597 RID: 17815
		public int ignoreDelta;
	}

	// Token: 0x02000661 RID: 1633
	[DoNotObfuscateNGUI]
	public enum EventType
	{
		// Token: 0x04004599 RID: 17817
		World_3D,
		// Token: 0x0400459A RID: 17818
		UI_3D,
		// Token: 0x0400459B RID: 17819
		World_2D,
		// Token: 0x0400459C RID: 17820
		UI_2D
	}

	// Token: 0x02000662 RID: 1634
	// (Invoke) Token: 0x06002AF2 RID: 10994
	public delegate bool GetKeyStateFunc(KeyCode key);

	// Token: 0x02000663 RID: 1635
	// (Invoke) Token: 0x06002AF6 RID: 10998
	public delegate float GetAxisFunc(string name);

	// Token: 0x02000664 RID: 1636
	// (Invoke) Token: 0x06002AFA RID: 11002
	public delegate bool GetAnyKeyFunc();

	// Token: 0x02000665 RID: 1637
	// (Invoke) Token: 0x06002AFE RID: 11006
	public delegate UICamera.MouseOrTouch GetMouseDelegate(int button);

	// Token: 0x02000666 RID: 1638
	// (Invoke) Token: 0x06002B02 RID: 11010
	public delegate UICamera.MouseOrTouch GetTouchDelegate(int id, bool createIfMissing);

	// Token: 0x02000667 RID: 1639
	// (Invoke) Token: 0x06002B06 RID: 11014
	public delegate void RemoveTouchDelegate(int id);

	// Token: 0x02000668 RID: 1640
	// (Invoke) Token: 0x06002B0A RID: 11018
	public delegate void OnScreenResize();

	// Token: 0x02000669 RID: 1641
	[DoNotObfuscateNGUI]
	public enum ProcessEventsIn
	{
		// Token: 0x0400459E RID: 17822
		Update,
		// Token: 0x0400459F RID: 17823
		LateUpdate
	}

	// Token: 0x0200066A RID: 1642
	// (Invoke) Token: 0x06002B0E RID: 11022
	public delegate void OnCustomInput();

	// Token: 0x0200066B RID: 1643
	// (Invoke) Token: 0x06002B12 RID: 11026
	public delegate void OnSchemeChange();

	// Token: 0x0200066C RID: 1644
	// (Invoke) Token: 0x06002B16 RID: 11030
	public delegate void MoveDelegate(Vector2 delta);

	// Token: 0x0200066D RID: 1645
	// (Invoke) Token: 0x06002B1A RID: 11034
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x0200066E RID: 1646
	// (Invoke) Token: 0x06002B1E RID: 11038
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x0200066F RID: 1647
	// (Invoke) Token: 0x06002B22 RID: 11042
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x02000670 RID: 1648
	// (Invoke) Token: 0x06002B26 RID: 11046
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x02000671 RID: 1649
	// (Invoke) Token: 0x06002B2A RID: 11050
	public delegate void ObjectDelegate(GameObject go, GameObject obj);

	// Token: 0x02000672 RID: 1650
	// (Invoke) Token: 0x06002B2E RID: 11054
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

	// Token: 0x02000673 RID: 1651
	private struct DepthEntry
	{
		// Token: 0x040045A0 RID: 17824
		public int depth;

		// Token: 0x040045A1 RID: 17825
		public RaycastHit hit;

		// Token: 0x040045A2 RID: 17826
		public Vector3 point;

		// Token: 0x040045A3 RID: 17827
		public GameObject go;
	}

	// Token: 0x02000674 RID: 1652
	public class Touch
	{
		// Token: 0x040045A4 RID: 17828
		public int fingerId;

		// Token: 0x040045A5 RID: 17829
		public TouchPhase phase;

		// Token: 0x040045A6 RID: 17830
		public Vector2 position;

		// Token: 0x040045A7 RID: 17831
		public int tapCount;
	}

	// Token: 0x02000675 RID: 1653
	// (Invoke) Token: 0x06002B33 RID: 11059
	public delegate int GetTouchCountCallback();

	// Token: 0x02000676 RID: 1654
	// (Invoke) Token: 0x06002B37 RID: 11063
	public delegate UICamera.Touch GetTouchCallback(int index);
}

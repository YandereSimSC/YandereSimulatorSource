using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A8 RID: 168
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x0600088F RID: 2191 RVA: 0x0004514D File Offset: 0x0004334D
	public UIRoot.Constraint constraint
	{
		get
		{
			if (this.fitWidth)
			{
				if (this.fitHeight)
				{
					return UIRoot.Constraint.Fit;
				}
				return UIRoot.Constraint.FitWidth;
			}
			else
			{
				if (this.fitHeight)
				{
					return UIRoot.Constraint.FitHeight;
				}
				return UIRoot.Constraint.Fill;
			}
		}
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06000890 RID: 2192 RVA: 0x00045170 File Offset: 0x00043370
	public UIRoot.Scaling activeScaling
	{
		get
		{
			UIRoot.Scaling scaling = this.scalingStyle;
			if (scaling == UIRoot.Scaling.ConstrainedOnMobiles)
			{
				return UIRoot.Scaling.Flexible;
			}
			return scaling;
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06000891 RID: 2193 RVA: 0x0004518C File Offset: 0x0004338C
	public int activeHeight
	{
		get
		{
			if (this.activeScaling == UIRoot.Scaling.Flexible)
			{
				Vector2 screenSize = NGUITools.screenSize;
				float num = screenSize.x / screenSize.y;
				if (screenSize.y < (float)this.minimumHeight)
				{
					screenSize.y = (float)this.minimumHeight;
					screenSize.x = screenSize.y * num;
				}
				else if (screenSize.y > (float)this.maximumHeight)
				{
					screenSize.y = (float)this.maximumHeight;
					screenSize.x = screenSize.y * num;
				}
				int num2 = Mathf.RoundToInt((this.shrinkPortraitUI && screenSize.y > screenSize.x) ? (screenSize.y / num) : screenSize.y);
				if (!this.adjustByDPI)
				{
					return num2;
				}
				return NGUIMath.AdjustByDPI((float)num2);
			}
			else
			{
				UIRoot.Constraint constraint = this.constraint;
				if (constraint == UIRoot.Constraint.FitHeight)
				{
					return this.manualHeight;
				}
				Vector2 screenSize2 = NGUITools.screenSize;
				float num3 = screenSize2.x / screenSize2.y;
				float num4 = (float)this.manualWidth / (float)this.manualHeight;
				switch (constraint)
				{
				case UIRoot.Constraint.Fit:
					if (num4 <= num3)
					{
						return this.manualHeight;
					}
					return Mathf.RoundToInt((float)this.manualWidth / num3);
				case UIRoot.Constraint.Fill:
					if (num4 >= num3)
					{
						return this.manualHeight;
					}
					return Mathf.RoundToInt((float)this.manualWidth / num3);
				case UIRoot.Constraint.FitWidth:
					return Mathf.RoundToInt((float)this.manualWidth / num3);
				default:
					return this.manualHeight;
				}
			}
		}
	}

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x06000892 RID: 2194 RVA: 0x000452F0 File Offset: 0x000434F0
	public float pixelSizeAdjustment
	{
		get
		{
			int num = Mathf.RoundToInt(NGUITools.screenSize.y);
			if (num != -1)
			{
				return this.GetPixelSizeAdjustment(num);
			}
			return 1f;
		}
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00045320 File Offset: 0x00043520
	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uiroot = NGUITools.FindInParents<UIRoot>(go);
		if (!(uiroot != null))
		{
			return 1f;
		}
		return uiroot.pixelSizeAdjustment;
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0004534C File Offset: 0x0004354C
	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);
		if (this.activeScaling == UIRoot.Scaling.Constrained)
		{
			return (float)this.activeHeight / (float)height;
		}
		if (height < this.minimumHeight)
		{
			return (float)this.minimumHeight / (float)height;
		}
		if (height > this.maximumHeight)
		{
			return (float)this.maximumHeight / (float)height;
		}
		return 1f;
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x000453A3 File Offset: 0x000435A3
	protected virtual void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x000453B1 File Offset: 0x000435B1
	protected virtual void OnEnable()
	{
		UIRoot.list.Add(this);
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x000453BE File Offset: 0x000435BE
	protected virtual void OnDisable()
	{
		UIRoot.list.Remove(this);
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x000453CC File Offset: 0x000435CC
	protected virtual void Start()
	{
		UIOrthoCamera componentInChildren = base.GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
				return;
			}
		}
		else
		{
			this.UpdateScale(false);
		}
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00045423 File Offset: 0x00043623
	private void Update()
	{
		this.UpdateScale(true);
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0004542C File Offset: 0x0004362C
	public void UpdateScale(bool updateAnchors = true)
	{
		if (this.mTrans != null)
		{
			float num = (float)this.activeHeight;
			if (num > 0f)
			{
				float num2 = 2f / num;
				Vector3 localScale = this.mTrans.localScale;
				if (Mathf.Abs(localScale.x - num2) > 1E-45f || Mathf.Abs(localScale.y - num2) > 1E-45f || Mathf.Abs(localScale.z - num2) > 1E-45f)
				{
					this.mTrans.localScale = new Vector3(num2, num2, num2);
					if (updateAnchors)
					{
						base.BroadcastMessage("UpdateAnchors", SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x000454CC File Offset: 0x000436CC
	public static void Broadcast(string funcName)
	{
		int i = 0;
		int count = UIRoot.list.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.list[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
			}
			i++;
		}
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00045510 File Offset: 0x00043710
	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
			return;
		}
		int i = 0;
		int count = UIRoot.list.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.list[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
			}
			i++;
		}
	}

	// Token: 0x0400075F RID: 1887
	public static List<UIRoot> list = new List<UIRoot>();

	// Token: 0x04000760 RID: 1888
	public UIRoot.Scaling scalingStyle;

	// Token: 0x04000761 RID: 1889
	public int manualWidth = 1280;

	// Token: 0x04000762 RID: 1890
	public int manualHeight = 720;

	// Token: 0x04000763 RID: 1891
	public int minimumHeight = 320;

	// Token: 0x04000764 RID: 1892
	public int maximumHeight = 1536;

	// Token: 0x04000765 RID: 1893
	public bool fitWidth;

	// Token: 0x04000766 RID: 1894
	public bool fitHeight = true;

	// Token: 0x04000767 RID: 1895
	public bool adjustByDPI;

	// Token: 0x04000768 RID: 1896
	public bool shrinkPortraitUI;

	// Token: 0x04000769 RID: 1897
	private Transform mTrans;

	// Token: 0x02000686 RID: 1670
	[DoNotObfuscateNGUI]
	public enum Scaling
	{
		// Token: 0x040045DC RID: 17884
		Flexible,
		// Token: 0x040045DD RID: 17885
		Constrained,
		// Token: 0x040045DE RID: 17886
		ConstrainedOnMobiles
	}

	// Token: 0x02000687 RID: 1671
	[DoNotObfuscateNGUI]
	public enum Constraint
	{
		// Token: 0x040045E0 RID: 17888
		Fit,
		// Token: 0x040045E1 RID: 17889
		Fill,
		// Token: 0x040045E2 RID: 17890
		FitWidth,
		// Token: 0x040045E3 RID: 17891
		FitHeight
	}
}

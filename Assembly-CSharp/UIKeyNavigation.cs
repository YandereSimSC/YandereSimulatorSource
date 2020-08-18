using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
[AddComponentMenu("NGUI/Interaction/Key Navigation")]
public class UIKeyNavigation : MonoBehaviour
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x0600023C RID: 572 RVA: 0x00018990 File Offset: 0x00016B90
	public static UIKeyNavigation current
	{
		get
		{
			GameObject hoveredObject = UICamera.hoveredObject;
			if (hoveredObject == null)
			{
				return null;
			}
			return hoveredObject.GetComponent<UIKeyNavigation>();
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x0600023D RID: 573 RVA: 0x000189B4 File Offset: 0x00016BB4
	public bool isColliderEnabled
	{
		get
		{
			if (!base.enabled || !base.gameObject.activeInHierarchy)
			{
				return false;
			}
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00018A07 File Offset: 0x00016C07
	protected virtual void OnEnable()
	{
		UIKeyNavigation.list.Add(this);
		if (this.mStarted)
		{
			base.Invoke("Start", 0.001f);
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00018A2C File Offset: 0x00016C2C
	private void Start()
	{
		this.mStarted = true;
		if (this.startsSelected && this.isColliderEnabled)
		{
			UICamera.selectedObject = base.gameObject;
		}
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00018A50 File Offset: 0x00016C50
	protected virtual void OnDisable()
	{
		UIKeyNavigation.list.Remove(this);
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00018A60 File Offset: 0x00016C60
	private static bool IsActive(GameObject go)
	{
		if (!go || !go.activeInHierarchy)
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

	// Token: 0x06000242 RID: 578 RVA: 0x00018AAE File Offset: 0x00016CAE
	public GameObject GetLeft()
	{
		if (UIKeyNavigation.IsActive(this.onLeft))
		{
			return this.onLeft;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Vertical || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.left, 1f, 2f);
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00018AED File Offset: 0x00016CED
	public GameObject GetRight()
	{
		if (UIKeyNavigation.IsActive(this.onRight))
		{
			return this.onRight;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Vertical || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.right, 1f, 2f);
	}

	// Token: 0x06000244 RID: 580 RVA: 0x00018B2C File Offset: 0x00016D2C
	public GameObject GetUp()
	{
		if (UIKeyNavigation.IsActive(this.onUp))
		{
			return this.onUp;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Horizontal || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.up, 2f, 1f);
	}

	// Token: 0x06000245 RID: 581 RVA: 0x00018B6B File Offset: 0x00016D6B
	public GameObject GetDown()
	{
		if (UIKeyNavigation.IsActive(this.onDown))
		{
			return this.onDown;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Horizontal || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.down, 2f, 1f);
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00018BAC File Offset: 0x00016DAC
	public GameObject Get(Vector3 myDir, float x = 1f, float y = 1f)
	{
		Transform transform = base.transform;
		myDir = transform.TransformDirection(myDir);
		Vector3 center = UIKeyNavigation.GetCenter(base.gameObject);
		float num = float.MaxValue;
		GameObject result = null;
		for (int i = 0; i < UIKeyNavigation.list.size; i++)
		{
			UIKeyNavigation uikeyNavigation = UIKeyNavigation.list.buffer[i];
			if (!(uikeyNavigation == this) && uikeyNavigation.constraint != UIKeyNavigation.Constraint.Explicit && uikeyNavigation.isColliderEnabled)
			{
				UIWidget component = uikeyNavigation.GetComponent<UIWidget>();
				if (!(component != null) || component.alpha != 0f)
				{
					Vector3 direction = UIKeyNavigation.GetCenter(uikeyNavigation.gameObject) - center;
					if (Vector3.Dot(myDir, direction.normalized) >= 0.707f)
					{
						direction = transform.InverseTransformDirection(direction);
						direction.x *= x;
						direction.y *= y;
						float sqrMagnitude = direction.sqrMagnitude;
						if (sqrMagnitude <= num)
						{
							result = uikeyNavigation.gameObject;
							num = sqrMagnitude;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00018CB8 File Offset: 0x00016EB8
	protected static Vector3 GetCenter(GameObject go)
	{
		UIWidget component = go.GetComponent<UIWidget>();
		UICamera uicamera = UICamera.FindCameraForLayer(go.layer);
		if (uicamera != null)
		{
			Vector3 vector = go.transform.position;
			if (component != null)
			{
				Vector3[] worldCorners = component.worldCorners;
				vector = (worldCorners[0] + worldCorners[2]) * 0.5f;
			}
			vector = uicamera.cachedCamera.WorldToScreenPoint(vector);
			vector.z = 0f;
			return vector;
		}
		if (component != null)
		{
			Vector3[] worldCorners2 = component.worldCorners;
			return (worldCorners2[0] + worldCorners2[2]) * 0.5f;
		}
		return go.transform.position;
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00018D74 File Offset: 0x00016F74
	public virtual void OnNavigate(KeyCode key)
	{
		if (UIPopupList.isOpen)
		{
			return;
		}
		if (UIKeyNavigation.mLastFrame == Time.frameCount)
		{
			return;
		}
		UIKeyNavigation.mLastFrame = Time.frameCount;
		GameObject gameObject = null;
		switch (key)
		{
		case KeyCode.UpArrow:
			gameObject = this.GetUp();
			break;
		case KeyCode.DownArrow:
			gameObject = this.GetDown();
			break;
		case KeyCode.RightArrow:
			gameObject = this.GetRight();
			break;
		case KeyCode.LeftArrow:
			gameObject = this.GetLeft();
			break;
		}
		if (gameObject != null)
		{
			UICamera.hoveredObject = gameObject;
		}
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00018DF4 File Offset: 0x00016FF4
	public virtual void OnKey(KeyCode key)
	{
		if (UIPopupList.isOpen)
		{
			return;
		}
		if (UIKeyNavigation.mLastFrame == Time.frameCount)
		{
			return;
		}
		UIKeyNavigation.mLastFrame = Time.frameCount;
		if (key == KeyCode.Tab)
		{
			GameObject gameObject = this.onTab;
			if (gameObject == null)
			{
				if (UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift))
				{
					gameObject = this.GetLeft();
					if (gameObject == null)
					{
						gameObject = this.GetUp();
					}
					if (gameObject == null)
					{
						gameObject = this.GetDown();
					}
					if (gameObject == null)
					{
						gameObject = this.GetRight();
					}
				}
				else
				{
					gameObject = this.GetRight();
					if (gameObject == null)
					{
						gameObject = this.GetDown();
					}
					if (gameObject == null)
					{
						gameObject = this.GetUp();
					}
					if (gameObject == null)
					{
						gameObject = this.GetLeft();
					}
				}
			}
			if (gameObject != null)
			{
				UICamera.currentScheme = UICamera.ControlScheme.Controller;
				UICamera.hoveredObject = gameObject;
				UIInput component = gameObject.GetComponent<UIInput>();
				if (component != null)
				{
					component.isSelected = true;
				}
			}
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x00018EF9 File Offset: 0x000170F9
	protected virtual void OnClick()
	{
		if (NGUITools.GetActive(this.onClick))
		{
			UICamera.hoveredObject = this.onClick;
		}
	}

	// Token: 0x040003B2 RID: 946
	public static BetterList<UIKeyNavigation> list = new BetterList<UIKeyNavigation>();

	// Token: 0x040003B3 RID: 947
	public UIKeyNavigation.Constraint constraint;

	// Token: 0x040003B4 RID: 948
	public GameObject onUp;

	// Token: 0x040003B5 RID: 949
	public GameObject onDown;

	// Token: 0x040003B6 RID: 950
	public GameObject onLeft;

	// Token: 0x040003B7 RID: 951
	public GameObject onRight;

	// Token: 0x040003B8 RID: 952
	public GameObject onClick;

	// Token: 0x040003B9 RID: 953
	public GameObject onTab;

	// Token: 0x040003BA RID: 954
	public bool startsSelected;

	// Token: 0x040003BB RID: 955
	[NonSerialized]
	private bool mStarted;

	// Token: 0x040003BC RID: 956
	public static int mLastFrame = 0;

	// Token: 0x0200061A RID: 1562
	[DoNotObfuscateNGUI]
	public enum Constraint
	{
		// Token: 0x040044A8 RID: 17576
		None,
		// Token: 0x040044A9 RID: 17577
		Vertical,
		// Token: 0x040044AA RID: 17578
		Horizontal,
		// Token: 0x040044AB RID: 17579
		Explicit
	}
}

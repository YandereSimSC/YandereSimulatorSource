using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004F RID: 79
[AddComponentMenu("NGUI/Interaction/Drag and Drop Item")]
public class UIDragDropItem : MonoBehaviour
{
	// Token: 0x060001B7 RID: 439 RVA: 0x00015AF4 File Offset: 0x00013CF4
	public static bool IsDragged(GameObject go)
	{
		using (List<UIDragDropItem>.Enumerator enumerator = UIDragDropItem.draggedItems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.gameObject == go)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00015B54 File Offset: 0x00013D54
	protected virtual void Awake()
	{
		this.mTrans = base.transform;
		this.mCollider = base.gameObject.GetComponent<Collider>();
		this.mCollider2D = base.gameObject.GetComponent<Collider2D>();
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x00002ACE File Offset: 0x00000CCE
	protected virtual void OnEnable()
	{
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00015B84 File Offset: 0x00013D84
	protected virtual void OnDisable()
	{
		if (this.mDragging)
		{
			this.StopDragging(null);
			UICamera.onPress = (UICamera.BoolDelegate)Delegate.Remove(UICamera.onPress, new UICamera.BoolDelegate(this.OnGlobalPress));
			UICamera.onClick = (UICamera.VoidDelegate)Delegate.Remove(UICamera.onClick, new UICamera.VoidDelegate(this.OnGlobalClick));
			UICamera.onMouseMove = (UICamera.MoveDelegate)Delegate.Remove(UICamera.onMouseMove, new UICamera.MoveDelegate(this.OnDrag));
		}
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00015C01 File Offset: 0x00013E01
	protected virtual void Start()
	{
		this.mButton = base.GetComponent<UIButton>();
		this.mDragScrollView = base.GetComponent<UIDragScrollView>();
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00015C1C File Offset: 0x00013E1C
	protected virtual void OnPress(bool isPressed)
	{
		if (!this.interactable || UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (isPressed)
		{
			if (!this.mPressed)
			{
				this.mTouch = UICamera.currentTouch;
				this.mDragStartTime = RealTime.time + this.pressAndHoldDelay;
				this.mPressed = true;
				return;
			}
		}
		else if (this.mPressed && this.mTouch == UICamera.currentTouch)
		{
			this.mPressed = false;
			if (!this.mDragging || !this.clickToDrag)
			{
				this.mTouch = null;
			}
		}
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00015CA8 File Offset: 0x00013EA8
	protected virtual void OnClick()
	{
		if (UIDragDropItem.mIgnoreClick == Time.frameCount)
		{
			return;
		}
		if (this.clickToDrag && !this.mDragging && UICamera.currentTouchID == -1 && UIDragDropItem.draggedItems.Count == 0)
		{
			this.mTouch = UICamera.currentTouch;
			UIDragDropItem uidragDropItem = this.StartDragging();
			if (this.clickToDrag && uidragDropItem != null)
			{
				UICamera.onMouseMove = (UICamera.MoveDelegate)Delegate.Combine(UICamera.onMouseMove, new UICamera.MoveDelegate(uidragDropItem.OnDrag));
				UICamera.onPress = (UICamera.BoolDelegate)Delegate.Combine(UICamera.onPress, new UICamera.BoolDelegate(uidragDropItem.OnGlobalPress));
				UICamera.onClick = (UICamera.VoidDelegate)Delegate.Combine(UICamera.onClick, new UICamera.VoidDelegate(uidragDropItem.OnGlobalClick));
			}
		}
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00015D78 File Offset: 0x00013F78
	protected void OnGlobalPress(GameObject go, bool state)
	{
		if (state && UICamera.currentTouchID != -1)
		{
			UIDragDropItem.mIgnoreClick = Time.frameCount;
			this.StopDragging(null);
			UICamera.onPress = (UICamera.BoolDelegate)Delegate.Remove(UICamera.onPress, new UICamera.BoolDelegate(this.OnGlobalPress));
			UICamera.onClick = (UICamera.VoidDelegate)Delegate.Remove(UICamera.onClick, new UICamera.VoidDelegate(this.OnGlobalClick));
			UICamera.onMouseMove = (UICamera.MoveDelegate)Delegate.Remove(UICamera.onMouseMove, new UICamera.MoveDelegate(this.OnDrag));
		}
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00015E04 File Offset: 0x00014004
	protected void OnGlobalClick(GameObject go)
	{
		UIDragDropItem.mIgnoreClick = Time.frameCount;
		if (UICamera.currentTouchID == -1)
		{
			this.StopDragging(go);
		}
		else
		{
			this.StopDragging(null);
		}
		UICamera.onPress = (UICamera.BoolDelegate)Delegate.Remove(UICamera.onPress, new UICamera.BoolDelegate(this.OnGlobalPress));
		UICamera.onClick = (UICamera.VoidDelegate)Delegate.Remove(UICamera.onClick, new UICamera.VoidDelegate(this.OnGlobalClick));
		UICamera.onMouseMove = (UICamera.MoveDelegate)Delegate.Remove(UICamera.onMouseMove, new UICamera.MoveDelegate(this.OnDrag));
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00015E94 File Offset: 0x00014094
	protected virtual void Update()
	{
		if (this.restriction == UIDragDropItem.Restriction.PressAndHold && this.mPressed && !this.mDragging && this.mDragStartTime < RealTime.time)
		{
			this.StartDragging();
		}
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00015EC4 File Offset: 0x000140C4
	protected virtual void OnDragStart()
	{
		if (!this.interactable)
		{
			return;
		}
		if (!base.enabled || this.mTouch != UICamera.currentTouch)
		{
			return;
		}
		if (this.restriction != UIDragDropItem.Restriction.None)
		{
			if (this.restriction == UIDragDropItem.Restriction.Horizontal)
			{
				Vector2 totalDelta = this.mTouch.totalDelta;
				if (Mathf.Abs(totalDelta.x) < Mathf.Abs(totalDelta.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragDropItem.Restriction.Vertical)
			{
				Vector2 totalDelta2 = this.mTouch.totalDelta;
				if (Mathf.Abs(totalDelta2.x) > Mathf.Abs(totalDelta2.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragDropItem.Restriction.PressAndHold)
			{
				return;
			}
		}
		this.StartDragging();
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00015F68 File Offset: 0x00014168
	public virtual UIDragDropItem StartDragging()
	{
		if (!this.interactable || !base.transform || !base.transform.parent)
		{
			return null;
		}
		if (this.mDragging)
		{
			return null;
		}
		if (this.cloneOnDrag)
		{
			this.mPressed = false;
			GameObject gameObject = base.transform.parent.gameObject.AddChild(base.gameObject);
			gameObject.transform.localPosition = base.transform.localPosition;
			gameObject.transform.localRotation = base.transform.localRotation;
			gameObject.transform.localScale = base.transform.localScale;
			UIButtonColor component = gameObject.GetComponent<UIButtonColor>();
			if (component != null)
			{
				component.defaultColor = base.GetComponent<UIButtonColor>().defaultColor;
			}
			if (this.mTouch != null && this.mTouch.pressed == base.gameObject)
			{
				this.mTouch.current = gameObject;
				this.mTouch.pressed = gameObject;
				this.mTouch.dragged = gameObject;
				this.mTouch.last = gameObject;
			}
			UIDragDropItem component2 = gameObject.GetComponent<UIDragDropItem>();
			component2.mTouch = this.mTouch;
			component2.mPressed = true;
			component2.mDragging = true;
			component2.Start();
			component2.OnClone(base.gameObject);
			component2.OnDragDropStart();
			if (UICamera.currentTouch == null)
			{
				UICamera.currentTouch = this.mTouch;
			}
			this.mTouch = null;
			UICamera.Notify(base.gameObject, "OnPress", false);
			UICamera.Notify(base.gameObject, "OnHover", false);
			return component2;
		}
		this.mDragging = true;
		this.OnDragDropStart();
		return this;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00002ACE File Offset: 0x00000CCE
	protected virtual void OnClone(GameObject original)
	{
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00016118 File Offset: 0x00014318
	protected virtual void OnDrag(Vector2 delta)
	{
		if (!this.interactable)
		{
			return;
		}
		if (!this.mDragging || !base.enabled || this.mTouch != UICamera.currentTouch)
		{
			return;
		}
		if (this.mRoot != null)
		{
			this.OnDragDropMove(delta * this.mRoot.pixelSizeAdjustment);
			return;
		}
		this.OnDragDropMove(delta);
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0001617C File Offset: 0x0001437C
	protected virtual void OnDragEnd()
	{
		if (!this.interactable)
		{
			return;
		}
		if (!base.enabled || this.mTouch != UICamera.currentTouch)
		{
			return;
		}
		this.StopDragging((UICamera.lastHit.collider != null) ? UICamera.lastHit.collider.gameObject : null);
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x000161D2 File Offset: 0x000143D2
	public void StopDragging(GameObject go = null)
	{
		if (this.mDragging)
		{
			this.mDragging = false;
			this.OnDragDropRelease(go);
		}
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x000161EC File Offset: 0x000143EC
	protected virtual void OnDragDropStart()
	{
		if (!UIDragDropItem.draggedItems.Contains(this))
		{
			UIDragDropItem.draggedItems.Add(this);
		}
		if (this.mDragScrollView != null)
		{
			this.mDragScrollView.enabled = false;
		}
		if (this.mButton != null)
		{
			this.mButton.isEnabled = false;
		}
		else if (this.mCollider != null)
		{
			this.mCollider.enabled = false;
		}
		else if (this.mCollider2D != null)
		{
			this.mCollider2D.enabled = false;
		}
		this.mParent = this.mTrans.parent;
		this.mRoot = NGUITools.FindInParents<UIRoot>(this.mParent);
		this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
		this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
		if (UIDragDropRoot.root != null)
		{
			this.mTrans.parent = UIDragDropRoot.root;
		}
		Vector3 localPosition = this.mTrans.localPosition;
		localPosition.z = 0f;
		this.mTrans.localPosition = localPosition;
		TweenPosition component = base.GetComponent<TweenPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
		SpringPosition component2 = base.GetComponent<SpringPosition>();
		if (component2 != null)
		{
			component2.enabled = false;
		}
		NGUITools.MarkParentAsChanged(base.gameObject);
		if (this.mTable != null)
		{
			this.mTable.repositionNow = true;
		}
		if (this.mGrid != null)
		{
			this.mGrid.repositionNow = true;
		}
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0001636F File Offset: 0x0001456F
	protected virtual void OnDragDropMove(Vector2 delta)
	{
		if (this.mParent != null)
		{
			this.mTrans.localPosition += this.mTrans.InverseTransformDirection(delta);
		}
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x000163A8 File Offset: 0x000145A8
	protected virtual void OnDragDropRelease(GameObject surface)
	{
		if (!this.cloneOnDrag)
		{
			UIDragScrollView[] componentsInChildren = base.GetComponentsInChildren<UIDragScrollView>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].scrollView = null;
			}
			if (this.mButton != null)
			{
				this.mButton.isEnabled = true;
			}
			else if (this.mCollider != null)
			{
				this.mCollider.enabled = true;
			}
			else if (this.mCollider2D != null)
			{
				this.mCollider2D.enabled = true;
			}
			UIDragDropContainer uidragDropContainer = surface ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null;
			if (uidragDropContainer != null)
			{
				this.mTrans.parent = ((uidragDropContainer.reparentTarget != null) ? uidragDropContainer.reparentTarget : uidragDropContainer.transform);
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.z = 0f;
				this.mTrans.localPosition = localPosition;
			}
			else
			{
				this.mTrans.parent = this.mParent;
			}
			this.mParent = this.mTrans.parent;
			this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
			this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
			if (this.mDragScrollView != null)
			{
				base.Invoke("EnableDragScrollView", 0.001f);
			}
			NGUITools.MarkParentAsChanged(base.gameObject);
			if (this.mTable != null)
			{
				this.mTable.repositionNow = true;
			}
			if (this.mGrid != null)
			{
				this.mGrid.repositionNow = true;
			}
		}
		this.OnDragDropEnd(surface);
		if (this.cloneOnDrag)
		{
			this.DestroySelf();
		}
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0001654E File Offset: 0x0001474E
	protected virtual void DestroySelf()
	{
		NGUITools.Destroy(base.gameObject);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0001655B File Offset: 0x0001475B
	protected virtual void OnDragDropEnd(GameObject surface)
	{
		UIDragDropItem.draggedItems.Remove(this);
		this.mParent = null;
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00016570 File Offset: 0x00014770
	protected void EnableDragScrollView()
	{
		if (this.mDragScrollView != null)
		{
			this.mDragScrollView.enabled = true;
		}
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0001658C File Offset: 0x0001478C
	protected void OnApplicationFocus(bool focus)
	{
		if (!focus)
		{
			this.StopDragging(null);
		}
	}

	// Token: 0x04000335 RID: 821
	[Tooltip("What kind of restriction is applied to the drag & drop logic before dragging is made possible.")]
	public UIDragDropItem.Restriction restriction;

	// Token: 0x04000336 RID: 822
	[Tooltip("By default, dragging only happens while holding the mouse button / touch. If desired, you can opt to use a click-based approach instead. Note that this only works with a mouse.")]
	public bool clickToDrag;

	// Token: 0x04000337 RID: 823
	[Tooltip("Whether a copy of the item will be dragged instead of the item itself.")]
	public bool cloneOnDrag;

	// Token: 0x04000338 RID: 824
	[Tooltip("Whether this drag and drop item can be interacted with. If not, only tooltips will work.")]
	public bool interactable = true;

	// Token: 0x04000339 RID: 825
	[Tooltip("How long the user has to press on an item before the drag action activates.")]
	[HideInInspector]
	public float pressAndHoldDelay = 1f;

	// Token: 0x0400033A RID: 826
	[NonSerialized]
	protected Transform mTrans;

	// Token: 0x0400033B RID: 827
	[NonSerialized]
	protected Transform mParent;

	// Token: 0x0400033C RID: 828
	[NonSerialized]
	protected Collider mCollider;

	// Token: 0x0400033D RID: 829
	[NonSerialized]
	protected Collider2D mCollider2D;

	// Token: 0x0400033E RID: 830
	[NonSerialized]
	protected UIButton mButton;

	// Token: 0x0400033F RID: 831
	[NonSerialized]
	protected UIRoot mRoot;

	// Token: 0x04000340 RID: 832
	[NonSerialized]
	protected UIGrid mGrid;

	// Token: 0x04000341 RID: 833
	[NonSerialized]
	protected UITable mTable;

	// Token: 0x04000342 RID: 834
	[NonSerialized]
	protected float mDragStartTime;

	// Token: 0x04000343 RID: 835
	[NonSerialized]
	protected UIDragScrollView mDragScrollView;

	// Token: 0x04000344 RID: 836
	[NonSerialized]
	protected bool mPressed;

	// Token: 0x04000345 RID: 837
	[NonSerialized]
	protected bool mDragging;

	// Token: 0x04000346 RID: 838
	[NonSerialized]
	protected UICamera.MouseOrTouch mTouch;

	// Token: 0x04000347 RID: 839
	[NonSerialized]
	public static List<UIDragDropItem> draggedItems = new List<UIDragDropItem>();

	// Token: 0x04000348 RID: 840
	[NonSerialized]
	private static int mIgnoreClick = 0;

	// Token: 0x02000613 RID: 1555
	[DoNotObfuscateNGUI]
	public enum Restriction
	{
		// Token: 0x0400448B RID: 17547
		None,
		// Token: 0x0400448C RID: 17548
		Horizontal,
		// Token: 0x0400448D RID: 17549
		Vertical,
		// Token: 0x0400448E RID: 17550
		PressAndHold
	}
}

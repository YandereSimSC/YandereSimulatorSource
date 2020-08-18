using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000055 RID: 85
[AddComponentMenu("NGUI/Interaction/Event Trigger")]
public class UIEventTrigger : MonoBehaviour
{
	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060001F8 RID: 504 RVA: 0x000177D4 File Offset: 0x000159D4
	public bool isColliderEnabled
	{
		get
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00017810 File Offset: 0x00015A10
	private void OnHover(bool isOver)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (isOver)
		{
			EventDelegate.Execute(this.onHoverOver);
		}
		else
		{
			EventDelegate.Execute(this.onHoverOut);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0001784F File Offset: 0x00015A4F
	private void OnPress(bool pressed)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (pressed)
		{
			EventDelegate.Execute(this.onPress);
		}
		else
		{
			EventDelegate.Execute(this.onRelease);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0001788E File Offset: 0x00015A8E
	private void OnSelect(bool selected)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (selected)
		{
			EventDelegate.Execute(this.onSelect);
		}
		else
		{
			EventDelegate.Execute(this.onDeselect);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x000178CD File Offset: 0x00015ACD
	private void OnClick()
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x000178FC File Offset: 0x00015AFC
	private void OnDoubleClick()
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDoubleClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0001792B File Offset: 0x00015B2B
	private void OnDragStart()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragStart);
		UIEventTrigger.current = null;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00017952 File Offset: 0x00015B52
	private void OnDragEnd()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragEnd);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00017979 File Offset: 0x00015B79
	private void OnDragOver(GameObject go)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOver);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x000179A8 File Offset: 0x00015BA8
	private void OnDragOut(GameObject go)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOut);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x000179D7 File Offset: 0x00015BD7
	private void OnDrag(Vector2 delta)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDrag);
		UIEventTrigger.current = null;
	}

	// Token: 0x0400037E RID: 894
	public static UIEventTrigger current;

	// Token: 0x0400037F RID: 895
	public List<EventDelegate> onHoverOver = new List<EventDelegate>();

	// Token: 0x04000380 RID: 896
	public List<EventDelegate> onHoverOut = new List<EventDelegate>();

	// Token: 0x04000381 RID: 897
	public List<EventDelegate> onPress = new List<EventDelegate>();

	// Token: 0x04000382 RID: 898
	public List<EventDelegate> onRelease = new List<EventDelegate>();

	// Token: 0x04000383 RID: 899
	public List<EventDelegate> onSelect = new List<EventDelegate>();

	// Token: 0x04000384 RID: 900
	public List<EventDelegate> onDeselect = new List<EventDelegate>();

	// Token: 0x04000385 RID: 901
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x04000386 RID: 902
	public List<EventDelegate> onDoubleClick = new List<EventDelegate>();

	// Token: 0x04000387 RID: 903
	public List<EventDelegate> onDragStart = new List<EventDelegate>();

	// Token: 0x04000388 RID: 904
	public List<EventDelegate> onDragEnd = new List<EventDelegate>();

	// Token: 0x04000389 RID: 905
	public List<EventDelegate> onDragOver = new List<EventDelegate>();

	// Token: 0x0400038A RID: 906
	public List<EventDelegate> onDragOut = new List<EventDelegate>();

	// Token: 0x0400038B RID: 907
	public List<EventDelegate> onDrag = new List<EventDelegate>();
}

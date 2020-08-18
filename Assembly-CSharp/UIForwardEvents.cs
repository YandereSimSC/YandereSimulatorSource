using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
[AddComponentMenu("NGUI/Interaction/Forward Events (Legacy)")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x06000204 RID: 516 RVA: 0x00017AA2 File Offset: 0x00015CA2
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00017AD1 File Offset: 0x00015CD1
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00017B00 File Offset: 0x00015D00
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00017B29 File Offset: 0x00015D29
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00017B52 File Offset: 0x00015D52
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00017B81 File Offset: 0x00015D81
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00017BB0 File Offset: 0x00015DB0
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00017BDA File Offset: 0x00015DDA
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00017C03 File Offset: 0x00015E03
	private void OnScroll(float delta)
	{
		if (this.onScroll && this.target != null)
		{
			this.target.SendMessage("OnScroll", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0400038C RID: 908
	public GameObject target;

	// Token: 0x0400038D RID: 909
	public bool onHover;

	// Token: 0x0400038E RID: 910
	public bool onPress;

	// Token: 0x0400038F RID: 911
	public bool onClick;

	// Token: 0x04000390 RID: 912
	public bool onDoubleClick;

	// Token: 0x04000391 RID: 913
	public bool onSelect;

	// Token: 0x04000392 RID: 914
	public bool onDrag;

	// Token: 0x04000393 RID: 915
	public bool onDrop;

	// Token: 0x04000394 RID: 916
	public bool onSubmit;

	// Token: 0x04000395 RID: 917
	public bool onScroll;
}

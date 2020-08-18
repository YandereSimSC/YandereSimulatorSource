using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
[AddComponentMenu("NGUI/Interaction/Button Message (Legacy)")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x06000184 RID: 388 RVA: 0x00014B18 File Offset: 0x00012D18
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00014B21 File Offset: 0x00012D21
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00014B3C File Offset: 0x00012D3C
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut)))
		{
			this.Send();
		}
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00014B64 File Offset: 0x00012D64
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00014B8C File Offset: 0x00012D8C
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00014BA8 File Offset: 0x00012DA8
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00014BC0 File Offset: 0x00012DC0
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00014BDC File Offset: 0x00012DDC
	private void Send()
	{
		if (string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			this.target = base.gameObject;
		}
		if (this.includeChildren)
		{
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				componentsInChildren[i].gameObject.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
				i++;
			}
			return;
		}
		this.target.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x04000315 RID: 789
	public GameObject target;

	// Token: 0x04000316 RID: 790
	public string functionName;

	// Token: 0x04000317 RID: 791
	public UIButtonMessage.Trigger trigger;

	// Token: 0x04000318 RID: 792
	public bool includeChildren;

	// Token: 0x04000319 RID: 793
	private bool mStarted;

	// Token: 0x02000611 RID: 1553
	[DoNotObfuscateNGUI]
	public enum Trigger
	{
		// Token: 0x04004484 RID: 17540
		OnClick,
		// Token: 0x04004485 RID: 17541
		OnMouseOver,
		// Token: 0x04004486 RID: 17542
		OnMouseOut,
		// Token: 0x04004487 RID: 17543
		OnPress,
		// Token: 0x04004488 RID: 17544
		OnRelease,
		// Token: 0x04004489 RID: 17545
		OnDoubleClick
	}
}

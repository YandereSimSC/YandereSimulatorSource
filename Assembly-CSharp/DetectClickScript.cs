using System;
using UnityEngine;

// Token: 0x02000264 RID: 612
public class DetectClickScript : MonoBehaviour
{
	// Token: 0x06001335 RID: 4917 RVA: 0x000A06A7 File Offset: 0x0009E8A7
	private void Start()
	{
		this.OriginalPosition = base.transform.localPosition;
		this.OriginalColor = this.Sprite.color;
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x000A06CC File Offset: 0x0009E8CC
	private void Update()
	{
		RaycastHit raycastHit;
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(this.GUICamera.ScreenPointToRay(Input.mousePosition), out raycastHit, 100f) && raycastHit.collider == this.MyCollider && this.Label.color.a == 1f)
		{
			this.Sprite.color = new Color(1f, 1f, 1f, 1f);
			this.Clicked = true;
		}
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x000A0755 File Offset: 0x0009E955
	private void OnTriggerEnter()
	{
		if (this.Label.color.a == 1f)
		{
			this.Sprite.color = Color.white;
		}
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x000A077E File Offset: 0x0009E97E
	private void OnTriggerExit()
	{
		this.Sprite.color = this.OriginalColor;
	}

	// Token: 0x040019F0 RID: 6640
	public Vector3 OriginalPosition;

	// Token: 0x040019F1 RID: 6641
	public Color OriginalColor;

	// Token: 0x040019F2 RID: 6642
	public Collider MyCollider;

	// Token: 0x040019F3 RID: 6643
	public Camera GUICamera;

	// Token: 0x040019F4 RID: 6644
	public UISprite Sprite;

	// Token: 0x040019F5 RID: 6645
	public UILabel Label;

	// Token: 0x040019F6 RID: 6646
	public bool Clicked;
}

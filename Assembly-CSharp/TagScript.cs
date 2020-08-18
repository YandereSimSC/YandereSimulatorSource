using System;
using UnityEngine;

// Token: 0x02000411 RID: 1041
public class TagScript : MonoBehaviour
{
	// Token: 0x06001BF2 RID: 7154 RVA: 0x00147464 File Offset: 0x00145664
	private void Start()
	{
		this.Sprite.color = new Color(1f, 0f, 0f, 0f);
		this.MainCameraCamera = this.MainCamera.GetComponent<Camera>();
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x0014749C File Offset: 0x0014569C
	private void Update()
	{
		if (this.Target != null && Vector3.Angle(this.MainCamera.forward, this.MainCamera.position - this.Target.position) > 90f)
		{
			Vector2 vector = this.MainCameraCamera.WorldToScreenPoint(this.Target.position);
			base.transform.position = this.UICamera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, 1f));
		}
	}

	// Token: 0x040033E2 RID: 13282
	public UISprite Sprite;

	// Token: 0x040033E3 RID: 13283
	public Camera UICamera;

	// Token: 0x040033E4 RID: 13284
	public Camera MainCameraCamera;

	// Token: 0x040033E5 RID: 13285
	public Transform MainCamera;

	// Token: 0x040033E6 RID: 13286
	public Transform Target;
}

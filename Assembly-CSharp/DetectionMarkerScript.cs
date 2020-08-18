using System;
using UnityEngine;

// Token: 0x02000266 RID: 614
[Serializable]
public class DetectionMarkerScript : MonoBehaviour
{
	// Token: 0x0600133C RID: 4924 RVA: 0x000A0808 File Offset: 0x0009EA08
	private void Start()
	{
		base.transform.LookAt(new Vector3(this.Target.position.x, base.transform.position.y, this.Target.position.z));
		this.Tex.transform.localScale = new Vector3(1f, 0f, 1f);
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		this.Tex.color = new Color(this.Tex.color.r, this.Tex.color.g, this.Tex.color.b, 0f);
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x000A08E0 File Offset: 0x0009EAE0
	private void Update()
	{
		if (this.Tex.color.a > 0f && base.transform != null && this.Target != null)
		{
			base.transform.LookAt(new Vector3(this.Target.position.x, base.transform.position.y, this.Target.position.z));
		}
	}

	// Token: 0x040019F8 RID: 6648
	public Transform Target;

	// Token: 0x040019F9 RID: 6649
	public UITexture Tex;
}

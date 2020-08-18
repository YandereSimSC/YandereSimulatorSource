using System;
using UnityEngine;

// Token: 0x02000482 RID: 1154
public class YanvaniaTeleportEffectScript : MonoBehaviour
{
	// Token: 0x06001DC5 RID: 7621 RVA: 0x001724DC File Offset: 0x001706DC
	private void Start()
	{
		this.FirstBeam.material.color = new Color(this.FirstBeam.material.color.r, this.FirstBeam.material.color.g, this.FirstBeam.material.color.b, 0f);
		this.SecondBeam.material.color = new Color(this.SecondBeam.material.color.r, this.SecondBeam.material.color.g, this.SecondBeam.material.color.b, 0f);
		this.FirstBeam.transform.localScale = new Vector3(0f, this.FirstBeam.transform.localScale.y, 0f);
		this.SecondBeamParent.transform.localScale = new Vector3(this.SecondBeamParent.transform.localScale.x, 0f, this.SecondBeamParent.transform.localScale.z);
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x04003ADD RID: 15069
	public YanvaniaDraculaScript Dracula;

	// Token: 0x04003ADE RID: 15070
	public Transform SecondBeamParent;

	// Token: 0x04003ADF RID: 15071
	public Renderer SecondBeam;

	// Token: 0x04003AE0 RID: 15072
	public Renderer FirstBeam;

	// Token: 0x04003AE1 RID: 15073
	public bool InformedDracula;

	// Token: 0x04003AE2 RID: 15074
	public float Timer;
}

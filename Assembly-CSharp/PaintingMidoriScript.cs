using System;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class PaintingMidoriScript : MonoBehaviour
{
	// Token: 0x0600189B RID: 6299 RVA: 0x000E1054 File Offset: 0x000DF254
	private void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			this.ID++;
		}
		if (this.ID == 0)
		{
			this.Anim.CrossFade("f02_painting_00");
		}
		else if (this.ID == 1)
		{
			this.Anim.CrossFade("f02_shock_00");
			this.Rotation = Mathf.Lerp(this.Rotation, -180f, Time.deltaTime * 10f);
		}
		else if (this.ID == 2)
		{
			base.transform.position -= new Vector3(Time.deltaTime * 2f, 0f, 0f);
		}
		base.transform.localEulerAngles = new Vector3(0f, this.Rotation, 0f);
	}

	// Token: 0x0400243C RID: 9276
	public Animation Anim;

	// Token: 0x0400243D RID: 9277
	public float Rotation;

	// Token: 0x0400243E RID: 9278
	public int ID;
}

using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class MGPMProjectileScript : MonoBehaviour
{
	// Token: 0x06000091 RID: 145 RVA: 0x000077B0 File Offset: 0x000059B0
	private void Update()
	{
		if (base.gameObject.layer == 8)
		{
			base.transform.Translate(Vector3.up * Time.deltaTime * this.Speed);
		}
		else
		{
			base.transform.Translate(Vector3.forward * Time.deltaTime * this.Speed);
		}
		if (this.Angle == 1)
		{
			base.transform.Translate(Vector3.right * Time.deltaTime * this.Speed * 0.2f);
		}
		else if (this.Angle == -1)
		{
			base.transform.Translate(Vector3.right * Time.deltaTime * this.Speed * -0.2f);
		}
		if (base.transform.localPosition.y > 300f || base.transform.localPosition.y < -300f || base.transform.localPosition.x > 134f || base.transform.localPosition.x < -134f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000115 RID: 277
	public Transform Sprite;

	// Token: 0x04000116 RID: 278
	public int Angle;

	// Token: 0x04000117 RID: 279
	public float Speed;
}

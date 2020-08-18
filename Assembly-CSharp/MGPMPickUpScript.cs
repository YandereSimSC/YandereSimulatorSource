using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class MGPMPickUpScript : MonoBehaviour
{
	// Token: 0x0600008F RID: 143 RVA: 0x00007750 File Offset: 0x00005950
	private void Update()
	{
		base.transform.Translate(Vector3.up * Time.deltaTime * this.Speed * -1f);
		if (base.transform.localPosition.y < -300f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000114 RID: 276
	public float Speed;
}

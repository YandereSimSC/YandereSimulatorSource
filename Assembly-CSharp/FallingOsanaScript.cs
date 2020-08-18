using System;
using UnityEngine;

// Token: 0x0200029E RID: 670
public class FallingOsanaScript : MonoBehaviour
{
	// Token: 0x060013FF RID: 5119 RVA: 0x000AE8F8 File Offset: 0x000ACAF8
	private void Update()
	{
		if (base.transform.parent.position.y > 0f)
		{
			this.Osana.CharacterAnimation.Play(this.Osana.IdleAnim);
			base.transform.parent.position += new Vector3(0f, -1.0001f, 0f);
		}
		if (base.transform.parent.position.y < 0f)
		{
			base.transform.parent.position = new Vector3(base.transform.parent.position.x, 0f, base.transform.parent.position.z);
			UnityEngine.Object.Instantiate<GameObject>(this.GroundImpact, base.transform.parent.position, Quaternion.identity);
		}
	}

	// Token: 0x04001C0B RID: 7179
	public StudentScript Osana;

	// Token: 0x04001C0C RID: 7180
	public GameObject GroundImpact;
}

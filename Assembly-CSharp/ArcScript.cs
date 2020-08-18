using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class ArcScript : MonoBehaviour
{
	// Token: 0x06000A0F RID: 2575 RVA: 0x0004FBB8 File Offset: 0x0004DDB8
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 1f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.ArcTrail, base.transform.position, base.transform.rotation).GetComponent<Rigidbody>().AddRelativeForce(ArcScript.NEW_ARC_RELATIVE_FORCE);
			this.Timer = 0f;
		}
	}

	// Token: 0x04000A10 RID: 2576
	private static readonly Vector3 NEW_ARC_RELATIVE_FORCE = Vector3.forward * 250f;

	// Token: 0x04000A11 RID: 2577
	public GameObject ArcTrail;

	// Token: 0x04000A12 RID: 2578
	public float Timer;
}

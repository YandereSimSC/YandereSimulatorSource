using System;
using UnityEngine;

// Token: 0x020003D6 RID: 982
public class ShadowScript : MonoBehaviour
{
	// Token: 0x06001A6F RID: 6767 RVA: 0x00103874 File Offset: 0x00101A74
	private void Update()
	{
		Vector3 position = base.transform.position;
		Vector3 position2 = this.Foot.position;
		position.x = position2.x;
		position.z = position2.z;
		base.transform.position = position;
	}

	// Token: 0x04002A07 RID: 10759
	public Transform Foot;
}

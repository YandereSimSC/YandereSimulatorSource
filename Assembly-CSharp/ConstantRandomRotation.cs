using System;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class ConstantRandomRotation : MonoBehaviour
{
	// Token: 0x0600128C RID: 4748 RVA: 0x0008AAE8 File Offset: 0x00088CE8
	private void Update()
	{
		int num = UnityEngine.Random.Range(0, 360);
		int num2 = UnityEngine.Random.Range(0, 360);
		int num3 = UnityEngine.Random.Range(0, 360);
		base.transform.Rotate((float)num, (float)num2, (float)num3);
	}
}

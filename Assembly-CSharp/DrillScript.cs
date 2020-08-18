using System;
using UnityEngine;

// Token: 0x02000272 RID: 626
public class DrillScript : MonoBehaviour
{
	// Token: 0x06001365 RID: 4965 RVA: 0x000A683E File Offset: 0x000A4A3E
	private void LateUpdate()
	{
		base.transform.Rotate(Vector3.up * Time.deltaTime * 3600f);
	}
}

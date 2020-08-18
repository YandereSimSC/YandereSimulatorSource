using System;
using UnityEngine;

// Token: 0x02000251 RID: 593
public class CreepyArmScript : MonoBehaviour
{
	// Token: 0x060012C4 RID: 4804 RVA: 0x00095FB8 File Offset: 0x000941B8
	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime * 0.1f, base.transform.position.z);
	}
}

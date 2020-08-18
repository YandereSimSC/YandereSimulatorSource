using System;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class BushSpawnerScript : MonoBehaviour
{
	// Token: 0x06000A9C RID: 2716 RVA: 0x00057D2C File Offset: 0x00055F2C
	private void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			this.Begin = true;
		}
		if (this.Begin)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Bush, new Vector3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(0f, 4f), UnityEngine.Random.Range(-16f, 16f)), Quaternion.identity);
		}
	}

	// Token: 0x04000B36 RID: 2870
	public GameObject Bush;

	// Token: 0x04000B37 RID: 2871
	public bool Begin;
}

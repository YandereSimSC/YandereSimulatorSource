using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class ObstacleDetectorScript : MonoBehaviour
{
	// Token: 0x06001882 RID: 6274 RVA: 0x000E0023 File Offset: 0x000DE223
	private void Start()
	{
		this.ControllerX.SetActive(false);
		this.KeyboardX.SetActive(false);
	}

	// Token: 0x040023FD RID: 9213
	public YandereScript Yandere;

	// Token: 0x040023FE RID: 9214
	public GameObject ControllerX;

	// Token: 0x040023FF RID: 9215
	public GameObject KeyboardX;

	// Token: 0x04002400 RID: 9216
	public Collider[] ObstacleArray;

	// Token: 0x04002401 RID: 9217
	public int Obstacles;

	// Token: 0x04002402 RID: 9218
	public bool Add;

	// Token: 0x04002403 RID: 9219
	public int ID;
}

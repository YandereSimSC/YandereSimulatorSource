using System;
using UnityEngine;

// Token: 0x0200036B RID: 875
public class PoliceWalk : MonoBehaviour
{
	// Token: 0x0600190D RID: 6413 RVA: 0x000EC464 File Offset: 0x000EA664
	private void Update()
	{
		Vector3 position = base.transform.position;
		position.z += Time.deltaTime;
		base.transform.position = position;
	}
}

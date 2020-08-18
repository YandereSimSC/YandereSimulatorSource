using System;
using UnityEngine;

// Token: 0x020002A5 RID: 677
public class FollowYandereScript : MonoBehaviour
{
	// Token: 0x06001410 RID: 5136 RVA: 0x000AFB40 File Offset: 0x000ADD40
	private void Update()
	{
		base.transform.position = new Vector3(this.Yandere.position.x, base.transform.position.y, this.Yandere.position.z);
	}

	// Token: 0x04001C46 RID: 7238
	public Transform Yandere;
}

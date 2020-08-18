using System;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class HomeCorkboardPhotoScript : MonoBehaviour
{
	// Token: 0x0600170B RID: 5899 RVA: 0x000C2AE4 File Offset: 0x000C0CE4
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 4)
		{
			base.transform.localScale = new Vector3(Mathf.MoveTowards(base.transform.localScale.x, 1f, Time.deltaTime * 10f), Mathf.MoveTowards(base.transform.localScale.y, 1f, Time.deltaTime * 10f), Mathf.MoveTowards(base.transform.localScale.z, 1f, Time.deltaTime * 10f));
		}
	}

	// Token: 0x04001F2C RID: 7980
	public int ArrayID;

	// Token: 0x04001F2D RID: 7981
	public int ID;
}

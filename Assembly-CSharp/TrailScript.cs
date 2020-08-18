using System;
using UnityEngine;

// Token: 0x0200042B RID: 1067
public class TrailScript : MonoBehaviour
{
	// Token: 0x06001C5A RID: 7258 RVA: 0x00152DBB File Offset: 0x00150FBB
	private void Start()
	{
		Physics.IgnoreCollision(GameObject.Find("YandereChan").GetComponent<Collider>(), base.GetComponent<Collider>());
		UnityEngine.Object.Destroy(this);
	}
}

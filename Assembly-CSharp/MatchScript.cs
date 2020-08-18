using System;
using UnityEngine;

// Token: 0x02000329 RID: 809
public class MatchScript : MonoBehaviour
{
	// Token: 0x0600180F RID: 6159 RVA: 0x000D64C0 File Offset: 0x000D46C0
	private void Update()
	{
		if (base.GetComponent<Rigidbody>().useGravity)
		{
			base.transform.Rotate(Vector3.right * (Time.deltaTime * 360f));
			if (this.Timer > 0f && this.MyCollider.isTrigger)
			{
				this.MyCollider.isTrigger = false;
			}
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z - Time.deltaTime);
				if (base.transform.localScale.z < 0f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x040022CE RID: 8910
	public float Timer;

	// Token: 0x040022CF RID: 8911
	public Collider MyCollider;
}

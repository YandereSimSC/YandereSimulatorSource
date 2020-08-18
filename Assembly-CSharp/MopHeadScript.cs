using System;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class MopHeadScript : MonoBehaviour
{
	// Token: 0x06001836 RID: 6198 RVA: 0x000D87A0 File Offset: 0x000D69A0
	private void OnTriggerStay(Collider other)
	{
		if (this.Mop.Bloodiness < 100f && other.tag == "Puddle")
		{
			this.BloodPool = other.gameObject.GetComponent<BloodPoolScript>();
			if (this.BloodPool != null)
			{
				this.BloodPool.Grow = false;
				other.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
				if (this.BloodPool.Blood)
				{
					this.Mop.Bloodiness += Time.deltaTime * 10f;
					this.Mop.UpdateBlood();
				}
				if (other.transform.localScale.x < 0.1f)
				{
					UnityEngine.Object.Destroy(other.gameObject);
					return;
				}
			}
			else
			{
				UnityEngine.Object.Destroy(other.gameObject);
			}
		}
	}

	// Token: 0x04002319 RID: 8985
	public BloodPoolScript BloodPool;

	// Token: 0x0400231A RID: 8986
	public MopScript Mop;
}

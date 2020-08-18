using System;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class BloodSprayColliderScript : MonoBehaviour
{
	// Token: 0x06000A5D RID: 2653 RVA: 0x000555BC File Offset: 0x000537BC
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 13)
		{
			YandereScript component = other.gameObject.GetComponent<YandereScript>();
			if (component != null)
			{
				component.Bloodiness = 100f;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}

using System;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class HideColliderScript : MonoBehaviour
{
	// Token: 0x060016F9 RID: 5881 RVA: 0x000C1ACC File Offset: 0x000BFCCC
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 11)
		{
			GameObject gameObject = other.gameObject.transform.root.gameObject;
			if (!gameObject.GetComponent<StudentScript>().Alive)
			{
				this.Corpse = gameObject.GetComponent<RagdollScript>();
				if (!this.Corpse.Hidden)
				{
					this.Corpse.HideCollider = this.MyCollider;
					this.Corpse.Police.HiddenCorpses++;
					this.Corpse.Hidden = true;
				}
			}
		}
	}

	// Token: 0x04001EED RID: 7917
	public RagdollScript Corpse;

	// Token: 0x04001EEE RID: 7918
	public Collider MyCollider;
}

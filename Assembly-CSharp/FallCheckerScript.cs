using System;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class FallCheckerScript : MonoBehaviour
{
	// Token: 0x060013FC RID: 5116 RVA: 0x000AE6D4 File Offset: 0x000AC8D4
	private void OnTriggerEnter(Collider other)
	{
		if (this.Ragdoll == null && other.gameObject.layer == 11)
		{
			this.Ragdoll = other.transform.root.gameObject.GetComponent<RagdollScript>();
			this.Ragdoll.Prompt.Hide();
			this.Ragdoll.Prompt.enabled = false;
			this.Ragdoll.Prompt.MyCollider.enabled = false;
			this.Ragdoll.BloodPoolSpawner.enabled = false;
			this.Ragdoll.HideCollider = this.MyCollider;
			this.Ragdoll.Police.HiddenCorpses++;
			this.Ragdoll.Hidden = true;
			this.Dumpster.Corpse = this.Ragdoll.gameObject;
			this.Dumpster.Victim = this.Ragdoll.Student;
		}
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x000AE7C8 File Offset: 0x000AC9C8
	private void Update()
	{
		if (this.Ragdoll != null)
		{
			if (this.Ragdoll.Prompt.transform.localPosition.y > -10.5f)
			{
				this.Ragdoll.Prompt.transform.localEulerAngles = new Vector3(-90f, 90f, 0f);
				this.Ragdoll.AllColliders[2].transform.localEulerAngles = Vector3.zero;
				this.Ragdoll.AllColliders[7].transform.localEulerAngles = new Vector3(0f, 0f, -80f);
				this.Ragdoll.Prompt.transform.position = new Vector3(this.Dumpster.transform.position.x, this.Ragdoll.Prompt.transform.position.y, this.Dumpster.transform.position.z);
				return;
			}
			base.GetComponent<AudioSource>().Play();
			this.Dumpster.Slide = true;
			this.Ragdoll = null;
		}
	}

	// Token: 0x04001C08 RID: 7176
	public DumpsterLidScript Dumpster;

	// Token: 0x04001C09 RID: 7177
	public RagdollScript Ragdoll;

	// Token: 0x04001C0A RID: 7178
	public Collider MyCollider;
}

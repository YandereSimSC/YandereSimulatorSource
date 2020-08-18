using System;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class RooftopCorpseDisposalScript : MonoBehaviour
{
	// Token: 0x060019CC RID: 6604 RVA: 0x000FC3D7 File Offset: 0x000FA5D7
	private void Start()
	{
		if (SchoolGlobals.RoofFence)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060019CD RID: 6605 RVA: 0x000FC3EC File Offset: 0x000FA5EC
	private void Update()
	{
		if (this.MyCollider.bounds.Contains(this.Yandere.transform.position))
		{
			if (!(this.Yandere.Ragdoll != null))
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				return;
			}
			if (!this.Yandere.Dropping)
			{
				this.Prompt.enabled = true;
				this.Prompt.transform.position = new Vector3(this.Yandere.transform.position.x, this.Yandere.transform.position.y + 1.66666f, this.Yandere.transform.position.z);
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.DropSpot.position = new Vector3(this.DropSpot.position.x, this.DropSpot.position.y, this.Yandere.transform.position.z);
					this.Yandere.CharacterAnimation.CrossFade(this.Yandere.Carrying ? "f02_carryIdleA_00" : "f02_dragIdle_00");
					this.Yandere.DropSpot = this.DropSpot;
					this.Yandere.Dropping = true;
					this.Yandere.CanMove = false;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					this.Yandere.Ragdoll.GetComponent<RagdollScript>().BloodPoolSpawner.Falling = true;
					return;
				}
			}
		}
		else
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x0400285D RID: 10333
	public YandereScript Yandere;

	// Token: 0x0400285E RID: 10334
	public PromptScript Prompt;

	// Token: 0x0400285F RID: 10335
	public Collider MyCollider;

	// Token: 0x04002860 RID: 10336
	public Transform DropSpot;
}

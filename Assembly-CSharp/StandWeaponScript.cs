using System;
using UnityEngine;

// Token: 0x020003F5 RID: 1013
public class StandWeaponScript : MonoBehaviour
{
	// Token: 0x06001AE4 RID: 6884 RVA: 0x0010E098 File Offset: 0x0010C298
	private void Update()
	{
		if (this.Prompt.enabled)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.MoveToStand();
				return;
			}
		}
		else
		{
			base.transform.Rotate(Vector3.forward * (Time.deltaTime * this.RotationSpeed));
			base.transform.Rotate(Vector3.right * (Time.deltaTime * this.RotationSpeed));
			base.transform.Rotate(Vector3.up * (Time.deltaTime * this.RotationSpeed));
		}
	}

	// Token: 0x06001AE5 RID: 6885 RVA: 0x0010E138 File Offset: 0x0010C338
	private void MoveToStand()
	{
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Prompt.MyCollider.enabled = false;
		this.Stand.Weapons++;
		base.transform.parent = this.Stand.Hands[this.Stand.Weapons];
		base.transform.localPosition = new Vector3(-0.277f, 0f, 0f);
	}

	// Token: 0x04002B9B RID: 11163
	public PromptScript Prompt;

	// Token: 0x04002B9C RID: 11164
	public StandScript Stand;

	// Token: 0x04002B9D RID: 11165
	public float RotationSpeed;
}

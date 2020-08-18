using System;
using UnityEngine;

// Token: 0x02000229 RID: 553
public class CarryableCardboardBoxScript : MonoBehaviour
{
	// Token: 0x0600121E RID: 4638 RVA: 0x0007FE40 File Offset: 0x0007E040
	private void Update()
	{
		if (!this.Closed)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Label[0].text = "     Insert Box Cutter";
				this.MyRenderer.mesh = this.ClosedMesh;
				this.Prompt.HideButton[0] = true;
				this.Closed = true;
				return;
			}
		}
		else if (this.MyCutter == null)
		{
			this.Prompt.HideButton[0] = true;
			if (!this.Prompt.Yandere.Armed)
			{
				this.Prompt.HideButton[0] = true;
				return;
			}
			if (this.Prompt.Yandere.EquippedWeapon.WeaponID != 5 || this.Prompt.Yandere.EquippedWeapon.Blood.enabled)
			{
				this.Prompt.HideButton[0] = true;
				return;
			}
			this.Prompt.HideButton[0] = false;
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.MyCutter = this.Prompt.Yandere.EquippedWeapon;
				Physics.IgnoreCollision(base.GetComponent<Collider>(), this.MyCutter.MyCollider);
				this.Prompt.Yandere.DropTimer[this.Prompt.Yandere.Equipped] = 0.5f;
				this.Prompt.Yandere.DropWeapon(this.Prompt.Yandere.Equipped);
				this.MyCutter.MyRigidbody.useGravity = false;
				this.MyCutter.MyRigidbody.isKinematic = true;
				this.MyCutter.MyCollider.isTrigger = true;
				this.MyCutter.transform.parent = base.transform;
				this.MyCutter.transform.localPosition = new Vector3(0f, 0.24f, 0f);
				this.MyCutter.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
				this.MyCutter.Prompt.Hide();
				this.MyCutter.Prompt.enabled = false;
				this.MyCutter.enabled = false;
				this.MyCutter.gameObject.SetActive(true);
				this.Prompt.HideButton[0] = true;
				this.Prompt.HideButton[3] = false;
				this.PickUp.StuckBoxCutter = this.MyCutter;
				this.PickUp.enabled = true;
				return;
			}
		}
		else if (this.MyCutter.transform.parent != base.transform)
		{
			this.MyCutter = null;
		}
	}

	// Token: 0x04001543 RID: 5443
	public WeaponScript MyCutter;

	// Token: 0x04001544 RID: 5444
	public PickUpScript PickUp;

	// Token: 0x04001545 RID: 5445
	public PromptScript Prompt;

	// Token: 0x04001546 RID: 5446
	public MeshFilter MyRenderer;

	// Token: 0x04001547 RID: 5447
	public Mesh ClosedMesh;

	// Token: 0x04001548 RID: 5448
	public bool Closed;
}

using System;
using UnityEngine;

// Token: 0x0200042F RID: 1071
public class TrashCanScript : MonoBehaviour
{
	// Token: 0x06001C65 RID: 7269 RVA: 0x00153598 File Offset: 0x00151798
	private void Update()
	{
		if (!this.Occupied)
		{
			if (this.Prompt.HideButton[0])
			{
				if (this.Yandere.Armed)
				{
					this.UpdatePrompt();
				}
			}
			else if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				if (this.Yandere.PickUp != null)
				{
					this.Item = this.Yandere.PickUp.gameObject;
					this.Yandere.MyController.radius = 0.5f;
					this.Yandere.EmptyHands();
				}
				else
				{
					this.Item = this.Yandere.EquippedWeapon.gameObject;
					this.Yandere.DropTimer[this.Yandere.Equipped] = 0.5f;
					this.Yandere.DropWeapon(this.Yandere.Equipped);
					this.Weapon = true;
				}
				this.Item.transform.parent = this.TrashPosition;
				this.Item.GetComponent<Rigidbody>().useGravity = false;
				this.Item.GetComponent<Collider>().enabled = false;
				this.Item.GetComponent<PromptScript>().Hide();
				this.Item.GetComponent<PromptScript>().enabled = false;
				this.Occupied = true;
				this.UpdatePrompt();
			}
		}
		else if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.Item.GetComponent<PromptScript>().Circle[3].fillAmount = -1f;
			this.Item.GetComponent<PromptScript>().enabled = true;
			this.Item = null;
			this.Occupied = false;
			this.Weapon = false;
			this.UpdatePrompt();
		}
		if (this.Item != null)
		{
			if (this.Weapon)
			{
				this.Item.transform.localPosition = new Vector3(0f, 0.29f, 0f);
				this.Item.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
			}
			else
			{
				this.Item.transform.localPosition = new Vector3(0f, 0f, -0.021f);
				this.Item.transform.localEulerAngles = Vector3.zero;
			}
		}
		if (this.Wearable && this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.Circle[3].fillAmount = 1f;
			base.transform.parent = this.Prompt.Yandere.Backpack;
			base.transform.localPosition = Vector3.zero;
			base.transform.localEulerAngles = new Vector3(90f, -154f, 0f);
			this.Prompt.Yandere.Container = this.Container;
			this.Prompt.Yandere.WeaponMenu.UpdateSprites();
			this.Prompt.Yandere.ObstacleDetector.gameObject.SetActive(true);
			this.Prompt.MyCollider.enabled = false;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			Rigidbody component = base.GetComponent<Rigidbody>();
			component.isKinematic = true;
			component.useGravity = false;
		}
	}

	// Token: 0x06001C66 RID: 7270 RVA: 0x0015392C File Offset: 0x00151B2C
	public void UpdatePrompt()
	{
		if (this.Occupied)
		{
			this.Prompt.Label[0].text = "     Remove";
			this.Prompt.HideButton[0] = false;
			return;
		}
		if (this.Yandere.Armed)
		{
			this.Prompt.Label[0].text = "     Insert";
			this.Prompt.HideButton[0] = false;
			return;
		}
		if (!(this.Yandere.PickUp != null))
		{
			this.Prompt.HideButton[0] = true;
			return;
		}
		if (this.Yandere.PickUp.Evidence || this.Yandere.PickUp.Suspicious)
		{
			this.Prompt.Label[0].text = "     Insert";
			this.Prompt.HideButton[0] = false;
			return;
		}
		this.Prompt.HideButton[0] = true;
	}

	// Token: 0x0400354D RID: 13645
	public ContainerScript Container;

	// Token: 0x0400354E RID: 13646
	public YandereScript Yandere;

	// Token: 0x0400354F RID: 13647
	public PromptScript Prompt;

	// Token: 0x04003550 RID: 13648
	public Transform TrashPosition;

	// Token: 0x04003551 RID: 13649
	public GameObject Item;

	// Token: 0x04003552 RID: 13650
	public bool Occupied;

	// Token: 0x04003553 RID: 13651
	public bool Wearable;

	// Token: 0x04003554 RID: 13652
	public bool Weapon;
}

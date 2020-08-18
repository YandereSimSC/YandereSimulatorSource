using System;
using UnityEngine;

// Token: 0x02000247 RID: 583
public class ContainerScript : MonoBehaviour
{
	// Token: 0x0600128E RID: 4750 RVA: 0x0008AB2C File Offset: 0x00088D2C
	public void Start()
	{
		this.GardenArea = GameObject.Find("GardenArea").GetComponent<Collider>();
		this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
		this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
		this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
		this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x0008ABA4 File Offset: 0x00088DA4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.Open = !this.Open;
			this.UpdatePrompts();
		}
		if (this.Prompt.Circle[1].fillAmount == 0f)
		{
			this.Prompt.Circle[1].fillAmount = 1f;
			if (this.Prompt.Yandere.Armed)
			{
				this.Weapon = this.Prompt.Yandere.EquippedWeapon;
				this.Prompt.Yandere.EmptyHands();
				this.Weapon.transform.parent = this.WeaponSpot;
				this.Weapon.transform.localPosition = Vector3.zero;
				this.Weapon.transform.localEulerAngles = Vector3.zero;
				this.Weapon.gameObject.GetComponent<Rigidbody>().useGravity = false;
				this.Weapon.MyCollider.enabled = false;
				this.Weapon.Prompt.Hide();
				this.Weapon.Prompt.enabled = false;
			}
			else
			{
				this.BodyPart = this.Prompt.Yandere.PickUp;
				this.Prompt.Yandere.EmptyHands();
				this.BodyPart.transform.parent = this.BodyPartPositions[this.BodyPart.GetComponent<BodyPartScript>().Type];
				this.BodyPart.transform.localPosition = Vector3.zero;
				this.BodyPart.transform.localEulerAngles = Vector3.zero;
				this.BodyPart.gameObject.GetComponent<Rigidbody>().useGravity = false;
				this.BodyPart.MyCollider.enabled = false;
				this.BodyParts[this.BodyPart.GetComponent<BodyPartScript>().Type] = this.BodyPart;
			}
			this.Contents++;
			this.UpdatePrompts();
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.Circle[3].fillAmount = 1f;
			if (!this.Open)
			{
				base.transform.parent = this.Prompt.Yandere.Backpack;
				base.transform.localPosition = Vector3.zero;
				base.transform.localEulerAngles = Vector3.zero;
				this.Prompt.Yandere.Container = this;
				this.Prompt.Yandere.WeaponMenu.UpdateSprites();
				this.Prompt.Yandere.ObstacleDetector.gameObject.SetActive(true);
				this.Prompt.MyCollider.enabled = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				Rigidbody component = base.GetComponent<Rigidbody>();
				component.isKinematic = true;
				component.useGravity = false;
			}
			else
			{
				if (this.Weapon != null)
				{
					this.Weapon.Prompt.Circle[3].fillAmount = -1f;
					this.Weapon.Prompt.enabled = true;
					this.Weapon = null;
				}
				else
				{
					this.BodyPart = null;
					this.ID = 1;
					while (this.BodyPart == null)
					{
						this.BodyPart = this.BodyParts[this.ID];
						this.BodyParts[this.ID] = null;
						this.ID++;
					}
					this.BodyPart.Prompt.Circle[3].fillAmount = -1f;
				}
				this.Contents--;
				this.UpdatePrompts();
			}
		}
		this.Lid.localEulerAngles = new Vector3(this.Lid.localEulerAngles.x, this.Lid.localEulerAngles.y, Mathf.Lerp(this.Lid.localEulerAngles.z, this.Open ? 90f : 0f, Time.deltaTime * 10f));
		if (this.Weapon != null)
		{
			this.Weapon.transform.localPosition = Vector3.zero;
			this.Weapon.transform.localEulerAngles = Vector3.zero;
		}
		this.ID = 1;
		while (this.ID < this.BodyParts.Length)
		{
			if (this.BodyParts[this.ID] != null)
			{
				this.BodyParts[this.ID].transform.localPosition = Vector3.zero;
				this.BodyParts[this.ID].transform.localEulerAngles = Vector3.zero;
			}
			this.ID++;
		}
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x0008B08C File Offset: 0x0008928C
	public void Drop()
	{
		base.transform.parent = null;
		if (base.enabled)
		{
			base.transform.position = this.Prompt.Yandere.ObstacleDetector.transform.position + new Vector3(0f, 0.5f, 0f);
			base.transform.eulerAngles = this.Prompt.Yandere.ObstacleDetector.transform.eulerAngles;
		}
		this.Prompt.Yandere.Container = null;
		this.Prompt.MyCollider.enabled = true;
		this.Prompt.enabled = true;
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.isKinematic = false;
		component.useGravity = true;
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x0008B154 File Offset: 0x00089354
	public void UpdatePrompts()
	{
		if (!this.Open)
		{
			if (this.Prompt.Label[0] != null)
			{
				this.Prompt.Label[0].text = "     Open";
				this.Prompt.HideButton[1] = true;
				this.Prompt.Label[3].text = "     Wear";
				this.Prompt.HideButton[3] = false;
			}
			return;
		}
		this.Prompt.Label[0].text = "     Close";
		if (this.Contents > 0)
		{
			this.Prompt.Label[3].text = "     Remove";
			this.Prompt.HideButton[3] = false;
		}
		else
		{
			this.Prompt.HideButton[3] = true;
		}
		if (this.Prompt.Yandere.Armed)
		{
			if (this.Prompt.Yandere.EquippedWeapon.Concealable)
			{
				this.Prompt.HideButton[1] = true;
				return;
			}
			if (this.Weapon == null)
			{
				this.Prompt.Label[1].text = "     Insert";
				this.Prompt.HideButton[1] = false;
				return;
			}
			this.Prompt.HideButton[1] = true;
			return;
		}
		else
		{
			if (!(this.Prompt.Yandere.PickUp != null))
			{
				this.Prompt.HideButton[1] = true;
				return;
			}
			if (!(this.Prompt.Yandere.PickUp.BodyPart != null))
			{
				this.Prompt.HideButton[1] = true;
				return;
			}
			if (this.BodyParts[this.Prompt.Yandere.PickUp.gameObject.GetComponent<BodyPartScript>().Type] == null)
			{
				this.Prompt.Label[1].text = "     Insert";
				this.Prompt.HideButton[1] = false;
				return;
			}
			this.Prompt.HideButton[1] = true;
			return;
		}
	}

	// Token: 0x040016B8 RID: 5816
	public Transform[] BodyPartPositions;

	// Token: 0x040016B9 RID: 5817
	public Transform WeaponSpot;

	// Token: 0x040016BA RID: 5818
	public Transform Lid;

	// Token: 0x040016BB RID: 5819
	public Collider GardenArea;

	// Token: 0x040016BC RID: 5820
	public Collider NEStairs;

	// Token: 0x040016BD RID: 5821
	public Collider NWStairs;

	// Token: 0x040016BE RID: 5822
	public Collider SEStairs;

	// Token: 0x040016BF RID: 5823
	public Collider SWStairs;

	// Token: 0x040016C0 RID: 5824
	public PickUpScript[] BodyParts;

	// Token: 0x040016C1 RID: 5825
	public PickUpScript BodyPart;

	// Token: 0x040016C2 RID: 5826
	public WeaponScript Weapon;

	// Token: 0x040016C3 RID: 5827
	public PromptScript Prompt;

	// Token: 0x040016C4 RID: 5828
	public string SpriteName = string.Empty;

	// Token: 0x040016C5 RID: 5829
	public bool CanDrop;

	// Token: 0x040016C6 RID: 5830
	public bool Open;

	// Token: 0x040016C7 RID: 5831
	public int Contents;

	// Token: 0x040016C8 RID: 5832
	public int ID;
}

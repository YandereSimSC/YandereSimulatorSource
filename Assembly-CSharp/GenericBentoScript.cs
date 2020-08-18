using System;
using UnityEngine;

// Token: 0x020002B2 RID: 690
public class GenericBentoScript : MonoBehaviour
{
	// Token: 0x0600143D RID: 5181 RVA: 0x000B2D5C File Offset: 0x000B0F5C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Inventory.EmeticPoison)
			{
				this.Prompt.Yandere.Inventory.EmeticPoison = false;
				this.Prompt.Yandere.PoisonType = 1;
			}
			else
			{
				this.Prompt.Yandere.Inventory.RatPoison = false;
				this.Prompt.Yandere.PoisonType = 3;
			}
			this.Emetic = true;
			this.ShutOff();
			return;
		}
		if (this.Prompt.Circle[1].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Inventory.Sedative)
			{
				this.Prompt.Yandere.Inventory.Sedative = false;
			}
			else
			{
				this.Prompt.Yandere.Inventory.Tranquilizer = false;
			}
			this.Prompt.Yandere.PoisonType = 4;
			this.Tranquil = true;
			this.ShutOff();
			return;
		}
		if (this.Prompt.Circle[2].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Inventory.LethalPoison)
			{
				this.Prompt.Yandere.Inventory.LethalPoison = false;
				this.Prompt.Yandere.PoisonType = 2;
			}
			else
			{
				this.Prompt.Yandere.Inventory.ChemicalPoison = false;
				this.Prompt.Yandere.PoisonType = 2;
			}
			this.Lethal = true;
			this.ShutOff();
			return;
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.HeadachePoison = false;
			this.Prompt.Yandere.PoisonType = 5;
			this.Headache = true;
			this.ShutOff();
		}
	}

	// Token: 0x0600143E RID: 5182 RVA: 0x000B2F50 File Offset: 0x000B1150
	private void ShutOff()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, base.transform.position, Quaternion.identity);
		this.PoisonSpot = gameObject.transform;
		this.PoisonSpot.position = new Vector3(this.PoisonSpot.position.x, this.Prompt.Yandere.transform.position.y, this.PoisonSpot.position.z);
		this.PoisonSpot.LookAt(this.Prompt.Yandere.transform.position);
		this.PoisonSpot.Translate(Vector3.forward * 0.25f);
		this.Prompt.Yandere.CharacterAnimation["f02_poisoning_00"].speed = 2f;
		this.Prompt.Yandere.CharacterAnimation.CrossFade("f02_poisoning_00");
		this.Prompt.Yandere.StudentManager.UpdateAllBentos();
		this.Prompt.Yandere.TargetBento = this;
		this.Prompt.Yandere.Poisoning = true;
		this.Prompt.Yandere.CanMove = false;
		this.Tampered = true;
		base.enabled = false;
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x000B30B4 File Offset: 0x000B12B4
	public void UpdatePrompts()
	{
		this.Prompt.HideButton[0] = true;
		this.Prompt.HideButton[1] = true;
		this.Prompt.HideButton[2] = true;
		this.Prompt.HideButton[3] = true;
		if (this.Prompt.Yandere.Inventory.EmeticPoison || this.Prompt.Yandere.Inventory.RatPoison)
		{
			this.Prompt.HideButton[0] = false;
		}
		if (this.Prompt.Yandere.Inventory.Tranquilizer || this.Prompt.Yandere.Inventory.Sedative)
		{
			this.Prompt.HideButton[1] = false;
		}
		if (this.Prompt.Yandere.Inventory.LethalPoison || this.Prompt.Yandere.Inventory.ChemicalPoison)
		{
			this.Prompt.HideButton[2] = false;
		}
		if (this.Prompt.Yandere.Inventory.HeadachePoison)
		{
			this.Prompt.HideButton[3] = false;
		}
	}

	// Token: 0x04001CD9 RID: 7385
	public GameObject EmptyGameObject;

	// Token: 0x04001CDA RID: 7386
	public Transform PoisonSpot;

	// Token: 0x04001CDB RID: 7387
	public PromptScript Prompt;

	// Token: 0x04001CDC RID: 7388
	public bool Emetic;

	// Token: 0x04001CDD RID: 7389
	public bool Tranquil;

	// Token: 0x04001CDE RID: 7390
	public bool Headache;

	// Token: 0x04001CDF RID: 7391
	public bool Lethal;

	// Token: 0x04001CE0 RID: 7392
	public bool Tampered;

	// Token: 0x04001CE1 RID: 7393
	public int StudentID;
}

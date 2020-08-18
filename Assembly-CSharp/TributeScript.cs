using System;
using UnityEngine;

// Token: 0x02000432 RID: 1074
public class TributeScript : MonoBehaviour
{
	// Token: 0x06001C6F RID: 7279 RVA: 0x00153CF4 File Offset: 0x00151EF4
	private void Start()
	{
		if (GameGlobals.LoveSick || MissionModeGlobals.MissionMode)
		{
			base.enabled = false;
		}
		this.Rainey.SetActive(false);
	}

	// Token: 0x06001C70 RID: 7280 RVA: 0x00153D18 File Offset: 0x00151F18
	private void Update()
	{
		if (this.RiggedAttacher.gameObject.activeInHierarchy)
		{
			this.RiggedAttacher.newRenderer.SetBlendShapeWeight(0, 100f);
			this.RiggedAttacher.newRenderer.SetBlendShapeWeight(1, 100f);
			base.enabled = false;
			return;
		}
		if (!this.Yandere.PauseScreen.Show && this.Yandere.CanMove)
		{
			if (Input.GetKeyDown(this.Letter[this.ID]))
			{
				this.ID++;
				if (this.ID == this.Letter.Length)
				{
					this.Rainey.SetActive(true);
					base.enabled = false;
				}
			}
			if (Input.GetKeyDown(this.AzurLane[this.AzurID]))
			{
				this.AzurID++;
				if (this.AzurID == this.AzurLane.Length)
				{
					this.Yandere.AzurLane();
					base.enabled = false;
				}
			}
			if (Input.GetKeyDown(this.NurseLetters[this.NurseID]))
			{
				this.NurseID++;
				if (this.NurseID == this.NurseLetters.Length)
				{
					this.RiggedAttacher.root = this.StudentManager.Students[90].Hips.parent.gameObject;
					this.RiggedAttacher.Student = this.StudentManager.Students[90];
					this.RiggedAttacher.gameObject.SetActive(true);
					this.StudentManager.Students[90].MyRenderer.enabled = false;
				}
			}
			if (this.Yandere.Armed && this.Yandere.EquippedWeapon.WeaponID == 14 && Input.GetKeyDown(this.MiyukiLetters[this.MiyukiID]))
			{
				this.MiyukiID++;
				if (this.MiyukiID == this.MiyukiLetters.Length)
				{
					this.Henshin.TransformYandere();
					this.Yandere.CanMove = false;
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x04003560 RID: 13664
	public RiggedAccessoryAttacher RiggedAttacher;

	// Token: 0x04003561 RID: 13665
	public StudentManagerScript StudentManager;

	// Token: 0x04003562 RID: 13666
	public HenshinScript Henshin;

	// Token: 0x04003563 RID: 13667
	public YandereScript Yandere;

	// Token: 0x04003564 RID: 13668
	public GameObject Rainey;

	// Token: 0x04003565 RID: 13669
	public string[] MiyukiLetters;

	// Token: 0x04003566 RID: 13670
	public string[] NurseLetters;

	// Token: 0x04003567 RID: 13671
	public string[] AzurLane;

	// Token: 0x04003568 RID: 13672
	public string[] Letter;

	// Token: 0x04003569 RID: 13673
	public int MiyukiID;

	// Token: 0x0400356A RID: 13674
	public int NurseID;

	// Token: 0x0400356B RID: 13675
	public int AzurID;

	// Token: 0x0400356C RID: 13676
	public int ID;

	// Token: 0x0400356D RID: 13677
	public Mesh ThiccMesh;
}

﻿using System;
using UnityEngine;

// Token: 0x02000234 RID: 564
public class ChemistScannerScript : MonoBehaviour
{
	// Token: 0x06001238 RID: 4664 RVA: 0x00080E4C File Offset: 0x0007F04C
	private void Update()
	{
		if (this.Student.Ragdoll != null && this.Student.Ragdoll.enabled)
		{
			this.MyRenderer.materials[1].mainTexture = this.DeadEyes;
			base.enabled = false;
			return;
		}
		if (this.Student.Dying)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.AlarmedEyes)
			{
				this.MyRenderer.materials[1].mainTexture = this.AlarmedEyes;
				return;
			}
		}
		else if (this.Student.Emetic || this.Student.Lethal || this.Student.Tranquil || this.Student.Headache)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.Textures[6])
			{
				this.MyRenderer.materials[1].mainTexture = this.Textures[6];
				return;
			}
		}
		else if (this.Student.Grudge)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.Textures[1])
			{
				this.MyRenderer.materials[1].mainTexture = this.Textures[1];
				return;
			}
		}
		else if (this.Student.LostTeacherTrust)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.SadEyes)
			{
				this.MyRenderer.materials[1].mainTexture = this.SadEyes;
				return;
			}
		}
		else if (this.Student.WitnessedMurder || this.Student.WitnessedCorpse)
		{
			if (this.MyRenderer.materials[1].mainTexture != this.AlarmedEyes)
			{
				this.MyRenderer.materials[1].mainTexture = this.AlarmedEyes;
				return;
			}
		}
		else
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 2f)
			{
				while (this.ID == this.PreviousID)
				{
					this.ID = UnityEngine.Random.Range(0, this.Textures.Length);
				}
				this.MyRenderer.materials[1].mainTexture = this.Textures[this.ID];
				this.PreviousID = this.ID;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x0400157C RID: 5500
	public StudentScript Student;

	// Token: 0x0400157D RID: 5501
	public Renderer MyRenderer;

	// Token: 0x0400157E RID: 5502
	public Texture AlarmedEyes;

	// Token: 0x0400157F RID: 5503
	public Texture DeadEyes;

	// Token: 0x04001580 RID: 5504
	public Texture SadEyes;

	// Token: 0x04001581 RID: 5505
	public Texture[] Textures;

	// Token: 0x04001582 RID: 5506
	public float Timer;

	// Token: 0x04001583 RID: 5507
	public int PreviousID;

	// Token: 0x04001584 RID: 5508
	public int ID;
}

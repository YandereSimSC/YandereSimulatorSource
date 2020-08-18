using System;
using UnityEngine;

// Token: 0x020003A3 RID: 931
public class RobotArmScript : MonoBehaviour
{
	// Token: 0x060019C8 RID: 6600 RVA: 0x000FBCC8 File Offset: 0x000F9EC8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.ActivateArms();
		}
		if (this.Prompt.Circle[1].fillAmount == 0f)
		{
			this.ToggleWork();
		}
		if (this.UpdateArms)
		{
			if (this.On[0])
			{
				this.ArmValue[0] = Mathf.Lerp(this.ArmValue[0], 0f, Time.deltaTime * 5f);
				this.RobotArms.SetBlendShapeWeight(0, this.ArmValue[0]);
				if (this.ArmValue[0] < 0.1f)
				{
					this.RobotArms.SetBlendShapeWeight(0, 0f);
					this.UpdateArms = false;
					this.ArmValue[0] = 0f;
				}
			}
			else
			{
				this.ArmValue[0] = Mathf.Lerp(this.ArmValue[0], 100f, Time.deltaTime * 5f);
				this.RobotArms.SetBlendShapeWeight(0, this.ArmValue[0]);
				if (this.ArmValue[0] > 99.9f)
				{
					this.RobotArms.SetBlendShapeWeight(0, 100f);
					this.UpdateArms = false;
					this.ArmValue[0] = 100f;
				}
			}
		}
		if (this.Work)
		{
			if (this.StartWorkTimer <= 0f)
			{
				this.ID = 1;
				while (this.ID < 9)
				{
					this.Timer[this.ID] -= Time.deltaTime;
					if (this.Timer[this.ID] < 0f)
					{
						this.Sparks[this.ID].Stop();
						this.Sparks[this.ID + 1].Stop();
						if (UnityEngine.Random.Range(0, 2) == 1)
						{
							this.On[this.ID] = true;
						}
						else
						{
							this.On[this.ID] = false;
						}
						this.Timer[this.ID] = UnityEngine.Random.Range(1f, 2f);
					}
					if (this.On[this.ID])
					{
						this.ArmValue[this.ID] = Mathf.Lerp(this.ArmValue[this.ID], 0f, Time.deltaTime * 5f);
						this.ArmValue[this.ID + 1] = Mathf.Lerp(this.ArmValue[this.ID + 1], 100f, Time.deltaTime * 5f);
						this.RobotArms.SetBlendShapeWeight(this.ID, this.ArmValue[this.ID]);
						this.RobotArms.SetBlendShapeWeight(this.ID + 1, this.ArmValue[this.ID + 1]);
						if (this.ArmValue[this.ID] < 1f)
						{
							this.Sparks[this.ID].Play();
							this.RobotArms.SetBlendShapeWeight(this.ID, 0f);
							this.RobotArms.SetBlendShapeWeight(this.ID + 1, 100f);
							this.ArmValue[this.ID] = 0f;
							this.ArmValue[this.ID + 1] = 100f;
						}
					}
					else
					{
						this.ArmValue[this.ID] = Mathf.Lerp(this.ArmValue[this.ID], 100f, Time.deltaTime * 5f);
						this.ArmValue[this.ID + 1] = Mathf.Lerp(this.ArmValue[this.ID + 1], 0f, Time.deltaTime * 5f);
						this.RobotArms.SetBlendShapeWeight(this.ID, this.ArmValue[this.ID]);
						this.RobotArms.SetBlendShapeWeight(this.ID + 1, this.ArmValue[this.ID + 1]);
						if (this.ArmValue[this.ID] > 99f)
						{
							this.Sparks[this.ID + 1].Play();
							this.RobotArms.SetBlendShapeWeight(this.ID, 100f);
							this.RobotArms.SetBlendShapeWeight(this.ID + 1, 0f);
							this.ArmValue[this.ID] = 100f;
							this.ArmValue[this.ID + 1] = 0f;
						}
					}
					this.ID += 2;
				}
				return;
			}
			this.ID = 1;
			while (this.ID < 9)
			{
				this.ArmValue[this.ID] = Mathf.Lerp(this.ArmValue[this.ID], 100f, Time.deltaTime * 5f);
				this.RobotArms.SetBlendShapeWeight(this.ID, this.ArmValue[this.ID]);
				this.ID += 2;
			}
			this.StartWorkTimer -= Time.deltaTime;
			if (this.StartWorkTimer < 0f)
			{
				this.ID = 1;
				while (this.ID < 9)
				{
					this.RobotArms.SetBlendShapeWeight(this.ID, 100f);
					this.ID += 2;
				}
				return;
			}
		}
		else if (this.StopWorkTimer > 0f)
		{
			this.ID = 1;
			while (this.ID < 9)
			{
				this.ArmValue[this.ID] = Mathf.Lerp(this.ArmValue[this.ID], 0f, Time.deltaTime * 5f);
				this.RobotArms.SetBlendShapeWeight(this.ID, this.ArmValue[this.ID]);
				this.Sparks[this.ID].Stop();
				this.ID++;
			}
			this.StopWorkTimer -= Time.deltaTime;
			if (this.StopWorkTimer < 0f)
			{
				this.ID = 1;
				while (this.ID < 9)
				{
					this.RobotArms.SetBlendShapeWeight(this.ID, 0f);
					this.On[this.ID] = false;
					this.ID++;
				}
			}
		}
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x000FC2F4 File Offset: 0x000FA4F4
	public void ActivateArms()
	{
		this.Prompt.Circle[0].fillAmount = 1f;
		this.UpdateArms = true;
		this.On[0] = !this.On[0];
		if (this.On[0])
		{
			this.Prompt.HideButton[1] = false;
			this.MyAudio.clip = this.ArmsOn;
		}
		else
		{
			this.Prompt.HideButton[1] = true;
			this.MyAudio.clip = this.ArmsOff;
			this.StopWorkTimer = 5f;
			this.Work = false;
		}
		this.MyAudio.Play();
	}

	// Token: 0x060019CA RID: 6602 RVA: 0x000FC399 File Offset: 0x000FA599
	public void ToggleWork()
	{
		this.Prompt.Circle[1].fillAmount = 1f;
		this.StartWorkTimer = 1f;
		this.StopWorkTimer = 5f;
		this.Work = !this.Work;
	}

	// Token: 0x0400284E RID: 10318
	public SkinnedMeshRenderer RobotArms;

	// Token: 0x0400284F RID: 10319
	public AudioSource MyAudio;

	// Token: 0x04002850 RID: 10320
	public PromptScript Prompt;

	// Token: 0x04002851 RID: 10321
	public Transform TerminalTarget;

	// Token: 0x04002852 RID: 10322
	public ParticleSystem[] Sparks;

	// Token: 0x04002853 RID: 10323
	public AudioClip ArmsOff;

	// Token: 0x04002854 RID: 10324
	public AudioClip ArmsOn;

	// Token: 0x04002855 RID: 10325
	public float StartWorkTimer;

	// Token: 0x04002856 RID: 10326
	public float StopWorkTimer;

	// Token: 0x04002857 RID: 10327
	public float[] ArmValue;

	// Token: 0x04002858 RID: 10328
	public float[] Timer;

	// Token: 0x04002859 RID: 10329
	public bool UpdateArms;

	// Token: 0x0400285A RID: 10330
	public bool Work;

	// Token: 0x0400285B RID: 10331
	public bool[] On;

	// Token: 0x0400285C RID: 10332
	public int ID;
}

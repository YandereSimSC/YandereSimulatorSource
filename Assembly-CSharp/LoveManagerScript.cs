using System;
using UnityEngine;

// Token: 0x02000323 RID: 803
public class LoveManagerScript : MonoBehaviour
{
	// Token: 0x060017FA RID: 6138 RVA: 0x000D40E5 File Offset: 0x000D22E5
	private void Start()
	{
		this.SuitorProgress = DatingGlobals.SuitorProgress;
		Debug.Log("DatingGlobals.Affection is: " + DatingGlobals.Affection);
		if (DatingGlobals.Affection == 100f)
		{
			this.ConfessToSuitor = true;
		}
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x000D4120 File Offset: 0x000D2320
	private void LateUpdate()
	{
		if (this.Follower != null && this.Follower.Alive && !this.Follower.InCouple)
		{
			this.ID = 0;
			while (this.ID < this.TotalTargets)
			{
				Transform transform = this.Targets[this.ID];
				if (transform != null && this.Follower.transform.position.y > transform.position.y - 2f && this.Follower.transform.position.y < transform.position.y + 2f && Vector3.Distance(this.Follower.transform.position, new Vector3(transform.position.x, this.Follower.transform.position.y, transform.position.z)) < 2.5f)
				{
					if (Mathf.Abs(Vector3.Angle(this.Follower.transform.forward, this.Follower.transform.position - new Vector3(transform.position.x, this.Follower.transform.position.y, transform.position.z))) > this.AngleLimit)
					{
						if (!this.Follower.Gush)
						{
							this.Follower.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1f);
							this.Follower.GushTarget = transform;
							ParticleSystem.EmissionModule emission = this.Follower.Hearts.emission;
							emission.enabled = true;
							emission.rateOverTime = 5f;
							this.Follower.Hearts.Play();
							this.Follower.Gush = true;
						}
					}
					else
					{
						this.Follower.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 0f);
						this.Follower.Hearts.emission.enabled = false;
						this.Follower.Gush = false;
					}
				}
				this.ID++;
			}
		}
		if (this.LeftNote)
		{
			if (this.Rival == null)
			{
				this.Rival = this.StudentManager.Students[this.RivalID];
			}
			if (this.Suitor == null)
			{
				if (this.ConfessToSuitor)
				{
					this.Suitor = this.StudentManager.Students[this.SuitorID];
				}
				else
				{
					this.Suitor = this.StudentManager.Students[1];
				}
			}
			if (this.Rival != null && this.Suitor != null && this.Rival.Alive && this.Suitor.Alive && !this.Rival.Dying && !this.Suitor.Dying && this.Rival.ConfessPhase == 5 && this.Suitor.ConfessPhase == 3)
			{
				this.WaitingToConfess = true;
				float num = Vector3.Distance(this.Yandere.transform.position, this.MythHill.position);
				if (this.WaitingToConfess && !this.Yandere.Chased && this.Yandere.Chasers == 0 && num > 10f && num < 25f)
				{
					this.BeginConfession();
				}
			}
		}
		if (this.HoldingHands)
		{
			if (this.Rival == null)
			{
				this.Rival = this.StudentManager.Students[this.RivalID];
			}
			if (this.Suitor == null)
			{
				this.Suitor = this.StudentManager.Students[this.SuitorID];
			}
			this.Rival.MyController.Move(base.transform.forward * Time.deltaTime);
			this.Suitor.transform.position = new Vector3(this.Rival.transform.position.x - 0.5f, this.Rival.transform.position.y, this.Rival.transform.position.z);
			if (this.Rival.transform.position.z > -50f)
			{
				this.Suitor.MyController.radius = 0.12f;
				this.Suitor.enabled = true;
				this.Suitor.Cosmetic.MyRenderer.materials[this.Suitor.Cosmetic.FaceID].SetFloat("_BlendAmount", 0f);
				this.Suitor.Hearts.emission.enabled = false;
				this.Rival.MyController.radius = 0.12f;
				this.Rival.enabled = true;
				this.Rival.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 0f);
				this.Rival.Hearts.emission.enabled = false;
				this.Suitor.HoldingHands = false;
				this.Rival.HoldingHands = false;
				this.HoldingHands = false;
			}
		}
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x000D46BC File Offset: 0x000D28BC
	public void CoupleCheck()
	{
		if (this.SuitorProgress == 2)
		{
			this.Rival = this.StudentManager.Students[this.RivalID];
			this.Suitor = this.StudentManager.Students[this.SuitorID];
			if (this.Rival != null && this.Suitor != null)
			{
				this.Suitor.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.Suitor.CharacterAnimation.enabled = true;
				this.Rival.CharacterAnimation.enabled = true;
				this.Suitor.CharacterAnimation.Play("walkHands_00");
				this.Suitor.transform.eulerAngles = Vector3.zero;
				this.Suitor.transform.position = new Vector3(-0.25f, 0f, -90f);
				this.Suitor.Pathfinding.canSearch = false;
				this.Suitor.Pathfinding.canMove = false;
				this.Suitor.MyController.radius = 0f;
				this.Suitor.enabled = false;
				this.Rival.CharacterAnimation.Play("f02_walkHands_00");
				this.Rival.transform.eulerAngles = Vector3.zero;
				this.Rival.transform.position = new Vector3(0.25f, 0f, -90f);
				this.Rival.Pathfinding.canSearch = false;
				this.Rival.Pathfinding.canMove = false;
				this.Rival.MyController.radius = 0f;
				this.Rival.enabled = false;
				Physics.SyncTransforms();
				this.Suitor.Cosmetic.MyRenderer.materials[this.Suitor.Cosmetic.FaceID].SetFloat("_BlendAmount", 1f);
				ParticleSystem.EmissionModule emission = this.Suitor.Hearts.emission;
				emission.enabled = true;
				emission.rateOverTime = 5f;
				this.Suitor.Hearts.Play();
				this.Rival.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1f);
				ParticleSystem.EmissionModule emission2 = this.Rival.Hearts.emission;
				emission2.enabled = true;
				emission2.rateOverTime = 5f;
				this.Rival.Hearts.Play();
				this.Suitor.HoldingHands = true;
				this.Rival.HoldingHands = true;
				this.Suitor.CoupleID = this.SuitorID;
				this.Rival.CoupleID = this.RivalID;
				this.HoldingHands = true;
				Debug.Log("Students are now holding hands.");
			}
		}
	}

	// Token: 0x060017FD RID: 6141 RVA: 0x000D49B0 File Offset: 0x000D2BB0
	public void BeginConfession()
	{
		this.Suitor.EmptyHands();
		this.Rival.EmptyHands();
		this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.CanMove = false;
		this.StudentManager.DisableEveryone();
		this.Suitor.gameObject.SetActive(true);
		this.Rival.gameObject.SetActive(true);
		this.Suitor.enabled = false;
		this.Rival.enabled = false;
		if (!this.ConfessToSuitor)
		{
			this.ConfessionManager.gameObject.SetActive(true);
		}
		else
		{
			this.ConfessionScene.enabled = true;
		}
		this.Clock.StopTime = true;
		this.LeftNote = false;
	}

	// Token: 0x04002286 RID: 8838
	public ConfessionManagerScript ConfessionManager;

	// Token: 0x04002287 RID: 8839
	public ConfessionSceneScript ConfessionScene;

	// Token: 0x04002288 RID: 8840
	public StudentManagerScript StudentManager;

	// Token: 0x04002289 RID: 8841
	public YandereScript Yandere;

	// Token: 0x0400228A RID: 8842
	public ClockScript Clock;

	// Token: 0x0400228B RID: 8843
	public StudentScript Follower;

	// Token: 0x0400228C RID: 8844
	public StudentScript Suitor;

	// Token: 0x0400228D RID: 8845
	public StudentScript Rival;

	// Token: 0x0400228E RID: 8846
	public Transform FriendWaitSpot;

	// Token: 0x0400228F RID: 8847
	public Transform[] Targets;

	// Token: 0x04002290 RID: 8848
	public Transform MythHill;

	// Token: 0x04002291 RID: 8849
	public int SuitorProgress;

	// Token: 0x04002292 RID: 8850
	public int TotalTargets;

	// Token: 0x04002293 RID: 8851
	public int Phase = 1;

	// Token: 0x04002294 RID: 8852
	public int ID;

	// Token: 0x04002295 RID: 8853
	public int SuitorID = 28;

	// Token: 0x04002296 RID: 8854
	public int RivalID = 30;

	// Token: 0x04002297 RID: 8855
	public float AngleLimit;

	// Token: 0x04002298 RID: 8856
	public bool WaitingToConfess;

	// Token: 0x04002299 RID: 8857
	public bool ConfessToSuitor;

	// Token: 0x0400229A RID: 8858
	public bool HoldingHands;

	// Token: 0x0400229B RID: 8859
	public bool RivalWaiting;

	// Token: 0x0400229C RID: 8860
	public bool LeftNote;

	// Token: 0x0400229D RID: 8861
	public bool Courted;
}

using System;
using UnityEngine;

// Token: 0x020003F4 RID: 1012
public class StandScript : MonoBehaviour
{
	// Token: 0x06001ADF RID: 6879 RVA: 0x0010DA8D File Offset: 0x0010BC8D
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x0010DAA0 File Offset: 0x0010BCA0
	private void Update()
	{
		if (!this.Stand.activeInHierarchy)
		{
			if (this.Weapons == 8 && this.Yandere.transform.position.y > 11.9f && Input.GetButtonDown("RB") && !MissionModeGlobals.MissionMode && !this.Yandere.Laughing && this.Yandere.CanMove)
			{
				this.Yandere.Jojo();
				return;
			}
		}
		else if (this.Phase == 0)
		{
			if (this.Stand.GetComponent<Animation>()["StandSummon"].time >= 2f && this.Stand.GetComponent<Animation>()["StandSummon"].time <= 2.5f)
			{
				if (!this.SFX)
				{
					AudioSource.PlayClipAtPoint(this.SummonSFX, base.transform.position);
					this.SFX = true;
				}
				UnityEngine.Object.Instantiate<GameObject>(this.SummonEffect, this.SummonTransform.position, Quaternion.identity);
			}
			if (this.Stand.GetComponent<Animation>()["StandSummon"].time >= this.Stand.GetComponent<Animation>()["StandSummon"].length)
			{
				this.Stand.GetComponent<Animation>().CrossFade("StandIdle");
				this.Phase++;
				return;
			}
		}
		else
		{
			float axis = Input.GetAxis("Vertical");
			float axis2 = Input.GetAxis("Horizontal");
			if (this.Yandere.CanMove)
			{
				this.Return();
				if (axis == 0f && axis2 == 0f)
				{
					this.Stand.GetComponent<Animation>().CrossFade("StandIdle");
					return;
				}
				if (this.Yandere.Running)
				{
					this.Stand.GetComponent<Animation>().CrossFade("StandRun");
					return;
				}
				this.Stand.GetComponent<Animation>().CrossFade("StandWalk");
				return;
			}
			else if (this.Yandere.RPGCamera.enabled)
			{
				if (this.Yandere.Laughing)
				{
					if (Vector3.Distance(this.Stand.transform.localPosition, new Vector3(0f, 0.2f, -0.4f)) > 0.01f)
					{
						this.Stand.transform.localPosition = Vector3.Lerp(this.Stand.transform.localPosition, new Vector3(0f, 0.2f, 0.1f), Time.deltaTime * 10f);
						this.Stand.transform.localEulerAngles = new Vector3(Mathf.Lerp(this.Stand.transform.localEulerAngles.x, 22.5f, Time.deltaTime * 10f), this.Stand.transform.localEulerAngles.y, this.Stand.transform.localEulerAngles.z);
					}
					this.Stand.GetComponent<Animation>().CrossFade("StandAttack");
					this.StandPunch.MyCollider.enabled = true;
					this.ReadyForFinisher = true;
					return;
				}
				if (this.ReadyForFinisher)
				{
					if (this.Phase == 1)
					{
						base.GetComponent<AudioSource>().Play();
						this.Finisher = UnityEngine.Random.Range(1, 3);
						this.Stand.GetComponent<Animation>().CrossFade("StandFinisher" + this.Finisher.ToString());
						this.Phase++;
						return;
					}
					if (this.Phase == 2)
					{
						if (this.Stand.GetComponent<Animation>()["StandFinisher" + this.Finisher.ToString()].time >= 0.5f)
						{
							this.FalconPunch.MyCollider.enabled = true;
							this.StandPunch.MyCollider.enabled = false;
							this.Phase++;
							return;
						}
					}
					else if (this.Phase == 3 && (this.StandPunch.MyCollider.enabled || this.Stand.GetComponent<Animation>()["StandFinisher" + this.Finisher.ToString()].time >= this.Stand.GetComponent<Animation>()["StandFinisher" + this.Finisher.ToString()].length))
					{
						this.Stand.GetComponent<Animation>().CrossFade("StandIdle");
						this.FalconPunch.MyCollider.enabled = false;
						this.ReadyForFinisher = false;
						this.Yandere.CanMove = true;
						this.Phase = 1;
					}
				}
			}
		}
	}

	// Token: 0x06001AE1 RID: 6881 RVA: 0x0010DF54 File Offset: 0x0010C154
	public void Spawn()
	{
		this.FalconPunch.MyCollider.enabled = false;
		this.StandPunch.MyCollider.enabled = false;
		this.StandCamera.SetActive(true);
		this.MotionBlur.enabled = true;
		this.Stand.SetActive(true);
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x0010DFA8 File Offset: 0x0010C1A8
	private void Return()
	{
		if (Vector3.Distance(this.Stand.transform.localPosition, new Vector3(0f, 0f, -0.5f)) > 0.01f)
		{
			this.Stand.transform.localPosition = Vector3.Lerp(this.Stand.transform.localPosition, new Vector3(0f, 0f, -0.5f), Time.deltaTime * 10f);
			this.Stand.transform.localEulerAngles = new Vector3(Mathf.Lerp(this.Stand.transform.localEulerAngles.x, 0f, Time.deltaTime * 10f), this.Stand.transform.localEulerAngles.y, this.Stand.transform.localEulerAngles.z);
		}
	}

	// Token: 0x04002B8B RID: 11147
	public AmplifyMotionEffect MotionBlur;

	// Token: 0x04002B8C RID: 11148
	public FalconPunchScript FalconPunch;

	// Token: 0x04002B8D RID: 11149
	public StandPunchScript StandPunch;

	// Token: 0x04002B8E RID: 11150
	public Transform SummonTransform;

	// Token: 0x04002B8F RID: 11151
	public GameObject SummonEffect;

	// Token: 0x04002B90 RID: 11152
	public GameObject StandCamera;

	// Token: 0x04002B91 RID: 11153
	public YandereScript Yandere;

	// Token: 0x04002B92 RID: 11154
	public GameObject Stand;

	// Token: 0x04002B93 RID: 11155
	public Transform[] Hands;

	// Token: 0x04002B94 RID: 11156
	public int FinishPhase;

	// Token: 0x04002B95 RID: 11157
	public int Finisher;

	// Token: 0x04002B96 RID: 11158
	public int Weapons;

	// Token: 0x04002B97 RID: 11159
	public int Phase;

	// Token: 0x04002B98 RID: 11160
	public AudioClip SummonSFX;

	// Token: 0x04002B99 RID: 11161
	public bool ReadyForFinisher;

	// Token: 0x04002B9A RID: 11162
	public bool SFX;
}

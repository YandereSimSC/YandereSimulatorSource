using System;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public class HomePrisonerChanScript : MonoBehaviour
{
	// Token: 0x06001734 RID: 5940 RVA: 0x000C680C File Offset: 0x000C4A0C
	private void Start()
	{
		if (SchoolGlobals.KidnapVictim > 0)
		{
			this.StudentID = SchoolGlobals.KidnapVictim;
			if (StudentGlobals.GetStudentSanity(this.StudentID) == 100f)
			{
				this.AnkleRopes.SetActive(false);
			}
			this.PermanentAngleR = this.TwintailR.eulerAngles;
			this.PermanentAngleL = this.TwintailL.eulerAngles;
			if (StudentGlobals.GetStudentArrested(this.StudentID) || StudentGlobals.GetStudentDead(this.StudentID))
			{
				SchoolGlobals.KidnapVictim = 0;
				base.gameObject.SetActive(false);
				return;
			}
			this.Cosmetic.StudentID = this.StudentID;
			this.Cosmetic.enabled = true;
			this.BreastSize = this.JSON.Students[this.StudentID].BreastSize;
			this.RightEyeRotOrigin = this.RightEye.localEulerAngles;
			this.LeftEyeRotOrigin = this.LeftEye.localEulerAngles;
			this.RightEyeOrigin = this.RightEye.localPosition;
			this.LeftEyeOrigin = this.LeftEye.localPosition;
			this.UpdateSanity();
			this.TwintailR.transform.localEulerAngles = new Vector3(0f, 180f, -90f);
			this.TwintailL.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
			this.Blindfold.SetActive(false);
			this.Tripod.SetActive(false);
			if (this.StudentID == 81 && !StudentGlobals.GetStudentBroken(81) && SchemeGlobals.GetSchemeStage(6) > 4)
			{
				this.Blindfold.SetActive(true);
				this.Tripod.SetActive(true);
				return;
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001735 RID: 5941 RVA: 0x000C69C8 File Offset: 0x000C4BC8
	private void LateUpdate()
	{
		this.Skirt.transform.localPosition = new Vector3(0f, -0.135f, 0.01f);
		this.Skirt.transform.localScale = new Vector3(this.Skirt.transform.localScale.x, 1.2f, this.Skirt.transform.localScale.z);
		if (!this.Tortured)
		{
			if (this.Sanity > 0f)
			{
				if (this.LookAhead)
				{
					this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x - 45f, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
				}
				else if (this.YandereDetector.YandereDetected && Vector3.Distance(base.transform.position, this.HomeYandere.position) < 2f)
				{
					Quaternion b;
					if (this.HomeCamera.Target == this.HomeCamera.Targets[10])
					{
						b = Quaternion.LookRotation(this.HomeCamera.transform.position + Vector3.down * (1.5f * ((100f - this.Sanity) / 100f)) - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot1, Time.deltaTime * 2f);
					}
					else
					{
						b = Quaternion.LookRotation(this.HomeYandere.position + Vector3.up * 1.5f - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot2, Time.deltaTime * 2f);
					}
					this.Neck.rotation = Quaternion.Slerp(this.LastRotation, b, Time.deltaTime * 2f);
					this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
					this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
				}
				else
				{
					if (this.HomeCamera.Target == this.HomeCamera.Targets[10])
					{
						Quaternion b2 = Quaternion.LookRotation(this.HomeCamera.transform.position + Vector3.down * (1.5f * ((100f - this.Sanity) / 100f)) - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot3, Time.deltaTime * 2f);
					}
					else
					{
						Quaternion b2 = Quaternion.LookRotation(base.transform.position + base.transform.forward - this.Neck.position);
						this.Neck.rotation = Quaternion.Slerp(this.LastRotation, b2, Time.deltaTime * 2f);
					}
					this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot4, Time.deltaTime * 2f);
					this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
					this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
				}
			}
			else
			{
				this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x - 45f, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
			}
		}
		this.LastRotation = this.Neck.rotation;
		if (!this.Tortured && this.Sanity < 100f && this.Sanity > 0f)
		{
			this.TwitchTimer += Time.deltaTime;
			if (this.TwitchTimer > this.NextTwitch)
			{
				this.Twitch = new Vector3((1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f), (1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f), (1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f));
				this.NextTwitch = UnityEngine.Random.Range(0f, 1f);
				this.TwitchTimer = 0f;
			}
			this.Twitch = Vector3.Lerp(this.Twitch, Vector3.zero, Time.deltaTime * 10f);
			this.Neck.localEulerAngles += this.Twitch;
		}
		if (this.Tortured)
		{
			this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot5, Time.deltaTime * 2f);
			this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
			this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
		}
	}

	// Token: 0x06001736 RID: 5942 RVA: 0x000C6F78 File Offset: 0x000C5178
	public void UpdateSanity()
	{
		this.Sanity = StudentGlobals.GetStudentSanity(this.StudentID);
		bool active = this.Sanity == 0f;
		this.RightMindbrokenEye.SetActive(active);
		this.LeftMindbrokenEye.SetActive(active);
	}

	// Token: 0x04001FC5 RID: 8133
	public HomeYandereDetectorScript YandereDetector;

	// Token: 0x04001FC6 RID: 8134
	public HomeCameraScript HomeCamera;

	// Token: 0x04001FC7 RID: 8135
	public CosmeticScript Cosmetic;

	// Token: 0x04001FC8 RID: 8136
	public JsonScript JSON;

	// Token: 0x04001FC9 RID: 8137
	public Vector3 RightEyeRotOrigin;

	// Token: 0x04001FCA RID: 8138
	public Vector3 LeftEyeRotOrigin;

	// Token: 0x04001FCB RID: 8139
	public Vector3 PermanentAngleR;

	// Token: 0x04001FCC RID: 8140
	public Vector3 PermanentAngleL;

	// Token: 0x04001FCD RID: 8141
	public Vector3 RightEyeOrigin;

	// Token: 0x04001FCE RID: 8142
	public Vector3 LeftEyeOrigin;

	// Token: 0x04001FCF RID: 8143
	public Vector3 Twitch;

	// Token: 0x04001FD0 RID: 8144
	public Quaternion LastRotation;

	// Token: 0x04001FD1 RID: 8145
	public Transform HomeYandere;

	// Token: 0x04001FD2 RID: 8146
	public Transform RightBreast;

	// Token: 0x04001FD3 RID: 8147
	public Transform LeftBreast;

	// Token: 0x04001FD4 RID: 8148
	public Transform TwintailR;

	// Token: 0x04001FD5 RID: 8149
	public Transform TwintailL;

	// Token: 0x04001FD6 RID: 8150
	public Transform RightEye;

	// Token: 0x04001FD7 RID: 8151
	public Transform LeftEye;

	// Token: 0x04001FD8 RID: 8152
	public Transform Skirt;

	// Token: 0x04001FD9 RID: 8153
	public Transform Neck;

	// Token: 0x04001FDA RID: 8154
	public GameObject RightMindbrokenEye;

	// Token: 0x04001FDB RID: 8155
	public GameObject LeftMindbrokenEye;

	// Token: 0x04001FDC RID: 8156
	public GameObject AnkleRopes;

	// Token: 0x04001FDD RID: 8157
	public GameObject Blindfold;

	// Token: 0x04001FDE RID: 8158
	public GameObject Character;

	// Token: 0x04001FDF RID: 8159
	public GameObject Tripod;

	// Token: 0x04001FE0 RID: 8160
	public float HairRotation;

	// Token: 0x04001FE1 RID: 8161
	public float TwitchTimer;

	// Token: 0x04001FE2 RID: 8162
	public float NextTwitch;

	// Token: 0x04001FE3 RID: 8163
	public float BreastSize;

	// Token: 0x04001FE4 RID: 8164
	public float EyeShrink;

	// Token: 0x04001FE5 RID: 8165
	public float Sanity;

	// Token: 0x04001FE6 RID: 8166
	public float HairRot1;

	// Token: 0x04001FE7 RID: 8167
	public float HairRot2;

	// Token: 0x04001FE8 RID: 8168
	public float HairRot3;

	// Token: 0x04001FE9 RID: 8169
	public float HairRot4;

	// Token: 0x04001FEA RID: 8170
	public float HairRot5;

	// Token: 0x04001FEB RID: 8171
	public bool LookAhead;

	// Token: 0x04001FEC RID: 8172
	public bool Tortured;

	// Token: 0x04001FED RID: 8173
	public bool Male;

	// Token: 0x04001FEE RID: 8174
	public int StudentID;
}

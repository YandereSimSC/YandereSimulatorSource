using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200045E RID: 1118
public class WalkToSchoolManagerScript : MonoBehaviour
{
	// Token: 0x06001CF6 RID: 7414 RVA: 0x001570B8 File Offset: 0x001552B8
	private void Start()
	{
		Application.targetFrameRate = 60;
		if (SchoolGlobals.SchoolAtmosphere < 0.5f || GameGlobals.LoveSick)
		{
			this.Darkness.color = new Color(0f, 0f, 0f, 1f);
		}
		else
		{
			this.Darkness.color = new Color(1f, 1f, 1f, 1f);
		}
		this.Window.localScale = new Vector3(0f, 0f, 0f);
		this.Yandere.Character.GetComponent<Animation>()["f02_newWalk_00"].time = UnityEngine.Random.Range(0f, this.Yandere.Character.GetComponent<Animation>()["f02_newWalk_00"].length);
		this.Yandere.WearOutdoorShoes();
		this.Senpai.WearOutdoorShoes();
		this.Rival.WearOutdoorShoes();
	}

	// Token: 0x06001CF7 RID: 7415 RVA: 0x001571B4 File Offset: 0x001553B4
	private void Update()
	{
		for (int i = 1; i < 3; i++)
		{
			Transform transform = this.Neighborhood[i];
			transform.position = new Vector3(transform.position.x - Time.deltaTime * this.ScrollSpeed, transform.position.y, transform.position.z);
			if (transform.position.x < -160f)
			{
				transform.position = new Vector3(transform.position.x + 320f, transform.position.y, transform.position.z);
			}
		}
		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				if (!this.ShowWindow)
				{
					if (!this.Ending)
					{
						if (Input.GetButtonDown("A"))
						{
							this.Timer = 1f;
						}
						this.Timer += Time.deltaTime;
						if (this.Timer > 1f)
						{
							this.RivalEyeRTarget = this.RivalEyeR.localEulerAngles.y;
							this.RivalEyeLTarget = this.RivalEyeL.localEulerAngles.y;
							this.SenpaiEyeRTarget = this.SenpaiEyeR.localEulerAngles.y;
							this.SenpaiEyeLTarget = this.SenpaiEyeL.localEulerAngles.y;
							this.ShowWindow = true;
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Continue";
							this.PromptBar.Label[2].text = "Skip";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
						}
					}
					else
					{
						this.Window.localScale = Vector3.Lerp(this.Window.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
						if ((double)this.Window.localScale.x < 0.01)
						{
							this.Timer += Time.deltaTime;
							if (this.Timer > 1f)
							{
								this.FadeOut = true;
							}
						}
					}
				}
				else
				{
					this.Window.localScale = Vector3.Lerp(this.Window.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
					if ((double)this.Window.localScale.x > 0.99)
					{
						if (this.Frame > 3)
						{
							this.Typewriter.mLabel.color = new Color(1f, 1f, 1f, 1f);
						}
						this.Frame++;
					}
					if (!this.Talk)
					{
						if ((double)this.Window.localScale.x > 0.99)
						{
							this.Talk = true;
							this.UpdateNameLabel();
							this.Typewriter.enabled = true;
							this.Typewriter.ResetToBeginning();
							this.Typewriter.mFullText = this.Lines[this.ID];
							this.Typewriter.mLabel.text = this.Lines[this.ID];
							this.Typewriter.mLabel.color = new Color(1f, 1f, 1f, 0f);
							this.MyAudio.clip = this.Speech[this.ID];
							this.MyAudio.Play();
						}
					}
					else
					{
						Debug.Log("Waiting for button press.");
						if (this.Auto && !this.MyAudio.isPlaying)
						{
							this.AutoTimer += Time.deltaTime;
						}
						if (Input.GetButtonDown("A") || this.AutoTimer > 1f)
						{
							Debug.Log("Detected button press.");
							this.AutoTimer = 0f;
							if (this.ID < this.Lines.Length - 1)
							{
								if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
								{
									Debug.Log("Line not finished yet.");
									this.Typewriter.Finish();
									this.Typewriter.mCurrentOffset = this.Typewriter.mFullText.Length;
								}
								else
								{
									Debug.Log("Line finished.");
									this.ID++;
									this.Frame = 0;
									this.Typewriter.ResetToBeginning();
									this.Typewriter.mFullText = this.Lines[this.ID];
									this.Typewriter.mLabel.text = this.Lines[this.ID];
									this.Typewriter.mLabel.color = new Color(1f, 1f, 1f, 0f);
									this.MyAudio.clip = this.Speech[this.ID];
									this.MyAudio.Play();
									this.UpdateNameLabel();
								}
							}
							else if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
							{
								this.Typewriter.Finish();
							}
							else
							{
								this.End();
							}
						}
						if (Input.GetButtonDown("X"))
						{
							this.End();
						}
					}
				}
			}
		}
		else
		{
			this.MyAudio.volume -= Time.deltaTime;
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f && !this.Debugging)
			{
				SceneManager.LoadScene("LoadingScene");
			}
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 10f;
		}
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 10f;
		}
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x00157868 File Offset: 0x00155A68
	private void LateUpdate()
	{
		if (this.Talk)
		{
			if (!this.Ending)
			{
				this.RivalNeckTarget = Mathf.Lerp(this.RivalNeckTarget, 15f, Time.deltaTime * 3.6f);
				this.RivalHeadTarget = Mathf.Lerp(this.RivalHeadTarget, 15f, Time.deltaTime * 3.6f);
				this.RivalEyeRTarget = Mathf.Lerp(this.RivalEyeRTarget, 95f, Time.deltaTime * 3.6f);
				this.RivalEyeLTarget = Mathf.Lerp(this.RivalEyeLTarget, 275f, Time.deltaTime * 3.6f);
				this.SenpaiNeckTarget = Mathf.Lerp(this.SenpaiNeckTarget, -15f, Time.deltaTime * 3.6f);
				this.SenpaiHeadTarget = Mathf.Lerp(this.SenpaiHeadTarget, -15f, Time.deltaTime * 3.6f);
				this.SenpaiEyeRTarget = Mathf.Lerp(this.SenpaiEyeRTarget, 85f, Time.deltaTime * 3.6f);
				this.SenpaiEyeLTarget = Mathf.Lerp(this.SenpaiEyeLTarget, 265f, Time.deltaTime * 3.6f);
				this.YandereNeckTarget = Mathf.Lerp(this.YandereNeckTarget, 7.5f, Time.deltaTime * 3.6f);
				this.YandereHeadTarget = Mathf.Lerp(this.YandereHeadTarget, 7.5f, Time.deltaTime * 3.6f);
			}
			else
			{
				this.RivalNeckTarget = Mathf.Lerp(this.RivalNeckTarget, 0f, Time.deltaTime * 3.6f);
				this.RivalHeadTarget = Mathf.Lerp(this.RivalHeadTarget, 0f, Time.deltaTime * 3.6f);
				this.RivalEyeRTarget = Mathf.Lerp(this.RivalEyeRTarget, 90f, Time.deltaTime * 3.6f);
				this.RivalEyeLTarget = Mathf.Lerp(this.RivalEyeLTarget, 270f, Time.deltaTime * 3.6f);
				this.SenpaiNeckTarget = Mathf.Lerp(this.SenpaiNeckTarget, 0f, Time.deltaTime * 3.6f);
				this.SenpaiHeadTarget = Mathf.Lerp(this.SenpaiHeadTarget, 0f, Time.deltaTime * 3.6f);
				this.SenpaiEyeRTarget = Mathf.Lerp(this.SenpaiEyeRTarget, 90f, Time.deltaTime * 3.6f);
				this.SenpaiEyeLTarget = Mathf.Lerp(this.SenpaiEyeLTarget, 270f, Time.deltaTime * 3.6f);
				this.YandereNeckTarget = Mathf.Lerp(this.YandereNeckTarget, 0f, Time.deltaTime * 3.6f);
				this.YandereHeadTarget = Mathf.Lerp(this.YandereHeadTarget, 0f, Time.deltaTime * 3.6f);
			}
			this.RivalNeck.localEulerAngles = new Vector3(this.RivalNeck.localEulerAngles.x, this.RivalNeckTarget, this.RivalNeck.localEulerAngles.z);
			this.RivalHead.localEulerAngles = new Vector3(this.RivalHead.localEulerAngles.x, this.RivalHeadTarget, this.RivalHead.localEulerAngles.z);
			this.RivalEyeR.localEulerAngles = new Vector3(this.RivalEyeR.localEulerAngles.x, this.RivalEyeRTarget, this.RivalEyeR.localEulerAngles.z);
			this.RivalEyeL.localEulerAngles = new Vector3(this.RivalEyeL.localEulerAngles.x, this.RivalEyeLTarget, this.RivalEyeL.localEulerAngles.z);
			this.SenpaiNeck.localEulerAngles = new Vector3(this.SenpaiNeck.localEulerAngles.x, this.SenpaiNeckTarget, this.SenpaiNeck.localEulerAngles.z);
			this.SenpaiHead.localEulerAngles = new Vector3(this.SenpaiHead.localEulerAngles.x, this.SenpaiHeadTarget, this.SenpaiHead.localEulerAngles.z);
			this.SenpaiEyeR.localEulerAngles = new Vector3(this.SenpaiEyeR.localEulerAngles.x, this.SenpaiEyeRTarget, this.SenpaiEyeR.localEulerAngles.z);
			this.SenpaiEyeL.localEulerAngles = new Vector3(this.SenpaiEyeL.localEulerAngles.x, this.SenpaiEyeLTarget, this.SenpaiEyeL.localEulerAngles.z);
			this.YandereNeck.localEulerAngles = new Vector3(this.YandereNeck.localEulerAngles.x, this.YandereNeckTarget, this.YandereNeck.localEulerAngles.z);
			this.YandereHead.localEulerAngles = new Vector3(this.YandereHead.localEulerAngles.x, this.YandereHeadTarget, this.YandereHead.localEulerAngles.z);
			if (this.MyAudio.isPlaying)
			{
				this.MouthTimer += Time.deltaTime;
				if (this.MouthTimer > this.TimerLimit)
				{
					this.MouthTarget = UnityEngine.Random.Range(40f, 40f + this.MouthExtent);
					this.MouthTimer = 0f;
				}
				if (this.Speakers[this.ID])
				{
					this.RivalJaw.localEulerAngles = new Vector3(this.RivalJaw.localEulerAngles.x, this.RivalJaw.localEulerAngles.y, Mathf.Lerp(this.RivalJaw.localEulerAngles.z, this.MouthTarget, Time.deltaTime * this.TalkSpeed));
					this.RivalLipL.localPosition = new Vector3(this.RivalLipL.localPosition.x, Mathf.Lerp(this.RivalLipL.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.RivalLipL.localPosition.z);
					this.RivalLipR.localPosition = new Vector3(this.RivalLipR.localPosition.x, Mathf.Lerp(this.RivalLipR.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.RivalLipR.localPosition.z);
					return;
				}
				this.SenpaiJaw.localEulerAngles = new Vector3(this.SenpaiJaw.localEulerAngles.x, this.SenpaiJaw.localEulerAngles.y, Mathf.Lerp(this.SenpaiJaw.localEulerAngles.z, this.MouthTarget, Time.deltaTime * this.TalkSpeed));
				this.SenpaiLipL.localPosition = new Vector3(this.SenpaiLipL.localPosition.x, Mathf.Lerp(this.SenpaiLipL.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.SenpaiLipL.localPosition.z);
				this.SenpaiLipR.localPosition = new Vector3(this.SenpaiLipR.localPosition.x, Mathf.Lerp(this.SenpaiLipR.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.SenpaiLipR.localPosition.z);
			}
		}
	}

	// Token: 0x06001CF9 RID: 7417 RVA: 0x00157FE8 File Offset: 0x001561E8
	public void UpdateNameLabel()
	{
		if (this.Speakers[this.ID])
		{
			this.NameLabel.text = "Osana-chan";
			return;
		}
		this.NameLabel.text = "Senpai-kun";
	}

	// Token: 0x06001CFA RID: 7418 RVA: 0x0015801A File Offset: 0x0015621A
	public void End()
	{
		this.PromptBar.Show = false;
		this.ShowWindow = false;
		this.Ending = true;
		this.Timer = 0f;
	}

	// Token: 0x04003622 RID: 13858
	public PromptBarScript PromptBar;

	// Token: 0x04003623 RID: 13859
	public CosmeticScript Yandere;

	// Token: 0x04003624 RID: 13860
	public CosmeticScript Senpai;

	// Token: 0x04003625 RID: 13861
	public CosmeticScript Rival;

	// Token: 0x04003626 RID: 13862
	public UISprite Darkness;

	// Token: 0x04003627 RID: 13863
	public Transform[] Neighborhood;

	// Token: 0x04003628 RID: 13864
	public Transform Window;

	// Token: 0x04003629 RID: 13865
	public Transform RivalNeck;

	// Token: 0x0400362A RID: 13866
	public Transform RivalHead;

	// Token: 0x0400362B RID: 13867
	public Transform RivalEyeR;

	// Token: 0x0400362C RID: 13868
	public Transform RivalEyeL;

	// Token: 0x0400362D RID: 13869
	public Transform RivalJaw;

	// Token: 0x0400362E RID: 13870
	public Transform RivalLipL;

	// Token: 0x0400362F RID: 13871
	public Transform RivalLipR;

	// Token: 0x04003630 RID: 13872
	public Transform SenpaiNeck;

	// Token: 0x04003631 RID: 13873
	public Transform SenpaiHead;

	// Token: 0x04003632 RID: 13874
	public Transform SenpaiEyeR;

	// Token: 0x04003633 RID: 13875
	public Transform SenpaiEyeL;

	// Token: 0x04003634 RID: 13876
	public Transform SenpaiJaw;

	// Token: 0x04003635 RID: 13877
	public Transform SenpaiLipL;

	// Token: 0x04003636 RID: 13878
	public Transform SenpaiLipR;

	// Token: 0x04003637 RID: 13879
	public Transform YandereNeck;

	// Token: 0x04003638 RID: 13880
	public Transform YandereHead;

	// Token: 0x04003639 RID: 13881
	public Transform YandereEyeR;

	// Token: 0x0400363A RID: 13882
	public Transform YandereEyeL;

	// Token: 0x0400363B RID: 13883
	public AudioSource MyAudio;

	// Token: 0x0400363C RID: 13884
	public float ScrollSpeed = 1f;

	// Token: 0x0400363D RID: 13885
	public float LipStrength = 0.0001f;

	// Token: 0x0400363E RID: 13886
	public float TimerLimit = 0.1f;

	// Token: 0x0400363F RID: 13887
	public float TalkSpeed = 10f;

	// Token: 0x04003640 RID: 13888
	public float AutoTimer;

	// Token: 0x04003641 RID: 13889
	public float Timer;

	// Token: 0x04003642 RID: 13890
	public float MouthExtent = 5f;

	// Token: 0x04003643 RID: 13891
	public float MouthTarget;

	// Token: 0x04003644 RID: 13892
	public float MouthTimer;

	// Token: 0x04003645 RID: 13893
	public float RivalNeckTarget;

	// Token: 0x04003646 RID: 13894
	public float RivalHeadTarget;

	// Token: 0x04003647 RID: 13895
	public float RivalEyeRTarget;

	// Token: 0x04003648 RID: 13896
	public float RivalEyeLTarget;

	// Token: 0x04003649 RID: 13897
	public float SenpaiNeckTarget;

	// Token: 0x0400364A RID: 13898
	public float SenpaiHeadTarget;

	// Token: 0x0400364B RID: 13899
	public float SenpaiEyeRTarget;

	// Token: 0x0400364C RID: 13900
	public float SenpaiEyeLTarget;

	// Token: 0x0400364D RID: 13901
	public float YandereNeckTarget;

	// Token: 0x0400364E RID: 13902
	public float YandereHeadTarget;

	// Token: 0x0400364F RID: 13903
	public bool ShowWindow;

	// Token: 0x04003650 RID: 13904
	public bool Debugging;

	// Token: 0x04003651 RID: 13905
	public bool FadeOut;

	// Token: 0x04003652 RID: 13906
	public bool Ending;

	// Token: 0x04003653 RID: 13907
	public bool Auto;

	// Token: 0x04003654 RID: 13908
	public bool Talk;

	// Token: 0x04003655 RID: 13909
	public TypewriterEffect Typewriter;

	// Token: 0x04003656 RID: 13910
	public UILabel NameLabel;

	// Token: 0x04003657 RID: 13911
	public AudioClip[] Speech;

	// Token: 0x04003658 RID: 13912
	public string[] Lines;

	// Token: 0x04003659 RID: 13913
	public bool[] Speakers;

	// Token: 0x0400365A RID: 13914
	public int Frame;

	// Token: 0x0400365B RID: 13915
	public int ID;
}

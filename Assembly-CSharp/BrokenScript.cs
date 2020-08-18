using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class BrokenScript : MonoBehaviour
{
	// Token: 0x06000A70 RID: 2672 RVA: 0x00055E28 File Offset: 0x00054028
	private void Start()
	{
		this.HairPhysics[0].enabled = false;
		this.HairPhysics[1].enabled = false;
		this.PermanentAngleR = this.TwintailR.eulerAngles;
		this.PermanentAngleL = this.TwintailL.eulerAngles;
		this.Subtitle = GameObject.Find("EventSubtitle").GetComponent<UILabel>();
		this.Yandere = GameObject.Find("YandereChan");
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x00055E98 File Offset: 0x00054098
	private void Update()
	{
		if (!this.Done)
		{
			float num = Vector3.Distance(this.Yandere.transform.position, base.transform.root.position);
			if (num < 6f)
			{
				if (num < 5f)
				{
					if (!this.Hunting)
					{
						this.Timer += Time.deltaTime;
						if (this.VoiceClip == null)
						{
							this.Subtitle.text = "";
						}
						if (this.Timer > 5f)
						{
							this.Timer = 0f;
							this.Subtitle.text = this.MutterTexts[this.ID];
							AudioClipPlayer.PlayAttached(this.Mutters[this.ID], base.transform.position, base.transform, 1f, 5f, out this.VoiceClip, this.Yandere.transform.position.y);
							this.ID++;
							if (this.ID == this.Mutters.Length)
							{
								this.ID = 1;
							}
						}
					}
					else if (!this.Began)
					{
						if (this.VoiceClip != null)
						{
							UnityEngine.Object.Destroy(this.VoiceClip);
						}
						this.Subtitle.text = "Do it.";
						AudioClipPlayer.PlayAttached(this.DoIt, base.transform.position, base.transform, 1f, 5f, out this.VoiceClip, this.Yandere.transform.position.y);
						this.Began = true;
					}
					else if (this.VoiceClip == null)
					{
						this.Subtitle.text = "...kill...kill...kill...";
						AudioClipPlayer.PlayAttached(this.KillKillKill, base.transform.position, base.transform, 1f, 5f, out this.VoiceClip, this.Yandere.transform.position.y);
					}
					float num2 = Mathf.Abs((num - 5f) * 0.2f);
					num2 = ((num2 > 1f) ? 1f : num2);
					this.Subtitle.transform.localScale = new Vector3(num2, num2, num2);
				}
				else
				{
					this.Subtitle.transform.localScale = Vector3.zero;
				}
			}
		}
		Vector3 eulerAngles = this.TwintailR.eulerAngles;
		Vector3 eulerAngles2 = this.TwintailL.eulerAngles;
		eulerAngles.x = this.PermanentAngleR.x;
		eulerAngles.z = this.PermanentAngleR.z;
		eulerAngles2.x = this.PermanentAngleL.x;
		eulerAngles2.z = this.PermanentAngleL.z;
		this.TwintailR.eulerAngles = eulerAngles;
		this.TwintailL.eulerAngles = eulerAngles2;
	}

	// Token: 0x04000AEC RID: 2796
	public DynamicBone[] HairPhysics;

	// Token: 0x04000AED RID: 2797
	public string[] MutterTexts;

	// Token: 0x04000AEE RID: 2798
	public AudioClip[] Mutters;

	// Token: 0x04000AEF RID: 2799
	public Vector3 PermanentAngleR;

	// Token: 0x04000AF0 RID: 2800
	public Vector3 PermanentAngleL;

	// Token: 0x04000AF1 RID: 2801
	public Transform TwintailR;

	// Token: 0x04000AF2 RID: 2802
	public Transform TwintailL;

	// Token: 0x04000AF3 RID: 2803
	public AudioClip KillKillKill;

	// Token: 0x04000AF4 RID: 2804
	public AudioClip Stab;

	// Token: 0x04000AF5 RID: 2805
	public AudioClip DoIt;

	// Token: 0x04000AF6 RID: 2806
	public GameObject VoiceClip;

	// Token: 0x04000AF7 RID: 2807
	public GameObject Yandere;

	// Token: 0x04000AF8 RID: 2808
	public UILabel Subtitle;

	// Token: 0x04000AF9 RID: 2809
	public AudioSource MyAudio;

	// Token: 0x04000AFA RID: 2810
	public bool Hunting;

	// Token: 0x04000AFB RID: 2811
	public bool Stabbed;

	// Token: 0x04000AFC RID: 2812
	public bool Began;

	// Token: 0x04000AFD RID: 2813
	public bool Done;

	// Token: 0x04000AFE RID: 2814
	public float SuicideTimer;

	// Token: 0x04000AFF RID: 2815
	public float Timer;

	// Token: 0x04000B00 RID: 2816
	public int ID = 1;
}

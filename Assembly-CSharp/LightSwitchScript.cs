using System;
using UnityEngine;

// Token: 0x0200031C RID: 796
public class LightSwitchScript : MonoBehaviour
{
	// Token: 0x060017E6 RID: 6118 RVA: 0x000D2577 File Offset: 0x000D0777
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x000D2590 File Offset: 0x000D0790
	private void Update()
	{
		if (this.Flicker)
		{
			this.FlickerTimer += Time.deltaTime;
			if (this.FlickerTimer > 0.1f)
			{
				this.FlickerTimer = 0f;
				this.BathroomLight.SetActive(!this.BathroomLight.activeInHierarchy);
			}
		}
		if (!this.Panel.useGravity)
		{
			if (this.Yandere.Armed)
			{
				this.Prompt.HideButton[3] = (this.Yandere.EquippedWeapon.WeaponID != 6);
			}
			else
			{
				this.Prompt.HideButton[3] = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.BathroomLight.activeInHierarchy)
			{
				this.Prompt.Label[0].text = "     Turn On";
				this.BathroomLight.SetActive(false);
				component.clip = this.Flick[1];
				component.Play();
				if (this.ToiletEvent.EventActive && (this.ToiletEvent.EventPhase == 2 || this.ToiletEvent.EventPhase == 3))
				{
					this.ReactionID = UnityEngine.Random.Range(1, 4);
					AudioClipPlayer.Play(this.ReactionClips[this.ReactionID], this.ToiletEvent.EventStudent.transform.position, 5f, 10f, out this.ToiletEvent.VoiceClip);
					this.ToiletEvent.EventSubtitle.text = this.ReactionTexts[this.ReactionID];
					this.SubtitleTimer += Time.deltaTime;
				}
			}
			else
			{
				this.Prompt.Label[0].text = "     Turn Off";
				this.BathroomLight.SetActive(true);
				component.clip = this.Flick[0];
				component.Play();
			}
		}
		if (this.SubtitleTimer > 0f)
		{
			this.SubtitleTimer += Time.deltaTime;
			if (this.SubtitleTimer > 3f)
			{
				this.ToiletEvent.EventSubtitle.text = string.Empty;
				this.SubtitleTimer = 0f;
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.HideButton[3] = true;
			this.Wires.localScale = new Vector3(this.Wires.localScale.x, this.Wires.localScale.y, 1f);
			this.Panel.useGravity = true;
			this.Panel.AddForce(0f, 0f, 10f);
		}
	}

	// Token: 0x04002240 RID: 8768
	public ToiletEventScript ToiletEvent;

	// Token: 0x04002241 RID: 8769
	public YandereScript Yandere;

	// Token: 0x04002242 RID: 8770
	public PromptScript Prompt;

	// Token: 0x04002243 RID: 8771
	public Transform ElectrocutionSpot;

	// Token: 0x04002244 RID: 8772
	public GameObject BathroomLight;

	// Token: 0x04002245 RID: 8773
	public GameObject Electricity;

	// Token: 0x04002246 RID: 8774
	public Rigidbody Panel;

	// Token: 0x04002247 RID: 8775
	public Transform Wires;

	// Token: 0x04002248 RID: 8776
	public AudioClip[] ReactionClips;

	// Token: 0x04002249 RID: 8777
	public string[] ReactionTexts;

	// Token: 0x0400224A RID: 8778
	public AudioClip[] Flick;

	// Token: 0x0400224B RID: 8779
	public float SubtitleTimer;

	// Token: 0x0400224C RID: 8780
	public float FlickerTimer;

	// Token: 0x0400224D RID: 8781
	public int ReactionID;

	// Token: 0x0400224E RID: 8782
	public bool Flicker;
}

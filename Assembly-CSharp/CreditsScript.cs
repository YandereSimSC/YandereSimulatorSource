using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000250 RID: 592
public class CreditsScript : MonoBehaviour
{
	// Token: 0x17000345 RID: 837
	// (get) Token: 0x060012BF RID: 4799 RVA: 0x00095CB5 File Offset: 0x00093EB5
	private bool ShouldStopCredits
	{
		get
		{
			return this.ID == this.JSON.Credits.Length;
		}
	}

	// Token: 0x060012C0 RID: 4800 RVA: 0x00095CCC File Offset: 0x00093ECC
	private GameObject SpawnLabel(int size)
	{
		return UnityEngine.Object.Instantiate<GameObject>((size == 1) ? this.SmallCreditsLabel : this.BigCreditsLabel, this.SpawnPoint.position, Quaternion.identity);
	}

	// Token: 0x060012C1 RID: 4801 RVA: 0x00095CF8 File Offset: 0x00093EF8
	private void Start()
	{
		if (DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			this.Jukebox.clip = this.DarkCreditsMusic;
			this.Darkness.color = new Color(0f, 0f, 0f, 0f);
			this.Speed = 1.1f;
		}
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x00095D4C File Offset: 0x00093F4C
	private void Update()
	{
		if (!this.Begin)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Begin = true;
				this.Jukebox.Play();
				this.Timer = 0f;
			}
		}
		else
		{
			if (!this.ShouldStopCredits)
			{
				if (this.Timer == 0f)
				{
					CreditJson creditJson = this.JSON.Credits[this.ID];
					GameObject gameObject = this.SpawnLabel(creditJson.Size);
					this.TimerLimit = (float)creditJson.Size * this.SpeedUpFactor;
					gameObject.transform.parent = this.Panel;
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					gameObject.GetComponent<UILabel>().text = creditJson.Name;
					this.ID++;
				}
				this.Timer += Time.deltaTime * this.Speed;
				if (this.Timer >= this.TimerLimit)
				{
					this.Timer = 0f;
				}
			}
			if (Input.GetButtonDown("B") || !this.Jukebox.isPlaying)
			{
				this.FadeOut = true;
			}
		}
		if (this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			this.Jukebox.volume -= Time.deltaTime;
			if (this.Darkness.color.a == 1f)
			{
				if (this.Darkness.color.r == 1f)
				{
					SceneManager.LoadScene("TitleScene");
				}
				else
				{
					SceneManager.LoadScene("PostCreditsScene");
				}
			}
		}
		bool keyDown = Input.GetKeyDown(KeyCode.Minus);
		bool keyDown2 = Input.GetKeyDown(KeyCode.Equals);
		if (keyDown)
		{
			Time.timeScale -= 1f;
		}
		else if (keyDown2)
		{
			Time.timeScale += 1f;
		}
		if (keyDown || keyDown2)
		{
			this.Jukebox.pitch = Time.timeScale;
		}
	}

	// Token: 0x04001855 RID: 6229
	[SerializeField]
	private JsonScript JSON;

	// Token: 0x04001856 RID: 6230
	[SerializeField]
	private Transform SpawnPoint;

	// Token: 0x04001857 RID: 6231
	[SerializeField]
	private Transform Panel;

	// Token: 0x04001858 RID: 6232
	[SerializeField]
	private GameObject SmallCreditsLabel;

	// Token: 0x04001859 RID: 6233
	[SerializeField]
	private GameObject BigCreditsLabel;

	// Token: 0x0400185A RID: 6234
	[SerializeField]
	private UISprite Darkness;

	// Token: 0x0400185B RID: 6235
	[SerializeField]
	private int ID;

	// Token: 0x0400185C RID: 6236
	[SerializeField]
	private float SpeedUpFactor;

	// Token: 0x0400185D RID: 6237
	[SerializeField]
	private float TimerLimit;

	// Token: 0x0400185E RID: 6238
	[SerializeField]
	private float FadeTimer;

	// Token: 0x0400185F RID: 6239
	[SerializeField]
	private float Speed = 1f;

	// Token: 0x04001860 RID: 6240
	[SerializeField]
	private float Timer;

	// Token: 0x04001861 RID: 6241
	[SerializeField]
	private bool FadeOut;

	// Token: 0x04001862 RID: 6242
	[SerializeField]
	private bool Begin;

	// Token: 0x04001863 RID: 6243
	private const int SmallTextSize = 1;

	// Token: 0x04001864 RID: 6244
	private const int BigTextSize = 2;

	// Token: 0x04001865 RID: 6245
	public AudioClip DarkCreditsMusic;

	// Token: 0x04001866 RID: 6246
	public AudioSource Jukebox;
}

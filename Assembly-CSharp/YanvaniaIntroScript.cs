using System;
using UnityEngine;

// Token: 0x0200047D RID: 1149
public class YanvaniaIntroScript : MonoBehaviour
{
	// Token: 0x06001DB7 RID: 7607 RVA: 0x00171CCC File Offset: 0x0016FECC
	private void Start()
	{
		this.BlackRight.gameObject.SetActive(true);
		this.BlackLeft.gameObject.SetActive(true);
		this.FinalStage.gameObject.SetActive(true);
		this.Heartbreak.gameObject.SetActive(true);
		this.Triangle.gameObject.SetActive(true);
		this.Triangle.transform.localScale = Vector3.zero;
		this.Heartbreak.transform.localPosition = new Vector3(1300f, this.Heartbreak.transform.localPosition.y, this.Heartbreak.transform.localPosition.z);
		this.FinalStage.transform.localPosition = new Vector3(-1300f, this.FinalStage.transform.localPosition.y, this.FinalStage.transform.localPosition.z);
	}

	// Token: 0x06001DB8 RID: 7608 RVA: 0x00171DCC File Offset: 0x0016FFCC
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 1f && this.Timer < 3f)
		{
			if (!this.Jukebox.activeInHierarchy)
			{
				this.Jukebox.SetActive(true);
			}
			this.Triangle.transform.localScale = Vector3.Lerp(this.Triangle.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			this.Heartbreak.transform.localPosition = new Vector3(Mathf.Lerp(this.Heartbreak.transform.localPosition.x, 0f, Time.deltaTime * 10f), this.Heartbreak.transform.localPosition.y, this.Heartbreak.transform.localPosition.z);
			this.FinalStage.transform.localPosition = new Vector3(Mathf.Lerp(this.FinalStage.transform.localPosition.x, 0f, Time.deltaTime * 10f), this.FinalStage.transform.localPosition.y, this.FinalStage.transform.localPosition.z);
		}
		else if (this.Timer > 3f)
		{
			if (!this.Jukebox.activeInHierarchy)
			{
				this.Jukebox.SetActive(true);
			}
			this.Triangle.transform.localEulerAngles = new Vector3(this.Triangle.transform.localEulerAngles.x, this.Triangle.transform.localEulerAngles.y, this.Triangle.transform.localEulerAngles.z + 36f * Time.deltaTime);
			this.Triangle.color = new Color(this.Triangle.color.r, this.Triangle.color.g, this.Triangle.color.b, this.Triangle.color.a - Time.deltaTime);
			this.Heartbreak.color = new Color(this.Heartbreak.color.r, this.Heartbreak.color.g, this.Heartbreak.color.b, this.Heartbreak.color.a - Time.deltaTime);
			this.FinalStage.color = new Color(this.FinalStage.color.r, this.FinalStage.color.g, this.FinalStage.color.b, this.FinalStage.color.a - Time.deltaTime);
		}
		if (this.Timer > 4f)
		{
			this.Finish();
		}
		if (this.Timer > this.LeaveTime)
		{
			this.Position += ((this.Position == 0f) ? Time.deltaTime : (this.Position * 0.1f));
			if (this.BlackLeft.localPosition.x > -2100f)
			{
				this.BlackRight.localPosition = new Vector3(this.BlackRight.localPosition.x + this.Position, this.BlackRight.localPosition.y, this.BlackRight.localPosition.z);
				this.BlackLeft.localPosition = new Vector3(this.BlackLeft.localPosition.x - this.Position, this.BlackLeft.localPosition.y, this.BlackLeft.localPosition.z);
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.Finish();
		}
	}

	// Token: 0x06001DB9 RID: 7609 RVA: 0x001721D2 File Offset: 0x001703D2
	private void Finish()
	{
		if (!this.Jukebox.activeInHierarchy)
		{
			this.Jukebox.SetActive(true);
		}
		this.ZombieSpawner.enabled = true;
		this.Yanmont.CanMove = true;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04003AC6 RID: 15046
	public YanvaniaZombieSpawnerScript ZombieSpawner;

	// Token: 0x04003AC7 RID: 15047
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003AC8 RID: 15048
	public GameObject Jukebox;

	// Token: 0x04003AC9 RID: 15049
	public Transform BlackRight;

	// Token: 0x04003ACA RID: 15050
	public Transform BlackLeft;

	// Token: 0x04003ACB RID: 15051
	public UILabel FinalStage;

	// Token: 0x04003ACC RID: 15052
	public UILabel Heartbreak;

	// Token: 0x04003ACD RID: 15053
	public UITexture Triangle;

	// Token: 0x04003ACE RID: 15054
	public float LeaveTime;

	// Token: 0x04003ACF RID: 15055
	public float Position;

	// Token: 0x04003AD0 RID: 15056
	public float Timer;
}

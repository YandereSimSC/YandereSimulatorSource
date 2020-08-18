using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200045F RID: 1119
public class WarningScript : MonoBehaviour
{
	// Token: 0x06001CFC RID: 7420 RVA: 0x00158080 File Offset: 0x00156280
	private void Start()
	{
		this.WarningLabel.gameObject.SetActive(false);
		this.Label.text = string.Empty;
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
	}

	// Token: 0x06001CFD RID: 7421 RVA: 0x001580F4 File Offset: 0x001562F4
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				if (this.Timer == 0f)
				{
					this.WarningLabel.gameObject.SetActive(true);
					component.Play();
				}
				this.Timer += Time.deltaTime;
				if (this.ID < this.Triggers.Length && this.Timer > this.Triggers[this.ID])
				{
					this.Label.text = this.Text[this.ID];
					this.ID++;
				}
			}
		}
		else
		{
			component.volume = Mathf.MoveTowards(component.volume, 0f, Time.deltaTime);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				SceneManager.LoadScene("SponsorScene");
			}
		}
		if (Input.anyKey)
		{
			this.FadeOut = true;
		}
	}

	// Token: 0x0400365C RID: 13916
	public float[] Triggers;

	// Token: 0x0400365D RID: 13917
	public string[] Text;

	// Token: 0x0400365E RID: 13918
	public UILabel WarningLabel;

	// Token: 0x0400365F RID: 13919
	public UISprite Darkness;

	// Token: 0x04003660 RID: 13920
	public UILabel Label;

	// Token: 0x04003661 RID: 13921
	public bool FadeOut;

	// Token: 0x04003662 RID: 13922
	public float Timer;

	// Token: 0x04003663 RID: 13923
	public int ID;
}

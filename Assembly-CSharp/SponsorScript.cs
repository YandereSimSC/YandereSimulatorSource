using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003ED RID: 1005
public class SponsorScript : MonoBehaviour
{
	// Token: 0x06001ACA RID: 6858 RVA: 0x0010C7C8 File Offset: 0x0010A9C8
	private void Start()
	{
		Time.timeScale = 1f;
		this.Set[1].SetActive(true);
		this.Set[2].SetActive(false);
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
	}

	// Token: 0x06001ACB RID: 6859 RVA: 0x0010C840 File Offset: 0x0010AA40
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer < 3.2f)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			return;
		}
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
		if (this.Darkness.color.a == 1f)
		{
			SceneManager.LoadScene("TitleScene");
		}
	}

	// Token: 0x04002B4F RID: 11087
	public GameObject[] Set;

	// Token: 0x04002B50 RID: 11088
	public UISprite Darkness;

	// Token: 0x04002B51 RID: 11089
	public float Timer;

	// Token: 0x04002B52 RID: 11090
	public int ID;
}

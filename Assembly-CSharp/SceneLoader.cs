using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003C5 RID: 965
public class SceneLoader : MonoBehaviour
{
	// Token: 0x06001A3A RID: 6714 RVA: 0x00100B74 File Offset: 0x000FED74
	private void Start()
	{
		Time.timeScale = 1f;
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
			PlayerGlobals.Money = 10f;
		}
		if (SchoolGlobals.SchoolAtmosphere < 0.5f || GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = new Color(0f, 0f, 0f, 1f);
			this.loadingText.color = new Color(1f, 0f, 0f, 1f);
			this.crashText.color = new Color(1f, 0f, 0f, 1f);
			this.KeyboardGraphic.color = new Color(1f, 0f, 0f, 1f);
			this.ControllerLines.color = new Color(1f, 0f, 0f, 1f);
			this.LightAnimation.SetActive(false);
			this.DarkAnimation.SetActive(true);
			for (int i = 1; i < this.ControllerText.Length; i++)
			{
				this.ControllerText[i].color = new Color(1f, 0f, 0f, 1f);
			}
			for (int i = 1; i < this.KeyboardText.Length; i++)
			{
				this.KeyboardText[i].color = new Color(1f, 0f, 0f, 1f);
			}
		}
		if (PlayerGlobals.UsingGamepad)
		{
			this.Keyboard.SetActive(false);
			this.Gamepad.SetActive(true);
		}
		if (!this.Debugging)
		{
			base.StartCoroutine(this.LoadNewScene());
		}
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x00100D33 File Offset: 0x000FEF33
	private void Update()
	{
		if (this.Debugging)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 10f)
			{
				this.Debugging = false;
				base.StartCoroutine(this.LoadNewScene());
			}
		}
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x00100D70 File Offset: 0x000FEF70
	private IEnumerator LoadNewScene()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("SchoolScene");
		while (!async.isDone)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x04002949 RID: 10569
	[SerializeField]
	private UILabel loadingText;

	// Token: 0x0400294A RID: 10570
	[SerializeField]
	private UILabel crashText;

	// Token: 0x0400294B RID: 10571
	private float timer;

	// Token: 0x0400294C RID: 10572
	public UILabel[] ControllerText;

	// Token: 0x0400294D RID: 10573
	public UILabel[] KeyboardText;

	// Token: 0x0400294E RID: 10574
	public GameObject LightAnimation;

	// Token: 0x0400294F RID: 10575
	public GameObject DarkAnimation;

	// Token: 0x04002950 RID: 10576
	public GameObject Keyboard;

	// Token: 0x04002951 RID: 10577
	public GameObject Gamepad;

	// Token: 0x04002952 RID: 10578
	public UITexture ControllerLines;

	// Token: 0x04002953 RID: 10579
	public UITexture KeyboardGraphic;

	// Token: 0x04002954 RID: 10580
	public bool Debugging;

	// Token: 0x04002955 RID: 10581
	public float Timer;
}

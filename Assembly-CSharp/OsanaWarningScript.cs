using System;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

// Token: 0x0200034C RID: 844
public class OsanaWarningScript : MonoBehaviour
{
	// Token: 0x0600188C RID: 6284 RVA: 0x000E0AB4 File Offset: 0x000DECB4
	private void Start()
	{
		this.Darkness.color = new Color(0f, 0f, 0f, 1f);
		ColorGradingModel.Settings settings = this.Profile.colorGrading.settings;
		settings.basic.saturation = 1f;
		settings.channelMixer.red = new Vector3(1f, 0f, 0f);
		settings.channelMixer.green = new Vector3(0f, 1f, 0f);
		settings.channelMixer.blue = new Vector3(0f, 0f, 1f);
		this.Profile.colorGrading.settings = settings;
		DepthOfFieldModel.Settings settings2 = this.Profile.depthOfField.settings;
		settings2.focusDistance = 10f;
		settings2.aperture = 11.2f;
		this.Profile.depthOfField.settings = settings2;
		BloomModel.Settings settings3 = this.Profile.bloom.settings;
		settings3.bloom.intensity = 1f;
		this.Profile.bloom.settings = settings3;
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x000E0BE8 File Offset: 0x000DEDE8
	private void Update()
	{
		if (this.FadeOut)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
		}
		else
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime);
		}
		this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
		if (this.Alpha == 0f)
		{
			if (Input.GetButtonDown("A"))
			{
				this.FadeOut = true;
				return;
			}
		}
		else if (this.Alpha == 1f)
		{
			SceneManager.LoadScene("CalendarScene");
		}
	}

	// Token: 0x0600188E RID: 6286 RVA: 0x000E0C94 File Offset: 0x000DEE94
	private void LateUpdate()
	{
		this.RightHand.localEulerAngles += new Vector3(UnityEngine.Random.Range(1f, -1f), UnityEngine.Random.Range(1f, -1f), UnityEngine.Random.Range(1f, -1f));
	}

	// Token: 0x0400241F RID: 9247
	public PostProcessingProfile Profile;

	// Token: 0x04002420 RID: 9248
	public Transform RightHand;

	// Token: 0x04002421 RID: 9249
	public UISprite Darkness;

	// Token: 0x04002422 RID: 9250
	public float Alpha = 1f;

	// Token: 0x04002423 RID: 9251
	public bool FadeOut;
}

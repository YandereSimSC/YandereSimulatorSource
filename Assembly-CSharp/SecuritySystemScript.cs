using System;
using UnityEngine;

// Token: 0x020003D0 RID: 976
public class SecuritySystemScript : MonoBehaviour
{
	// Token: 0x06001A58 RID: 6744 RVA: 0x00101DE2 File Offset: 0x000FFFE2
	private void Start()
	{
		if (!SchoolGlobals.HighSecurity)
		{
			base.enabled = false;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x06001A59 RID: 6745 RVA: 0x00101E0C File Offset: 0x0010000C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			for (int i = 0; i < this.Cameras.Length; i++)
			{
				this.Cameras[i].transform.parent.transform.parent.gameObject.GetComponent<AudioSource>().Stop();
				this.Cameras[i].gameObject.SetActive(false);
			}
			for (int i = 0; i < this.Detectors.Length; i++)
			{
				this.Detectors[i].MyCollider.enabled = false;
				this.Detectors[i].enabled = false;
			}
			base.GetComponent<AudioSource>().Play();
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Evidence = false;
			base.enabled = false;
		}
	}

	// Token: 0x040029BA RID: 10682
	public PromptScript Prompt;

	// Token: 0x040029BB RID: 10683
	public bool Evidence;

	// Token: 0x040029BC RID: 10684
	public bool Masked;

	// Token: 0x040029BD RID: 10685
	public SecurityCameraScript[] Cameras;

	// Token: 0x040029BE RID: 10686
	public MetalDetectorScript[] Detectors;
}

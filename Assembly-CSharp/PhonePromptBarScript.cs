using System;
using UnityEngine;

// Token: 0x0200035F RID: 863
public class PhonePromptBarScript : MonoBehaviour
{
	// Token: 0x060018C6 RID: 6342 RVA: 0x000E56B4 File Offset: 0x000E38B4
	private void Start()
	{
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, 630f, base.transform.localPosition.z);
		this.Panel.enabled = false;
	}

	// Token: 0x060018C7 RID: 6343 RVA: 0x000E5704 File Offset: 0x000E3904
	private void Update()
	{
		float t = Time.unscaledDeltaTime * 10f;
		if (!this.Show)
		{
			if (this.Panel.enabled)
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, 631f, t), base.transform.localPosition.z);
				if (base.transform.localPosition.y < 630f)
				{
					base.transform.localPosition = new Vector3(base.transform.localPosition.x, 631f, base.transform.localPosition.z);
					this.Panel.enabled = false;
					return;
				}
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, 530f, t), base.transform.localPosition.z);
		}
	}

	// Token: 0x040024F3 RID: 9459
	public UIPanel Panel;

	// Token: 0x040024F4 RID: 9460
	public bool Show;

	// Token: 0x040024F5 RID: 9461
	public UILabel Label;
}

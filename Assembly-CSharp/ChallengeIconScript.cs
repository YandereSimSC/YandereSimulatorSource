using System;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class ChallengeIconScript : MonoBehaviour
{
	// Token: 0x06000AA4 RID: 2724 RVA: 0x00058A20 File Offset: 0x00056C20
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			this.R = 1f;
			this.G = 0f;
			this.B = 0f;
			return;
		}
		this.R = 1f;
		this.G = 1f;
		this.B = 1f;
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x00058A78 File Offset: 0x00056C78
	private void Update()
	{
		if (base.transform.position.x > -0.125f && base.transform.position.x < 0.125f)
		{
			if (this.Icon != null)
			{
				this.LargeIcon.mainTexture = this.Icon.mainTexture;
			}
			this.Dark -= Time.deltaTime * 10f;
			if (this.Dark < 0f)
			{
				this.Dark = 0f;
			}
		}
		else
		{
			this.Dark += Time.deltaTime * 10f;
			if (this.Dark > 1f)
			{
				this.Dark = 1f;
			}
		}
		this.IconFrame.color = new Color(this.Dark * this.R, this.Dark * this.G, this.Dark * this.B, 1f);
		this.NameFrame.color = new Color(this.Dark * this.R, this.Dark * this.G, this.Dark * this.B, 1f);
		this.Name.color = new Color(this.Dark * this.R, this.Dark * this.G, this.Dark * this.B, 1f);
		if (GameGlobals.LoveSick)
		{
			if (base.transform.position.x > -0.125f && base.transform.position.x < 0.125f)
			{
				this.IconFrame.color = Color.white;
				this.NameFrame.color = Color.white;
				this.Name.color = Color.white;
				return;
			}
			this.IconFrame.color = new Color(this.R, this.G, this.B, 1f);
			this.NameFrame.color = new Color(this.R, this.G, this.B, 1f);
			this.Name.color = new Color(this.R, this.G, this.B, 1f);
		}
	}

	// Token: 0x04000B52 RID: 2898
	public UITexture LargeIcon;

	// Token: 0x04000B53 RID: 2899
	public UISprite IconFrame;

	// Token: 0x04000B54 RID: 2900
	public UISprite NameFrame;

	// Token: 0x04000B55 RID: 2901
	public UITexture Icon;

	// Token: 0x04000B56 RID: 2902
	public UILabel Name;

	// Token: 0x04000B57 RID: 2903
	public float Dark;

	// Token: 0x04000B58 RID: 2904
	private float R;

	// Token: 0x04000B59 RID: 2905
	private float G;

	// Token: 0x04000B5A RID: 2906
	private float B;
}

using System;
using UnityEngine;

// Token: 0x020001FA RID: 506
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Broken Glass2")]
public class CameraFilterPack_TV_BrokenGlass2 : MonoBehaviour
{
	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06001112 RID: 4370 RVA: 0x0007B3B6 File Offset: 0x000795B6
	private Material material
	{
		get
		{
			if (this.SCMaterial == null)
			{
				this.SCMaterial = new Material(this.SCShader);
				this.SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.SCMaterial;
		}
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x0007B3EA File Offset: 0x000795EA
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_TV_BrokenGlass_2") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/TV_BrokenGlass2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x0007B420 File Offset: 0x00079620
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			if (this.Bullet_1 < 0f)
			{
				this.Bullet_1 = 0f;
			}
			if (this.Bullet_2 < 0f)
			{
				this.Bullet_2 = 0f;
			}
			if (this.Bullet_3 < 0f)
			{
				this.Bullet_3 = 0f;
			}
			if (this.Bullet_4 < 0f)
			{
				this.Bullet_4 = 0f;
			}
			if (this.Bullet_5 < 0f)
			{
				this.Bullet_5 = 0f;
			}
			if (this.Bullet_6 < 0f)
			{
				this.Bullet_6 = 0f;
			}
			if (this.Bullet_7 < 0f)
			{
				this.Bullet_7 = 0f;
			}
			if (this.Bullet_8 < 0f)
			{
				this.Bullet_8 = 0f;
			}
			if (this.Bullet_9 < 0f)
			{
				this.Bullet_9 = 0f;
			}
			if (this.Bullet_10 < 0f)
			{
				this.Bullet_10 = 0f;
			}
			if (this.Bullet_11 < 0f)
			{
				this.Bullet_11 = 0f;
			}
			if (this.Bullet_12 < 0f)
			{
				this.Bullet_12 = 0f;
			}
			if (this.Bullet_1 > 1f)
			{
				this.Bullet_1 = 1f;
			}
			if (this.Bullet_2 > 1f)
			{
				this.Bullet_2 = 1f;
			}
			if (this.Bullet_3 > 1f)
			{
				this.Bullet_3 = 1f;
			}
			if (this.Bullet_4 > 1f)
			{
				this.Bullet_4 = 1f;
			}
			if (this.Bullet_5 > 1f)
			{
				this.Bullet_5 = 1f;
			}
			if (this.Bullet_6 > 1f)
			{
				this.Bullet_6 = 1f;
			}
			if (this.Bullet_7 > 1f)
			{
				this.Bullet_7 = 1f;
			}
			if (this.Bullet_8 > 1f)
			{
				this.Bullet_8 = 1f;
			}
			if (this.Bullet_9 > 1f)
			{
				this.Bullet_9 = 1f;
			}
			if (this.Bullet_10 > 1f)
			{
				this.Bullet_10 = 1f;
			}
			if (this.Bullet_11 > 1f)
			{
				this.Bullet_11 = 1f;
			}
			if (this.Bullet_12 > 1f)
			{
				this.Bullet_12 = 1f;
			}
			this.material.SetFloat("_Bullet_1", this.Bullet_1);
			this.material.SetFloat("_Bullet_2", this.Bullet_2);
			this.material.SetFloat("_Bullet_3", this.Bullet_3);
			this.material.SetFloat("_Bullet_4", this.Bullet_4);
			this.material.SetFloat("_Bullet_5", this.Bullet_5);
			this.material.SetFloat("_Bullet_6", this.Bullet_6);
			this.material.SetFloat("_Bullet_7", this.Bullet_7);
			this.material.SetFloat("_Bullet_8", this.Bullet_8);
			this.material.SetFloat("_Bullet_9", this.Bullet_9);
			this.material.SetFloat("_Bullet_10", this.Bullet_10);
			this.material.SetFloat("_Bullet_11", this.Bullet_11);
			this.material.SetFloat("_Bullet_12", this.Bullet_12);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x0007B7F1 File Offset: 0x000799F1
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001416 RID: 5142
	public Shader SCShader;

	// Token: 0x04001417 RID: 5143
	private float TimeX = 1f;

	// Token: 0x04001418 RID: 5144
	[Range(0f, 1f)]
	public float Bullet_1;

	// Token: 0x04001419 RID: 5145
	[Range(0f, 1f)]
	public float Bullet_2;

	// Token: 0x0400141A RID: 5146
	[Range(0f, 1f)]
	public float Bullet_3;

	// Token: 0x0400141B RID: 5147
	[Range(0f, 1f)]
	public float Bullet_4 = 1f;

	// Token: 0x0400141C RID: 5148
	[Range(0f, 1f)]
	public float Bullet_5;

	// Token: 0x0400141D RID: 5149
	[Range(0f, 1f)]
	public float Bullet_6;

	// Token: 0x0400141E RID: 5150
	[Range(0f, 1f)]
	public float Bullet_7;

	// Token: 0x0400141F RID: 5151
	[Range(0f, 1f)]
	public float Bullet_8;

	// Token: 0x04001420 RID: 5152
	[Range(0f, 1f)]
	public float Bullet_9;

	// Token: 0x04001421 RID: 5153
	[Range(0f, 1f)]
	public float Bullet_10;

	// Token: 0x04001422 RID: 5154
	[Range(0f, 1f)]
	public float Bullet_11;

	// Token: 0x04001423 RID: 5155
	[Range(0f, 1f)]
	public float Bullet_12;

	// Token: 0x04001424 RID: 5156
	private Material SCMaterial;

	// Token: 0x04001425 RID: 5157
	private Texture2D Texture2;
}

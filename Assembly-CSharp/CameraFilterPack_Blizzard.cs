using System;
using UnityEngine;

// Token: 0x02000137 RID: 311
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/Blizzard")]
public class CameraFilterPack_Blizzard : MonoBehaviour
{
	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06000C5A RID: 3162 RVA: 0x000667DD File Offset: 0x000649DD
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

	// Token: 0x06000C5B RID: 3163 RVA: 0x00066811 File Offset: 0x00064A11
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Blizzard1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Blizzard");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00066848 File Offset: 0x00064A48
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
			this.material.SetFloat("_Value", this._Speed);
			this.material.SetFloat("_Value2", this._Size);
			this.material.SetFloat("_Value3", this._Fade);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x00066913 File Offset: 0x00064B13
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F14 RID: 3860
	public Shader SCShader;

	// Token: 0x04000F15 RID: 3861
	private float TimeX = 1f;

	// Token: 0x04000F16 RID: 3862
	[Range(0f, 2f)]
	public float _Speed = 1f;

	// Token: 0x04000F17 RID: 3863
	[Range(0.2f, 2f)]
	public float _Size = 1f;

	// Token: 0x04000F18 RID: 3864
	[Range(0f, 1f)]
	public float _Fade = 1f;

	// Token: 0x04000F19 RID: 3865
	private Material SCMaterial;

	// Token: 0x04000F1A RID: 3866
	private Texture2D Texture2;
}

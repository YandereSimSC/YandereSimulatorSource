using System;
using UnityEngine;

// Token: 0x02000140 RID: 320
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blur/Noise")]
public class CameraFilterPack_Blur_Noise : MonoBehaviour
{
	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06000C90 RID: 3216 RVA: 0x000675C8 File Offset: 0x000657C8
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

	// Token: 0x06000C91 RID: 3217 RVA: 0x000675FC File Offset: 0x000657FC
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Blur_Noise");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x00067620 File Offset: 0x00065820
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
			this.material.SetFloat("_Level", (float)this.Level);
			this.material.SetVector("_Distance", this.Distance);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x000676F2 File Offset: 0x000658F2
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F4A RID: 3914
	public Shader SCShader;

	// Token: 0x04000F4B RID: 3915
	private float TimeX = 1f;

	// Token: 0x04000F4C RID: 3916
	private Material SCMaterial;

	// Token: 0x04000F4D RID: 3917
	[Range(2f, 16f)]
	public int Level = 4;

	// Token: 0x04000F4E RID: 3918
	public Vector2 Distance = new Vector2(30f, 0f);
}

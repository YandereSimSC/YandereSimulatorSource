using System;
using UnityEngine;

// Token: 0x02000223 RID: 547
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Warp2")]
public class CameraFilterPack_Vision_Warp2 : MonoBehaviour
{
	// Token: 0x17000343 RID: 835
	// (get) Token: 0x06001208 RID: 4616 RVA: 0x0007F5C5 File Offset: 0x0007D7C5
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

	// Token: 0x06001209 RID: 4617 RVA: 0x0007F5F9 File Offset: 0x0007D7F9
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Warp2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x0007F61C File Offset: 0x0007D81C
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
			this.material.SetFloat("_Value", this.Value);
			this.material.SetFloat("_Value2", this.Value2);
			this.material.SetFloat("_Value3", this.Intensity);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x0007F714 File Offset: 0x0007D914
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001523 RID: 5411
	public Shader SCShader;

	// Token: 0x04001524 RID: 5412
	private float TimeX = 1f;

	// Token: 0x04001525 RID: 5413
	private Material SCMaterial;

	// Token: 0x04001526 RID: 5414
	[Range(0f, 1f)]
	public float Value = 0.5f;

	// Token: 0x04001527 RID: 5415
	[Range(0f, 1f)]
	public float Value2 = 0.2f;

	// Token: 0x04001528 RID: 5416
	[Range(-1f, 2f)]
	public float Intensity = 1f;

	// Token: 0x04001529 RID: 5417
	[Range(0f, 10f)]
	private float Value4 = 1f;
}

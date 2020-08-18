using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Fly_Vision")]
public class CameraFilterPack_Fly_Vision : MonoBehaviour
{
	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00072A2E File Offset: 0x00070C2E
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

	// Token: 0x06000F58 RID: 3928 RVA: 0x00072A62 File Offset: 0x00070C62
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Fly_VisionFX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Fly_Vision");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x00072A98 File Offset: 0x00070C98
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
			this.material.SetFloat("_Value", this.Zoom);
			this.material.SetFloat("_Value2", this.Distortion);
			this.material.SetFloat("_Value3", this.Fade);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x00072BA6 File Offset: 0x00070DA6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001207 RID: 4615
	public Shader SCShader;

	// Token: 0x04001208 RID: 4616
	private float TimeX = 1f;

	// Token: 0x04001209 RID: 4617
	private Material SCMaterial;

	// Token: 0x0400120A RID: 4618
	[Range(0.04f, 1.5f)]
	public float Zoom = 0.25f;

	// Token: 0x0400120B RID: 4619
	[Range(0f, 1f)]
	public float Distortion = 0.4f;

	// Token: 0x0400120C RID: 4620
	[Range(0f, 1f)]
	public float Fade = 0.4f;

	// Token: 0x0400120D RID: 4621
	[Range(0f, 10f)]
	private float Value4 = 1f;

	// Token: 0x0400120E RID: 4622
	private Texture2D Texture2;
}

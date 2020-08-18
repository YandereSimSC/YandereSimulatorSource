using System;
using UnityEngine;

// Token: 0x02000206 RID: 518
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/TV/Planet Mars")]
public class CameraFilterPack_TV_PlanetMars : MonoBehaviour
{
	// Token: 0x17000326 RID: 806
	// (get) Token: 0x0600115A RID: 4442 RVA: 0x0007C747 File Offset: 0x0007A947
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

	// Token: 0x0600115B RID: 4443 RVA: 0x0007C77B File Offset: 0x0007A97B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/TV_PlanetMars");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x0007C79C File Offset: 0x0007A99C
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
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetFloat("Fade", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x0007C868 File Offset: 0x0007AA68
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001460 RID: 5216
	public Shader SCShader;

	// Token: 0x04001461 RID: 5217
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04001462 RID: 5218
	private float TimeX = 1f;

	// Token: 0x04001463 RID: 5219
	[Range(-10f, 10f)]
	public float Distortion = 1f;

	// Token: 0x04001464 RID: 5220
	private Material SCMaterial;
}

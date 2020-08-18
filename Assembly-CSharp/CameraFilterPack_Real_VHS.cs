using System;
using UnityEngine;

// Token: 0x020001EF RID: 495
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/VHS/Real VHS HQ")]
public class CameraFilterPack_Real_VHS : MonoBehaviour
{
	// Token: 0x1700030F RID: 783
	// (get) Token: 0x060010CF RID: 4303 RVA: 0x0007A2A7 File Offset: 0x000784A7
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

	// Token: 0x060010D0 RID: 4304 RVA: 0x0007A2DC File Offset: 0x000784DC
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Real_VHS");
		this.VHS = (Resources.Load("CameraFilterPack_VHS1") as Texture2D);
		this.VHS2 = (Resources.Load("CameraFilterPack_VHS2") as Texture2D);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0007A332 File Offset: 0x00078532
	public static Texture2D GetRTPixels(Texture2D t, RenderTexture rt, int sx, int sy)
	{
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = rt;
		t.ReadPixels(new Rect(0f, 0f, (float)t.width, (float)t.height), 0, 0);
		RenderTexture.active = active;
		return t;
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x0007A36C File Offset: 0x0007856C
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.material.SetTexture("VHS", this.VHS);
			this.material.SetTexture("VHS2", this.VHS2);
			this.material.SetFloat("TRACKING", this.TRACKING);
			this.material.SetFloat("JITTER", this.JITTER);
			this.material.SetFloat("GLITCH", this.GLITCH);
			this.material.SetFloat("NOISE", this.NOISE);
			this.material.SetFloat("Brightness", this.Brightness);
			this.material.SetFloat("CONTRAST", 1f - this.Constrast);
			int width = 382;
			int height = 576;
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
			temporary.filterMode = FilterMode.Trilinear;
			Graphics.Blit(sourceTexture, temporary, this.material);
			Graphics.Blit(temporary, destTexture);
			RenderTexture.ReleaseTemporary(temporary);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x0007A47C File Offset: 0x0007867C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013D1 RID: 5073
	public Shader SCShader;

	// Token: 0x040013D2 RID: 5074
	private Material SCMaterial;

	// Token: 0x040013D3 RID: 5075
	private Texture2D VHS;

	// Token: 0x040013D4 RID: 5076
	private Texture2D VHS2;

	// Token: 0x040013D5 RID: 5077
	[Range(0f, 1f)]
	public float TRACKING = 0.212f;

	// Token: 0x040013D6 RID: 5078
	[Range(0f, 1f)]
	public float JITTER = 1f;

	// Token: 0x040013D7 RID: 5079
	[Range(0f, 1f)]
	public float GLITCH = 1f;

	// Token: 0x040013D8 RID: 5080
	[Range(0f, 1f)]
	public float NOISE = 1f;

	// Token: 0x040013D9 RID: 5081
	[Range(-1f, 1f)]
	public float Brightness;

	// Token: 0x040013DA RID: 5082
	[Range(0f, 1.5f)]
	public float Constrast = 1f;
}

using System;
using UnityEngine;

// Token: 0x020001ED RID: 493
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Pixelisation/Pixelisation_Sweater")]
public class CameraFilterPack_Pixelisation_Sweater : MonoBehaviour
{
	// Token: 0x1700030D RID: 781
	// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00079DC2 File Offset: 0x00077FC2
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

	// Token: 0x060010C4 RID: 4292 RVA: 0x00079DF6 File Offset: 0x00077FF6
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Sweater") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Pixelisation_Sweater");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x00079E2C File Offset: 0x0007802C
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
			this.material.SetFloat("_Fade", this.Fade);
			this.material.SetFloat("_SweaterSize", this.SweaterSize);
			this.material.SetFloat("_Intensity", this._Intensity);
			this.material.SetTexture("Texture2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x00079EF7 File Offset: 0x000780F7
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013C0 RID: 5056
	public Shader SCShader;

	// Token: 0x040013C1 RID: 5057
	private float TimeX = 1f;

	// Token: 0x040013C2 RID: 5058
	private Material SCMaterial;

	// Token: 0x040013C3 RID: 5059
	[Range(16f, 128f)]
	public float SweaterSize = 64f;

	// Token: 0x040013C4 RID: 5060
	[Range(0f, 2f)]
	public float _Intensity = 1.4f;

	// Token: 0x040013C5 RID: 5061
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x040013C6 RID: 5062
	private Texture2D Texture2;
}

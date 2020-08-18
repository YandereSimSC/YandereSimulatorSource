using System;
using UnityEngine;

// Token: 0x020001D5 RID: 469
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/NewGlitch1")]
public class CameraFilterPack_NewGlitch1 : MonoBehaviour
{
	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x0600102D RID: 4141 RVA: 0x00077050 File Offset: 0x00075250
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

	// Token: 0x0600102E RID: 4142 RVA: 0x00077084 File Offset: 0x00075284
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_NewGlitch1");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600102F RID: 4143 RVA: 0x000770A8 File Offset: 0x000752A8
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
			this.material.SetFloat("Seed", this._Seed);
			this.material.SetFloat("Size", this._Size);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x00077174 File Offset: 0x00075374
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001314 RID: 4884
	public Shader SCShader;

	// Token: 0x04001315 RID: 4885
	private float TimeX = 1f;

	// Token: 0x04001316 RID: 4886
	private Material SCMaterial;

	// Token: 0x04001317 RID: 4887
	[Range(0f, 1f)]
	public float _Seed = 1f;

	// Token: 0x04001318 RID: 4888
	[Range(0f, 1f)]
	public float _Size = 1f;
}

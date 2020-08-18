using System;
using UnityEngine;

// Token: 0x020001D6 RID: 470
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glitch/NewGlitch2")]
public class CameraFilterPack_NewGlitch2 : MonoBehaviour
{
	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06001033 RID: 4147 RVA: 0x000771B7 File Offset: 0x000753B7
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

	// Token: 0x06001034 RID: 4148 RVA: 0x000771EB File Offset: 0x000753EB
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/CameraFilterPack_NewGlitch2");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0007720C File Offset: 0x0007540C
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
			this.material.SetFloat("_Speed", this.__Speed);
			this.material.SetFloat("RedFade", this._RedFade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x000772D8 File Offset: 0x000754D8
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001319 RID: 4889
	public Shader SCShader;

	// Token: 0x0400131A RID: 4890
	private float TimeX = 1f;

	// Token: 0x0400131B RID: 4891
	private Material SCMaterial;

	// Token: 0x0400131C RID: 4892
	[Range(0f, 1f)]
	public float __Speed = 1f;

	// Token: 0x0400131D RID: 4893
	[Range(0f, 1f)]
	public float _RedFade = 1f;
}

using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Dream")]
public class CameraFilterPack_Distortion_Dream : MonoBehaviour
{
	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06000D82 RID: 3458 RVA: 0x0006B8A1 File Offset: 0x00069AA1
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

	// Token: 0x06000D83 RID: 3459 RVA: 0x0006B8D5 File Offset: 0x00069AD5
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Dream");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0006B8F8 File Offset: 0x00069AF8
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
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x0006B97E File Offset: 0x00069B7E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001040 RID: 4160
	public Shader SCShader;

	// Token: 0x04001041 RID: 4161
	private float TimeX = 1f;

	// Token: 0x04001042 RID: 4162
	[Range(1f, 10f)]
	public float Distortion = 1f;

	// Token: 0x04001043 RID: 4163
	private Material SCMaterial;
}

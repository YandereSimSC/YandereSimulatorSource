using System;
using UnityEngine;

// Token: 0x02000217 RID: 535
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/AuraDistortion")]
public class CameraFilterPack_Vision_AuraDistortion : MonoBehaviour
{
	// Token: 0x17000337 RID: 823
	// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0007DFBC File Offset: 0x0007C1BC
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

	// Token: 0x060011C1 RID: 4545 RVA: 0x0007DFF0 File Offset: 0x0007C1F0
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_AuraDistortion");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x0007E014 File Offset: 0x0007C214
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
			this.material.SetFloat("_Value", this.Twist);
			this.material.SetColor("_Value2", this.Color);
			this.material.SetFloat("_Value3", this.PosX);
			this.material.SetFloat("_Value4", this.PosY);
			this.material.SetFloat("_Value5", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x0007E122 File Offset: 0x0007C322
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014C1 RID: 5313
	public Shader SCShader;

	// Token: 0x040014C2 RID: 5314
	private float TimeX = 1f;

	// Token: 0x040014C3 RID: 5315
	private Material SCMaterial;

	// Token: 0x040014C4 RID: 5316
	[Range(0f, 2f)]
	public float Twist = 1f;

	// Token: 0x040014C5 RID: 5317
	[Range(-4f, 4f)]
	public float Speed = 1f;

	// Token: 0x040014C6 RID: 5318
	public Color Color = new Color(0.16f, 0.57f, 0.19f);

	// Token: 0x040014C7 RID: 5319
	[Range(-1f, 2f)]
	public float PosX = 0.5f;

	// Token: 0x040014C8 RID: 5320
	[Range(-1f, 2f)]
	public float PosY = 0.5f;
}

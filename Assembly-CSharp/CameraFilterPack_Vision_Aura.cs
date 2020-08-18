using System;
using UnityEngine;

// Token: 0x02000216 RID: 534
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Aura")]
public class CameraFilterPack_Vision_Aura : MonoBehaviour
{
	// Token: 0x17000336 RID: 822
	// (get) Token: 0x060011BA RID: 4538 RVA: 0x0007DDDA File Offset: 0x0007BFDA
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

	// Token: 0x060011BB RID: 4539 RVA: 0x0007DE0E File Offset: 0x0007C00E
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Aura");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x0007DE30 File Offset: 0x0007C030
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

	// Token: 0x060011BD RID: 4541 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x0007DF3E File Offset: 0x0007C13E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014B9 RID: 5305
	public Shader SCShader;

	// Token: 0x040014BA RID: 5306
	private float TimeX = 1f;

	// Token: 0x040014BB RID: 5307
	private Material SCMaterial;

	// Token: 0x040014BC RID: 5308
	[Range(0f, 2f)]
	public float Twist = 1f;

	// Token: 0x040014BD RID: 5309
	[Range(-4f, 4f)]
	public float Speed = 1f;

	// Token: 0x040014BE RID: 5310
	public Color Color = new Color(0.16f, 0.57f, 0.19f);

	// Token: 0x040014BF RID: 5311
	[Range(-1f, 2f)]
	public float PosX = 0.5f;

	// Token: 0x040014C0 RID: 5312
	[Range(-1f, 2f)]
	public float PosY = 0.5f;
}

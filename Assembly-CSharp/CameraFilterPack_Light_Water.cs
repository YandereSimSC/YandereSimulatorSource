using System;
using UnityEngine;

// Token: 0x020001CC RID: 460
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Light/Water")]
public class CameraFilterPack_Light_Water : MonoBehaviour
{
	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06000FDB RID: 4059 RVA: 0x000753C2 File Offset: 0x000735C2
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

	// Token: 0x06000FDC RID: 4060 RVA: 0x000753F6 File Offset: 0x000735F6
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Light_Water");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x00075418 File Offset: 0x00073618
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime * this.Speed;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Alpha", this.Alpha);
			this.material.SetFloat("_Distance", this.Distance);
			this.material.SetFloat("_Size", this.Size);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x00075501 File Offset: 0x00073701
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012BF RID: 4799
	public Shader SCShader;

	// Token: 0x040012C0 RID: 4800
	private float TimeX = 1f;

	// Token: 0x040012C1 RID: 4801
	private Material SCMaterial;

	// Token: 0x040012C2 RID: 4802
	[Range(0f, 1f)]
	public float Size = 4f;

	// Token: 0x040012C3 RID: 4803
	[Range(0f, 2f)]
	public float Alpha = 0.07f;

	// Token: 0x040012C4 RID: 4804
	[Range(0f, 32f)]
	public float Distance = 10f;

	// Token: 0x040012C5 RID: 4805
	[Range(-2f, 2f)]
	public float Speed = 0.4f;
}

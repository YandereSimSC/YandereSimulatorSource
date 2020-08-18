using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000190 RID: 400
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/EXTRA/SHOWFPS")]
public class CameraFilterPack_EXTRA_SHOWFPS : MonoBehaviour
{
	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0006F453 File Offset: 0x0006D653
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

	// Token: 0x06000E73 RID: 3699 RVA: 0x0006F487 File Offset: 0x0006D687
	private void Start()
	{
		this.FPS = 0;
		base.StartCoroutine(this.FPSX());
		this.SCShader = Shader.Find("CameraFilterPack/EXTRA_SHOWFPS");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x0006F4BC File Offset: 0x0006D6BC
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
			this.material.SetFloat("_Value", this.Size);
			this.material.SetFloat("_Value2", (float)this.FPS);
			this.material.SetFloat("_Value3", this.Value3);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0006F5B5 File Offset: 0x0006D7B5
	private IEnumerator FPSX()
	{
		for (;;)
		{
			float num = this.accum / (float)this.frames;
			this.FPS = (int)num;
			this.accum = 0f;
			this.frames = 0;
			yield return new WaitForSeconds(this.frequency);
		}
		yield break;
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x0006F5C4 File Offset: 0x0006D7C4
	private void Update()
	{
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0006F5EC File Offset: 0x0006D7EC
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001139 RID: 4409
	public Shader SCShader;

	// Token: 0x0400113A RID: 4410
	private float TimeX = 1f;

	// Token: 0x0400113B RID: 4411
	private Material SCMaterial;

	// Token: 0x0400113C RID: 4412
	[Range(8f, 42f)]
	public float Size = 12f;

	// Token: 0x0400113D RID: 4413
	[Range(0f, 100f)]
	private int FPS = 1;

	// Token: 0x0400113E RID: 4414
	[Range(0f, 10f)]
	private float Value3 = 1f;

	// Token: 0x0400113F RID: 4415
	[Range(0f, 10f)]
	private float Value4 = 1f;

	// Token: 0x04001140 RID: 4416
	private float accum;

	// Token: 0x04001141 RID: 4417
	private int frames;

	// Token: 0x04001142 RID: 4418
	public float frequency = 0.5f;
}

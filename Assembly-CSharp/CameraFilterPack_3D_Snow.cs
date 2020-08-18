using System;
using UnityEngine;

// Token: 0x02000107 RID: 263
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Snow")]
public class CameraFilterPack_3D_Snow : MonoBehaviour
{
	// Token: 0x17000227 RID: 551
	// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0005FAD0 File Offset: 0x0005DCD0
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

	// Token: 0x06000AFC RID: 2812 RVA: 0x0005FB04 File Offset: 0x0005DD04
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Blizzard1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/3D_Snow");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x0005FB3C File Offset: 0x0005DD3C
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("_Value4", this.Speed * 6f);
			this.material.SetFloat("_Value5", this.Size);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("Drop_Near", this.Snow_Near);
			this.material.SetFloat("Drop_Far", this.Snow_Far);
			this.material.SetFloat("Drop_With_Obj", this.SnowWithoutObject);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x0005FCD1 File Offset: 0x0005DED1
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D43 RID: 3395
	public Shader SCShader;

	// Token: 0x04000D44 RID: 3396
	public bool _Visualize;

	// Token: 0x04000D45 RID: 3397
	private float TimeX = 1f;

	// Token: 0x04000D46 RID: 3398
	private Material SCMaterial;

	// Token: 0x04000D47 RID: 3399
	[Range(0f, 100f)]
	public float _FixDistance = 5f;

	// Token: 0x04000D48 RID: 3400
	[Range(-0.5f, 0.99f)]
	public float Snow_Near = 0.08f;

	// Token: 0x04000D49 RID: 3401
	[Range(0f, 1f)]
	public float Snow_Far = 0.55f;

	// Token: 0x04000D4A RID: 3402
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000D4B RID: 3403
	[Range(0f, 2f)]
	public float Intensity = 1f;

	// Token: 0x04000D4C RID: 3404
	[Range(0.4f, 2f)]
	public float Size = 1f;

	// Token: 0x04000D4D RID: 3405
	[Range(0f, 0.5f)]
	public float Speed = 0.275f;

	// Token: 0x04000D4E RID: 3406
	[Range(0f, 1f)]
	public float SnowWithoutObject = 1f;

	// Token: 0x04000D4F RID: 3407
	private Texture2D Texture2;
}

using System;
using UnityEngine;

// Token: 0x02000100 RID: 256
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Ghost")]
public class CameraFilterPack_3D_Ghost : MonoBehaviour
{
	// Token: 0x17000220 RID: 544
	// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0005E803 File Offset: 0x0005CA03
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

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0005E837 File Offset: 0x0005CA37
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Ghost");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0005E858 File Offset: 0x0005CA58
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
			this.material.SetFloat("_Value2", this.Intensity);
			this.material.SetFloat("GhostPosX", this.GhostPosX);
			this.material.SetFloat("GhostPosY", this.GhostPosY);
			this.material.SetFloat("GhostFade", this.GhostFade);
			this.material.SetFloat("GhostFade2", this.GhostFade2);
			this.material.SetFloat("GhostSize", this.GhostSize);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("Drop_Near", this.Ghost_Near);
			this.material.SetFloat("Drop_Far", this.Ghost_Far);
			this.material.SetFloat("Drop_With_Obj", this.GhostWithoutObject);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0005E9FD File Offset: 0x0005CBFD
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000CE7 RID: 3303
	public Shader SCShader;

	// Token: 0x04000CE8 RID: 3304
	private float TimeX = 1f;

	// Token: 0x04000CE9 RID: 3305
	public bool _Visualize;

	// Token: 0x04000CEA RID: 3306
	private Material SCMaterial;

	// Token: 0x04000CEB RID: 3307
	[Range(0f, 100f)]
	public float _FixDistance = 5f;

	// Token: 0x04000CEC RID: 3308
	[Range(-0.5f, 0.99f)]
	public float Ghost_Near = 0.08f;

	// Token: 0x04000CED RID: 3309
	[Range(0f, 1f)]
	public float Ghost_Far = 0.55f;

	// Token: 0x04000CEE RID: 3310
	[Range(0f, 2f)]
	public float Intensity = 1f;

	// Token: 0x04000CEF RID: 3311
	[Range(0f, 1f)]
	public float GhostWithoutObject = 1f;

	// Token: 0x04000CF0 RID: 3312
	[Range(-1f, 1f)]
	public float GhostPosX;

	// Token: 0x04000CF1 RID: 3313
	[Range(-1f, 1f)]
	public float GhostPosY;

	// Token: 0x04000CF2 RID: 3314
	[Range(0.1f, 8f)]
	public float GhostFade2 = 2f;

	// Token: 0x04000CF3 RID: 3315
	[Range(-1f, 1f)]
	public float GhostFade;

	// Token: 0x04000CF4 RID: 3316
	[Range(0.5f, 1.5f)]
	public float GhostSize = 0.9f;
}

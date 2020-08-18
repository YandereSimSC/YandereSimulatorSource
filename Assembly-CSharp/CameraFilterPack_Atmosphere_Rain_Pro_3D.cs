using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/Rain_Pro_3D")]
public class CameraFilterPack_Atmosphere_Rain_Pro_3D : MonoBehaviour
{
	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06000B4F RID: 2895 RVA: 0x000618F6 File Offset: 0x0005FAF6
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

	// Token: 0x06000B50 RID: 2896 RVA: 0x0006192A File Offset: 0x0005FB2A
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Atmosphere_Rain_FX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Atmosphere_Rain_Pro_3D");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x00061960 File Offset: 0x0005FB60
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
			if (this.DirectionFollowCameraZ)
			{
				float z = base.GetComponent<Camera>().transform.rotation.z;
				if (z > 0f && z < 360f)
				{
					this.material.SetFloat("_Value3", z);
				}
				if (z < 0f)
				{
					this.material.SetFloat("_Value3", z);
				}
			}
			else
			{
				this.material.SetFloat("_Value3", this.DirectionX);
			}
			this.material.SetFloat("_Value4", this.Speed);
			this.material.SetFloat("_Value5", this.Size);
			this.material.SetFloat("_Value6", this.Distortion);
			this.material.SetFloat("_Value7", this.StormFlashOnOff);
			this.material.SetFloat("_Value8", this.DropOnOff);
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("Drop_Near", this.Drop_Near);
			this.material.SetFloat("Drop_Far", this.Drop_Far);
			this.material.SetFloat("Drop_With_Obj", 1f - this.Drop_With_Obj);
			this.material.SetFloat("Myst", this.Myst);
			this.material.SetColor("Myst_Color", this.Myst_Color);
			this.material.SetFloat("Drop_Floor_Fluid", this.Drop_Floor_Fluid);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00061BE9 File Offset: 0x0005FDE9
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000DD5 RID: 3541
	public Shader SCShader;

	// Token: 0x04000DD6 RID: 3542
	public bool _Visualize;

	// Token: 0x04000DD7 RID: 3543
	private float TimeX = 1f;

	// Token: 0x04000DD8 RID: 3544
	private Material SCMaterial;

	// Token: 0x04000DD9 RID: 3545
	[Range(0f, 100f)]
	public float _FixDistance = 3f;

	// Token: 0x04000DDA RID: 3546
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000DDB RID: 3547
	[Range(0f, 2f)]
	public float Intensity = 0.5f;

	// Token: 0x04000DDC RID: 3548
	public bool DirectionFollowCameraZ;

	// Token: 0x04000DDD RID: 3549
	[Range(-0.45f, 0.45f)]
	public float DirectionX = 0.12f;

	// Token: 0x04000DDE RID: 3550
	[Range(0.4f, 2f)]
	public float Size = 1.5f;

	// Token: 0x04000DDF RID: 3551
	[Range(0f, 0.5f)]
	public float Speed = 0.275f;

	// Token: 0x04000DE0 RID: 3552
	[Range(0f, 0.5f)]
	public float Distortion = 0.025f;

	// Token: 0x04000DE1 RID: 3553
	[Range(0f, 1f)]
	public float StormFlashOnOff = 1f;

	// Token: 0x04000DE2 RID: 3554
	[Range(0f, 1f)]
	public float DropOnOff = 1f;

	// Token: 0x04000DE3 RID: 3555
	[Range(-0.5f, 0.99f)]
	public float Drop_Near;

	// Token: 0x04000DE4 RID: 3556
	[Range(0f, 1f)]
	public float Drop_Far = 0.5f;

	// Token: 0x04000DE5 RID: 3557
	[Range(0f, 1f)]
	public float Drop_With_Obj = 0.2f;

	// Token: 0x04000DE6 RID: 3558
	[Range(0f, 1f)]
	public float Myst = 0.1f;

	// Token: 0x04000DE7 RID: 3559
	[Range(0f, 1f)]
	public float Drop_Floor_Fluid;

	// Token: 0x04000DE8 RID: 3560
	public Color Myst_Color = new Color(0.5f, 0.5f, 0.5f, 1f);

	// Token: 0x04000DE9 RID: 3561
	private Texture2D Texture2;
}

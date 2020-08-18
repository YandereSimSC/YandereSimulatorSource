using System;
using UnityEngine;

// Token: 0x020000FA RID: 250
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/3D/Anomaly")]
public class CameraFilterPack_3D_Anomaly : MonoBehaviour
{
	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0005D842 File Offset: 0x0005BA42
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

	// Token: 0x06000AAE RID: 2734 RVA: 0x0005D876 File Offset: 0x0005BA76
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/3D_Anomaly");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0005D898 File Offset: 0x0005BA98
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
			this.material.SetFloat("Anomaly_Distortion", this.Anomaly_Distortion);
			this.material.SetFloat("Anomaly_Distortion_Size", this.Anomaly_Distortion_Size);
			this.material.SetFloat("Anomaly_Intensity", this.Anomaly_Intensity);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("Anomaly_Near", this.Anomaly_Near);
			this.material.SetFloat("Anomaly_Far", this.Anomaly_Far);
			this.material.SetFloat("Anomaly_With_Obj", this.AnomalyWithoutObject);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0005DA11 File Offset: 0x0005BC11
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000C9B RID: 3227
	public Shader SCShader;

	// Token: 0x04000C9C RID: 3228
	public bool _Visualize;

	// Token: 0x04000C9D RID: 3229
	private float TimeX = 1f;

	// Token: 0x04000C9E RID: 3230
	private Material SCMaterial;

	// Token: 0x04000C9F RID: 3231
	[Range(0f, 100f)]
	public float _FixDistance = 23f;

	// Token: 0x04000CA0 RID: 3232
	[Range(-0.5f, 0.99f)]
	public float Anomaly_Near = 0.045f;

	// Token: 0x04000CA1 RID: 3233
	[Range(0f, 1f)]
	public float Anomaly_Far = 0.11f;

	// Token: 0x04000CA2 RID: 3234
	[Range(0f, 2f)]
	public float Intensity = 1f;

	// Token: 0x04000CA3 RID: 3235
	[Range(0f, 1f)]
	public float AnomalyWithoutObject = 1f;

	// Token: 0x04000CA4 RID: 3236
	[Range(0.1f, 1f)]
	public float Anomaly_Distortion = 0.25f;

	// Token: 0x04000CA5 RID: 3237
	[Range(4f, 64f)]
	public float Anomaly_Distortion_Size = 12f;

	// Token: 0x04000CA6 RID: 3238
	[Range(-4f, 8f)]
	public float Anomaly_Intensity = 2f;
}

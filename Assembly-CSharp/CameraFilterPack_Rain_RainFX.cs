using System;
using UnityEngine;

// Token: 0x020001EE RID: 494
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/New Rain FX")]
public class CameraFilterPack_Rain_RainFX : MonoBehaviour
{
	// Token: 0x1700030E RID: 782
	// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00079F45 File Offset: 0x00078145
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

	// Token: 0x060010CA RID: 4298 RVA: 0x00079F7C File Offset: 0x0007817C
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_RainFX_Anm2") as Texture2D);
		this.Texture3 = (Resources.Load("CameraFilterPack_RainFX_Anm") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/RainFX");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x00079FD4 File Offset: 0x000781D4
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
			this.material.SetFloat("_Speed", this.Speed);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			AnimationCurve animationCurve = new AnimationCurve();
			animationCurve = new AnimationCurve();
			animationCurve.AddKey(0f, 0.01f);
			animationCurve.AddKey(64f, 5f);
			animationCurve.AddKey(128f, 80f);
			animationCurve.AddKey(255f, 255f);
			animationCurve.AddKey(300f, 255f);
			for (int i = 0; i < 4; i++)
			{
				Vector4[] coord = this.Coord;
				int num = i;
				coord[num].z = coord[num].z + 0.5f;
				if (this.Coord[i].w == -1f)
				{
					this.Coord[i].x = -5f;
				}
				if (this.Coord[i].z > 254f)
				{
					this.Coord[i] = new Vector4(UnityEngine.Random.Range(0f, 0.9f), UnityEngine.Random.Range(0.2f, 1.1f), 0f, (float)UnityEngine.Random.Range(0, 3));
				}
				this.material.SetVector("Coord" + (i + 1).ToString(), new Vector4(this.Coord[i].x, this.Coord[i].y, (float)((int)animationCurve.Evaluate(this.Coord[i].z)), this.Coord[i].w));
			}
			this.material.SetTexture("Texture2", this.Texture2);
			this.material.SetTexture("Texture3", this.Texture3);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0007A258 File Offset: 0x00078458
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013C7 RID: 5063
	public Shader SCShader;

	// Token: 0x040013C8 RID: 5064
	private float TimeX = 1f;

	// Token: 0x040013C9 RID: 5065
	private Material SCMaterial;

	// Token: 0x040013CA RID: 5066
	[Range(-8f, 8f)]
	public float Speed = 1f;

	// Token: 0x040013CB RID: 5067
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x040013CC RID: 5068
	[HideInInspector]
	public int Count;

	// Token: 0x040013CD RID: 5069
	private Vector4[] Coord = new Vector4[4];

	// Token: 0x040013CE RID: 5070
	public static Color ChangeColorRGB;

	// Token: 0x040013CF RID: 5071
	private Texture2D Texture2;

	// Token: 0x040013D0 RID: 5072
	private Texture2D Texture3;
}

using System;
using UnityEngine;

// Token: 0x0200021C RID: 540
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Hell_Blood")]
public class CameraFilterPack_Vision_Hell_Blood : MonoBehaviour
{
	// Token: 0x1700033C RID: 828
	// (get) Token: 0x060011DE RID: 4574 RVA: 0x0007E841 File Offset: 0x0007CA41
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

	// Token: 0x060011DF RID: 4575 RVA: 0x0007E875 File Offset: 0x0007CA75
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Hell_Blood");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x0007E898 File Offset: 0x0007CA98
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
			this.material.SetFloat("_Value", this.Hole_Size);
			this.material.SetFloat("_Value2", this.Hole_Smooth);
			this.material.SetFloat("_Value3", this.Hole_Speed * 15f);
			this.material.SetColor("ColorBlood", this.ColorBlood);
			this.material.SetFloat("_Value4", this.Intensity);
			this.material.SetFloat("BloodAlternative1", this.BloodAlternative1);
			this.material.SetFloat("BloodAlternative2", this.BloodAlternative2);
			this.material.SetFloat("BloodAlternative3", this.BloodAlternative3);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x0007E9EE File Offset: 0x0007CBEE
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014E5 RID: 5349
	public Shader SCShader;

	// Token: 0x040014E6 RID: 5350
	private float TimeX = 1f;

	// Token: 0x040014E7 RID: 5351
	private Material SCMaterial;

	// Token: 0x040014E8 RID: 5352
	[Range(0f, 1f)]
	public float Hole_Size = 0.57f;

	// Token: 0x040014E9 RID: 5353
	[Range(0f, 0.5f)]
	public float Hole_Smooth = 0.362f;

	// Token: 0x040014EA RID: 5354
	[Range(-2f, 2f)]
	public float Hole_Speed = 0.85f;

	// Token: 0x040014EB RID: 5355
	[Range(-10f, 10f)]
	public float Intensity = 0.24f;

	// Token: 0x040014EC RID: 5356
	public Color ColorBlood = new Color(1f, 0f, 0f, 1f);

	// Token: 0x040014ED RID: 5357
	[Range(-1f, 1f)]
	public float BloodAlternative1;

	// Token: 0x040014EE RID: 5358
	[Range(-1f, 1f)]
	public float BloodAlternative2;

	// Token: 0x040014EF RID: 5359
	[Range(-1f, 1f)]
	public float BloodAlternative3 = -1f;
}

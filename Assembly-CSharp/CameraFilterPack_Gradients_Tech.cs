using System;
using UnityEngine;

// Token: 0x020001C8 RID: 456
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Gradients/Tech")]
public class CameraFilterPack_Gradients_Tech : MonoBehaviour
{
	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00074E5A File Offset: 0x0007305A
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

	// Token: 0x06000FC4 RID: 4036 RVA: 0x00074E8E File Offset: 0x0007308E
	private void Start()
	{
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x00074EB0 File Offset: 0x000730B0
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
			this.material.SetFloat("_Value", this.Switch);
			this.material.SetFloat("_Value2", this.Fade);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x00074F7C File Offset: 0x0007317C
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040012AB RID: 4779
	public Shader SCShader;

	// Token: 0x040012AC RID: 4780
	private string ShaderName = "CameraFilterPack/Gradients_Tech";

	// Token: 0x040012AD RID: 4781
	private float TimeX = 1f;

	// Token: 0x040012AE RID: 4782
	private Material SCMaterial;

	// Token: 0x040012AF RID: 4783
	[Range(0f, 1f)]
	public float Switch = 1f;

	// Token: 0x040012B0 RID: 4784
	[Range(0f, 1f)]
	public float Fade = 1f;
}

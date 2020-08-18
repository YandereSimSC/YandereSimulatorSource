using System;
using UnityEngine;

// Token: 0x0200048E RID: 1166
[ExecuteInEditMode]
public class BlacklightEffect : MonoBehaviour
{
	// Token: 0x06001DF5 RID: 7669 RVA: 0x001777FC File Offset: 0x001759FC
	private void Update()
	{
		if (this.camera != null)
		{
			this.camera.depthTextureMode = (DepthTextureMode.Depth | DepthTextureMode.DepthNormals);
		}
		if (this.post != null)
		{
			this.post.SetFloat("_DepthDistance", this.fogDepth);
			this.post.SetColor("_FogColorDark", this.fogColorDark);
			this.post.SetColor("_FogColorLight", this.fogColorLight);
			this.post.SetFloat("_FogOpacity", this.fogOpacity);
			this.post.SetFloat("_GlowBias", this.glowBias);
			this.post.SetColor("_GlowColor", this.glowColor);
			this.post.SetColor("_GlowColor2", this.glowColorSecondary);
			this.post.SetFloat("_GlowAmount", (float)(this.glow ? 1 : 0));
			this.post.SetColor("_EdgeColor", this.edgeColor);
			this.post.SetFloat("_EdgeThreshold", this.threshold);
			this.post.SetFloat("_EdgeStrength", this.edgeOpacity);
			this.post.SetColor("_OverlayTop", this.overlayTop);
			this.post.SetColor("_OverlayBottom", this.overlayBottom);
			this.post.SetFloat("_OverlayOpacity", this.overlayOpacity);
			this.post.SetFloat("_HighlightFlip", this.glowFlip);
			this.post.SetFloat("_HighlightTargetSmooth", this.smoothDropoff);
			if (this.highlightTargets != null)
			{
				for (int i = 0; i < this.highlightTargets.Length; i++)
				{
					this.hTargets[i] = this.highlightTargets[i].TargetColor;
					this.hThresholds[i] = this.highlightTargets[i].Threshold;
					this.hColors[i] = this.highlightTargets[i].ReplacementColor;
				}
			}
			if (this.highlightTargets != null && this.highlightTargets.Length != 0)
			{
				this.post.SetInt("_HighlightTargetsLength", Mathf.Clamp(this.highlightTargets.Length, 0, 100));
			}
			this.post.SetColorArray("_HighlightTargets", this.hTargets);
			this.post.SetFloatArray("_HighlightTargetThresholds", this.hThresholds);
			this.post.SetColorArray("_HighlightColors", this.hColors);
		}
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x00177A7C File Offset: 0x00175C7C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.camera == null)
		{
			this.camera = base.GetComponent<Camera>();
			return;
		}
		if (this.post == null)
		{
			this.post = new Material(Shader.Find("Abcight/BlacklightPost"));
		}
		Graphics.Blit(source, destination, this.post);
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x00177AD4 File Offset: 0x00175CD4
	[ContextMenu("Refresh")]
	public void Refresh()
	{
		UnityEngine.Object.DestroyImmediate(this.post);
		this.post = null;
	}

	// Token: 0x04003BBA RID: 15290
	[SerializeField]
	private Color fogColorDark = new Color32(14, 11, 31, byte.MaxValue);

	// Token: 0x04003BBB RID: 15291
	[SerializeField]
	private Color fogColorLight = new Color32(87, 89, 111, byte.MaxValue);

	// Token: 0x04003BBC RID: 15292
	[SerializeField]
	[Range(0f, 1f)]
	private float fogOpacity = 1f;

	// Token: 0x04003BBD RID: 15293
	[SerializeField]
	private float fogDepth = 8f;

	// Token: 0x04003BBE RID: 15294
	[Space(5f)]
	[Header("Glow")]
	[SerializeField]
	[ColorUsage(true, true, 0f, 3f, 0f, 3f)]
	private Color glowColor = new Color(0f, 0.48235294f, 0.7490196f) * 9f;

	// Token: 0x04003BBF RID: 15295
	[SerializeField]
	[ColorUsage(true, true, 0f, 3f, 0f, 3f)]
	private Color glowColorSecondary = new Color(0.7490196f, 0f, 0.6784314f) * 9f;

	// Token: 0x04003BC0 RID: 15296
	[SerializeField]
	private float glowBias = 13f;

	// Token: 0x04003BC1 RID: 15297
	[SerializeField]
	[Range(0f, 1f)]
	private float glowFlip;

	// Token: 0x04003BC2 RID: 15298
	[SerializeField]
	private bool glow = true;

	// Token: 0x04003BC3 RID: 15299
	[Space(5f)]
	[Header("Targetted highlighting")]
	[SerializeField]
	private HighlightTarget[] highlightTargets;

	// Token: 0x04003BC4 RID: 15300
	[SerializeField]
	[Range(0f, 1f)]
	private float smoothDropoff;

	// Token: 0x04003BC5 RID: 15301
	[Space(5f)]
	[Header("Edge")]
	[SerializeField]
	private Color edgeColor = new Color32(7, byte.MaxValue, 83, byte.MaxValue);

	// Token: 0x04003BC6 RID: 15302
	[SerializeField]
	[Range(0.01f, 1f)]
	private float threshold = 0.45f;

	// Token: 0x04003BC7 RID: 15303
	[SerializeField]
	[Range(0f, 1f)]
	private float edgeOpacity = 1f;

	// Token: 0x04003BC8 RID: 15304
	[Space(5f)]
	[Header("Overlay")]
	[SerializeField]
	private Color overlayTop = new Color32(233, 0, byte.MaxValue, byte.MaxValue);

	// Token: 0x04003BC9 RID: 15305
	[SerializeField]
	private Color overlayBottom = new Color32(0, 38, byte.MaxValue, byte.MaxValue);

	// Token: 0x04003BCA RID: 15306
	[SerializeField]
	[Range(0f, 1f)]
	private float overlayOpacity = 0.06f;

	// Token: 0x04003BCB RID: 15307
	private Color[] hTargets = new Color[100];

	// Token: 0x04003BCC RID: 15308
	private float[] hThresholds = new float[100];

	// Token: 0x04003BCD RID: 15309
	private Color[] hColors = new Color[100];

	// Token: 0x04003BCE RID: 15310
	private Camera camera;

	// Token: 0x04003BCF RID: 15311
	private Material post;
}

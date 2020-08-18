using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A9 RID: 169
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Sprite")]
public class UISprite : UIBasicSprite
{
	// Token: 0x170001BA RID: 442
	// (get) Token: 0x0600089F RID: 2207 RVA: 0x000455A8 File Offset: 0x000437A8
	// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00040545 File Offset: 0x0003E745
	public override Texture mainTexture
	{
		get
		{
			Material material = null;
			INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
			if (inguiatlas != null)
			{
				material = inguiatlas.spriteMaterial;
			}
			if (!(material != null))
			{
				return null;
			}
			return material.mainTexture;
		}
		set
		{
			base.mainTexture = value;
		}
	}

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x060008A1 RID: 2209 RVA: 0x000455E0 File Offset: 0x000437E0
	// (set) Token: 0x060008A2 RID: 2210 RVA: 0x000404F2 File Offset: 0x0003E6F2
	public override Material material
	{
		get
		{
			Material material = base.material;
			if (material != null)
			{
				return material;
			}
			INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
			if (inguiatlas != null)
			{
				return inguiatlas.spriteMaterial;
			}
			return null;
		}
		set
		{
			base.material = value;
		}
	}

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00045616 File Offset: 0x00043816
	// (set) Token: 0x060008A4 RID: 2212 RVA: 0x00045624 File Offset: 0x00043824
	public INGUIAtlas atlas
	{
		get
		{
			return this.mAtlas as INGUIAtlas;
		}
		set
		{
			if (this.mAtlas as INGUIAtlas != value)
			{
				base.RemoveFromPanel();
				this.mAtlas = (value as UnityEngine.Object);
				this.mSpriteSet = false;
				this.mSprite = null;
				if (string.IsNullOrEmpty(this.mSpriteName))
				{
					INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
					if (inguiatlas != null && inguiatlas.spriteList.Count > 0)
					{
						this.SetAtlasSprite(inguiatlas.spriteList[0]);
						this.mSpriteName = this.mSprite.name;
					}
				}
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					string spriteName = this.mSpriteName;
					this.mSpriteName = "";
					this.spriteName = spriteName;
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x060008A5 RID: 2213 RVA: 0x000456DB File Offset: 0x000438DB
	// (set) Token: 0x060008A6 RID: 2214 RVA: 0x000456E3 File Offset: 0x000438E3
	public bool fixedAspect
	{
		get
		{
			return this.mFixedAspect;
		}
		set
		{
			if (this.mFixedAspect != value)
			{
				this.mFixedAspect = value;
				this.mDrawRegion = new Vector4(0f, 0f, 1f, 1f);
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0004571C File Offset: 0x0004391C
	public UISpriteData GetSprite(string spriteName)
	{
		INGUIAtlas atlas = this.atlas;
		if (atlas == null)
		{
			return null;
		}
		return atlas.GetSprite(spriteName);
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0004573C File Offset: 0x0004393C
	public override void MarkAsChanged()
	{
		this.mSprite = null;
		this.mSpriteSet = false;
		base.MarkAsChanged();
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00045752 File Offset: 0x00043952
	// (set) Token: 0x060008AA RID: 2218 RVA: 0x0004575C File Offset: 0x0004395C
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (this.mSpriteName != value)
				{
					this.mSpriteName = value;
					this.mSprite = null;
					this.mChanged = true;
					this.mSpriteSet = false;
					this.MarkAsChanged();
				}
				return;
			}
			if (string.IsNullOrEmpty(this.mSpriteName))
			{
				return;
			}
			this.mSpriteName = "";
			this.mSprite = null;
			this.mChanged = true;
			this.mSpriteSet = false;
			this.MarkAsChanged();
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x060008AB RID: 2219 RVA: 0x000457D6 File Offset: 0x000439D6
	public bool isValid
	{
		get
		{
			return this.GetAtlasSprite() != null;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x060008AC RID: 2220 RVA: 0x000457E1 File Offset: 0x000439E1
	// (set) Token: 0x060008AD RID: 2221 RVA: 0x000457EC File Offset: 0x000439EC
	[Obsolete("Use 'centerType' instead")]
	public bool fillCenter
	{
		get
		{
			return this.centerType > UIBasicSprite.AdvancedType.Invisible;
		}
		set
		{
			if (value != this.centerType > UIBasicSprite.AdvancedType.Invisible)
			{
				this.centerType = (value ? UIBasicSprite.AdvancedType.Sliced : UIBasicSprite.AdvancedType.Invisible);
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x060008AE RID: 2222 RVA: 0x0004580D File Offset: 0x00043A0D
	// (set) Token: 0x060008AF RID: 2223 RVA: 0x00045815 File Offset: 0x00043A15
	public bool applyGradient
	{
		get
		{
			return this.mApplyGradient;
		}
		set
		{
			if (this.mApplyGradient != value)
			{
				this.mApplyGradient = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0004582D File Offset: 0x00043A2D
	// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00045835 File Offset: 0x00043A35
	public Color gradientTop
	{
		get
		{
			return this.mGradientTop;
		}
		set
		{
			if (this.mGradientTop != value)
			{
				this.mGradientTop = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0004585A File Offset: 0x00043A5A
	// (set) Token: 0x060008B3 RID: 2227 RVA: 0x00045862 File Offset: 0x00043A62
	public Color gradientBottom
	{
		get
		{
			return this.mGradientBottom;
		}
		set
		{
			if (this.mGradientBottom != value)
			{
				this.mGradientBottom = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00045888 File Offset: 0x00043A88
	public override Vector4 border
	{
		get
		{
			UISpriteData atlasSprite = this.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return base.border;
			}
			return new Vector4((float)atlasSprite.borderLeft, (float)atlasSprite.borderBottom, (float)atlasSprite.borderRight, (float)atlasSprite.borderTop);
		}
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x060008B5 RID: 2229 RVA: 0x000458C8 File Offset: 0x00043AC8
	protected override Vector4 padding
	{
		get
		{
			UISpriteData atlasSprite = this.GetAtlasSprite();
			Vector4 result = new Vector4(0f, 0f, 0f, 0f);
			if (atlasSprite != null)
			{
				result.x = (float)atlasSprite.paddingLeft;
				result.y = (float)atlasSprite.paddingBottom;
				result.z = (float)atlasSprite.paddingRight;
				result.w = (float)atlasSprite.paddingTop;
			}
			return result;
		}
	}

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00045934 File Offset: 0x00043B34
	public override float pixelSize
	{
		get
		{
			if (this.mAtlas == null)
			{
				return 1f;
			}
			INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
			if (inguiatlas != null)
			{
				return inguiatlas.pixelSize;
			}
			return 1f;
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00045970 File Offset: 0x00043B70
	public override int minWidth
	{
		get
		{
			if (this.type == UIBasicSprite.Type.Sliced || this.type == UIBasicSprite.Type.Advanced)
			{
				float pixelSize = this.pixelSize;
				Vector4 vector = this.border * this.pixelSize;
				int num = Mathf.RoundToInt(vector.x + vector.z);
				UISpriteData atlasSprite = this.GetAtlasSprite();
				if (atlasSprite != null)
				{
					num += Mathf.RoundToInt(pixelSize * (float)(atlasSprite.paddingLeft + atlasSprite.paddingRight));
				}
				return Mathf.Max(base.minWidth, ((num & 1) == 1) ? (num + 1) : num);
			}
			return base.minWidth;
		}
	}

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x060008B8 RID: 2232 RVA: 0x000459FC File Offset: 0x00043BFC
	public override int minHeight
	{
		get
		{
			if (this.type == UIBasicSprite.Type.Sliced || this.type == UIBasicSprite.Type.Advanced)
			{
				float pixelSize = this.pixelSize;
				Vector4 vector = this.border * this.pixelSize;
				int num = Mathf.RoundToInt(vector.y + vector.w);
				UISpriteData atlasSprite = this.GetAtlasSprite();
				if (atlasSprite != null)
				{
					num += Mathf.RoundToInt(pixelSize * (float)(atlasSprite.paddingTop + atlasSprite.paddingBottom));
				}
				return Mathf.Max(base.minHeight, ((num & 1) == 1) ? (num + 1) : num);
			}
			return base.minHeight;
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00045A88 File Offset: 0x00043C88
	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			if (this.GetAtlasSprite() != null && this.mType != UIBasicSprite.Type.Tiled)
			{
				int num5 = this.mSprite.paddingLeft;
				int num6 = this.mSprite.paddingBottom;
				int num7 = this.mSprite.paddingRight;
				int num8 = this.mSprite.paddingTop;
				if (this.mType != UIBasicSprite.Type.Simple)
				{
					float pixelSize = this.pixelSize;
					if (pixelSize != 1f)
					{
						num5 = Mathf.RoundToInt(pixelSize * (float)num5);
						num6 = Mathf.RoundToInt(pixelSize * (float)num6);
						num7 = Mathf.RoundToInt(pixelSize * (float)num7);
						num8 = Mathf.RoundToInt(pixelSize * (float)num8);
					}
				}
				int num9 = this.mSprite.width + num5 + num7;
				int num10 = this.mSprite.height + num6 + num8;
				float num11 = 1f;
				float num12 = 1f;
				if (num9 > 0 && num10 > 0 && (this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled))
				{
					if ((num9 & 1) != 0)
					{
						num7++;
					}
					if ((num10 & 1) != 0)
					{
						num8++;
					}
					num11 = 1f / (float)num9 * (float)this.mWidth;
					num12 = 1f / (float)num10 * (float)this.mHeight;
				}
				if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num += (float)num7 * num11;
					num3 -= (float)num5 * num11;
				}
				else
				{
					num += (float)num5 * num11;
					num3 -= (float)num7 * num11;
				}
				if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num2 += (float)num8 * num12;
					num4 -= (float)num6 * num12;
				}
				else
				{
					num2 += (float)num6 * num12;
					num4 -= (float)num8 * num12;
				}
			}
			if (this.mDrawRegion.x != 0f || this.mDrawRegion.y != 0f || this.mDrawRegion.z != 1f || this.mDrawRegion.w != 0f)
			{
				float num13;
				float num14;
				if (this.mFixedAspect)
				{
					num13 = 0f;
					num14 = 0f;
				}
				else
				{
					Vector4 vector = (this.mAtlas != null) ? (this.border * this.pixelSize) : Vector4.zero;
					num13 = vector.x + vector.z;
					num14 = vector.y + vector.w;
				}
				float x = Mathf.Lerp(num, num3 - num13, this.mDrawRegion.x);
				float y = Mathf.Lerp(num2, num4 - num14, this.mDrawRegion.y);
				float z = Mathf.Lerp(num + num13, num3, this.mDrawRegion.z);
				float w = Mathf.Lerp(num2 + num14, num4, this.mDrawRegion.w);
				return new Vector4(x, y, z, w);
			}
			return new Vector4(num, num2, num3, num4);
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x060008BA RID: 2234 RVA: 0x00045D80 File Offset: 0x00043F80
	public override bool premultipliedAlpha
	{
		get
		{
			INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
			return inguiatlas != null && inguiatlas.premultipliedAlpha;
		}
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00045DA4 File Offset: 0x00043FA4
	public UISpriteData GetAtlasSprite()
	{
		if (!this.mSpriteSet)
		{
			this.mSprite = null;
		}
		if (this.mSprite == null)
		{
			INGUIAtlas inguiatlas = this.mAtlas as INGUIAtlas;
			if (inguiatlas != null)
			{
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					UISpriteData sprite = inguiatlas.GetSprite(this.mSpriteName);
					if (sprite == null)
					{
						return null;
					}
					this.SetAtlasSprite(sprite);
				}
				if (this.mSprite == null && inguiatlas.spriteList.Count > 0)
				{
					UISpriteData uispriteData = inguiatlas.spriteList[0];
					if (uispriteData == null)
					{
						return null;
					}
					this.SetAtlasSprite(uispriteData);
					if (this.mSprite == null)
					{
						Debug.LogError((inguiatlas as UnityEngine.Object).name + " seems to have a null sprite!");
						return null;
					}
					this.mSpriteName = this.mSprite.name;
				}
			}
		}
		return this.mSprite;
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00045E70 File Offset: 0x00044070
	protected void SetAtlasSprite(UISpriteData sp)
	{
		this.mChanged = true;
		this.mSpriteSet = true;
		if (sp != null)
		{
			this.mSprite = sp;
			this.mSpriteName = this.mSprite.name;
			return;
		}
		this.mSpriteName = ((this.mSprite != null) ? this.mSprite.name : "");
		this.mSprite = sp;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00045ED0 File Offset: 0x000440D0
	public override void MakePixelPerfect()
	{
		if (!this.isValid)
		{
			return;
		}
		base.MakePixelPerfect();
		if (this.mType == UIBasicSprite.Type.Tiled)
		{
			return;
		}
		UISpriteData atlasSprite = this.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return;
		}
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		if ((this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled || !atlasSprite.hasBorder) && mainTexture != null)
		{
			int num = Mathf.RoundToInt(this.pixelSize * (float)(atlasSprite.width + atlasSprite.paddingLeft + atlasSprite.paddingRight));
			int num2 = Mathf.RoundToInt(this.pixelSize * (float)(atlasSprite.height + atlasSprite.paddingTop + atlasSprite.paddingBottom));
			if ((num & 1) == 1)
			{
				num++;
			}
			if ((num2 & 1) == 1)
			{
				num2++;
			}
			base.width = num;
			base.height = num2;
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00045F9A File Offset: 0x0004419A
	protected override void OnInit()
	{
		if (!this.mFillCenter)
		{
			this.mFillCenter = true;
			this.centerType = UIBasicSprite.AdvancedType.Invisible;
		}
		base.OnInit();
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00045FB8 File Offset: 0x000441B8
	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (this.mChanged || !this.mSpriteSet)
		{
			this.mSpriteSet = true;
			this.mSprite = null;
			this.mChanged = true;
		}
		if (this.mFixedAspect)
		{
			if ((!this.mSpriteSet || this.mSprite == null) && this.GetAtlasSprite() == null)
			{
				return;
			}
			if (this.mSprite != null)
			{
				int paddingLeft = this.mSprite.paddingLeft;
				int paddingBottom = this.mSprite.paddingBottom;
				int paddingRight = this.mSprite.paddingRight;
				int paddingTop = this.mSprite.paddingTop;
				float num = (float)Mathf.RoundToInt((float)this.mSprite.width);
				int num2 = Mathf.RoundToInt((float)this.mSprite.height);
				float num3 = num + (float)(paddingLeft + paddingRight);
				num2 += paddingTop + paddingBottom;
				float num4 = (float)this.mWidth;
				float num5 = (float)this.mHeight;
				float num6 = num4 / num5;
				float num7 = num3 / (float)num2;
				if (num7 < num6)
				{
					float num8 = (num4 - num5 * num7) / num4 * 0.5f;
					base.drawRegion = new Vector4(num8, 0f, 1f - num8, 1f);
					return;
				}
				float num9 = (num5 - num4 / num7) / num5 * 0.5f;
				base.drawRegion = new Vector4(0f, num9, 1f, 1f - num9);
			}
		}
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0004610C File Offset: 0x0004430C
	public override void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		if ((!this.mSpriteSet || this.mSprite == null) && this.GetAtlasSprite() == null)
		{
			return;
		}
		Rect rect = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
		Rect rect2 = new Rect((float)(this.mSprite.x + this.mSprite.borderLeft), (float)(this.mSprite.y + this.mSprite.borderTop), (float)(this.mSprite.width - this.mSprite.borderLeft - this.mSprite.borderRight), (float)(this.mSprite.height - this.mSprite.borderBottom - this.mSprite.borderTop));
		rect = NGUIMath.ConvertToTexCoords(rect, mainTexture.width, mainTexture.height);
		rect2 = NGUIMath.ConvertToTexCoords(rect2, mainTexture.width, mainTexture.height);
		int count = verts.Count;
		base.Fill(verts, uvs, cols, rect, rect2);
		if (this.onPostFill != null)
		{
			this.onPostFill(this, count, verts, uvs, cols);
		}
	}

	// Token: 0x0400076A RID: 1898
	[HideInInspector]
	[SerializeField]
	private UnityEngine.Object mAtlas;

	// Token: 0x0400076B RID: 1899
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x0400076C RID: 1900
	[HideInInspector]
	[SerializeField]
	private bool mFixedAspect;

	// Token: 0x0400076D RID: 1901
	[HideInInspector]
	[SerializeField]
	private bool mFillCenter = true;

	// Token: 0x0400076E RID: 1902
	[NonSerialized]
	protected UISpriteData mSprite;

	// Token: 0x0400076F RID: 1903
	[NonSerialized]
	private bool mSpriteSet;
}

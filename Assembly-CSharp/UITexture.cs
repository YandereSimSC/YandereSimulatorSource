using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AF RID: 175
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Texture")]
public class UITexture : UIBasicSprite
{
	// Token: 0x170001DF RID: 479
	// (get) Token: 0x06000913 RID: 2323 RVA: 0x000481CC File Offset: 0x000463CC
	// (set) Token: 0x06000914 RID: 2324 RVA: 0x00048200 File Offset: 0x00046400
	public override Texture mainTexture
	{
		get
		{
			if (this.mTexture != null)
			{
				return this.mTexture;
			}
			if (this.mMat != null)
			{
				return this.mMat.mainTexture;
			}
			return null;
		}
		set
		{
			if (this.mTexture != value)
			{
				if (this.drawCall != null && this.drawCall.widgetCount == 1 && this.mMat == null)
				{
					this.mTexture = value;
					this.drawCall.mainTexture = value;
					return;
				}
				base.RemoveFromPanel();
				this.mTexture = value;
				this.mPMA = -1;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x06000915 RID: 2325 RVA: 0x00032AFB File Offset: 0x00030CFB
	// (set) Token: 0x06000916 RID: 2326 RVA: 0x00048273 File Offset: 0x00046473
	public override Material material
	{
		get
		{
			return this.mMat;
		}
		set
		{
			if (this.mMat != value)
			{
				base.RemoveFromPanel();
				this.mShader = null;
				this.mMat = value;
				this.mPMA = -1;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x06000917 RID: 2327 RVA: 0x000482A4 File Offset: 0x000464A4
	// (set) Token: 0x06000918 RID: 2328 RVA: 0x000482E4 File Offset: 0x000464E4
	public override Shader shader
	{
		get
		{
			if (this.mMat != null)
			{
				return this.mMat.shader;
			}
			if (this.mShader == null)
			{
				this.mShader = Shader.Find("Unlit/Transparent Colored");
			}
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				if (this.drawCall != null && this.drawCall.widgetCount == 1 && this.mMat == null)
				{
					this.mShader = value;
					this.drawCall.shader = value;
					return;
				}
				base.RemoveFromPanel();
				this.mShader = value;
				this.mPMA = -1;
				this.mMat = null;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06000919 RID: 2329 RVA: 0x00048360 File Offset: 0x00046560
	public override bool premultipliedAlpha
	{
		get
		{
			if (this.mPMA == -1)
			{
				Material material = this.material;
				this.mPMA = ((material != null && material.shader != null && material.shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x0600091A RID: 2330 RVA: 0x000483BE File Offset: 0x000465BE
	// (set) Token: 0x0600091B RID: 2331 RVA: 0x000483C6 File Offset: 0x000465C6
	public override Vector4 border
	{
		get
		{
			return this.mBorder;
		}
		set
		{
			if (this.mBorder != value)
			{
				this.mBorder = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x0600091C RID: 2332 RVA: 0x000483E3 File Offset: 0x000465E3
	// (set) Token: 0x0600091D RID: 2333 RVA: 0x000483EB File Offset: 0x000465EB
	public Rect uvRect
	{
		get
		{
			return this.mRect;
		}
		set
		{
			if (this.mRect != value)
			{
				this.mRect = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x0600091E RID: 2334 RVA: 0x00048408 File Offset: 0x00046608
	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			if (this.mTexture != null && this.mType != UIBasicSprite.Type.Tiled)
			{
				int width = this.mTexture.width;
				int height = this.mTexture.height;
				int num5 = 0;
				int num6 = 0;
				float num7 = 1f;
				float num8 = 1f;
				if (width > 0 && height > 0 && (this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled))
				{
					if ((width & 1) != 0)
					{
						num5++;
					}
					if ((height & 1) != 0)
					{
						num6++;
					}
					num7 = 1f / (float)width * (float)this.mWidth;
					num8 = 1f / (float)height * (float)this.mHeight;
				}
				if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num += (float)num5 * num7;
				}
				else
				{
					num3 -= (float)num5 * num7;
				}
				if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num2 += (float)num6 * num8;
				}
				else
				{
					num4 -= (float)num6 * num8;
				}
			}
			float num9;
			float num10;
			if (this.mFixedAspect)
			{
				num9 = 0f;
				num10 = 0f;
			}
			else
			{
				Vector4 border = this.border;
				num9 = border.x + border.z;
				num10 = border.y + border.w;
			}
			float x = Mathf.Lerp(num, num3 - num9, this.mDrawRegion.x);
			float y = Mathf.Lerp(num2, num4 - num10, this.mDrawRegion.y);
			float z = Mathf.Lerp(num + num9, num3, this.mDrawRegion.z);
			float w = Mathf.Lerp(num2 + num10, num4, this.mDrawRegion.w);
			return new Vector4(x, y, z, w);
		}
	}

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x0600091F RID: 2335 RVA: 0x000485E5 File Offset: 0x000467E5
	// (set) Token: 0x06000920 RID: 2336 RVA: 0x000485ED File Offset: 0x000467ED
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

	// Token: 0x06000921 RID: 2337 RVA: 0x00048624 File Offset: 0x00046824
	public override void MakePixelPerfect()
	{
		base.MakePixelPerfect();
		if (this.mType == UIBasicSprite.Type.Tiled)
		{
			return;
		}
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		if ((this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled || !base.hasBorder) && mainTexture != null)
		{
			int num = mainTexture.width;
			int num2 = mainTexture.height;
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

	// Token: 0x06000922 RID: 2338 RVA: 0x000486A4 File Offset: 0x000468A4
	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (this.mFixedAspect)
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				int num = mainTexture.width;
				int num2 = mainTexture.height;
				if ((num & 1) == 1)
				{
					num++;
				}
				if ((num2 & 1) == 1)
				{
					num2++;
				}
				float num3 = (float)this.mWidth;
				float num4 = (float)this.mHeight;
				float num5 = num3 / num4;
				float num6 = (float)num / (float)num2;
				if (num6 < num5)
				{
					float num7 = (num3 - num4 * num6) / num3 * 0.5f;
					base.drawRegion = new Vector4(num7, 0f, 1f - num7, 1f);
					return;
				}
				float num8 = (num4 - num3 / num6) / num4 * 0.5f;
				base.drawRegion = new Vector4(0f, num8, 1f, 1f - num8);
			}
		}
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00048780 File Offset: 0x00046980
	public override void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		Rect rect = new Rect(this.mRect.x * (float)mainTexture.width, this.mRect.y * (float)mainTexture.height, (float)mainTexture.width * this.mRect.width, (float)mainTexture.height * this.mRect.height);
		Rect inner = rect;
		Vector4 border = this.border;
		inner.xMin += border.x;
		inner.yMin += border.y;
		inner.xMax -= border.z;
		inner.yMax -= border.w;
		float num = 1f / (float)mainTexture.width;
		float num2 = 1f / (float)mainTexture.height;
		rect.xMin *= num;
		rect.xMax *= num;
		rect.yMin *= num2;
		rect.yMax *= num2;
		inner.xMin *= num;
		inner.xMax *= num;
		inner.yMin *= num2;
		inner.yMax *= num2;
		int count = verts.Count;
		base.Fill(verts, uvs, cols, rect, inner);
		if (this.onPostFill != null)
		{
			this.onPostFill(this, count, verts, uvs, cols);
		}
	}

	// Token: 0x040007AC RID: 1964
	[HideInInspector]
	[SerializeField]
	private Rect mRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x040007AD RID: 1965
	[HideInInspector]
	[SerializeField]
	private Texture mTexture;

	// Token: 0x040007AE RID: 1966
	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	// Token: 0x040007AF RID: 1967
	[HideInInspector]
	[SerializeField]
	private Vector4 mBorder = Vector4.zero;

	// Token: 0x040007B0 RID: 1968
	[HideInInspector]
	[SerializeField]
	private bool mFixedAspect;

	// Token: 0x040007B1 RID: 1969
	[NonSerialized]
	private int mPMA = -1;
}

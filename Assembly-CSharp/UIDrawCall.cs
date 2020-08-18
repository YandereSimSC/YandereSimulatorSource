using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

// Token: 0x0200007F RID: 127
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Draw Call")]
public class UIDrawCall : MonoBehaviour
{
	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060004DC RID: 1244 RVA: 0x0002F811 File Offset: 0x0002DA11
	[Obsolete("Use UIDrawCall.activeList")]
	public static BetterList<UIDrawCall> list
	{
		get
		{
			return UIDrawCall.mActiveList;
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x060004DD RID: 1245 RVA: 0x0002F811 File Offset: 0x0002DA11
	public static BetterList<UIDrawCall> activeList
	{
		get
		{
			return UIDrawCall.mActiveList;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060004DE RID: 1246 RVA: 0x0002F818 File Offset: 0x0002DA18
	public static BetterList<UIDrawCall> inactiveList
	{
		get
		{
			return UIDrawCall.mInactiveList;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060004DF RID: 1247 RVA: 0x0002F81F File Offset: 0x0002DA1F
	// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0002F827 File Offset: 0x0002DA27
	public int renderQueue
	{
		get
		{
			return this.mRenderQueue;
		}
		set
		{
			if (this.mRenderQueue != value)
			{
				this.mRenderQueue = value;
				if (this.mDynamicMat != null)
				{
					this.mDynamicMat.renderQueue = value;
				}
			}
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0002F853 File Offset: 0x0002DA53
	// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0002F85B File Offset: 0x0002DA5B
	public int sortingOrder
	{
		get
		{
			return this.mSortingOrder;
		}
		set
		{
			if (this.mSortingOrder != value)
			{
				this.mSortingOrder = value;
				if (this.mRenderer != null)
				{
					this.mRenderer.sortingOrder = value;
				}
			}
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0002F887 File Offset: 0x0002DA87
	// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0002F8C4 File Offset: 0x0002DAC4
	public string sortingLayerName
	{
		get
		{
			if (!string.IsNullOrEmpty(this.mSortingLayerName))
			{
				return this.mSortingLayerName;
			}
			if (this.mRenderer == null)
			{
				return null;
			}
			this.mSortingLayerName = this.mRenderer.sortingLayerName;
			return this.mSortingLayerName;
		}
		set
		{
			if (this.mRenderer != null && this.mSortingLayerName != value)
			{
				this.mSortingLayerName = value;
				this.mRenderer.sortingLayerName = value;
			}
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0002F8F5 File Offset: 0x0002DAF5
	public int finalRenderQueue
	{
		get
		{
			if (!(this.mDynamicMat != null))
			{
				return this.mRenderQueue;
			}
			return this.mDynamicMat.renderQueue;
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0002F917 File Offset: 0x0002DB17
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0002F939 File Offset: 0x0002DB39
	// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0002F941 File Offset: 0x0002DB41
	public Material baseMaterial
	{
		get
		{
			return this.mMaterial;
		}
		set
		{
			if (this.mMaterial != value)
			{
				this.mMaterial = value;
				this.mRebuildMat = true;
			}
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0002F95F File Offset: 0x0002DB5F
	public Material dynamicMaterial
	{
		get
		{
			return this.mDynamicMat;
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060004EA RID: 1258 RVA: 0x0002F967 File Offset: 0x0002DB67
	// (set) Token: 0x060004EB RID: 1259 RVA: 0x0002F96F File Offset: 0x0002DB6F
	public Texture mainTexture
	{
		get
		{
			return this.mTexture;
		}
		set
		{
			this.mTexture = value;
			if (this.mBlock == null)
			{
				this.mBlock = new MaterialPropertyBlock();
			}
			this.mBlock.SetTexture("_MainTex", (value != null) ? value : Texture2D.whiteTexture);
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x060004EC RID: 1260 RVA: 0x0002F9AC File Offset: 0x0002DBAC
	// (set) Token: 0x060004ED RID: 1261 RVA: 0x0002F9B4 File Offset: 0x0002DBB4
	public Shader shader
	{
		get
		{
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				this.mShader = value;
				this.mRebuildMat = true;
			}
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x060004EE RID: 1262 RVA: 0x0002F9D2 File Offset: 0x0002DBD2
	// (set) Token: 0x060004EF RID: 1263 RVA: 0x0002F9DC File Offset: 0x0002DBDC
	public UIDrawCall.ShadowMode shadowMode
	{
		get
		{
			return this.mShadowMode;
		}
		set
		{
			if (this.mShadowMode != value)
			{
				this.mShadowMode = value;
				if (this.mRenderer != null)
				{
					if (this.mShadowMode == UIDrawCall.ShadowMode.None)
					{
						this.mRenderer.shadowCastingMode = ShadowCastingMode.Off;
						this.mRenderer.receiveShadows = false;
						return;
					}
					if (this.mShadowMode == UIDrawCall.ShadowMode.Receive)
					{
						this.mRenderer.shadowCastingMode = ShadowCastingMode.Off;
						this.mRenderer.receiveShadows = true;
						return;
					}
					this.mRenderer.shadowCastingMode = ShadowCastingMode.On;
					this.mRenderer.receiveShadows = true;
				}
			}
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0002FA62 File Offset: 0x0002DC62
	public int triangles
	{
		get
		{
			if (!(this.mMesh != null))
			{
				return 0;
			}
			return this.mTriangles;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0002FA7A File Offset: 0x0002DC7A
	public bool isClipped
	{
		get
		{
			return this.mClipCount != 0;
		}
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0002FA88 File Offset: 0x0002DC88
	private void CreateMaterial()
	{
		this.mTextureClip = false;
		this.mLegacyShader = false;
		this.mClipCount = this.panel.clipCount;
		string text = (this.mShader != null) ? this.mShader.name : ((this.mMaterial != null) ? this.mMaterial.shader.name : "Unlit/Transparent Colored");
		text = text.Replace("GUI/Text Shader", "Unlit/Text");
		if (text.Length > 2 && text[text.Length - 2] == ' ')
		{
			int num = (int)text[text.Length - 1];
			if (num > 48 && num <= 57)
			{
				text = text.Substring(0, text.Length - 2);
			}
		}
		if (text.StartsWith("Hidden/"))
		{
			text = text.Substring(7);
		}
		text = text.Replace(" (SoftClip)", "");
		text = text.Replace(" (TextureClip)", "");
		if (this.panel != null && this.panel.clipping == UIDrawCall.Clipping.TextureMask)
		{
			this.mTextureClip = true;
			this.shader = Shader.Find("Hidden/" + text + " (TextureClip)");
		}
		else if (this.mClipCount != 0)
		{
			this.shader = Shader.Find(string.Concat(new object[]
			{
				"Hidden/",
				text,
				" ",
				this.mClipCount
			}));
			if (this.shader == null)
			{
				this.shader = Shader.Find(text + " " + this.mClipCount);
			}
			if (this.shader == null && this.mClipCount == 1)
			{
				this.mLegacyShader = true;
				this.shader = Shader.Find(text + " (SoftClip)");
			}
		}
		else
		{
			this.shader = Shader.Find(text);
		}
		if (this.shader == null)
		{
			this.shader = Shader.Find("Unlit/Transparent Colored");
		}
		if (this.mMaterial != null)
		{
			this.mDynamicMat = new Material(this.mMaterial);
			this.mDynamicMat.name = "[NGUI] " + this.mMaterial.name;
			this.mDynamicMat.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			this.mDynamicMat.CopyPropertiesFromMaterial(this.mMaterial);
			string[] shaderKeywords = this.mMaterial.shaderKeywords;
			for (int i = 0; i < shaderKeywords.Length; i++)
			{
				this.mDynamicMat.EnableKeyword(shaderKeywords[i]);
			}
			if (this.shader != null)
			{
				this.mDynamicMat.shader = this.shader;
				return;
			}
			if (this.mClipCount != 0)
			{
				Debug.LogError(string.Concat(new object[]
				{
					text,
					" shader doesn't have a clipped shader version for ",
					this.mClipCount,
					" clip regions"
				}));
				return;
			}
		}
		else
		{
			this.mDynamicMat = new Material(this.shader);
			this.mDynamicMat.name = "[NGUI] " + this.shader.name;
			this.mDynamicMat.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0002FDB8 File Offset: 0x0002DFB8
	private Material RebuildMaterial()
	{
		NGUITools.DestroyImmediate(this.mDynamicMat);
		this.CreateMaterial();
		this.mDynamicMat.renderQueue = this.mRenderQueue;
		if (this.mRenderer != null)
		{
			this.mRenderer.sharedMaterials = new Material[]
			{
				this.mDynamicMat
			};
			this.mRenderer.sortingLayerName = this.mSortingLayerName;
			this.mRenderer.sortingOrder = this.mSortingOrder;
		}
		return this.mDynamicMat;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0002FE38 File Offset: 0x0002E038
	private void UpdateMaterials()
	{
		if (this.panel == null)
		{
			return;
		}
		if (this.mRebuildMat || this.mDynamicMat == null || this.mClipCount != this.panel.clipCount || this.mTextureClip != (this.panel.clipping == UIDrawCall.Clipping.TextureMask))
		{
			this.RebuildMaterial();
			this.mRebuildMat = false;
		}
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0002FEA4 File Offset: 0x0002E0A4
	public void UpdateGeometry(int widgetCount, bool needsBounds)
	{
		this.widgetCount = widgetCount;
		int count = this.verts.Count;
		if (count > 0 && count == this.uvs.Count && count == this.cols.Count && count % 4 == 0)
		{
			if (UIDrawCall.mColorSpace == ColorSpace.Uninitialized)
			{
				UIDrawCall.mColorSpace = QualitySettings.activeColorSpace;
			}
			if (UIDrawCall.mColorSpace == ColorSpace.Linear)
			{
				for (int i = 0; i < count; i++)
				{
					Color color = this.cols[i];
					color.r = Mathf.GammaToLinearSpace(color.r);
					color.g = Mathf.GammaToLinearSpace(color.g);
					color.b = Mathf.GammaToLinearSpace(color.b);
					color.a = Mathf.GammaToLinearSpace(color.a);
					this.cols[i] = color;
				}
			}
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.GetComponent<MeshFilter>();
			}
			if (this.mFilter == null)
			{
				this.mFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (count < 65000)
			{
				int num = (count >> 1) * 3;
				bool flag = this.mIndices == null || this.mIndices.Length != num;
				if (this.mMesh == null)
				{
					this.mMesh = new Mesh();
					this.mMesh.hideFlags = HideFlags.DontSave;
					this.mMesh.name = ((this.mMaterial != null) ? ("[NGUI] " + this.mMaterial.name) : "[NGUI] Mesh");
					if (UIDrawCall.dx9BugWorkaround == 0)
					{
						this.mMesh.MarkDynamic();
					}
					flag = true;
				}
				bool flag2 = this.uvs.Count != count || this.cols.Count != count || this.uv2.Count != count || this.norms.Count != count || this.tans.Count != count;
				if (!flag2 && this.panel != null && this.panel.renderQueue != UIPanel.RenderQueue.Automatic)
				{
					flag2 = (this.mMesh == null || this.mMesh.vertexCount != this.verts.Count);
				}
				if (!flag2 && count << 1 < this.verts.Count)
				{
					flag2 = true;
				}
				this.mTriangles = count >> 1;
				if (this.mMesh.vertexCount != count)
				{
					this.mMesh.Clear();
					flag = true;
				}
				this.mMesh.SetVertices(this.verts);
				this.mMesh.SetUVs(0, this.uvs);
				this.mMesh.SetColors(this.cols);
				this.mMesh.SetUVs(1, (this.uv2.Count == count) ? this.uv2 : null);
				this.mMesh.SetNormals((this.norms.Count == count) ? this.norms : null);
				this.mMesh.SetTangents((this.tans.Count == count) ? this.tans : null);
				if (flag)
				{
					this.mIndices = this.GenerateCachedIndexBuffer(count, num);
					this.mMesh.SetTriangles(this.mIndices, 0, needsBounds);
				}
				if (flag2 || !this.alwaysOnScreen)
				{
					this.mMesh.RecalculateBounds();
				}
				this.mFilter.mesh = this.mMesh;
			}
			else
			{
				this.mTriangles = 0;
				if (this.mMesh != null)
				{
					this.mMesh.Clear();
				}
				Debug.LogError("Too many vertices on one panel: " + count);
			}
			if (this.mRenderer == null)
			{
				this.mRenderer = base.gameObject.GetComponent<MeshRenderer>();
			}
			if (this.mRenderer == null)
			{
				this.mRenderer = base.gameObject.AddComponent<MeshRenderer>();
				if (this.mShadowMode == UIDrawCall.ShadowMode.None)
				{
					this.mRenderer.shadowCastingMode = ShadowCastingMode.Off;
					this.mRenderer.receiveShadows = false;
				}
				else if (this.mShadowMode == UIDrawCall.ShadowMode.Receive)
				{
					this.mRenderer.shadowCastingMode = ShadowCastingMode.Off;
					this.mRenderer.receiveShadows = true;
				}
				else
				{
					this.mRenderer.shadowCastingMode = ShadowCastingMode.On;
					this.mRenderer.receiveShadows = true;
				}
			}
			if (this.mIsNew)
			{
				this.mIsNew = false;
				if (this.onCreateDrawCall != null)
				{
					this.onCreateDrawCall(this, this.mFilter, this.mRenderer);
				}
			}
			this.UpdateMaterials();
		}
		else
		{
			if (this.mFilter.mesh != null)
			{
				this.mFilter.mesh.Clear();
			}
			Debug.LogError("UIWidgets must fill the buffer with 4 vertices per quad. Found " + count);
		}
		this.verts.Clear();
		this.uvs.Clear();
		this.uv2.Clear();
		this.cols.Clear();
		this.norms.Clear();
		this.tans.Clear();
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0003039C File Offset: 0x0002E59C
	private int[] GenerateCachedIndexBuffer(int vertexCount, int indexCount)
	{
		int i = 0;
		int count = UIDrawCall.mCache.Count;
		while (i < count)
		{
			int[] array = UIDrawCall.mCache[i];
			if (array != null && array.Length == indexCount)
			{
				return array;
			}
			i++;
		}
		int[] array2 = new int[indexCount];
		int num = 0;
		for (int j = 0; j < vertexCount; j += 4)
		{
			array2[num++] = j;
			array2[num++] = j + 1;
			array2[num++] = j + 2;
			array2[num++] = j + 2;
			array2[num++] = j + 3;
			array2[num++] = j;
		}
		if (UIDrawCall.mCache.Count > 10)
		{
			UIDrawCall.mCache.RemoveAt(0);
		}
		UIDrawCall.mCache.Add(array2);
		return array2;
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00030458 File Offset: 0x0002E658
	private void OnWillRenderObject()
	{
		this.UpdateMaterials();
		if (this.mBlock != null)
		{
			this.mRenderer.SetPropertyBlock(this.mBlock);
		}
		if (this.onRender != null)
		{
			this.onRender((this.mDynamicMat != null) ? this.mDynamicMat : this.mMaterial);
		}
		if (this.mDynamicMat == null || this.mClipCount == 0)
		{
			return;
		}
		if (this.mTextureClip)
		{
			Vector4 drawCallClipRange = this.panel.drawCallClipRange;
			Vector2 clipSoftness = this.panel.clipSoftness;
			Vector2 vector = new Vector2(1000f, 1000f);
			if (clipSoftness.x > 0f)
			{
				vector.x = drawCallClipRange.z / clipSoftness.x;
			}
			if (clipSoftness.y > 0f)
			{
				vector.y = drawCallClipRange.w / clipSoftness.y;
			}
			this.mDynamicMat.SetVector(UIDrawCall.ClipRange[0], new Vector4(-drawCallClipRange.x / drawCallClipRange.z, -drawCallClipRange.y / drawCallClipRange.w, 1f / drawCallClipRange.z, 1f / drawCallClipRange.w));
			this.mDynamicMat.SetTexture("_ClipTex", this.clipTexture);
			return;
		}
		if (!this.mLegacyShader)
		{
			UIPanel parentPanel = this.panel;
			int num = 0;
			while (parentPanel != null)
			{
				if (parentPanel.hasClipping)
				{
					float angle = 0f;
					Vector4 drawCallClipRange2 = parentPanel.drawCallClipRange;
					if (parentPanel != this.panel)
					{
						Vector3 vector2 = parentPanel.cachedTransform.InverseTransformPoint(this.panel.cachedTransform.position);
						drawCallClipRange2.x -= vector2.x;
						drawCallClipRange2.y -= vector2.y;
						Vector3 eulerAngles = this.panel.cachedTransform.rotation.eulerAngles;
						Vector3 vector3 = parentPanel.cachedTransform.rotation.eulerAngles - eulerAngles;
						vector3.x = NGUIMath.WrapAngle(vector3.x);
						vector3.y = NGUIMath.WrapAngle(vector3.y);
						vector3.z = NGUIMath.WrapAngle(vector3.z);
						if (Mathf.Abs(vector3.x) > 0.001f || Mathf.Abs(vector3.y) > 0.001f)
						{
							Debug.LogWarning("Panel can only be clipped properly if X and Y rotation is left at 0", this.panel);
						}
						angle = vector3.z;
					}
					this.SetClipping(num++, drawCallClipRange2, parentPanel.clipSoftness, angle);
				}
				parentPanel = parentPanel.parentPanel;
			}
			return;
		}
		Vector2 clipSoftness2 = this.panel.clipSoftness;
		Vector4 drawCallClipRange3 = this.panel.drawCallClipRange;
		Vector2 mainTextureOffset = new Vector2(-drawCallClipRange3.x / drawCallClipRange3.z, -drawCallClipRange3.y / drawCallClipRange3.w);
		Vector2 mainTextureScale = new Vector2(1f / drawCallClipRange3.z, 1f / drawCallClipRange3.w);
		Vector2 v = new Vector2(1000f, 1000f);
		if (clipSoftness2.x > 0f)
		{
			v.x = drawCallClipRange3.z / clipSoftness2.x;
		}
		if (clipSoftness2.y > 0f)
		{
			v.y = drawCallClipRange3.w / clipSoftness2.y;
		}
		this.mDynamicMat.mainTextureOffset = mainTextureOffset;
		this.mDynamicMat.mainTextureScale = mainTextureScale;
		this.mDynamicMat.SetVector("_ClipSharpness", v);
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x000307F0 File Offset: 0x0002E9F0
	private void SetClipping(int index, Vector4 cr, Vector2 soft, float angle)
	{
		angle *= -0.017453292f;
		Vector2 vector = new Vector2(1000f, 1000f);
		if (soft.x > 0f)
		{
			vector.x = cr.z / soft.x;
		}
		if (soft.y > 0f)
		{
			vector.y = cr.w / soft.y;
		}
		if (index < UIDrawCall.ClipRange.Length)
		{
			this.mDynamicMat.SetVector(UIDrawCall.ClipRange[index], new Vector4(-cr.x / cr.z, -cr.y / cr.w, 1f / cr.z, 1f / cr.w));
			this.mDynamicMat.SetVector(UIDrawCall.ClipArgs[index], new Vector4(vector.x, vector.y, Mathf.Sin(angle), Mathf.Cos(angle)));
		}
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x000308E0 File Offset: 0x0002EAE0
	private void Awake()
	{
		if (UIDrawCall.dx9BugWorkaround == -1)
		{
			UIDrawCall.dx9BugWorkaround = ((Application.platform == RuntimePlatform.WindowsPlayer && SystemInfo.graphicsShaderLevel < 40 && SystemInfo.graphicsDeviceVersion.Contains("Direct3D")) ? 1 : 0);
		}
		if (UIDrawCall.ClipRange == null)
		{
			UIDrawCall.ClipRange = new int[]
			{
				Shader.PropertyToID("_ClipRange0"),
				Shader.PropertyToID("_ClipRange1"),
				Shader.PropertyToID("_ClipRange2"),
				Shader.PropertyToID("_ClipRange4")
			};
		}
		if (UIDrawCall.ClipArgs == null)
		{
			UIDrawCall.ClipArgs = new int[]
			{
				Shader.PropertyToID("_ClipArgs0"),
				Shader.PropertyToID("_ClipArgs1"),
				Shader.PropertyToID("_ClipArgs2"),
				Shader.PropertyToID("_ClipArgs3")
			};
		}
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x000309AC File Offset: 0x0002EBAC
	private void OnEnable()
	{
		this.mRebuildMat = true;
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x000309B8 File Offset: 0x0002EBB8
	private void OnDisable()
	{
		this.depthStart = int.MaxValue;
		this.depthEnd = int.MinValue;
		this.panel = null;
		this.manager = null;
		this.mMaterial = null;
		this.mTexture = null;
		this.clipTexture = null;
		if (this.mRenderer != null)
		{
			this.mRenderer.sharedMaterials = new Material[0];
		}
		NGUITools.DestroyImmediate(this.mDynamicMat);
		this.mDynamicMat = null;
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00030A2F File Offset: 0x0002EC2F
	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(this.mMesh);
		this.mMesh = null;
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00030A43 File Offset: 0x0002EC43
	public static UIDrawCall Create(UIPanel panel, Material mat, Texture tex, Shader shader)
	{
		return UIDrawCall.Create(null, panel, mat, tex, shader);
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00030A50 File Offset: 0x0002EC50
	private static UIDrawCall Create(string name, UIPanel pan, Material mat, Texture tex, Shader shader)
	{
		UIDrawCall uidrawCall = UIDrawCall.Create(name);
		uidrawCall.gameObject.layer = pan.cachedGameObject.layer;
		uidrawCall.baseMaterial = mat;
		uidrawCall.mainTexture = tex;
		uidrawCall.shader = shader;
		uidrawCall.renderQueue = pan.startingRenderQueue;
		uidrawCall.sortingOrder = pan.sortingOrder;
		uidrawCall.manager = pan;
		return uidrawCall;
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x00030AB0 File Offset: 0x0002ECB0
	private static UIDrawCall Create(string name)
	{
		while (UIDrawCall.mInactiveList.size > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mInactiveList.Pop();
			if (uidrawCall != null)
			{
				UIDrawCall.mActiveList.Add(uidrawCall);
				if (name != null)
				{
					uidrawCall.name = name;
				}
				NGUITools.SetActive(uidrawCall.gameObject, true);
				return uidrawCall;
			}
		}
		GameObject gameObject = new GameObject(name);
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		UIDrawCall uidrawCall2 = gameObject.AddComponent<UIDrawCall>();
		UIDrawCall.mActiveList.Add(uidrawCall2);
		return uidrawCall2;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00030B24 File Offset: 0x0002ED24
	public static void ClearAll()
	{
		bool isPlaying = Application.isPlaying;
		int i = UIDrawCall.mActiveList.size;
		while (i > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mActiveList.buffer[--i];
			if (uidrawCall)
			{
				if (isPlaying)
				{
					NGUITools.SetActive(uidrawCall.gameObject, false);
				}
				else
				{
					NGUITools.DestroyImmediate(uidrawCall.gameObject);
				}
			}
		}
		UIDrawCall.mActiveList.Clear();
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00030B87 File Offset: 0x0002ED87
	public static void ReleaseAll()
	{
		UIDrawCall.ClearAll();
		UIDrawCall.ReleaseInactive();
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00030B94 File Offset: 0x0002ED94
	public static void ReleaseInactive()
	{
		int i = UIDrawCall.mInactiveList.size;
		while (i > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.mInactiveList.buffer[--i];
			if (uidrawCall)
			{
				NGUITools.DestroyImmediate(uidrawCall.gameObject);
			}
		}
		UIDrawCall.mInactiveList.Clear();
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x00030BE0 File Offset: 0x0002EDE0
	public static int Count(UIPanel panel)
	{
		int num = 0;
		for (int i = 0; i < UIDrawCall.mActiveList.size; i++)
		{
			if (UIDrawCall.mActiveList.buffer[i].manager == panel)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x00030C24 File Offset: 0x0002EE24
	public static void Destroy(UIDrawCall dc)
	{
		if (dc)
		{
			if (dc.onCreateDrawCall != null)
			{
				NGUITools.Destroy(dc.gameObject);
				return;
			}
			dc.onRender = null;
			if (Application.isPlaying)
			{
				if (UIDrawCall.mActiveList.Remove(dc))
				{
					NGUITools.SetActive(dc.gameObject, false);
					UIDrawCall.mInactiveList.Add(dc);
					dc.mIsNew = true;
					return;
				}
			}
			else
			{
				UIDrawCall.mActiveList.Remove(dc);
				NGUITools.DestroyImmediate(dc.gameObject);
			}
		}
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00030CA0 File Offset: 0x0002EEA0
	public static void MoveToScene(Scene scene)
	{
		foreach (UIDrawCall uidrawCall in UIDrawCall.activeList)
		{
			SceneManager.MoveGameObjectToScene(uidrawCall.gameObject, scene);
		}
		foreach (UIDrawCall uidrawCall2 in UIDrawCall.inactiveList)
		{
			SceneManager.MoveGameObjectToScene(uidrawCall2.gameObject, scene);
		}
	}

	// Token: 0x04000528 RID: 1320
	private static BetterList<UIDrawCall> mActiveList = new BetterList<UIDrawCall>();

	// Token: 0x04000529 RID: 1321
	private static BetterList<UIDrawCall> mInactiveList = new BetterList<UIDrawCall>();

	// Token: 0x0400052A RID: 1322
	[HideInInspector]
	[NonSerialized]
	public int widgetCount;

	// Token: 0x0400052B RID: 1323
	[HideInInspector]
	[NonSerialized]
	public int depthStart = int.MaxValue;

	// Token: 0x0400052C RID: 1324
	[HideInInspector]
	[NonSerialized]
	public int depthEnd = int.MinValue;

	// Token: 0x0400052D RID: 1325
	[HideInInspector]
	[NonSerialized]
	public UIPanel manager;

	// Token: 0x0400052E RID: 1326
	[HideInInspector]
	[NonSerialized]
	public UIPanel panel;

	// Token: 0x0400052F RID: 1327
	[HideInInspector]
	[NonSerialized]
	public Texture2D clipTexture;

	// Token: 0x04000530 RID: 1328
	[HideInInspector]
	[NonSerialized]
	public bool alwaysOnScreen;

	// Token: 0x04000531 RID: 1329
	[HideInInspector]
	[NonSerialized]
	public List<Vector3> verts = new List<Vector3>();

	// Token: 0x04000532 RID: 1330
	[HideInInspector]
	[NonSerialized]
	public List<Vector3> norms = new List<Vector3>();

	// Token: 0x04000533 RID: 1331
	[HideInInspector]
	[NonSerialized]
	public List<Vector4> tans = new List<Vector4>();

	// Token: 0x04000534 RID: 1332
	[HideInInspector]
	[NonSerialized]
	public List<Vector2> uvs = new List<Vector2>();

	// Token: 0x04000535 RID: 1333
	[HideInInspector]
	[NonSerialized]
	public List<Vector4> uv2 = new List<Vector4>();

	// Token: 0x04000536 RID: 1334
	[HideInInspector]
	[NonSerialized]
	public List<Color> cols = new List<Color>();

	// Token: 0x04000537 RID: 1335
	[NonSerialized]
	private Material mMaterial;

	// Token: 0x04000538 RID: 1336
	[NonSerialized]
	private Texture mTexture;

	// Token: 0x04000539 RID: 1337
	[NonSerialized]
	private Shader mShader;

	// Token: 0x0400053A RID: 1338
	[NonSerialized]
	private int mClipCount;

	// Token: 0x0400053B RID: 1339
	[NonSerialized]
	private Transform mTrans;

	// Token: 0x0400053C RID: 1340
	[NonSerialized]
	private Mesh mMesh;

	// Token: 0x0400053D RID: 1341
	[NonSerialized]
	private MeshFilter mFilter;

	// Token: 0x0400053E RID: 1342
	[NonSerialized]
	private MeshRenderer mRenderer;

	// Token: 0x0400053F RID: 1343
	[NonSerialized]
	private Material mDynamicMat;

	// Token: 0x04000540 RID: 1344
	[NonSerialized]
	private int[] mIndices;

	// Token: 0x04000541 RID: 1345
	[NonSerialized]
	private UIDrawCall.ShadowMode mShadowMode;

	// Token: 0x04000542 RID: 1346
	[NonSerialized]
	private bool mRebuildMat = true;

	// Token: 0x04000543 RID: 1347
	[NonSerialized]
	private bool mLegacyShader;

	// Token: 0x04000544 RID: 1348
	[NonSerialized]
	private int mRenderQueue = 3000;

	// Token: 0x04000545 RID: 1349
	[NonSerialized]
	private int mTriangles;

	// Token: 0x04000546 RID: 1350
	[NonSerialized]
	public bool isDirty;

	// Token: 0x04000547 RID: 1351
	[NonSerialized]
	private bool mTextureClip;

	// Token: 0x04000548 RID: 1352
	[NonSerialized]
	private bool mIsNew = true;

	// Token: 0x04000549 RID: 1353
	public UIDrawCall.OnRenderCallback onRender;

	// Token: 0x0400054A RID: 1354
	public UIDrawCall.OnCreateDrawCall onCreateDrawCall;

	// Token: 0x0400054B RID: 1355
	[NonSerialized]
	private string mSortingLayerName;

	// Token: 0x0400054C RID: 1356
	[NonSerialized]
	private int mSortingOrder;

	// Token: 0x0400054D RID: 1357
	private static ColorSpace mColorSpace = ColorSpace.Uninitialized;

	// Token: 0x0400054E RID: 1358
	private const int maxIndexBufferCache = 10;

	// Token: 0x0400054F RID: 1359
	private static List<int[]> mCache = new List<int[]>(10);

	// Token: 0x04000550 RID: 1360
	protected MaterialPropertyBlock mBlock;

	// Token: 0x04000551 RID: 1361
	private static int[] ClipRange = null;

	// Token: 0x04000552 RID: 1362
	private static int[] ClipArgs = null;

	// Token: 0x04000553 RID: 1363
	private static int dx9BugWorkaround = -1;

	// Token: 0x02000640 RID: 1600
	[DoNotObfuscateNGUI]
	public enum Clipping
	{
		// Token: 0x04004528 RID: 17704
		None,
		// Token: 0x04004529 RID: 17705
		TextureMask,
		// Token: 0x0400452A RID: 17706
		SoftClip = 3,
		// Token: 0x0400452B RID: 17707
		ConstrainButDontClip
	}

	// Token: 0x02000641 RID: 1601
	// (Invoke) Token: 0x06002AA8 RID: 10920
	public delegate void OnRenderCallback(Material mat);

	// Token: 0x02000642 RID: 1602
	// (Invoke) Token: 0x06002AAC RID: 10924
	public delegate void OnCreateDrawCall(UIDrawCall dc, MeshFilter filter, MeshRenderer ren);

	// Token: 0x02000643 RID: 1603
	[DoNotObfuscateNGUI]
	public enum ShadowMode
	{
		// Token: 0x0400452D RID: 17709
		None,
		// Token: 0x0400452E RID: 17710
		Receive,
		// Token: 0x0400452F RID: 17711
		CastAndReceive
	}
}

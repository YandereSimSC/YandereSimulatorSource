using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004AD RID: 1197
	public sealed class BuiltinDebugViewsComponent : PostProcessingComponentCommandBuffer<BuiltinDebugViewsModel>
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x0017AB10 File Offset: 0x00178D10
		public override bool active
		{
			get
			{
				return base.model.IsModeActive(BuiltinDebugViewsModel.Mode.Depth) || base.model.IsModeActive(BuiltinDebugViewsModel.Mode.Normals) || base.model.IsModeActive(BuiltinDebugViewsModel.Mode.MotionVectors);
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0017AB3C File Offset: 0x00178D3C
		public override DepthTextureMode GetCameraFlags()
		{
			BuiltinDebugViewsModel.Mode mode = base.model.settings.mode;
			DepthTextureMode depthTextureMode = DepthTextureMode.None;
			switch (mode)
			{
			case BuiltinDebugViewsModel.Mode.Depth:
				depthTextureMode |= DepthTextureMode.Depth;
				break;
			case BuiltinDebugViewsModel.Mode.Normals:
				depthTextureMode |= DepthTextureMode.DepthNormals;
				break;
			case BuiltinDebugViewsModel.Mode.MotionVectors:
				depthTextureMode |= (DepthTextureMode.Depth | DepthTextureMode.MotionVectors);
				break;
			}
			return depthTextureMode;
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0017AB83 File Offset: 0x00178D83
		public override CameraEvent GetCameraEvent()
		{
			if (base.model.settings.mode != BuiltinDebugViewsModel.Mode.MotionVectors)
			{
				return CameraEvent.BeforeImageEffectsOpaque;
			}
			return CameraEvent.BeforeImageEffects;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0017AB9D File Offset: 0x00178D9D
		public override string GetName()
		{
			return "Builtin Debug Views";
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0017ABA4 File Offset: 0x00178DA4
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			ref BuiltinDebugViewsModel.Settings settings = base.model.settings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			material.shaderKeywords = null;
			if (this.context.isGBufferAvailable)
			{
				material.EnableKeyword("SOURCE_GBUFFER");
			}
			switch (settings.mode)
			{
			case BuiltinDebugViewsModel.Mode.Depth:
				this.DepthPass(cb);
				break;
			case BuiltinDebugViewsModel.Mode.Normals:
				this.DepthNormalsPass(cb);
				break;
			case BuiltinDebugViewsModel.Mode.MotionVectors:
				this.MotionVectorsPass(cb);
				break;
			}
			this.context.Interrupt();
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0017AC34 File Offset: 0x00178E34
		private void DepthPass(CommandBuffer cb)
		{
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			BuiltinDebugViewsModel.DepthSettings depth = base.model.settings.depth;
			cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._DepthScale, 1f / depth.scale);
			cb.Blit(null, BuiltinRenderTextureType.CameraTarget, mat, 0);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0017AC90 File Offset: 0x00178E90
		private void DepthNormalsPass(CommandBuffer cb)
		{
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			cb.Blit(null, BuiltinRenderTextureType.CameraTarget, mat, 1);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0017ACC4 File Offset: 0x00178EC4
		private void MotionVectorsPass(CommandBuffer cb)
		{
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			BuiltinDebugViewsModel.MotionVectorsSettings motionVectors = base.model.settings.motionVectors;
			int nameID = BuiltinDebugViewsComponent.Uniforms._TempRT;
			cb.GetTemporaryRT(nameID, this.context.width, this.context.height, 0, FilterMode.Bilinear);
			cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.sourceOpacity);
			cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, BuiltinRenderTextureType.CameraTarget);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, nameID, material, 2);
			if (motionVectors.motionImageOpacity > 0f && motionVectors.motionImageAmplitude > 0f)
			{
				int tempRT = BuiltinDebugViewsComponent.Uniforms._TempRT2;
				cb.GetTemporaryRT(tempRT, this.context.width, this.context.height, 0, FilterMode.Bilinear);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.motionImageOpacity);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Amplitude, motionVectors.motionImageAmplitude);
				cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, nameID);
				cb.Blit(nameID, tempRT, material, 3);
				cb.ReleaseTemporaryRT(nameID);
				nameID = tempRT;
			}
			if (motionVectors.motionVectorsOpacity > 0f && motionVectors.motionVectorsAmplitude > 0f)
			{
				this.PrepareArrows();
				float num = 1f / (float)motionVectors.motionVectorsResolution;
				float x = num * (float)this.context.height / (float)this.context.width;
				cb.SetGlobalVector(BuiltinDebugViewsComponent.Uniforms._Scale, new Vector2(x, num));
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.motionVectorsOpacity);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Amplitude, motionVectors.motionVectorsAmplitude);
				cb.DrawMesh(this.m_Arrows.mesh, Matrix4x4.identity, material, 0, 4);
			}
			cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, nameID);
			cb.Blit(nameID, BuiltinRenderTextureType.CameraTarget);
			cb.ReleaseTemporaryRT(nameID);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0017AEB8 File Offset: 0x001790B8
		private void PrepareArrows()
		{
			int motionVectorsResolution = base.model.settings.motionVectors.motionVectorsResolution;
			int num = motionVectorsResolution * Screen.width / Screen.height;
			if (this.m_Arrows == null)
			{
				this.m_Arrows = new BuiltinDebugViewsComponent.ArrowArray();
			}
			if (this.m_Arrows.columnCount != num || this.m_Arrows.rowCount != motionVectorsResolution)
			{
				this.m_Arrows.Release();
				this.m_Arrows.BuildMesh(num, motionVectorsResolution);
			}
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0017AF30 File Offset: 0x00179130
		public override void OnDisable()
		{
			if (this.m_Arrows != null)
			{
				this.m_Arrows.Release();
			}
			this.m_Arrows = null;
		}

		// Token: 0x04003C4E RID: 15438
		private const string k_ShaderString = "Hidden/Post FX/Builtin Debug Views";

		// Token: 0x04003C4F RID: 15439
		private BuiltinDebugViewsComponent.ArrowArray m_Arrows;

		// Token: 0x020006B8 RID: 1720
		private static class Uniforms
		{
			// Token: 0x040046E8 RID: 18152
			internal static readonly int _DepthScale = Shader.PropertyToID("_DepthScale");

			// Token: 0x040046E9 RID: 18153
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");

			// Token: 0x040046EA RID: 18154
			internal static readonly int _Opacity = Shader.PropertyToID("_Opacity");

			// Token: 0x040046EB RID: 18155
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x040046EC RID: 18156
			internal static readonly int _TempRT2 = Shader.PropertyToID("_TempRT2");

			// Token: 0x040046ED RID: 18157
			internal static readonly int _Amplitude = Shader.PropertyToID("_Amplitude");

			// Token: 0x040046EE RID: 18158
			internal static readonly int _Scale = Shader.PropertyToID("_Scale");
		}

		// Token: 0x020006B9 RID: 1721
		private enum Pass
		{
			// Token: 0x040046F0 RID: 18160
			Depth,
			// Token: 0x040046F1 RID: 18161
			Normals,
			// Token: 0x040046F2 RID: 18162
			MovecOpacity,
			// Token: 0x040046F3 RID: 18163
			MovecImaging,
			// Token: 0x040046F4 RID: 18164
			MovecArrows
		}

		// Token: 0x020006BA RID: 1722
		private class ArrowArray
		{
			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x06002BDC RID: 11228 RVA: 0x001C6FCA File Offset: 0x001C51CA
			// (set) Token: 0x06002BDD RID: 11229 RVA: 0x001C6FD2 File Offset: 0x001C51D2
			public Mesh mesh { get; private set; }

			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x06002BDE RID: 11230 RVA: 0x001C6FDB File Offset: 0x001C51DB
			// (set) Token: 0x06002BDF RID: 11231 RVA: 0x001C6FE3 File Offset: 0x001C51E3
			public int columnCount { get; private set; }

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x001C6FEC File Offset: 0x001C51EC
			// (set) Token: 0x06002BE1 RID: 11233 RVA: 0x001C6FF4 File Offset: 0x001C51F4
			public int rowCount { get; private set; }

			// Token: 0x06002BE2 RID: 11234 RVA: 0x001C7000 File Offset: 0x001C5200
			public void BuildMesh(int columns, int rows)
			{
				Vector3[] array = new Vector3[]
				{
					new Vector3(0f, 0f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(-1f, 1f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(1f, 1f, 0f)
				};
				int num = 6 * columns * rows;
				List<Vector3> list = new List<Vector3>(num);
				List<Vector2> list2 = new List<Vector2>(num);
				for (int i = 0; i < rows; i++)
				{
					for (int j = 0; j < columns; j++)
					{
						Vector2 item = new Vector2((0.5f + (float)j) / (float)columns, (0.5f + (float)i) / (float)rows);
						for (int k = 0; k < 6; k++)
						{
							list.Add(array[k]);
							list2.Add(item);
						}
					}
				}
				int[] array2 = new int[num];
				for (int l = 0; l < num; l++)
				{
					array2[l] = l;
				}
				this.mesh = new Mesh
				{
					hideFlags = HideFlags.DontSave
				};
				this.mesh.SetVertices(list);
				this.mesh.SetUVs(0, list2);
				this.mesh.SetIndices(array2, MeshTopology.Lines, 0);
				this.mesh.UploadMeshData(true);
				this.columnCount = columns;
				this.rowCount = rows;
			}

			// Token: 0x06002BE3 RID: 11235 RVA: 0x001C71A3 File Offset: 0x001C53A3
			public void Release()
			{
				GraphicsUtils.Destroy(this.mesh);
				this.mesh = null;
			}
		}
	}
}

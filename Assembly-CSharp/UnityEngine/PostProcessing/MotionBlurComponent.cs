using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B6 RID: 1206
	public sealed class MotionBlurComponent : PostProcessingComponentCommandBuffer<MotionBlurModel>
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0017CD69 File Offset: 0x0017AF69
		public MotionBlurComponent.ReconstructionFilter reconstructionFilter
		{
			get
			{
				if (this.m_ReconstructionFilter == null)
				{
					this.m_ReconstructionFilter = new MotionBlurComponent.ReconstructionFilter();
				}
				return this.m_ReconstructionFilter;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x0017CD84 File Offset: 0x0017AF84
		public MotionBlurComponent.FrameBlendingFilter frameBlendingFilter
		{
			get
			{
				if (this.m_FrameBlendingFilter == null)
				{
					this.m_FrameBlendingFilter = new MotionBlurComponent.FrameBlendingFilter();
				}
				return this.m_FrameBlendingFilter;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x0017CDA0 File Offset: 0x0017AFA0
		public override bool active
		{
			get
			{
				MotionBlurModel.Settings settings = base.model.settings;
				return base.model.enabled && ((settings.shutterAngle > 0f && this.reconstructionFilter.IsSupported()) || settings.frameBlending > 0f) && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2 && !this.context.interrupted;
			}
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0017CE05 File Offset: 0x0017B005
		public override string GetName()
		{
			return "Motion Blur";
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x0017CE0C File Offset: 0x0017B00C
		public void ResetHistory()
		{
			if (this.m_FrameBlendingFilter != null)
			{
				this.m_FrameBlendingFilter.Dispose();
			}
			this.m_FrameBlendingFilter = null;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00096850 File Offset: 0x00094A50
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x0017CE28 File Offset: 0x0017B028
		public override CameraEvent GetCameraEvent()
		{
			return CameraEvent.BeforeImageEffects;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x0017CE2C File Offset: 0x0017B02C
		public override void OnEnable()
		{
			this.m_FirstFrame = true;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0017CE38 File Offset: 0x0017B038
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			if (this.m_FirstFrame)
			{
				this.m_FirstFrame = false;
				return;
			}
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Motion Blur");
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Blit");
			MotionBlurModel.Settings settings = base.model.settings;
			RenderTextureFormat format = this.context.isHdr ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
			int tempRT = MotionBlurComponent.Uniforms._TempRT;
			cb.GetTemporaryRT(tempRT, this.context.width, this.context.height, 0, FilterMode.Point, format);
			if (settings.shutterAngle > 0f && settings.frameBlending > 0f)
			{
				this.reconstructionFilter.ProcessImage(this.context, cb, ref settings, BuiltinRenderTextureType.CameraTarget, tempRT, material);
				this.frameBlendingFilter.BlendFrames(cb, settings.frameBlending, tempRT, BuiltinRenderTextureType.CameraTarget, material);
				this.frameBlendingFilter.PushFrame(cb, tempRT, this.context.width, this.context.height, material);
			}
			else if (settings.shutterAngle > 0f)
			{
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, BuiltinRenderTextureType.CameraTarget);
				cb.Blit(BuiltinRenderTextureType.CameraTarget, tempRT, mat, 0);
				this.reconstructionFilter.ProcessImage(this.context, cb, ref settings, tempRT, BuiltinRenderTextureType.CameraTarget, material);
			}
			else if (settings.frameBlending > 0f)
			{
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, BuiltinRenderTextureType.CameraTarget);
				cb.Blit(BuiltinRenderTextureType.CameraTarget, tempRT, mat, 0);
				this.frameBlendingFilter.BlendFrames(cb, settings.frameBlending, tempRT, BuiltinRenderTextureType.CameraTarget, material);
				this.frameBlendingFilter.PushFrame(cb, tempRT, this.context.width, this.context.height, material);
			}
			cb.ReleaseTemporaryRT(tempRT);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0017D02F File Offset: 0x0017B22F
		public override void OnDisable()
		{
			if (this.m_FrameBlendingFilter != null)
			{
				this.m_FrameBlendingFilter.Dispose();
			}
		}

		// Token: 0x04003C69 RID: 15465
		private MotionBlurComponent.ReconstructionFilter m_ReconstructionFilter;

		// Token: 0x04003C6A RID: 15466
		private MotionBlurComponent.FrameBlendingFilter m_FrameBlendingFilter;

		// Token: 0x04003C6B RID: 15467
		private bool m_FirstFrame = true;

		// Token: 0x020006C3 RID: 1731
		private static class Uniforms
		{
			// Token: 0x0400472C RID: 18220
			internal static readonly int _VelocityScale = Shader.PropertyToID("_VelocityScale");

			// Token: 0x0400472D RID: 18221
			internal static readonly int _MaxBlurRadius = Shader.PropertyToID("_MaxBlurRadius");

			// Token: 0x0400472E RID: 18222
			internal static readonly int _RcpMaxBlurRadius = Shader.PropertyToID("_RcpMaxBlurRadius");

			// Token: 0x0400472F RID: 18223
			internal static readonly int _VelocityTex = Shader.PropertyToID("_VelocityTex");

			// Token: 0x04004730 RID: 18224
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x04004731 RID: 18225
			internal static readonly int _Tile2RT = Shader.PropertyToID("_Tile2RT");

			// Token: 0x04004732 RID: 18226
			internal static readonly int _Tile4RT = Shader.PropertyToID("_Tile4RT");

			// Token: 0x04004733 RID: 18227
			internal static readonly int _Tile8RT = Shader.PropertyToID("_Tile8RT");

			// Token: 0x04004734 RID: 18228
			internal static readonly int _TileMaxOffs = Shader.PropertyToID("_TileMaxOffs");

			// Token: 0x04004735 RID: 18229
			internal static readonly int _TileMaxLoop = Shader.PropertyToID("_TileMaxLoop");

			// Token: 0x04004736 RID: 18230
			internal static readonly int _TileVRT = Shader.PropertyToID("_TileVRT");

			// Token: 0x04004737 RID: 18231
			internal static readonly int _NeighborMaxTex = Shader.PropertyToID("_NeighborMaxTex");

			// Token: 0x04004738 RID: 18232
			internal static readonly int _LoopCount = Shader.PropertyToID("_LoopCount");

			// Token: 0x04004739 RID: 18233
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");

			// Token: 0x0400473A RID: 18234
			internal static readonly int _History1LumaTex = Shader.PropertyToID("_History1LumaTex");

			// Token: 0x0400473B RID: 18235
			internal static readonly int _History2LumaTex = Shader.PropertyToID("_History2LumaTex");

			// Token: 0x0400473C RID: 18236
			internal static readonly int _History3LumaTex = Shader.PropertyToID("_History3LumaTex");

			// Token: 0x0400473D RID: 18237
			internal static readonly int _History4LumaTex = Shader.PropertyToID("_History4LumaTex");

			// Token: 0x0400473E RID: 18238
			internal static readonly int _History1ChromaTex = Shader.PropertyToID("_History1ChromaTex");

			// Token: 0x0400473F RID: 18239
			internal static readonly int _History2ChromaTex = Shader.PropertyToID("_History2ChromaTex");

			// Token: 0x04004740 RID: 18240
			internal static readonly int _History3ChromaTex = Shader.PropertyToID("_History3ChromaTex");

			// Token: 0x04004741 RID: 18241
			internal static readonly int _History4ChromaTex = Shader.PropertyToID("_History4ChromaTex");

			// Token: 0x04004742 RID: 18242
			internal static readonly int _History1Weight = Shader.PropertyToID("_History1Weight");

			// Token: 0x04004743 RID: 18243
			internal static readonly int _History2Weight = Shader.PropertyToID("_History2Weight");

			// Token: 0x04004744 RID: 18244
			internal static readonly int _History3Weight = Shader.PropertyToID("_History3Weight");

			// Token: 0x04004745 RID: 18245
			internal static readonly int _History4Weight = Shader.PropertyToID("_History4Weight");
		}

		// Token: 0x020006C4 RID: 1732
		private enum Pass
		{
			// Token: 0x04004747 RID: 18247
			VelocitySetup,
			// Token: 0x04004748 RID: 18248
			TileMax1,
			// Token: 0x04004749 RID: 18249
			TileMax2,
			// Token: 0x0400474A RID: 18250
			TileMaxV,
			// Token: 0x0400474B RID: 18251
			NeighborMax,
			// Token: 0x0400474C RID: 18252
			Reconstruction,
			// Token: 0x0400474D RID: 18253
			FrameCompression,
			// Token: 0x0400474E RID: 18254
			FrameBlendingChroma,
			// Token: 0x0400474F RID: 18255
			FrameBlendingRaw
		}

		// Token: 0x020006C5 RID: 1733
		public class ReconstructionFilter
		{
			// Token: 0x06002BEE RID: 11246 RVA: 0x001C769B File Offset: 0x001C589B
			public ReconstructionFilter()
			{
				this.CheckTextureFormatSupport();
			}

			// Token: 0x06002BEF RID: 11247 RVA: 0x001C76B8 File Offset: 0x001C58B8
			private void CheckTextureFormatSupport()
			{
				if (!SystemInfo.SupportsRenderTextureFormat(this.m_PackedRTFormat))
				{
					this.m_PackedRTFormat = RenderTextureFormat.ARGB32;
				}
			}

			// Token: 0x06002BF0 RID: 11248 RVA: 0x001C76CE File Offset: 0x001C58CE
			public bool IsSupported()
			{
				return SystemInfo.supportsMotionVectors;
			}

			// Token: 0x06002BF1 RID: 11249 RVA: 0x001C76D8 File Offset: 0x001C58D8
			public void ProcessImage(PostProcessingContext context, CommandBuffer cb, ref MotionBlurModel.Settings settings, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material)
			{
				int num = (int)(5f * (float)context.height / 100f);
				int num2 = ((num - 1) / 8 + 1) * 8;
				float value = settings.shutterAngle / 360f;
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._VelocityScale, value);
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._MaxBlurRadius, (float)num);
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._RcpMaxBlurRadius, 1f / (float)num);
				int velocityTex = MotionBlurComponent.Uniforms._VelocityTex;
				cb.GetTemporaryRT(velocityTex, context.width, context.height, 0, FilterMode.Point, this.m_PackedRTFormat, RenderTextureReadWrite.Linear);
				cb.Blit(null, velocityTex, material, 0);
				int tile2RT = MotionBlurComponent.Uniforms._Tile2RT;
				cb.GetTemporaryRT(tile2RT, context.width / 2, context.height / 2, 0, FilterMode.Point, this.m_VectorRTFormat, RenderTextureReadWrite.Linear);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, velocityTex);
				cb.Blit(velocityTex, tile2RT, material, 1);
				int tile4RT = MotionBlurComponent.Uniforms._Tile4RT;
				cb.GetTemporaryRT(tile4RT, context.width / 4, context.height / 4, 0, FilterMode.Point, this.m_VectorRTFormat, RenderTextureReadWrite.Linear);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, tile2RT);
				cb.Blit(tile2RT, tile4RT, material, 2);
				cb.ReleaseTemporaryRT(tile2RT);
				int tile8RT = MotionBlurComponent.Uniforms._Tile8RT;
				cb.GetTemporaryRT(tile8RT, context.width / 8, context.height / 8, 0, FilterMode.Point, this.m_VectorRTFormat, RenderTextureReadWrite.Linear);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, tile4RT);
				cb.Blit(tile4RT, tile8RT, material, 2);
				cb.ReleaseTemporaryRT(tile4RT);
				Vector2 v = Vector2.one * ((float)num2 / 8f - 1f) * -0.5f;
				cb.SetGlobalVector(MotionBlurComponent.Uniforms._TileMaxOffs, v);
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._TileMaxLoop, (float)((int)((float)num2 / 8f)));
				int tileVRT = MotionBlurComponent.Uniforms._TileVRT;
				cb.GetTemporaryRT(tileVRT, context.width / num2, context.height / num2, 0, FilterMode.Point, this.m_VectorRTFormat, RenderTextureReadWrite.Linear);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, tile8RT);
				cb.Blit(tile8RT, tileVRT, material, 3);
				cb.ReleaseTemporaryRT(tile8RT);
				int neighborMaxTex = MotionBlurComponent.Uniforms._NeighborMaxTex;
				int width = context.width / num2;
				int height = context.height / num2;
				cb.GetTemporaryRT(neighborMaxTex, width, height, 0, FilterMode.Point, this.m_VectorRTFormat, RenderTextureReadWrite.Linear);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, tileVRT);
				cb.Blit(tileVRT, neighborMaxTex, material, 4);
				cb.ReleaseTemporaryRT(tileVRT);
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._LoopCount, (float)Mathf.Clamp(settings.sampleCount / 2, 1, 64));
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, source);
				cb.Blit(source, destination, material, 5);
				cb.ReleaseTemporaryRT(velocityTex);
				cb.ReleaseTemporaryRT(neighborMaxTex);
			}

			// Token: 0x04004750 RID: 18256
			private RenderTextureFormat m_VectorRTFormat = RenderTextureFormat.RGHalf;

			// Token: 0x04004751 RID: 18257
			private RenderTextureFormat m_PackedRTFormat = RenderTextureFormat.ARGB2101010;
		}

		// Token: 0x020006C6 RID: 1734
		public class FrameBlendingFilter
		{
			// Token: 0x06002BF2 RID: 11250 RVA: 0x001C79BA File Offset: 0x001C5BBA
			public FrameBlendingFilter()
			{
				this.m_UseCompression = MotionBlurComponent.FrameBlendingFilter.CheckSupportCompression();
				this.m_RawTextureFormat = MotionBlurComponent.FrameBlendingFilter.GetPreferredRenderTextureFormat();
				this.m_FrameList = new MotionBlurComponent.FrameBlendingFilter.Frame[4];
			}

			// Token: 0x06002BF3 RID: 11251 RVA: 0x001C79E4 File Offset: 0x001C5BE4
			public void Dispose()
			{
				foreach (MotionBlurComponent.FrameBlendingFilter.Frame frame in this.m_FrameList)
				{
					frame.Release();
				}
			}

			// Token: 0x06002BF4 RID: 11252 RVA: 0x001C7A18 File Offset: 0x001C5C18
			public void PushFrame(CommandBuffer cb, RenderTargetIdentifier source, int width, int height, Material material)
			{
				int frameCount = Time.frameCount;
				if (frameCount == this.m_LastFrameCount)
				{
					return;
				}
				int num = frameCount % this.m_FrameList.Length;
				if (this.m_UseCompression)
				{
					this.m_FrameList[num].MakeRecord(cb, source, width, height, material);
				}
				else
				{
					this.m_FrameList[num].MakeRecordRaw(cb, source, width, height, this.m_RawTextureFormat);
				}
				this.m_LastFrameCount = frameCount;
			}

			// Token: 0x06002BF5 RID: 11253 RVA: 0x001C7A88 File Offset: 0x001C5C88
			public void BlendFrames(CommandBuffer cb, float strength, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material)
			{
				float time = Time.time;
				MotionBlurComponent.FrameBlendingFilter.Frame frameRelative = this.GetFrameRelative(-1);
				MotionBlurComponent.FrameBlendingFilter.Frame frameRelative2 = this.GetFrameRelative(-2);
				MotionBlurComponent.FrameBlendingFilter.Frame frameRelative3 = this.GetFrameRelative(-3);
				MotionBlurComponent.FrameBlendingFilter.Frame frameRelative4 = this.GetFrameRelative(-4);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History1LumaTex, frameRelative.lumaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History2LumaTex, frameRelative2.lumaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History3LumaTex, frameRelative3.lumaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History4LumaTex, frameRelative4.lumaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History1ChromaTex, frameRelative.chromaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History2ChromaTex, frameRelative2.chromaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History3ChromaTex, frameRelative3.chromaTexture);
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._History4ChromaTex, frameRelative4.chromaTexture);
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._History1Weight, frameRelative.CalculateWeight(strength, time));
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._History2Weight, frameRelative2.CalculateWeight(strength, time));
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._History3Weight, frameRelative3.CalculateWeight(strength, time));
				cb.SetGlobalFloat(MotionBlurComponent.Uniforms._History4Weight, frameRelative4.CalculateWeight(strength, time));
				cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, source);
				cb.Blit(source, destination, material, this.m_UseCompression ? 7 : 8);
			}

			// Token: 0x06002BF6 RID: 11254 RVA: 0x001C7BE4 File Offset: 0x001C5DE4
			private static bool CheckSupportCompression()
			{
				return SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.R8) && SystemInfo.supportedRenderTargetCount > 1;
			}

			// Token: 0x06002BF7 RID: 11255 RVA: 0x001C7BFC File Offset: 0x001C5DFC
			private static RenderTextureFormat GetPreferredRenderTextureFormat()
			{
				RenderTextureFormat[] array = new RenderTextureFormat[3];
				RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.51A7A390CD6DE245186881400B18C9D822EFE240).FieldHandle);
				foreach (RenderTextureFormat renderTextureFormat in array)
				{
					if (SystemInfo.SupportsRenderTextureFormat(renderTextureFormat))
					{
						return renderTextureFormat;
					}
				}
				return RenderTextureFormat.Default;
			}

			// Token: 0x06002BF8 RID: 11256 RVA: 0x001C7C38 File Offset: 0x001C5E38
			private MotionBlurComponent.FrameBlendingFilter.Frame GetFrameRelative(int offset)
			{
				int num = (Time.frameCount + this.m_FrameList.Length + offset) % this.m_FrameList.Length;
				return this.m_FrameList[num];
			}

			// Token: 0x04004752 RID: 18258
			private bool m_UseCompression;

			// Token: 0x04004753 RID: 18259
			private RenderTextureFormat m_RawTextureFormat;

			// Token: 0x04004754 RID: 18260
			private MotionBlurComponent.FrameBlendingFilter.Frame[] m_FrameList;

			// Token: 0x04004755 RID: 18261
			private int m_LastFrameCount;

			// Token: 0x02000795 RID: 1941
			private struct Frame
			{
				// Token: 0x06002DF4 RID: 11764 RVA: 0x001D2080 File Offset: 0x001D0280
				public float CalculateWeight(float strength, float currentTime)
				{
					if (Mathf.Approximately(this.m_Time, 0f))
					{
						return 0f;
					}
					float num = Mathf.Lerp(80f, 16f, strength);
					return Mathf.Exp((this.m_Time - currentTime) * num);
				}

				// Token: 0x06002DF5 RID: 11765 RVA: 0x001D20C8 File Offset: 0x001D02C8
				public void Release()
				{
					if (this.lumaTexture != null)
					{
						RenderTexture.ReleaseTemporary(this.lumaTexture);
					}
					if (this.chromaTexture != null)
					{
						RenderTexture.ReleaseTemporary(this.chromaTexture);
					}
					this.lumaTexture = null;
					this.chromaTexture = null;
				}

				// Token: 0x06002DF6 RID: 11766 RVA: 0x001D2118 File Offset: 0x001D0318
				public void MakeRecord(CommandBuffer cb, RenderTargetIdentifier source, int width, int height, Material material)
				{
					this.Release();
					this.lumaTexture = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.R8, RenderTextureReadWrite.Linear);
					this.chromaTexture = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.R8, RenderTextureReadWrite.Linear);
					this.lumaTexture.filterMode = FilterMode.Point;
					this.chromaTexture.filterMode = FilterMode.Point;
					if (this.m_MRT == null)
					{
						this.m_MRT = new RenderTargetIdentifier[2];
					}
					this.m_MRT[0] = this.lumaTexture;
					this.m_MRT[1] = this.chromaTexture;
					cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, source);
					cb.SetRenderTarget(this.m_MRT, this.lumaTexture);
					cb.DrawMesh(GraphicsUtils.quad, Matrix4x4.identity, material, 0, 6);
					this.m_Time = Time.time;
				}

				// Token: 0x06002DF7 RID: 11767 RVA: 0x001D21EC File Offset: 0x001D03EC
				public void MakeRecordRaw(CommandBuffer cb, RenderTargetIdentifier source, int width, int height, RenderTextureFormat format)
				{
					this.Release();
					this.lumaTexture = RenderTexture.GetTemporary(width, height, 0, format);
					this.lumaTexture.filterMode = FilterMode.Point;
					cb.SetGlobalTexture(MotionBlurComponent.Uniforms._MainTex, source);
					cb.Blit(source, this.lumaTexture);
					this.m_Time = Time.time;
				}

				// Token: 0x04004AF0 RID: 19184
				public RenderTexture lumaTexture;

				// Token: 0x04004AF1 RID: 19185
				public RenderTexture chromaTexture;

				// Token: 0x04004AF2 RID: 19186
				private float m_Time;

				// Token: 0x04004AF3 RID: 19187
				private RenderTargetIdentifier[] m_MRT;
			}
		}
	}
}

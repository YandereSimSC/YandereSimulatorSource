using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004B8 RID: 1208
	public sealed class TaaComponent : PostProcessingComponentRenderTexture<AntialiasingModel>
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0017D7D8 File Offset: 0x0017B9D8
		public override bool active
		{
			get
			{
				return base.model.enabled && base.model.settings.method == AntialiasingModel.Method.Taa && SystemInfo.supportsMotionVectors && SystemInfo.supportedRenderTargetCount >= 2 && !this.context.interrupted;
			}
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00096850 File Offset: 0x00094A50
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0017D824 File Offset: 0x0017BA24
		// (set) Token: 0x06001EAB RID: 7851 RVA: 0x0017D82C File Offset: 0x0017BA2C
		public Vector2 jitterVector { get; private set; }

		// Token: 0x06001EAC RID: 7852 RVA: 0x0017D835 File Offset: 0x0017BA35
		public void ResetHistory()
		{
			this.m_ResetHistory = true;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0017D840 File Offset: 0x0017BA40
		public void SetProjectionMatrix(Func<Vector2, Matrix4x4> jitteredFunc)
		{
			AntialiasingModel.TaaSettings taaSettings = base.model.settings.taaSettings;
			Vector2 vector = this.GenerateRandomOffset();
			vector *= taaSettings.jitterSpread;
			this.context.camera.nonJitteredProjectionMatrix = this.context.camera.projectionMatrix;
			if (jitteredFunc != null)
			{
				this.context.camera.projectionMatrix = jitteredFunc(vector);
			}
			else
			{
				this.context.camera.projectionMatrix = (this.context.camera.orthographic ? this.GetOrthographicProjectionMatrix(vector) : this.GetPerspectiveProjectionMatrix(vector));
			}
			this.context.camera.useJitteredProjectionMatrixForTransparentRendering = false;
			vector.x /= (float)this.context.width;
			vector.y /= (float)this.context.height;
			this.context.materialFactory.Get("Hidden/Post FX/Temporal Anti-aliasing").SetVector(TaaComponent.Uniforms._Jitter, vector);
			this.jitterVector = vector;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0017D94C File Offset: 0x0017BB4C
		public void Render(RenderTexture source, RenderTexture destination)
		{
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Temporal Anti-aliasing");
			material.shaderKeywords = null;
			AntialiasingModel.TaaSettings taaSettings = base.model.settings.taaSettings;
			if (this.m_ResetHistory || this.m_HistoryTexture == null || this.m_HistoryTexture.width != source.width || this.m_HistoryTexture.height != source.height)
			{
				if (this.m_HistoryTexture)
				{
					RenderTexture.ReleaseTemporary(this.m_HistoryTexture);
				}
				this.m_HistoryTexture = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
				this.m_HistoryTexture.name = "TAA History";
				Graphics.Blit(source, this.m_HistoryTexture, material, 2);
			}
			material.SetVector(TaaComponent.Uniforms._SharpenParameters, new Vector4(taaSettings.sharpen, 0f, 0f, 0f));
			material.SetVector(TaaComponent.Uniforms._FinalBlendParameters, new Vector4(taaSettings.stationaryBlending, taaSettings.motionBlending, 6000f, 0f));
			material.SetTexture(TaaComponent.Uniforms._MainTex, source);
			material.SetTexture(TaaComponent.Uniforms._HistoryTex, this.m_HistoryTexture);
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
			temporary.name = "TAA History";
			this.m_MRT[0] = destination.colorBuffer;
			this.m_MRT[1] = temporary.colorBuffer;
			Graphics.SetRenderTarget(this.m_MRT, source.depthBuffer);
			GraphicsUtils.Blit(material, this.context.camera.orthographic ? 1 : 0);
			RenderTexture.ReleaseTemporary(this.m_HistoryTexture);
			this.m_HistoryTexture = temporary;
			this.m_ResetHistory = false;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0017DB10 File Offset: 0x0017BD10
		private float GetHaltonValue(int index, int radix)
		{
			float num = 0f;
			float num2 = 1f / (float)radix;
			while (index > 0)
			{
				num += (float)(index % radix) * num2;
				index /= radix;
				num2 /= (float)radix;
			}
			return num;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0017DB48 File Offset: 0x0017BD48
		private Vector2 GenerateRandomOffset()
		{
			Vector2 result = new Vector2(this.GetHaltonValue(this.m_SampleIndex & 1023, 2), this.GetHaltonValue(this.m_SampleIndex & 1023, 3));
			int num = this.m_SampleIndex + 1;
			this.m_SampleIndex = num;
			if (num >= 8)
			{
				this.m_SampleIndex = 0;
			}
			return result;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0017DB9C File Offset: 0x0017BD9C
		private Matrix4x4 GetPerspectiveProjectionMatrix(Vector2 offset)
		{
			float num = Mathf.Tan(0.008726646f * this.context.camera.fieldOfView);
			float num2 = num * this.context.camera.aspect;
			offset.x *= num2 / (0.5f * (float)this.context.width);
			offset.y *= num / (0.5f * (float)this.context.height);
			float num3 = (offset.x - num2) * this.context.camera.nearClipPlane;
			float num4 = (offset.x + num2) * this.context.camera.nearClipPlane;
			float num5 = (offset.y + num) * this.context.camera.nearClipPlane;
			float num6 = (offset.y - num) * this.context.camera.nearClipPlane;
			Matrix4x4 result = default(Matrix4x4);
			result[0, 0] = 2f * this.context.camera.nearClipPlane / (num4 - num3);
			result[0, 1] = 0f;
			result[0, 2] = (num4 + num3) / (num4 - num3);
			result[0, 3] = 0f;
			result[1, 0] = 0f;
			result[1, 1] = 2f * this.context.camera.nearClipPlane / (num5 - num6);
			result[1, 2] = (num5 + num6) / (num5 - num6);
			result[1, 3] = 0f;
			result[2, 0] = 0f;
			result[2, 1] = 0f;
			result[2, 2] = -(this.context.camera.farClipPlane + this.context.camera.nearClipPlane) / (this.context.camera.farClipPlane - this.context.camera.nearClipPlane);
			result[2, 3] = -(2f * this.context.camera.farClipPlane * this.context.camera.nearClipPlane) / (this.context.camera.farClipPlane - this.context.camera.nearClipPlane);
			result[3, 0] = 0f;
			result[3, 1] = 0f;
			result[3, 2] = -1f;
			result[3, 3] = 0f;
			return result;
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0017DE24 File Offset: 0x0017C024
		private Matrix4x4 GetOrthographicProjectionMatrix(Vector2 offset)
		{
			float orthographicSize = this.context.camera.orthographicSize;
			float num = orthographicSize * this.context.camera.aspect;
			offset.x *= num / (0.5f * (float)this.context.width);
			offset.y *= orthographicSize / (0.5f * (float)this.context.height);
			float left = offset.x - num;
			float right = offset.x + num;
			float top = offset.y + orthographicSize;
			float bottom = offset.y - orthographicSize;
			return Matrix4x4.Ortho(left, right, bottom, top, this.context.camera.nearClipPlane, this.context.camera.farClipPlane);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0017DEE0 File Offset: 0x0017C0E0
		public override void OnDisable()
		{
			if (this.m_HistoryTexture != null)
			{
				RenderTexture.ReleaseTemporary(this.m_HistoryTexture);
			}
			this.m_HistoryTexture = null;
			this.m_SampleIndex = 0;
			this.ResetHistory();
		}

		// Token: 0x04003C71 RID: 15473
		private const string k_ShaderString = "Hidden/Post FX/Temporal Anti-aliasing";

		// Token: 0x04003C72 RID: 15474
		private const int k_SampleCount = 8;

		// Token: 0x04003C73 RID: 15475
		private readonly RenderBuffer[] m_MRT = new RenderBuffer[2];

		// Token: 0x04003C74 RID: 15476
		private int m_SampleIndex;

		// Token: 0x04003C75 RID: 15477
		private bool m_ResetHistory = true;

		// Token: 0x04003C76 RID: 15478
		private RenderTexture m_HistoryTexture;

		// Token: 0x020006C9 RID: 1737
		private static class Uniforms
		{
			// Token: 0x04004783 RID: 18307
			internal static int _Jitter = Shader.PropertyToID("_Jitter");

			// Token: 0x04004784 RID: 18308
			internal static int _SharpenParameters = Shader.PropertyToID("_SharpenParameters");

			// Token: 0x04004785 RID: 18309
			internal static int _FinalBlendParameters = Shader.PropertyToID("_FinalBlendParameters");

			// Token: 0x04004786 RID: 18310
			internal static int _HistoryTex = Shader.PropertyToID("_HistoryTex");

			// Token: 0x04004787 RID: 18311
			internal static int _MainTex = Shader.PropertyToID("_MainTex");
		}
	}
}

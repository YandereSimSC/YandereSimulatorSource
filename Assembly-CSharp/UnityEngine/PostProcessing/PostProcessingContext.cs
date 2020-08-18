using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004CF RID: 1231
	public class PostProcessingContext
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x0017F170 File Offset: 0x0017D370
		// (set) Token: 0x06001F22 RID: 7970 RVA: 0x0017F178 File Offset: 0x0017D378
		public bool interrupted { get; private set; }

		// Token: 0x06001F23 RID: 7971 RVA: 0x0017F181 File Offset: 0x0017D381
		public void Interrupt()
		{
			this.interrupted = true;
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0017F18A File Offset: 0x0017D38A
		public PostProcessingContext Reset()
		{
			this.profile = null;
			this.camera = null;
			this.materialFactory = null;
			this.renderTextureFactory = null;
			this.interrupted = false;
			return this;
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x0017F1B0 File Offset: 0x0017D3B0
		public bool isGBufferAvailable
		{
			get
			{
				return this.camera.actualRenderingPath == RenderingPath.DeferredShading;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0017F1C0 File Offset: 0x0017D3C0
		public bool isHdr
		{
			get
			{
				return this.camera.allowHDR;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x0017F1CD File Offset: 0x0017D3CD
		public int width
		{
			get
			{
				return this.camera.pixelWidth;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x0017F1DA File Offset: 0x0017D3DA
		public int height
		{
			get
			{
				return this.camera.pixelHeight;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0017F1E7 File Offset: 0x0017D3E7
		public Rect viewport
		{
			get
			{
				return this.camera.rect;
			}
		}

		// Token: 0x04003CA8 RID: 15528
		public PostProcessingProfile profile;

		// Token: 0x04003CA9 RID: 15529
		public Camera camera;

		// Token: 0x04003CAA RID: 15530
		public MaterialFactory materialFactory;

		// Token: 0x04003CAB RID: 15531
		public RenderTextureFactory renderTextureFactory;
	}
}

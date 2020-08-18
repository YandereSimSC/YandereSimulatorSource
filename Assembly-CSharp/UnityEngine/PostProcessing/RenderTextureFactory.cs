using System;
using System.Collections.Generic;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D5 RID: 1237
	public sealed class RenderTextureFactory : IDisposable
	{
		// Token: 0x06001F3F RID: 7999 RVA: 0x0017F83F File Offset: 0x0017DA3F
		public RenderTextureFactory()
		{
			this.m_TemporaryRTs = new HashSet<RenderTexture>();
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0017F854 File Offset: 0x0017DA54
		public RenderTexture Get(RenderTexture baseRenderTexture)
		{
			return this.Get(baseRenderTexture.width, baseRenderTexture.height, baseRenderTexture.depth, baseRenderTexture.format, baseRenderTexture.sRGB ? RenderTextureReadWrite.sRGB : RenderTextureReadWrite.Linear, baseRenderTexture.filterMode, baseRenderTexture.wrapMode, "FactoryTempTexture");
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0017F89C File Offset: 0x0017DA9C
		public RenderTexture Get(int width, int height, int depthBuffer = 0, RenderTextureFormat format = RenderTextureFormat.ARGBHalf, RenderTextureReadWrite rw = RenderTextureReadWrite.Default, FilterMode filterMode = FilterMode.Bilinear, TextureWrapMode wrapMode = TextureWrapMode.Clamp, string name = "FactoryTempTexture")
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, depthBuffer, format, rw);
			temporary.filterMode = filterMode;
			temporary.wrapMode = wrapMode;
			temporary.name = name;
			this.m_TemporaryRTs.Add(temporary);
			return temporary;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0017F8DC File Offset: 0x0017DADC
		public void Release(RenderTexture rt)
		{
			if (rt == null)
			{
				return;
			}
			if (!this.m_TemporaryRTs.Contains(rt))
			{
				throw new ArgumentException(string.Format("Attempting to remove a RenderTexture that was not allocated: {0}", rt));
			}
			this.m_TemporaryRTs.Remove(rt);
			RenderTexture.ReleaseTemporary(rt);
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0017F91C File Offset: 0x0017DB1C
		public void ReleaseAll()
		{
			foreach (RenderTexture temp in this.m_TemporaryRTs)
			{
				RenderTexture.ReleaseTemporary(temp);
			}
			this.m_TemporaryRTs.Clear();
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0017F957 File Offset: 0x0017DB57
		public void Dispose()
		{
			this.ReleaseAll();
		}

		// Token: 0x04003CC5 RID: 15557
		private HashSet<RenderTexture> m_TemporaryRTs;
	}
}

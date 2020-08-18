using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D3 RID: 1235
	public static class GraphicsUtils
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0017F3F4 File Offset: 0x0017D5F4
		public static bool isLinearColorSpace
		{
			get
			{
				return QualitySettings.activeColorSpace == ColorSpace.Linear;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0017F3FE File Offset: 0x0017D5FE
		public static bool supportsDX11
		{
			get
			{
				return SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0017F410 File Offset: 0x0017D610
		public static Texture2D whiteTexture
		{
			get
			{
				if (GraphicsUtils.s_WhiteTexture != null)
				{
					return GraphicsUtils.s_WhiteTexture;
				}
				GraphicsUtils.s_WhiteTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
				GraphicsUtils.s_WhiteTexture.SetPixel(0, 0, new Color(1f, 1f, 1f, 1f));
				GraphicsUtils.s_WhiteTexture.Apply();
				return GraphicsUtils.s_WhiteTexture;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x0017F474 File Offset: 0x0017D674
		public static Mesh quad
		{
			get
			{
				if (GraphicsUtils.s_Quad != null)
				{
					return GraphicsUtils.s_Quad;
				}
				Vector3[] vertices = new Vector3[]
				{
					new Vector3(-1f, -1f, 0f),
					new Vector3(1f, 1f, 0f),
					new Vector3(1f, -1f, 0f),
					new Vector3(-1f, 1f, 0f)
				};
				Vector2[] uv = new Vector2[]
				{
					new Vector2(0f, 0f),
					new Vector2(1f, 1f),
					new Vector2(1f, 0f),
					new Vector2(0f, 1f)
				};
				int[] triangles = new int[]
				{
					0,
					1,
					2,
					1,
					0,
					3
				};
				GraphicsUtils.s_Quad = new Mesh
				{
					vertices = vertices,
					uv = uv,
					triangles = triangles
				};
				GraphicsUtils.s_Quad.RecalculateNormals();
				GraphicsUtils.s_Quad.RecalculateBounds();
				return GraphicsUtils.s_Quad;
			}
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0017F5B0 File Offset: 0x0017D7B0
		public static void Blit(Material material, int pass)
		{
			GL.PushMatrix();
			GL.LoadOrtho();
			material.SetPass(pass);
			GL.Begin(5);
			GL.TexCoord2(0f, 0f);
			GL.Vertex3(0f, 0f, 0.1f);
			GL.TexCoord2(1f, 0f);
			GL.Vertex3(1f, 0f, 0.1f);
			GL.TexCoord2(0f, 1f);
			GL.Vertex3(0f, 1f, 0.1f);
			GL.TexCoord2(1f, 1f);
			GL.Vertex3(1f, 1f, 0.1f);
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0017F66C File Offset: 0x0017D86C
		public static void ClearAndBlit(Texture source, RenderTexture destination, Material material, int pass, bool clearColor = true, bool clearDepth = false)
		{
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = destination;
			GL.Clear(false, clearColor, Color.clear);
			GL.PushMatrix();
			GL.LoadOrtho();
			material.SetTexture("_MainTex", source);
			material.SetPass(pass);
			GL.Begin(5);
			GL.TexCoord2(0f, 0f);
			GL.Vertex3(0f, 0f, 0.1f);
			GL.TexCoord2(1f, 0f);
			GL.Vertex3(1f, 0f, 0.1f);
			GL.TexCoord2(0f, 1f);
			GL.Vertex3(0f, 1f, 0.1f);
			GL.TexCoord2(1f, 1f);
			GL.Vertex3(1f, 1f, 0.1f);
			GL.End();
			GL.PopMatrix();
			RenderTexture.active = active;
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0017F750 File Offset: 0x0017D950
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				Object.Destroy(obj);
			}
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0017F761 File Offset: 0x0017D961
		public static void Dispose()
		{
			GraphicsUtils.Destroy(GraphicsUtils.s_Quad);
		}

		// Token: 0x04003CC2 RID: 15554
		private static Texture2D s_WhiteTexture;

		// Token: 0x04003CC3 RID: 15555
		private static Mesh s_Quad;
	}
}

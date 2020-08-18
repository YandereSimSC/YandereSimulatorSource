using System;
using System.Collections.Generic;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D4 RID: 1236
	public sealed class MaterialFactory : IDisposable
	{
		// Token: 0x06001F3C RID: 7996 RVA: 0x0017F76D File Offset: 0x0017D96D
		public MaterialFactory()
		{
			this.m_Materials = new Dictionary<string, Material>();
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0017F780 File Offset: 0x0017D980
		public Material Get(string shaderName)
		{
			Material material;
			if (!this.m_Materials.TryGetValue(shaderName, out material))
			{
				Shader shader = Shader.Find(shaderName);
				if (shader == null)
				{
					throw new ArgumentException(string.Format("Shader not found ({0})", shaderName));
				}
				material = new Material(shader)
				{
					name = string.Format("PostFX - {0}", shaderName.Substring(shaderName.LastIndexOf("/") + 1)),
					hideFlags = HideFlags.DontSave
				};
				this.m_Materials.Add(shaderName, material);
			}
			return material;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x0017F7FC File Offset: 0x0017D9FC
		public void Dispose()
		{
			foreach (KeyValuePair<string, Material> keyValuePair in this.m_Materials)
			{
				GraphicsUtils.Destroy(keyValuePair.Value);
			}
			this.m_Materials.Clear();
		}

		// Token: 0x04003CC4 RID: 15556
		private Dictionary<string, Material> m_Materials;
	}
}

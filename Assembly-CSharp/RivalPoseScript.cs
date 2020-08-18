using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class RivalPoseScript : MonoBehaviour
{
	// Token: 0x06000A0D RID: 2573 RVA: 0x0004F90C File Offset: 0x0004DB0C
	private void Start()
	{
		int femaleUniform = StudentGlobals.FemaleUniform;
		this.MyRenderer.sharedMesh = this.FemaleUniforms[femaleUniform];
		if (femaleUniform == 1)
		{
			this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.HairTexture;
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniform];
			return;
		}
		if (femaleUniform == 2)
		{
			this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[1].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[2].mainTexture = this.HairTexture;
			this.MyRenderer.materials[3].mainTexture = this.HairTexture;
			return;
		}
		if (femaleUniform == 3)
		{
			this.MyRenderer.materials[0].mainTexture = this.HairTexture;
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniform];
			return;
		}
		if (femaleUniform == 4)
		{
			this.MyRenderer.materials[0].mainTexture = this.HairTexture;
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniform];
			return;
		}
		if (femaleUniform == 5)
		{
			this.MyRenderer.materials[0].mainTexture = this.HairTexture;
			this.MyRenderer.materials[1].mainTexture = this.HairTexture;
			this.MyRenderer.materials[2].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[3].mainTexture = this.FemaleUniformTextures[femaleUniform];
			return;
		}
		if (femaleUniform == 6)
		{
			this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[1].mainTexture = this.FemaleUniformTextures[femaleUniform];
			this.MyRenderer.materials[2].mainTexture = this.HairTexture;
			this.MyRenderer.materials[3].mainTexture = this.HairTexture;
		}
	}

	// Token: 0x04000A08 RID: 2568
	public GameObject Character;

	// Token: 0x04000A09 RID: 2569
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04000A0A RID: 2570
	public Texture[] FemaleUniformTextures;

	// Token: 0x04000A0B RID: 2571
	public Mesh[] FemaleUniforms;

	// Token: 0x04000A0C RID: 2572
	public Texture[] TestTextures;

	// Token: 0x04000A0D RID: 2573
	public Texture HairTexture;

	// Token: 0x04000A0E RID: 2574
	public string[] AnimNames;

	// Token: 0x04000A0F RID: 2575
	public int ID = -1;
}

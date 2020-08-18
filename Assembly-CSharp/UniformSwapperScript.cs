using System;
using UnityEngine;

// Token: 0x02000435 RID: 1077
public class UniformSwapperScript : MonoBehaviour
{
	// Token: 0x06001C7A RID: 7290 RVA: 0x00154D90 File Offset: 0x00152F90
	private void Start()
	{
		int maleUniform = StudentGlobals.MaleUniform;
		this.MyRenderer.sharedMesh = this.UniformMeshes[maleUniform];
		Texture mainTexture = this.UniformTextures[maleUniform];
		if (maleUniform == 1)
		{
			this.SkinID = 0;
			this.UniformID = 1;
			this.FaceID = 2;
		}
		else if (maleUniform == 2)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (maleUniform == 3)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (maleUniform == 4)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (maleUniform == 5)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (maleUniform == 6)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		this.MyRenderer.materials[this.FaceID].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[this.SkinID].mainTexture = mainTexture;
		this.MyRenderer.materials[this.UniformID].mainTexture = mainTexture;
	}

	// Token: 0x06001C7B RID: 7291 RVA: 0x00154EAF File Offset: 0x001530AF
	private void LateUpdate()
	{
		if (this.LookTarget != null)
		{
			this.Head.LookAt(this.LookTarget);
		}
	}

	// Token: 0x040035CC RID: 13772
	public Texture[] UniformTextures;

	// Token: 0x040035CD RID: 13773
	public Mesh[] UniformMeshes;

	// Token: 0x040035CE RID: 13774
	public Texture FaceTexture;

	// Token: 0x040035CF RID: 13775
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x040035D0 RID: 13776
	public int UniformID;

	// Token: 0x040035D1 RID: 13777
	public int FaceID;

	// Token: 0x040035D2 RID: 13778
	public int SkinID;

	// Token: 0x040035D3 RID: 13779
	public Transform LookTarget;

	// Token: 0x040035D4 RID: 13780
	public Transform Head;
}

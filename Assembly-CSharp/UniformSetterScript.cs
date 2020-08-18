using System;
using UnityEngine;

// Token: 0x02000434 RID: 1076
public class UniformSetterScript : MonoBehaviour
{
	// Token: 0x06001C76 RID: 7286 RVA: 0x00154B30 File Offset: 0x00152D30
	public void Start()
	{
		if (this.MyRenderer == null)
		{
			this.MyRenderer = base.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();
		}
		if (this.Male)
		{
			this.SetMaleUniform();
		}
		else
		{
			this.SetFemaleUniform();
		}
		if (this.AttachHair)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Hair[this.HairID], base.transform.position, base.transform.rotation);
			this.Head = base.transform.Find("Character/PelvisRoot/Hips/Spine/Spine1/Spine2/Spine3/Neck/Head").transform;
			gameObject.transform.parent = this.Head;
		}
	}

	// Token: 0x06001C77 RID: 7287 RVA: 0x00154BE0 File Offset: 0x00152DE0
	public void SetMaleUniform()
	{
		this.MyRenderer.sharedMesh = this.MaleUniforms[StudentGlobals.MaleUniform];
		if (StudentGlobals.MaleUniform == 1)
		{
			this.SkinID = 0;
			this.UniformID = 1;
			this.FaceID = 2;
		}
		else if (StudentGlobals.MaleUniform == 2 || StudentGlobals.MaleUniform == 3)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (StudentGlobals.MaleUniform == 4 || StudentGlobals.MaleUniform == 5 || StudentGlobals.MaleUniform == 6)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		this.MyRenderer.materials[this.FaceID].mainTexture = this.SenpaiFace;
		this.MyRenderer.materials[this.SkinID].mainTexture = this.SenpaiSkin;
		this.MyRenderer.materials[this.UniformID].mainTexture = this.MaleUniformTextures[StudentGlobals.MaleUniform];
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x00154CD4 File Offset: 0x00152ED4
	public void SetFemaleUniform()
	{
		this.MyRenderer.sharedMesh = this.FemaleUniforms[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[1].mainTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
		if (this.StudentID == 0)
		{
			this.MyRenderer.materials[2].mainTexture = this.RyobaFace;
			return;
		}
		if (this.StudentID == 1)
		{
			this.MyRenderer.materials[2].mainTexture = this.AyanoFace;
			return;
		}
		this.MyRenderer.materials[2].mainTexture = this.OsanaFace;
	}

	// Token: 0x040035B9 RID: 13753
	public Texture[] FemaleUniformTextures;

	// Token: 0x040035BA RID: 13754
	public Texture[] MaleUniformTextures;

	// Token: 0x040035BB RID: 13755
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x040035BC RID: 13756
	public Mesh[] FemaleUniforms;

	// Token: 0x040035BD RID: 13757
	public Mesh[] MaleUniforms;

	// Token: 0x040035BE RID: 13758
	public Texture SenpaiFace;

	// Token: 0x040035BF RID: 13759
	public Texture SenpaiSkin;

	// Token: 0x040035C0 RID: 13760
	public Texture RyobaFace;

	// Token: 0x040035C1 RID: 13761
	public Texture AyanoFace;

	// Token: 0x040035C2 RID: 13762
	public Texture OsanaFace;

	// Token: 0x040035C3 RID: 13763
	public int FaceID;

	// Token: 0x040035C4 RID: 13764
	public int SkinID;

	// Token: 0x040035C5 RID: 13765
	public int UniformID;

	// Token: 0x040035C6 RID: 13766
	public int StudentID;

	// Token: 0x040035C7 RID: 13767
	public bool AttachHair;

	// Token: 0x040035C8 RID: 13768
	public bool Male;

	// Token: 0x040035C9 RID: 13769
	public Transform Head;

	// Token: 0x040035CA RID: 13770
	public GameObject[] Hair;

	// Token: 0x040035CB RID: 13771
	public int HairID;
}

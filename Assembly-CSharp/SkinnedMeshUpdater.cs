using System;
using UnityEngine;

// Token: 0x020003DE RID: 990
public class SkinnedMeshUpdater : MonoBehaviour
{
	// Token: 0x06001A96 RID: 6806 RVA: 0x00108D55 File Offset: 0x00106F55
	public void Start()
	{
		this.GlassesCheck();
	}

	// Token: 0x06001A97 RID: 6807 RVA: 0x00108D60 File Offset: 0x00106F60
	public void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.TransformEffect, this.Prompt.Yandere.Hips.position, Quaternion.identity);
			this.Prompt.Yandere.CharacterAnimation.Play(this.Prompt.Yandere.IdleAnim);
			this.Prompt.Yandere.CanMove = false;
			this.Prompt.Yandere.Egg = true;
			this.BreastR.name = "RightBreast";
			this.BreastL.name = "LeftBreast";
			this.Timer = 1f;
			this.ID++;
			if (this.ID == this.Characters.Length)
			{
				this.ID = 1;
			}
			this.Prompt.Yandere.Hairstyle = 120 + this.ID;
			this.Prompt.Yandere.UpdateHair();
			this.GlassesCheck();
			this.UpdateSkin();
		}
		if (this.Timer > 0f)
		{
			this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
			if (this.Timer == 0f)
			{
				this.Prompt.Yandere.CanMove = true;
			}
		}
	}

	// Token: 0x06001A98 RID: 6808 RVA: 0x00108EC4 File Offset: 0x001070C4
	public void UpdateSkin()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Characters[this.ID], Vector3.zero, Quaternion.identity);
		this.TempRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
		this.UpdateMeshRenderer(this.TempRenderer);
		UnityEngine.Object.Destroy(gameObject);
		this.MyRenderer.materials[0].mainTexture = this.Bodies[this.ID];
		this.MyRenderer.materials[1].mainTexture = this.Bodies[this.ID];
		this.MyRenderer.materials[2].mainTexture = this.Faces[this.ID];
	}

	// Token: 0x06001A99 RID: 6809 RVA: 0x00108F6C File Offset: 0x0010716C
	private void UpdateMeshRenderer(SkinnedMeshRenderer newMeshRenderer)
	{
		SkinnedMeshUpdater.<>c__DisplayClass16_0 CS$<>8__locals1 = new SkinnedMeshUpdater.<>c__DisplayClass16_0();
		CS$<>8__locals1.newMeshRenderer = newMeshRenderer;
		SkinnedMeshRenderer myRenderer = this.Prompt.Yandere.MyRenderer;
		myRenderer.sharedMesh = CS$<>8__locals1.newMeshRenderer.sharedMesh;
		Transform[] componentsInChildren = this.Prompt.Yandere.transform.GetComponentsInChildren<Transform>(true);
		Transform[] array = new Transform[CS$<>8__locals1.newMeshRenderer.bones.Length];
		int boneOrder;
		int boneOrder2;
		for (boneOrder = 0; boneOrder < CS$<>8__locals1.newMeshRenderer.bones.Length; boneOrder = boneOrder2 + 1)
		{
			array[boneOrder] = Array.Find<Transform>(componentsInChildren, (Transform c) => c.name == CS$<>8__locals1.newMeshRenderer.bones[boneOrder].name);
			boneOrder2 = boneOrder;
		}
		myRenderer.bones = array;
	}

	// Token: 0x06001A9A RID: 6810 RVA: 0x00109040 File Offset: 0x00107240
	private void GlassesCheck()
	{
		this.FumiGlasses.SetActive(false);
		this.NinaGlasses.SetActive(false);
		if (this.ID == 7)
		{
			this.FumiGlasses.SetActive(true);
			return;
		}
		if (this.ID == 8)
		{
			this.NinaGlasses.SetActive(true);
		}
	}

	// Token: 0x04002AA7 RID: 10919
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04002AA8 RID: 10920
	public GameObject TransformEffect;

	// Token: 0x04002AA9 RID: 10921
	public GameObject[] Characters;

	// Token: 0x04002AAA RID: 10922
	public PromptScript Prompt;

	// Token: 0x04002AAB RID: 10923
	public GameObject BreastR;

	// Token: 0x04002AAC RID: 10924
	public GameObject BreastL;

	// Token: 0x04002AAD RID: 10925
	public GameObject FumiGlasses;

	// Token: 0x04002AAE RID: 10926
	public GameObject NinaGlasses;

	// Token: 0x04002AAF RID: 10927
	private SkinnedMeshRenderer TempRenderer;

	// Token: 0x04002AB0 RID: 10928
	public Texture[] Bodies;

	// Token: 0x04002AB1 RID: 10929
	public Texture[] Faces;

	// Token: 0x04002AB2 RID: 10930
	public float Timer;

	// Token: 0x04002AB3 RID: 10931
	public int ID;
}

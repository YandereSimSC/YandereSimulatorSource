using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class RiggedAccessoryAttacher : MonoBehaviour
{
	// Token: 0x060000C6 RID: 198 RVA: 0x000111A4 File Offset: 0x0000F3A4
	public void Start()
	{
		this.Initialized = true;
		if (this.PantyID == 99)
		{
			this.PantyID = PlayerGlobals.PantiesEquipped;
		}
		if (this.CookingClub)
		{
			if (this.Student.Male)
			{
				this.accessory = GameObject.Find("MaleCookingApron");
			}
			else
			{
				this.accessory = GameObject.Find("FemaleCookingApron");
			}
		}
		else if (this.ArtClub)
		{
			if (this.Student.Male)
			{
				this.accessory = GameObject.Find("PainterApron");
				this.accessoryMaterials = this.painterMaterials;
			}
			else
			{
				this.accessory = GameObject.Find("PainterApronFemale");
				this.accessoryMaterials = this.painterMaterials;
			}
		}
		else if (this.Gentle)
		{
			this.accessory = GameObject.Find("GentleEyes");
			this.accessoryMaterials = this.defaultMaterials;
		}
		else
		{
			if (this.ID == 1)
			{
				this.accessory = GameObject.Find("LabcoatFemale");
			}
			if (this.ID == 2)
			{
				this.accessory = GameObject.Find("LabcoatMale");
			}
			if (this.ID == 26)
			{
				this.accessory = GameObject.Find("OkaBlazer");
				this.accessoryMaterials = this.okaMaterials;
			}
			if (this.ID == 100)
			{
				this.accessory = this.Panties[this.PantyID];
				this.accessoryMaterials[0] = this.PantyMaterials[this.PantyID];
			}
		}
		this.AttachAccessory();
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x0001131C File Offset: 0x0000F51C
	public void AttachAccessory()
	{
		this.AddLimb(this.accessory, this.root, this.accessoryMaterials);
		if (this.ID == 100)
		{
			this.newRenderer.updateWhenOffscreen = true;
		}
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x0001134C File Offset: 0x0000F54C
	public void RemoveAccessory()
	{
		UnityEngine.Object.Destroy(this.newRenderer);
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x0001135C File Offset: 0x0000F55C
	private void AddLimb(GameObject bonedObj, GameObject rootObj, Material[] bonedObjMaterials)
	{
		foreach (SkinnedMeshRenderer thisRenderer in bonedObj.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			this.ProcessBonedObject(thisRenderer, rootObj, bonedObjMaterials);
		}
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00011390 File Offset: 0x0000F590
	private void ProcessBonedObject(SkinnedMeshRenderer thisRenderer, GameObject rootObj, Material[] thisRendererMaterials)
	{
		GameObject gameObject = new GameObject(thisRenderer.gameObject.name);
		gameObject.transform.parent = rootObj.transform;
		gameObject.layer = rootObj.layer;
		gameObject.AddComponent<SkinnedMeshRenderer>();
		this.newRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
		Transform[] array = new Transform[thisRenderer.bones.Length];
		for (int i = 0; i < thisRenderer.bones.Length; i++)
		{
			array[i] = this.FindChildByName(thisRenderer.bones[i].name, rootObj.transform);
		}
		this.newRenderer.bones = array;
		this.newRenderer.sharedMesh = thisRenderer.sharedMesh;
		if (thisRendererMaterials == null)
		{
			this.newRenderer.materials = thisRenderer.sharedMaterials;
		}
		else
		{
			this.newRenderer.materials = thisRendererMaterials;
		}
		if (this.UpdateBounds)
		{
			this.newRenderer.updateWhenOffscreen = true;
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00011470 File Offset: 0x0000F670
	private Transform FindChildByName(string thisName, Transform thisGameObj)
	{
		if (thisGameObj.name == thisName)
		{
			return thisGameObj.transform;
		}
		foreach (object obj in thisGameObj)
		{
			Transform thisGameObj2 = (Transform)obj;
			Transform transform = this.FindChildByName(thisName, thisGameObj2);
			if (transform)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x04000257 RID: 599
	public StudentScript Student;

	// Token: 0x04000258 RID: 600
	public GameObject root;

	// Token: 0x04000259 RID: 601
	public GameObject accessory;

	// Token: 0x0400025A RID: 602
	public Material[] accessoryMaterials;

	// Token: 0x0400025B RID: 603
	public Material[] okaMaterials;

	// Token: 0x0400025C RID: 604
	public Material[] ribaruMaterials;

	// Token: 0x0400025D RID: 605
	public Material[] defaultMaterials;

	// Token: 0x0400025E RID: 606
	public Material[] painterMaterials;

	// Token: 0x0400025F RID: 607
	public Material[] painterMaterialsFlipped;

	// Token: 0x04000260 RID: 608
	public GameObject[] Panties;

	// Token: 0x04000261 RID: 609
	public Material[] PantyMaterials;

	// Token: 0x04000262 RID: 610
	public SkinnedMeshRenderer newRenderer;

	// Token: 0x04000263 RID: 611
	public bool UpdateBounds;

	// Token: 0x04000264 RID: 612
	public bool Initialized;

	// Token: 0x04000265 RID: 613
	public bool CookingClub;

	// Token: 0x04000266 RID: 614
	public bool ScienceClub;

	// Token: 0x04000267 RID: 615
	public bool ArtClub;

	// Token: 0x04000268 RID: 616
	public bool Gentle;

	// Token: 0x04000269 RID: 617
	public int PantyID;

	// Token: 0x0400026A RID: 618
	public int ID;
}

using System;
using UnityEngine;

// Token: 0x0200036F RID: 879
public class PortraitScript : MonoBehaviour
{
	// Token: 0x0600191D RID: 6429 RVA: 0x000ED4D0 File Offset: 0x000EB6D0
	private void Start()
	{
		this.StudentObject[1].SetActive(false);
		this.StudentObject[2].SetActive(false);
		this.Selected = 1;
		this.UpdateHair();
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x000ED4FC File Offset: 0x000EB6FC
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.StudentObject[0].SetActive(true);
			this.StudentObject[1].SetActive(false);
			this.StudentObject[2].SetActive(false);
			this.Selected = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.StudentObject[0].SetActive(false);
			this.StudentObject[1].SetActive(true);
			this.StudentObject[2].SetActive(false);
			this.Selected = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.StudentObject[0].SetActive(false);
			this.StudentObject[1].SetActive(false);
			this.StudentObject[2].SetActive(true);
			this.Selected = 3;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.CurrentHair++;
			if (this.CurrentHair > this.HairSet1.Length - 1)
			{
				this.CurrentHair = 0;
			}
			this.UpdateHair();
		}
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x000ED5F4 File Offset: 0x000EB7F4
	private void UpdateHair()
	{
		Texture mainTexture = this.HairSet2[this.CurrentHair];
		this.Renderer1.materials[0].mainTexture = mainTexture;
		this.Renderer1.materials[3].mainTexture = mainTexture;
		this.Renderer2.materials[2].mainTexture = mainTexture;
		this.Renderer2.materials[3].mainTexture = mainTexture;
		this.Renderer3.materials[0].mainTexture = mainTexture;
		this.Renderer3.materials[1].mainTexture = mainTexture;
	}

	// Token: 0x04002609 RID: 9737
	public GameObject[] StudentObject;

	// Token: 0x0400260A RID: 9738
	public Renderer Renderer1;

	// Token: 0x0400260B RID: 9739
	public Renderer Renderer2;

	// Token: 0x0400260C RID: 9740
	public Renderer Renderer3;

	// Token: 0x0400260D RID: 9741
	public Texture[] HairSet1;

	// Token: 0x0400260E RID: 9742
	public Texture[] HairSet2;

	// Token: 0x0400260F RID: 9743
	public Texture[] HairSet3;

	// Token: 0x04002610 RID: 9744
	public int Selected;

	// Token: 0x04002611 RID: 9745
	public int CurrentHair;
}

using System;
using UnityEngine;

// Token: 0x020003E1 RID: 993
public class SmartphoneScript : MonoBehaviour
{
	// Token: 0x06001AA1 RID: 6817 RVA: 0x001099C8 File Offset: 0x00107BC8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.CrushingPhone = true;
			this.Prompt.Yandere.PhoneToCrush = this;
			this.Prompt.Yandere.CanMove = false;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, base.transform.position, Quaternion.identity);
			this.PhoneCrushingSpot = gameObject.transform;
			this.PhoneCrushingSpot.position = new Vector3(this.PhoneCrushingSpot.position.x, this.Prompt.Yandere.transform.position.y, this.PhoneCrushingSpot.position.z);
			this.PhoneCrushingSpot.LookAt(this.Prompt.Yandere.transform.position);
			this.PhoneCrushingSpot.Translate(Vector3.forward * 0.5f);
		}
	}

	// Token: 0x04002ACC RID: 10956
	public Transform PhoneCrushingSpot;

	// Token: 0x04002ACD RID: 10957
	public GameObject EmptyGameObject;

	// Token: 0x04002ACE RID: 10958
	public Texture SmashedTexture;

	// Token: 0x04002ACF RID: 10959
	public GameObject PhoneSmash;

	// Token: 0x04002AD0 RID: 10960
	public Renderer MyRenderer;

	// Token: 0x04002AD1 RID: 10961
	public PromptScript Prompt;

	// Token: 0x04002AD2 RID: 10962
	public MeshFilter MyMesh;

	// Token: 0x04002AD3 RID: 10963
	public Mesh SmashedMesh;
}

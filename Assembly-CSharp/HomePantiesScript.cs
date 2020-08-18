using System;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class HomePantiesScript : MonoBehaviour
{
	// Token: 0x0600172D RID: 5933 RVA: 0x000C61F0 File Offset: 0x000C43F0
	private void Start()
	{
		if (this.ID > 0 && !CollectibleGlobals.GetPantyPurchased(this.ID))
		{
			this.MyRenderer.material = this.Unselectable;
			this.MyRenderer.material.color = new Color(0f, 0f, 0f, 0.5f);
		}
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x000C6250 File Offset: 0x000C4450
	private void Update()
	{
		float y = (this.PantyChanger.Selected == this.ID) ? (base.transform.eulerAngles.y + Time.deltaTime * this.RotationSpeed) : 0f;
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
	}

	// Token: 0x04001FAD RID: 8109
	public HomePantyChangerScript PantyChanger;

	// Token: 0x04001FAE RID: 8110
	public float RotationSpeed;

	// Token: 0x04001FAF RID: 8111
	public Material Unselectable;

	// Token: 0x04001FB0 RID: 8112
	public Renderer MyRenderer;

	// Token: 0x04001FB1 RID: 8113
	public int ID;
}

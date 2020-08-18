using System;
using UnityEngine;

// Token: 0x020003DB RID: 987
public class SimpleDetectClickScript : MonoBehaviour
{
	// Token: 0x06001A8E RID: 6798 RVA: 0x00108758 File Offset: 0x00106958
	private void Update()
	{
		RaycastHit raycastHit;
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 100f) && raycastHit.collider == this.MyCollider)
		{
			this.Clicked = true;
		}
	}

	// Token: 0x04002A99 RID: 10905
	public InventoryItemScript InventoryItem;

	// Token: 0x04002A9A RID: 10906
	public Collider MyCollider;

	// Token: 0x04002A9B RID: 10907
	public bool Clicked;
}

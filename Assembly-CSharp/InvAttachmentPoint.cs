using System;
using UnityEngine;

// Token: 0x02000027 RID: 39
[AddComponentMenu("NGUI/Examples/Item Attachment Point")]
public class InvAttachmentPoint : MonoBehaviour
{
	// Token: 0x060000EE RID: 238 RVA: 0x00011F68 File Offset: 0x00010168
	public GameObject Attach(GameObject prefab)
	{
		if (this.mPrefab != prefab)
		{
			this.mPrefab = prefab;
			if (this.mChild != null)
			{
				UnityEngine.Object.Destroy(this.mChild);
			}
			if (this.mPrefab != null)
			{
				Transform transform = base.transform;
				this.mChild = UnityEngine.Object.Instantiate<GameObject>(this.mPrefab, transform.position, transform.rotation);
				Transform transform2 = this.mChild.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
			}
		}
		return this.mChild;
	}

	// Token: 0x04000289 RID: 649
	public InvBaseItem.Slot slot;

	// Token: 0x0400028A RID: 650
	private GameObject mPrefab;

	// Token: 0x0400028B RID: 651
	private GameObject mChild;
}

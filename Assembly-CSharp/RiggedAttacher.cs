using System;
using UnityEngine;

// Token: 0x02000498 RID: 1176
public class RiggedAttacher : MonoBehaviour
{
	// Token: 0x06001E12 RID: 7698 RVA: 0x001783D6 File Offset: 0x001765D6
	private void Start()
	{
		this.Attaching(this.BasePelvisRoot, this.AttachmentPelvisRoot);
	}

	// Token: 0x06001E13 RID: 7699 RVA: 0x001783EC File Offset: 0x001765EC
	private void Attaching(Transform Base, Transform Attachment)
	{
		Attachment.transform.SetParent(Base);
		Base.localEulerAngles = Vector3.zero;
		Base.localPosition = Vector3.zero;
		Transform[] componentsInChildren = Base.GetComponentsInChildren<Transform>();
		foreach (Transform transform in Attachment.GetComponentsInChildren<Transform>())
		{
			foreach (Transform transform2 in componentsInChildren)
			{
				if (transform.name == transform2.name)
				{
					transform.SetParent(transform2);
					transform.localEulerAngles = Vector3.zero;
					transform.localPosition = Vector3.zero;
				}
			}
		}
	}

	// Token: 0x04003BE6 RID: 15334
	public Transform BasePelvisRoot;

	// Token: 0x04003BE7 RID: 15335
	public Transform AttachmentPelvisRoot;
}

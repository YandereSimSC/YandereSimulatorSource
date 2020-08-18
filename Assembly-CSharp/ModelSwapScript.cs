using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000496 RID: 1174
public class ModelSwapScript : MonoBehaviour
{
	// Token: 0x06001E0C RID: 7692 RVA: 0x00178339 File Offset: 0x00176539
	public void Update()
	{
		Input.GetKeyDown("z");
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x00178346 File Offset: 0x00176546
	public void Attach(GameObject Attachment, bool Inactives)
	{
		base.StartCoroutine(this.Attach_Threat(this.PelvisRoot, Attachment, Inactives));
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x0017835D File Offset: 0x0017655D
	public IEnumerator Attach_Threat(Transform PelvisRoot, GameObject Attachment, bool Inactives)
	{
		Attachment.transform.SetParent(PelvisRoot);
		PelvisRoot.localEulerAngles = Vector3.zero;
		PelvisRoot.localPosition = Vector3.zero;
		Transform[] componentsInChildren = PelvisRoot.GetComponentsInChildren<Transform>(Inactives);
		foreach (Transform transform in Attachment.GetComponentsInChildren<Transform>(Inactives))
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
		yield return null;
		yield break;
	}

	// Token: 0x04003BE3 RID: 15331
	public Transform PelvisRoot;

	// Token: 0x04003BE4 RID: 15332
	public GameObject Attachment;
}

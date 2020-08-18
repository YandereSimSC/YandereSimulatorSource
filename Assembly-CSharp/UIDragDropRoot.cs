using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
[AddComponentMenu("NGUI/Interaction/Drag and Drop Root")]
public class UIDragDropRoot : MonoBehaviour
{
	// Token: 0x060001D0 RID: 464 RVA: 0x000165C4 File Offset: 0x000147C4
	private void OnEnable()
	{
		UIDragDropRoot.root = base.transform;
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x000165D1 File Offset: 0x000147D1
	private void OnDisable()
	{
		if (UIDragDropRoot.root == base.transform)
		{
			UIDragDropRoot.root = null;
		}
	}

	// Token: 0x04000349 RID: 841
	public static Transform root;
}

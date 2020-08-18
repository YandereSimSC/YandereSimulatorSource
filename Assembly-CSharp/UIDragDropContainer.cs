using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
[AddComponentMenu("NGUI/Interaction/Drag and Drop Container")]
public class UIDragDropContainer : MonoBehaviour
{
	// Token: 0x060001B5 RID: 437 RVA: 0x00015AD7 File Offset: 0x00013CD7
	protected virtual void Start()
	{
		if (this.reparentTarget == null)
		{
			this.reparentTarget = base.transform;
		}
	}

	// Token: 0x04000334 RID: 820
	public Transform reparentTarget;
}

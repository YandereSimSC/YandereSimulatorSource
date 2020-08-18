using System;
using UnityEngine;

// Token: 0x020003A8 RID: 936
public class RuntimeAnimRetarget : MonoBehaviour
{
	// Token: 0x060019D7 RID: 6615 RVA: 0x000FC912 File Offset: 0x000FAB12
	private void Start()
	{
		Debug.Log(this.Source.name);
		this.SourceSkelNodes = this.Source.GetComponentsInChildren<Component>();
		this.TargetSkelNodes = this.Target.GetComponentsInChildren<Component>();
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x000FC948 File Offset: 0x000FAB48
	private void LateUpdate()
	{
		this.TargetSkelNodes[1].transform.localPosition = this.SourceSkelNodes[1].transform.localPosition;
		for (int i = 0; i < 154; i++)
		{
			this.TargetSkelNodes[i].transform.localRotation = this.SourceSkelNodes[i].transform.localRotation;
		}
	}

	// Token: 0x0400286E RID: 10350
	public GameObject Source;

	// Token: 0x0400286F RID: 10351
	public GameObject Target;

	// Token: 0x04002870 RID: 10352
	private Component[] SourceSkelNodes;

	// Token: 0x04002871 RID: 10353
	private Component[] TargetSkelNodes;
}

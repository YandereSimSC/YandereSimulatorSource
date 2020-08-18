using System;
using UnityEngine;

// Token: 0x02000345 RID: 837
public class NurseScript : MonoBehaviour
{
	// Token: 0x06001879 RID: 6265 RVA: 0x000DFC6A File Offset: 0x000DDE6A
	private void Awake()
	{
		Animation component = this.Character.GetComponent<Animation>();
		component["f02_noBlink_00"].layer = 1;
		component.Blend("f02_noBlink_00");
	}

	// Token: 0x0600187A RID: 6266 RVA: 0x000DFC92 File Offset: 0x000DDE92
	private void LateUpdate()
	{
		this.SkirtCenter.localEulerAngles = new Vector3(-15f, this.SkirtCenter.localEulerAngles.y, this.SkirtCenter.localEulerAngles.z);
	}

	// Token: 0x040023F0 RID: 9200
	public GameObject Character;

	// Token: 0x040023F1 RID: 9201
	public Transform SkirtCenter;
}

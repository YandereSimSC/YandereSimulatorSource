using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class BuildingDestructionScript : MonoBehaviour
{
	// Token: 0x06000A99 RID: 2713 RVA: 0x00057BC8 File Offset: 0x00055DC8
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Phase++;
			this.Sink = true;
		}
		if (this.Sink)
		{
			if (this.Phase == 1)
			{
				base.transform.position = new Vector3(UnityEngine.Random.Range(-1f, 1f), base.transform.position.y - Time.deltaTime * 10f, UnityEngine.Random.Range(-19f, -21f));
				return;
			}
			if (this.NewSchool.position.y != 0f)
			{
				this.NewSchool.position = new Vector3(this.NewSchool.position.x, Mathf.MoveTowards(this.NewSchool.position.y, 0f, Time.deltaTime * 10f), this.NewSchool.position.z);
				base.transform.position = new Vector3(UnityEngine.Random.Range(-1f, 1f), base.transform.position.y, UnityEngine.Random.Range(13f, 15f));
				return;
			}
			base.transform.position = new Vector3(0f, base.transform.position.y, 14f);
		}
	}

	// Token: 0x04000B33 RID: 2867
	public Transform NewSchool;

	// Token: 0x04000B34 RID: 2868
	public bool Sink;

	// Token: 0x04000B35 RID: 2869
	public int Phase;
}

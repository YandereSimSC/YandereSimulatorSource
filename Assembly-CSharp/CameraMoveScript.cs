using System;
using UnityEngine;

// Token: 0x0200049B RID: 1179
public class CameraMoveScript : MonoBehaviour
{
	// Token: 0x06001E1B RID: 7707 RVA: 0x00178C4B File Offset: 0x00176E4B
	private void Start()
	{
		base.transform.position = this.StartPos.position;
		base.transform.rotation = this.StartPos.rotation;
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x00178C7C File Offset: 0x00176E7C
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Begin = true;
		}
		if (this.Begin)
		{
			this.Timer += Time.deltaTime * this.Speed;
			if (this.Timer > 0.1f)
			{
				this.OpenDoors = true;
				if (this.LeftDoor != null)
				{
					this.LeftDoor.transform.localPosition = new Vector3(Mathf.Lerp(this.LeftDoor.transform.localPosition.x, 1f, Time.deltaTime), this.LeftDoor.transform.localPosition.y, this.LeftDoor.transform.localPosition.z);
					this.RightDoor.transform.localPosition = new Vector3(Mathf.Lerp(this.RightDoor.transform.localPosition.x, -1f, Time.deltaTime), this.RightDoor.transform.localPosition.y, this.RightDoor.transform.localPosition.z);
				}
			}
			base.transform.position = Vector3.Lerp(base.transform.position, this.EndPos.position, Time.deltaTime * this.Timer);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.EndPos.rotation, Time.deltaTime * this.Timer);
		}
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x00178E0F File Offset: 0x0017700F
	private void LateUpdate()
	{
		if (this.Target != null)
		{
			base.transform.LookAt(this.Target);
		}
	}

	// Token: 0x04003BF0 RID: 15344
	public Transform StartPos;

	// Token: 0x04003BF1 RID: 15345
	public Transform EndPos;

	// Token: 0x04003BF2 RID: 15346
	public Transform RightDoor;

	// Token: 0x04003BF3 RID: 15347
	public Transform LeftDoor;

	// Token: 0x04003BF4 RID: 15348
	public Transform Target;

	// Token: 0x04003BF5 RID: 15349
	public bool OpenDoors;

	// Token: 0x04003BF6 RID: 15350
	public bool Begin;

	// Token: 0x04003BF7 RID: 15351
	public float Speed;

	// Token: 0x04003BF8 RID: 15352
	public float Timer;
}

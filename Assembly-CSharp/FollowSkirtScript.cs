using System;
using UnityEngine;

// Token: 0x020002A4 RID: 676
public class FollowSkirtScript : MonoBehaviour
{
	// Token: 0x0600140E RID: 5134 RVA: 0x000AFA38 File Offset: 0x000ADC38
	private void LateUpdate()
	{
		this.SkirtHips.position = this.TargetSkirtHips.position;
		for (int i = 0; i < 3; i++)
		{
			this.SkirtFront[i].position = this.TargetSkirtFront[i].position;
			this.SkirtFront[i].rotation = this.TargetSkirtFront[i].rotation;
			this.SkirtBack[i].position = this.TargetSkirtBack[i].position;
			this.SkirtBack[i].rotation = this.TargetSkirtBack[i].rotation;
			this.SkirtRight[i].position = this.TargetSkirtRight[i].position;
			this.SkirtRight[i].rotation = this.TargetSkirtRight[i].rotation;
			this.SkirtLeft[i].position = this.TargetSkirtLeft[i].position;
			this.SkirtLeft[i].rotation = this.TargetSkirtLeft[i].rotation;
		}
	}

	// Token: 0x04001C3C RID: 7228
	public Transform[] TargetSkirtFront;

	// Token: 0x04001C3D RID: 7229
	public Transform[] TargetSkirtBack;

	// Token: 0x04001C3E RID: 7230
	public Transform[] TargetSkirtRight;

	// Token: 0x04001C3F RID: 7231
	public Transform[] TargetSkirtLeft;

	// Token: 0x04001C40 RID: 7232
	public Transform[] SkirtFront;

	// Token: 0x04001C41 RID: 7233
	public Transform[] SkirtBack;

	// Token: 0x04001C42 RID: 7234
	public Transform[] SkirtRight;

	// Token: 0x04001C43 RID: 7235
	public Transform[] SkirtLeft;

	// Token: 0x04001C44 RID: 7236
	public Transform TargetSkirtHips;

	// Token: 0x04001C45 RID: 7237
	public Transform SkirtHips;
}

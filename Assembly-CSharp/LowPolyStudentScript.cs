using System;
using UnityEngine;

// Token: 0x02000324 RID: 804
public class LowPolyStudentScript : MonoBehaviour
{
	// Token: 0x060017FF RID: 6143 RVA: 0x000D4AAC File Offset: 0x000D2CAC
	private void Update()
	{
		if ((float)this.Student.StudentManager.LowDetailThreshold > 0f)
		{
			if (this.Student.Prompt.DistanceSqr > (float)this.Student.StudentManager.LowDetailThreshold)
			{
				if (!this.MyMesh.enabled)
				{
					this.Student.MyRenderer.enabled = false;
					this.MyMesh.enabled = true;
					return;
				}
			}
			else if (this.MyMesh.enabled)
			{
				this.Student.MyRenderer.enabled = true;
				this.MyMesh.enabled = false;
				return;
			}
		}
		else if (this.MyMesh.enabled)
		{
			this.Student.MyRenderer.enabled = true;
			this.MyMesh.enabled = false;
		}
	}

	// Token: 0x0400229E RID: 8862
	public StudentScript Student;

	// Token: 0x0400229F RID: 8863
	public Renderer TeacherMesh;

	// Token: 0x040022A0 RID: 8864
	public Renderer MyMesh;
}

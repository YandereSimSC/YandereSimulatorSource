using System;
using UnityEngine;

// Token: 0x020003CC RID: 972
public class SciFiTerminalScript : MonoBehaviour
{
	// Token: 0x06001A4C RID: 6732 RVA: 0x00101936 File Offset: 0x000FFB36
	private void Start()
	{
		if (this.Student.StudentID != 65)
		{
			base.enabled = false;
			return;
		}
		this.RobotArms = this.Student.StudentManager.RobotArms;
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x00101968 File Offset: 0x000FFB68
	private void Update()
	{
		if (this.RobotArms != null)
		{
			if ((double)Vector3.Distance(this.RobotArms.TerminalTarget.position, base.transform.position) < 0.3 || (double)Vector3.Distance(this.RobotArms.TerminalTarget.position, this.OtherFinger.position) < 0.3)
			{
				if (!this.Updated)
				{
					this.Updated = true;
					if (!this.RobotArms.On[0])
					{
						this.RobotArms.ActivateArms();
						return;
					}
					this.RobotArms.ToggleWork();
					return;
				}
			}
			else
			{
				this.Updated = false;
			}
		}
	}

	// Token: 0x040029AA RID: 10666
	public StudentScript Student;

	// Token: 0x040029AB RID: 10667
	public RobotArmScript RobotArms;

	// Token: 0x040029AC RID: 10668
	public Transform OtherFinger;

	// Token: 0x040029AD RID: 10669
	public bool Updated;
}

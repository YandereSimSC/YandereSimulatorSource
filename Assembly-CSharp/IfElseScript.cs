using System;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class IfElseScript : MonoBehaviour
{
	// Token: 0x06001765 RID: 5989 RVA: 0x000C9B7C File Offset: 0x000C7D7C
	private void Start()
	{
		this.SwitchCase();
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000C9B84 File Offset: 0x000C7D84
	private void IfElse()
	{
		if (this.ID == 1)
		{
			this.Day = "Monday";
			return;
		}
		if (this.ID == 2)
		{
			this.Day = "Tuesday";
			return;
		}
		if (this.ID == 3)
		{
			this.Day = "Wednesday";
			return;
		}
		if (this.ID == 4)
		{
			this.Day = "Thursday";
			return;
		}
		if (this.ID == 5)
		{
			this.Day = "Friday";
			return;
		}
		if (this.ID == 6)
		{
			this.Day = "Saturday";
			return;
		}
		if (this.ID == 7)
		{
			this.Day = "Sunday";
		}
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x000C9C24 File Offset: 0x000C7E24
	private void SwitchCase()
	{
		switch (this.ID)
		{
		case 1:
			this.Day = "Monday";
			return;
		case 2:
			this.Day = "Tuesday";
			return;
		case 3:
			this.Day = "Wednesday";
			return;
		case 4:
			this.Day = "Thursday";
			return;
		case 5:
			this.Day = "Friday";
			return;
		case 6:
			this.Day = "Saturday";
			return;
		case 7:
			this.Day = "Sunday";
			return;
		default:
			return;
		}
	}

	// Token: 0x04002073 RID: 8307
	public int ID;

	// Token: 0x04002074 RID: 8308
	public string Day;
}

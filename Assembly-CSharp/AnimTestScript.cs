using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class AnimTestScript : MonoBehaviour
{
	// Token: 0x060009F2 RID: 2546 RVA: 0x0004EC67 File Offset: 0x0004CE67
	private void Start()
	{
		Time.timeScale = 1f;
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0004EC74 File Offset: 0x0004CE74
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.ID++;
			if (this.ID > 4)
			{
				this.ID = 1;
			}
		}
		if (this.ID == 1)
		{
			this.CharacterB.transform.eulerAngles = new Vector3(0f, -90f, 0f);
			this.CharacterA.Play("f02_weightHighSanityA_00");
			this.CharacterB.Play("f02_weightHighSanityB_00");
			return;
		}
		if (this.ID == 2)
		{
			this.CharacterA.Play("f02_weightMedSanityA_00");
			this.CharacterB.Play("f02_weightMedSanityB_00");
			return;
		}
		if (this.ID == 3)
		{
			this.CharacterA.Play("f02_weightLowSanityA_00");
			this.CharacterB.Play("f02_weightLowSanityB_00");
			return;
		}
		if (this.ID == 4)
		{
			this.CharacterB.transform.eulerAngles = new Vector3(0f, 90f, 0f);
			this.CharacterA.Play("f02_weightStealthA_00");
			this.CharacterB.Play("f02_weightStealthB_00");
		}
	}

	// Token: 0x040009C6 RID: 2502
	public Animation CharacterA;

	// Token: 0x040009C7 RID: 2503
	public Animation CharacterB;

	// Token: 0x040009C8 RID: 2504
	public int ID;
}

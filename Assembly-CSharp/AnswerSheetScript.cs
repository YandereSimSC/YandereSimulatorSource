using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class AnswerSheetScript : MonoBehaviour
{
	// Token: 0x060009FD RID: 2557 RVA: 0x0004EED3 File Offset: 0x0004D0D3
	private void Start()
	{
		this.OriginalMesh = this.MyMesh.mesh;
		if (DateGlobals.Weekday != DayOfWeek.Friday)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0004EF08 File Offset: 0x0004D108
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Phase == 1)
			{
				SchemeGlobals.SetSchemeStage(5, 5);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.AnswerSheet = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.DoorGap.Prompt.enabled = true;
				this.MyMesh.mesh = null;
				this.Phase++;
				return;
			}
			SchemeGlobals.SetSchemeStage(5, 8);
			this.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.AnswerSheet = false;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.MyMesh.mesh = this.OriginalMesh;
			this.Phase++;
		}
	}

	// Token: 0x040009D7 RID: 2519
	public SchemesScript Schemes;

	// Token: 0x040009D8 RID: 2520
	public DoorGapScript DoorGap;

	// Token: 0x040009D9 RID: 2521
	public PromptScript Prompt;

	// Token: 0x040009DA RID: 2522
	public ClockScript Clock;

	// Token: 0x040009DB RID: 2523
	public Mesh OriginalMesh;

	// Token: 0x040009DC RID: 2524
	public MeshFilter MyMesh;

	// Token: 0x040009DD RID: 2525
	public int Phase = 1;
}

using System;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class PanelScript : MonoBehaviour
{
	// Token: 0x0600189D RID: 6301 RVA: 0x000E112C File Offset: 0x000DF32C
	private void Update()
	{
		if (this.Player.position.z > this.StairsZ || this.Player.position.z < -this.StairsZ)
		{
			this.Floor = "Stairs";
		}
		else if (this.Player.position.y < this.Floor1Height)
		{
			this.Floor = "First Floor";
		}
		else if (this.Player.position.y > this.Floor1Height && this.Player.position.y < this.Floor2Height)
		{
			this.Floor = "Second Floor";
		}
		else if (this.Player.position.y > this.Floor2Height && this.Player.position.y < this.Floor3Height)
		{
			this.Floor = "Third Floor";
		}
		else
		{
			this.Floor = "Rooftop";
		}
		if (this.Player.position.z < this.PracticeBuildingZ)
		{
			this.BuildingLabel.text = "Practice Building, " + this.Floor;
		}
		else
		{
			this.BuildingLabel.text = "Classroom Building, " + this.Floor;
		}
		this.DoorBox.Show = false;
	}

	// Token: 0x0400243F RID: 9279
	public UILabel BuildingLabel;

	// Token: 0x04002440 RID: 9280
	public DoorBoxScript DoorBox;

	// Token: 0x04002441 RID: 9281
	public Transform Player;

	// Token: 0x04002442 RID: 9282
	public string Floor = string.Empty;

	// Token: 0x04002443 RID: 9283
	public float PracticeBuildingZ;

	// Token: 0x04002444 RID: 9284
	public float StairsZ;

	// Token: 0x04002445 RID: 9285
	public float Floor1Height;

	// Token: 0x04002446 RID: 9286
	public float Floor2Height;

	// Token: 0x04002447 RID: 9287
	public float Floor3Height;
}

using System;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class CleaningManagerScript : MonoBehaviour
{
	// Token: 0x0600124D RID: 4685 RVA: 0x00082490 File Offset: 0x00080690
	private void Start()
	{
		if (SchoolGlobals.RoofFence)
		{
			for (int i = 1; i < this.ClappingSpots.Length; i++)
			{
				this.ClappingSpots[i].transform.position = new Vector3(this.ClappingSpots[i].transform.position.x, this.ClappingSpots[i].transform.position.y, this.ClappingSpots[i].transform.position.z + 0.5f);
			}
		}
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x0008251C File Offset: 0x0008071C
	public void GetRole(int StudentID)
	{
		switch (StudentID)
		{
		case 1:
			this.Role = 4;
			this.Spot = this.Toilets[0];
			return;
		case 2:
			this.Role = 1;
			this.Spot = this.Windows[4];
			return;
		case 3:
			this.Role = 2;
			this.Spot = this.Desks[4];
			return;
		case 4:
			this.Role = 3;
			this.Spot = this.Floors[4];
			return;
		case 5:
			this.Role = 5;
			this.Spot = this.Rooftops[4];
			return;
		case 6:
			this.Role = 3;
			this.Spot = this.Floors[12];
			return;
		case 7:
			this.Role = 3;
			this.Spot = this.Floors[13];
			return;
		case 8:
			this.Role = 3;
			this.Spot = this.Floors[14];
			return;
		case 9:
			this.Role = 3;
			this.Spot = this.Floors[15];
			return;
		case 10:
			if (this.StudentManager.Students[11] != null)
			{
				this.Role = 3;
				this.Spot = this.StudentManager.Students[11].transform;
				return;
			}
			break;
		case 11:
			this.Role = 4;
			this.Spot = this.Toilets[0];
			return;
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
		case 17:
		case 18:
		case 19:
		case 20:
			break;
		case 21:
			this.Role = 1;
			this.Spot = this.Windows[6];
			return;
		case 22:
			this.Role = 1;
			this.Spot = this.Windows[5];
			return;
		case 23:
			this.Role = 1;
			this.Spot = this.Windows[3];
			return;
		case 24:
			this.Role = 1;
			this.Spot = this.Windows[2];
			return;
		case 25:
			this.Role = 1;
			this.Spot = this.Windows[1];
			return;
		case 26:
			this.Role = 2;
			this.Spot = this.Desks[6];
			return;
		case 27:
			this.Role = 2;
			this.Spot = this.Desks[5];
			return;
		case 28:
			this.Role = 2;
			this.Spot = this.Desks[3];
			return;
		case 29:
			this.Role = 2;
			this.Spot = this.Desks[2];
			return;
		case 30:
			this.Role = 2;
			this.Spot = this.Desks[1];
			return;
		case 31:
			this.Role = 3;
			this.Spot = this.Floors[6];
			return;
		case 32:
			this.Role = 3;
			this.Spot = this.Floors[5];
			return;
		case 33:
			this.Role = 3;
			this.Spot = this.Floors[3];
			return;
		case 34:
			this.Role = 3;
			this.Spot = this.Floors[2];
			return;
		case 35:
			this.Role = 3;
			this.Spot = this.Floors[1];
			return;
		case 36:
			this.Role = 3;
			this.Spot = this.Floors[7];
			return;
		case 37:
			this.Role = 3;
			this.Spot = this.Floors[8];
			return;
		case 38:
			this.Role = 3;
			this.Spot = this.Floors[9];
			return;
		case 39:
			this.Role = 3;
			this.Spot = this.Floors[10];
			return;
		case 40:
			this.Role = 3;
			this.Spot = this.Floors[11];
			return;
		case 41:
			this.Role = 5;
			this.Spot = this.Rooftops[6];
			return;
		case 42:
			this.Role = 5;
			this.Spot = this.Rooftops[5];
			return;
		case 43:
			this.Role = 5;
			this.Spot = this.Rooftops[3];
			return;
		case 44:
			this.Role = 5;
			this.Spot = this.Rooftops[2];
			return;
		case 45:
			this.Role = 5;
			this.Spot = this.Rooftops[1];
			return;
		case 46:
			this.Role = 4;
			this.Spot = this.Toilets[1];
			return;
		case 47:
			this.Role = 4;
			this.Spot = this.Toilets[2];
			return;
		case 48:
			this.Role = 4;
			this.Spot = this.Toilets[3];
			return;
		case 49:
			this.Role = 3;
			this.Spot = this.Floors[16];
			return;
		case 50:
			this.Role = 3;
			this.Spot = this.Floors[17];
			return;
		case 51:
			this.Role = 4;
			this.Spot = this.Toilets[7];
			return;
		case 52:
			this.Role = 4;
			this.Spot = this.Toilets[8];
			return;
		case 53:
			this.Role = 4;
			this.Spot = this.Toilets[9];
			return;
		case 54:
			this.Role = 4;
			this.Spot = this.Toilets[10];
			return;
		case 55:
			this.Role = 4;
			this.Spot = this.Toilets[11];
			return;
		case 56:
			this.Role = 4;
			this.Spot = this.Toilets[4];
			return;
		case 57:
			this.Role = 4;
			this.Spot = this.Toilets[5];
			return;
		case 58:
			this.Role = 4;
			this.Spot = this.Toilets[6];
			return;
		case 59:
			this.Role = 3;
			this.Spot = this.Floors[18];
			return;
		case 60:
			this.Role = 3;
			this.Spot = this.Floors[19];
			return;
		case 61:
			this.Role = 3;
			this.Spot = this.Floors[20];
			return;
		case 62:
			this.Role = 3;
			this.Spot = this.Floors[21];
			return;
		case 63:
			this.Role = 3;
			this.Spot = this.Floors[22];
			return;
		case 64:
			this.Role = 3;
			this.Spot = this.Floors[23];
			return;
		case 65:
			this.Role = 3;
			this.Spot = this.Floors[24];
			return;
		case 66:
			this.Role = 3;
			this.Spot = this.Floors[25];
			return;
		case 67:
			this.Role = 3;
			this.Spot = this.Floors[26];
			return;
		case 68:
			this.Role = 3;
			this.Spot = this.Floors[27];
			return;
		case 69:
			this.Role = 3;
			this.Spot = this.Floors[28];
			return;
		case 70:
			this.Role = 3;
			this.Spot = this.Floors[29];
			return;
		case 71:
			this.Role = 3;
			this.Spot = this.Floors[30];
			return;
		case 72:
			this.Role = 3;
			this.Spot = this.Floors[31];
			return;
		case 73:
			this.Role = 3;
			this.Spot = this.Floors[32];
			return;
		case 74:
			this.Role = 3;
			this.Spot = this.Floors[33];
			return;
		case 75:
			this.Role = 3;
			this.Spot = this.Floors[34];
			break;
		default:
			return;
		}
	}

	// Token: 0x040015AF RID: 5551
	public StudentManagerScript StudentManager;

	// Token: 0x040015B0 RID: 5552
	public Transform[] Windows;

	// Token: 0x040015B1 RID: 5553
	public Transform[] Desks;

	// Token: 0x040015B2 RID: 5554
	public Transform[] Floors;

	// Token: 0x040015B3 RID: 5555
	public Transform[] Toilets;

	// Token: 0x040015B4 RID: 5556
	public Transform[] Rooftops;

	// Token: 0x040015B5 RID: 5557
	public Transform[] ClappingSpots;

	// Token: 0x040015B6 RID: 5558
	public Transform Spot;

	// Token: 0x040015B7 RID: 5559
	public int Role;
}

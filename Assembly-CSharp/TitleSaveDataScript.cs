using System;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public class TitleSaveDataScript : MonoBehaviour
{
	// Token: 0x06001C3D RID: 7229 RVA: 0x00151958 File Offset: 0x0014FB58
	public void Start()
	{
		if (PlayerPrefs.GetInt("ProfileCreated_" + this.ID) == 1)
		{
			GameGlobals.Profile = this.ID;
			this.EmptyFile.SetActive(false);
			this.Data.SetActive(true);
			this.Kills.text = "Kills: " + PlayerGlobals.Kills;
			this.Mood.text = "Mood: " + Mathf.RoundToInt(SchoolGlobals.SchoolAtmosphere * 100f);
			this.Alerts.text = "Alerts: " + PlayerGlobals.Alerts;
			this.Week.text = "Week: " + 1;
			this.Day.text = "Day: " + DateGlobals.Weekday;
			this.Rival.text = "Rival: Osana";
			this.Rep.text = "Rep: " + PlayerGlobals.Reputation;
			this.Club.text = "Club: " + ClubGlobals.Club;
			this.Friends.text = "Friends: " + PlayerGlobals.Friends;
			if (PlayerGlobals.Kills == 0)
			{
				this.Blood.mainTexture = null;
				return;
			}
			if (PlayerGlobals.Kills > 0)
			{
				this.Blood.mainTexture = this.Bloods[1];
				return;
			}
			if (PlayerGlobals.Kills > 5)
			{
				this.Blood.mainTexture = this.Bloods[2];
				return;
			}
			if (PlayerGlobals.Kills > 10)
			{
				this.Blood.mainTexture = this.Bloods[3];
				return;
			}
			if (PlayerGlobals.Kills > 15)
			{
				this.Blood.mainTexture = this.Bloods[4];
				return;
			}
			if (PlayerGlobals.Kills > 20)
			{
				this.Blood.mainTexture = this.Bloods[5];
				return;
			}
		}
		else
		{
			this.EmptyFile.SetActive(true);
			this.Data.SetActive(false);
			this.Blood.enabled = false;
		}
	}

	// Token: 0x040034F0 RID: 13552
	public GameObject EmptyFile;

	// Token: 0x040034F1 RID: 13553
	public GameObject Data;

	// Token: 0x040034F2 RID: 13554
	public Texture[] Bloods;

	// Token: 0x040034F3 RID: 13555
	public UITexture Blood;

	// Token: 0x040034F4 RID: 13556
	public UILabel Kills;

	// Token: 0x040034F5 RID: 13557
	public UILabel Mood;

	// Token: 0x040034F6 RID: 13558
	public UILabel Alerts;

	// Token: 0x040034F7 RID: 13559
	public UILabel Week;

	// Token: 0x040034F8 RID: 13560
	public UILabel Day;

	// Token: 0x040034F9 RID: 13561
	public UILabel Rival;

	// Token: 0x040034FA RID: 13562
	public UILabel Rep;

	// Token: 0x040034FB RID: 13563
	public UILabel Club;

	// Token: 0x040034FC RID: 13564
	public UILabel Friends;

	// Token: 0x040034FD RID: 13565
	public int ID;
}

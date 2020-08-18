using System;
using System.IO;
using UnityEngine;

// Token: 0x020003C4 RID: 964
public class SaveLoadScript : MonoBehaviour
{
	// Token: 0x06001A36 RID: 6710 RVA: 0x00100580 File Offset: 0x000FE780
	private void DetermineFilePath()
	{
		this.SaveProfile = GameGlobals.Profile;
		this.SaveSlot = PlayerPrefs.GetInt("SaveSlot");
		this.SaveFilePath = string.Concat(new object[]
		{
			Application.streamingAssetsPath,
			"/SaveData/Profile_",
			this.SaveProfile,
			"/Slot_",
			this.SaveSlot,
			"/Student_",
			this.Student.StudentID,
			"_Data.txt"
		});
	}

	// Token: 0x06001A37 RID: 6711 RVA: 0x00100610 File Offset: 0x000FE810
	public void SaveData()
	{
		this.DetermineFilePath();
		this.SerializedData = JsonUtility.ToJson(this.Student);
		File.WriteAllText(this.SaveFilePath, this.SerializedData);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_posX"
		}), base.transform.position.x);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_posY"
		}), base.transform.position.y);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_posZ"
		}), base.transform.position.z);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_rotX"
		}), base.transform.eulerAngles.x);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_rotY"
		}), base.transform.eulerAngles.y);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_rotZ"
		}), base.transform.eulerAngles.z);
	}

	// Token: 0x06001A38 RID: 6712 RVA: 0x001008E0 File Offset: 0x000FEAE0
	public void LoadData()
	{
		this.DetermineFilePath();
		if (File.Exists(this.SaveFilePath))
		{
			base.transform.position = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"_Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_posX"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"_Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_posY"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"_Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_posZ"
			})));
			base.transform.eulerAngles = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_rotX"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_rotY"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_rotZ"
			})));
			JsonUtility.FromJsonOverwrite(File.ReadAllText(this.SaveFilePath), this.Student);
		}
	}

	// Token: 0x04002944 RID: 10564
	public StudentScript Student;

	// Token: 0x04002945 RID: 10565
	public string SerializedData;

	// Token: 0x04002946 RID: 10566
	public string SaveFilePath;

	// Token: 0x04002947 RID: 10567
	public int SaveProfile;

	// Token: 0x04002948 RID: 10568
	public int SaveSlot;
}

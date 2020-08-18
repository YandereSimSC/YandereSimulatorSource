using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200024A RID: 586
public class CosmeticScript : MonoBehaviour
{
	// Token: 0x0600129B RID: 4763 RVA: 0x0008D730 File Offset: 0x0008B930
	public void Start()
	{
		bool kidnapped = this.Kidnapped;
		if (this.RightShoe != null)
		{
			this.RightShoe.SetActive(false);
			this.LeftShoe.SetActive(false);
		}
		this.ColorValue = new Color(1f, 1f, 1f, 1f);
		if (this.JSON == null)
		{
			this.JSON = this.Student.JSON;
		}
		string name = string.Empty;
		if (!this.Initialized)
		{
			this.Accessory = int.Parse(this.JSON.Students[this.StudentID].Accessory);
			this.Hairstyle = int.Parse(this.JSON.Students[this.StudentID].Hairstyle);
			this.Stockings = this.JSON.Students[this.StudentID].Stockings;
			this.BreastSize = this.JSON.Students[this.StudentID].BreastSize;
			this.EyeType = this.JSON.Students[this.StudentID].EyeType;
			this.HairColor = this.JSON.Students[this.StudentID].Color;
			this.EyeColor = this.JSON.Students[this.StudentID].Eyes;
			this.Club = this.JSON.Students[this.StudentID].Club;
			this.Name = this.JSON.Students[this.StudentID].Name;
			if (this.Yandere)
			{
				this.Accessory = 0;
				this.Hairstyle = 1;
				this.Stockings = "Black";
				this.BreastSize = 1f;
				this.HairColor = "White";
				this.EyeColor = "Black";
				this.Club = ClubType.None;
			}
			this.OriginalStockings = this.Stockings;
			this.Initialized = true;
		}
		if (this.StudentID == 36)
		{
			if (TaskGlobals.GetTaskStatus(36) < 3)
			{
				this.FacialHairstyle = 12;
				this.EyewearID = 8;
			}
			else
			{
				this.FacialHairstyle = 0;
				this.EyewearID = 9;
				this.Hairstyle = 49;
				this.Accessory = 0;
			}
		}
		if (this.StudentID == 51 && ClubGlobals.GetClubClosed(ClubType.LightMusic))
		{
			this.Hairstyle = 51;
		}
		if (GameGlobals.EmptyDemon && (this.StudentID == 21 || this.StudentID == 26 || this.StudentID == 31 || this.StudentID == 36 || this.StudentID == 41 || this.StudentID == 46 || this.StudentID == 51 || this.StudentID == 56 || this.StudentID == 61 || this.StudentID == 66 || this.StudentID == 71))
		{
			if (!this.Male)
			{
				this.Hairstyle = 52;
			}
			else
			{
				this.Hairstyle = 53;
			}
			this.FacialHairstyle = 0;
			this.EyewearID = 0;
			this.Accessory = 0;
			this.Stockings = "";
			this.BreastSize = 1f;
			this.Empty = true;
		}
		if (this.Name == "Random")
		{
			this.Randomize = true;
			if (!this.Male)
			{
				name = this.StudentManager.FirstNames[UnityEngine.Random.Range(0, this.StudentManager.FirstNames.Length)] + " " + this.StudentManager.LastNames[UnityEngine.Random.Range(0, this.StudentManager.LastNames.Length)];
				this.JSON.Students[this.StudentID].Name = name;
				this.Student.Name = name;
			}
			else
			{
				name = this.StudentManager.MaleNames[UnityEngine.Random.Range(0, this.StudentManager.MaleNames.Length)] + " " + this.StudentManager.LastNames[UnityEngine.Random.Range(0, this.StudentManager.LastNames.Length)];
				this.JSON.Students[this.StudentID].Name = name;
				this.Student.Name = name;
			}
			if (MissionModeGlobals.MissionMode && MissionModeGlobals.MissionTarget == this.StudentID)
			{
				this.JSON.Students[this.StudentID].Name = MissionModeGlobals.MissionTargetName;
				this.Student.Name = MissionModeGlobals.MissionTargetName;
				name = MissionModeGlobals.MissionTargetName;
			}
		}
		if (this.Randomize)
		{
			this.Teacher = false;
			this.BreastSize = UnityEngine.Random.Range(0.5f, 2f);
			this.Accessory = 0;
			this.Club = ClubType.None;
			if (!this.Male)
			{
				this.Hairstyle = 1;
				while (this.Hairstyle == 1 || this.Hairstyle == 20 || this.Hairstyle == 21)
				{
					this.Hairstyle = UnityEngine.Random.Range(1, this.FemaleHair.Length);
				}
			}
			else
			{
				this.SkinColor = UnityEngine.Random.Range(0, this.SkinTextures.Length);
				this.Hairstyle = UnityEngine.Random.Range(1, this.MaleHair.Length);
			}
		}
		if (!this.Male)
		{
			if (this.Hairstyle == 20 || this.Hairstyle == 21)
			{
				if (this.Direction == 1)
				{
					this.Hairstyle = 22;
				}
				else
				{
					this.Hairstyle = 19;
				}
			}
			this.ThickBrows.SetActive(false);
			if (!this.TakingPortrait)
			{
				this.Tongue.SetActive(false);
			}
			foreach (GameObject gameObject in this.PhoneCharms)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
			this.RightBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
			this.LeftBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
			this.RightWristband.SetActive(false);
			this.LeftWristband.SetActive(false);
			if (this.StudentID == 51)
			{
				this.RightTemple.name = "RENAMED";
				this.LeftTemple.name = "RENAMED";
				this.RightTemple.localScale = new Vector3(0f, 1f, 1f);
				this.LeftTemple.localScale = new Vector3(0f, 1f, 1f);
				if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
				{
					this.SadBrows.SetActive(true);
				}
				else
				{
					this.ThickBrows.SetActive(true);
				}
			}
			if (this.Club == ClubType.Bully)
			{
				if (!this.Kidnapped)
				{
					this.Student.SmartPhone.GetComponent<Renderer>().material.mainTexture = this.SmartphoneTextures[this.StudentID];
					this.Student.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.005f, 0.01f);
					this.Student.SmartPhone.transform.localEulerAngles = new Vector3(0f, -160f, 165f);
				}
				this.RightWristband.GetComponent<Renderer>().material.mainTexture = this.WristwearTextures[this.StudentID];
				this.LeftWristband.GetComponent<Renderer>().material.mainTexture = this.WristwearTextures[this.StudentID];
				this.Bookbag.GetComponent<Renderer>().material.mainTexture = this.BookbagTextures[this.StudentID];
				this.HoodieRenderer.material.mainTexture = this.HoodieTextures[this.StudentID];
				if (this.PhoneCharms.Length != 0)
				{
					this.PhoneCharms[this.StudentID].SetActive(true);
				}
				if (StudentGlobals.FemaleUniform < 2 || StudentGlobals.FemaleUniform == 3)
				{
					this.RightWristband.SetActive(true);
					this.LeftWristband.SetActive(true);
				}
				this.Bookbag.SetActive(true);
				this.Hoodie.SetActive(true);
				for (int j = 0; j < 10; j++)
				{
					this.Fingernails[j].material.color = this.BullyColor[this.StudentID];
				}
				this.Student.GymTexture = this.TanGymTexture;
				this.Student.TowelTexture = this.TanTowelTexture;
			}
			else
			{
				for (int k = 0; k < 10; k++)
				{
					this.Fingernails[k].gameObject.SetActive(false);
				}
				if (this.Club == ClubType.Gardening && !this.TakingPortrait && !this.Kidnapped)
				{
					this.CanRenderer.material.mainTexture = this.CanTextures[this.StudentID];
				}
			}
			if (!this.Kidnapped && SceneManager.GetActiveScene().name == "PortraitScene")
			{
				if (this.StudentID == 2)
				{
					this.CharacterAnimation.Play("succubus_a_idle_twins_01");
					base.transform.position = new Vector3(0.094f, 0f, 0f);
					this.LookCamera = true;
					this.CharacterAnimation["f02_smile_00"].layer = 1;
					this.CharacterAnimation.Play("f02_smile_00");
					this.CharacterAnimation["f02_smile_00"].weight = 1f;
				}
				else if (this.StudentID == 3)
				{
					this.CharacterAnimation.Play("succubus_b_idle_twins_01");
					base.transform.position = new Vector3(-0.332f, 0f, 0f);
					this.LookCamera = true;
					this.CharacterAnimation["f02_smile_00"].layer = 1;
					this.CharacterAnimation.Play("f02_smile_00");
					this.CharacterAnimation["f02_smile_00"].weight = 1f;
				}
				else if (this.StudentID == 4)
				{
					this.CharacterAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
				else if (this.StudentID == 5)
				{
					this.CharacterAnimation.Play("f02_shy_00");
					this.CharacterAnimation.Play("f02_shy_00");
					this.CharacterAnimation["f02_shy_00"].time = 1f;
				}
				else if (this.StudentID == 10)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
				}
				else if (this.StudentID == 24)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 1f;
				}
				else if (this.StudentID == 25)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 0f;
				}
				else if (this.StudentID == 30)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 0f;
				}
				else if (this.StudentID == 34)
				{
					this.CharacterAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
				else if (this.StudentID == 35)
				{
					this.CharacterAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
				else if (this.StudentID == 38)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 0f;
				}
				else if (this.StudentID == 39)
				{
					this.CharacterAnimation.Play("f02_socialCameraPose_00");
					base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.05f, base.transform.position.z);
				}
				else if (this.StudentID == 40)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 1f;
				}
				else if (this.StudentID == 51)
				{
					this.CharacterAnimation.Play("f02_musicPose_00");
					this.Tongue.SetActive(true);
				}
				else if (this.StudentID == 59)
				{
					this.CharacterAnimation.Play("f02_sleuthPortrait_00");
				}
				else if (this.StudentID == 60)
				{
					this.CharacterAnimation.Play("f02_sleuthPortrait_01");
				}
				else if (this.StudentID == 64)
				{
					this.CharacterAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
				else if (this.StudentID == 65)
				{
					this.CharacterAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
				else if (this.StudentID == 71)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 0f;
				}
				else if (this.StudentID == 72)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 0.66666f;
				}
				else if (this.StudentID == 73)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 1.33332f;
				}
				else if (this.StudentID == 74)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 1.99998f;
				}
				else if (this.StudentID == 75)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 2.66664f;
				}
				else if (this.StudentID == 81)
				{
					this.CharacterAnimation.Play("f02_socialCameraPose_00");
					base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.05f, base.transform.position.z);
				}
				else if (this.StudentID == 82 || this.StudentID == 52)
				{
					this.CharacterAnimation.Play("f02_galPose_01");
				}
				else if (this.StudentID == 83 || this.StudentID == 53)
				{
					this.CharacterAnimation.Play("f02_galPose_02");
				}
				else if (this.StudentID == 84 || this.StudentID == 54)
				{
					this.CharacterAnimation.Play("f02_galPose_03");
				}
				else if (this.StudentID == 85 || this.StudentID == 55)
				{
					this.CharacterAnimation.Play("f02_galPose_04");
				}
				else if (this.Club != ClubType.Council)
				{
					this.CharacterAnimation.Play("f02_idleShort_01");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
			}
		}
		else
		{
			this.ThickBrows.SetActive(false);
			GameObject[] array = this.GaloAccessories;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
			if (this.Club == ClubType.Occult)
			{
				this.CharacterAnimation["sadFace_00"].layer = 1;
				this.CharacterAnimation.Play("sadFace_00");
				this.CharacterAnimation["sadFace_00"].weight = 1f;
			}
			bool flag = false;
			if (this.StudentID == 28)
			{
				flag = true;
			}
			if (flag && StudentGlobals.CustomSuitor)
			{
				if (StudentGlobals.CustomSuitorHair > 0)
				{
					this.Hairstyle = StudentGlobals.CustomSuitorHair;
				}
				if (StudentGlobals.CustomSuitorAccessory > 0)
				{
					this.Accessory = StudentGlobals.CustomSuitorAccessory;
					if (this.Accessory == 1)
					{
						Transform transform = this.MaleAccessories[1].transform;
						transform.localScale = new Vector3(1.066666f, 1f, 1f);
						transform.localPosition = new Vector3(0f, -1.525f, 0.0066666f);
					}
				}
				if (StudentGlobals.CustomSuitorBlack)
				{
					this.HairColor = "SolidBlack";
				}
				if (StudentGlobals.CustomSuitorJewelry > 0)
				{
					array = this.GaloAccessories;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].SetActive(true);
					}
				}
			}
			if (this.StudentID == 36 || this.StudentID == 66)
			{
				this.CharacterAnimation["toughFace_00"].layer = 1;
				this.CharacterAnimation.Play("toughFace_00");
				this.CharacterAnimation["toughFace_00"].weight = 1f;
				if (this.StudentID == 66)
				{
					this.ThickBrows.SetActive(true);
				}
			}
			if (SceneManager.GetActiveScene().name == "PortraitScene")
			{
				if (this.StudentID == 26)
				{
					this.CharacterAnimation.Play("idleHaughty_00");
				}
				else if (this.StudentID == 36)
				{
					this.CharacterAnimation.Play("slouchIdle_00");
				}
				else if (this.StudentID == 56)
				{
					this.CharacterAnimation.Play("idleConfident_00");
				}
				else if (this.StudentID == 57)
				{
					this.CharacterAnimation.Play("sleuthPortrait_00");
				}
				else if (this.StudentID == 58)
				{
					this.CharacterAnimation.Play("sleuthPortrait_01");
				}
				else if (this.StudentID == 61)
				{
					this.CharacterAnimation.Play("scienceMad_00");
					base.transform.position = new Vector3(0f, 0.1f, 0f);
				}
				else if (this.StudentID == 62)
				{
					this.CharacterAnimation.Play("idleFrown_00");
				}
				else if (this.StudentID == 69)
				{
					this.CharacterAnimation.Play("idleFrown_00");
				}
				else if (this.StudentID == 76)
				{
					this.CharacterAnimation.Play("delinquentPoseB");
				}
				else if (this.StudentID == 77)
				{
					this.CharacterAnimation.Play("delinquentPoseA");
				}
				else if (this.StudentID == 78)
				{
					this.CharacterAnimation.Play("delinquentPoseC");
				}
				else if (this.StudentID == 79)
				{
					this.CharacterAnimation.Play("delinquentPoseD");
				}
				else if (this.StudentID == 80)
				{
					this.CharacterAnimation.Play("delinquentPoseE");
				}
			}
		}
		if (this.Club == ClubType.Teacher)
		{
			this.MyRenderer.sharedMesh = this.TeacherMesh;
			this.Teacher = true;
		}
		else if (this.Club == ClubType.GymTeacher)
		{
			if (!StudentGlobals.GetStudentReplaced(this.StudentID))
			{
				this.CharacterAnimation["f02_smile_00"].layer = 1;
				this.CharacterAnimation.Play("f02_smile_00");
				this.CharacterAnimation["f02_smile_00"].weight = 1f;
				this.RightEyeRenderer.gameObject.SetActive(false);
				this.LeftEyeRenderer.gameObject.SetActive(false);
			}
			this.MyRenderer.sharedMesh = this.CoachMesh;
			this.Teacher = true;
		}
		else if (this.Club == ClubType.Nurse)
		{
			this.MyRenderer.sharedMesh = this.NurseMesh;
			this.Teacher = true;
		}
		else if (this.Club == ClubType.Council)
		{
			this.Armband.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(-0.64375f, 0f));
			this.Armband.SetActive(true);
			string str = "";
			if (this.StudentID == 86)
			{
				str = "Strict";
			}
			if (this.StudentID == 87)
			{
				str = "Casual";
			}
			if (this.StudentID == 88)
			{
				str = "Grace";
			}
			if (this.StudentID == 89)
			{
				str = "Edgy";
			}
			this.CharacterAnimation["f02_faceCouncil" + str + "_00"].layer = 1;
			this.CharacterAnimation.Play("f02_faceCouncil" + str + "_00");
			this.CharacterAnimation["f02_idleCouncil" + str + "_00"].time = 1f;
			this.CharacterAnimation.Play("f02_idleCouncil" + str + "_00");
		}
		if (!ClubGlobals.GetClubClosed(this.Club) && (this.StudentID == 21 || this.StudentID == 26 || this.StudentID == 31 || this.StudentID == 36 || this.StudentID == 41 || this.StudentID == 46 || this.StudentID == 51 || this.StudentID == 56 || this.StudentID == 61 || this.StudentID == 66 || this.StudentID == 71))
		{
			this.Armband.SetActive(true);
			Renderer component = this.Armband.GetComponent<Renderer>();
			if (this.StudentID == 21)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(-0.63f, -0.22f));
			}
			else if (this.StudentID == 26)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0f, -0.22f));
			}
			else if (this.StudentID == 31)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0.69f, 0.01f));
			}
			else if (this.StudentID == 36)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(-0.633333f, -0.44f));
			}
			else if (this.StudentID == 41)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(-0.62f, -0.66666f));
			}
			else if (this.StudentID == 46)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0f, -0.66666f));
			}
			else if (this.StudentID == 51)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0.69f, 0.5566666f));
			}
			else if (this.StudentID == 56)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0f, 0.5533333f));
			}
			else if (this.StudentID == 61)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0f, 0f));
			}
			else if (this.StudentID == 66)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0.69f, -0.22f));
			}
			else if (this.StudentID == 71)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0.69f, 0.335f));
			}
		}
		foreach (GameObject gameObject2 in this.FemaleAccessories)
		{
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
		foreach (GameObject gameObject3 in this.MaleAccessories)
		{
			if (gameObject3 != null)
			{
				gameObject3.SetActive(false);
			}
		}
		foreach (GameObject gameObject4 in this.ClubAccessories)
		{
			if (gameObject4 != null)
			{
				gameObject4.SetActive(false);
			}
		}
		foreach (GameObject gameObject5 in this.TeacherAccessories)
		{
			if (gameObject5 != null)
			{
				gameObject5.SetActive(false);
			}
		}
		foreach (GameObject gameObject6 in this.TeacherHair)
		{
			if (gameObject6 != null)
			{
				gameObject6.SetActive(false);
			}
		}
		foreach (GameObject gameObject7 in this.FemaleHair)
		{
			if (gameObject7 != null)
			{
				gameObject7.SetActive(false);
			}
		}
		foreach (GameObject gameObject8 in this.MaleHair)
		{
			if (gameObject8 != null)
			{
				gameObject8.SetActive(false);
			}
		}
		foreach (GameObject gameObject9 in this.FacialHair)
		{
			if (gameObject9 != null)
			{
				gameObject9.SetActive(false);
			}
		}
		foreach (GameObject gameObject10 in this.Eyewear)
		{
			if (gameObject10 != null)
			{
				gameObject10.SetActive(false);
			}
		}
		foreach (GameObject gameObject11 in this.RightStockings)
		{
			if (gameObject11 != null)
			{
				gameObject11.SetActive(false);
			}
		}
		foreach (GameObject gameObject12 in this.LeftStockings)
		{
			if (gameObject12 != null)
			{
				gameObject12.SetActive(false);
			}
		}
		foreach (GameObject gameObject13 in this.Scanners)
		{
			if (gameObject13 != null)
			{
				gameObject13.SetActive(false);
			}
		}
		foreach (GameObject gameObject14 in this.Flowers)
		{
			if (gameObject14 != null)
			{
				gameObject14.SetActive(false);
			}
		}
		foreach (GameObject gameObject15 in this.Roses)
		{
			if (gameObject15 != null)
			{
				gameObject15.SetActive(false);
			}
		}
		foreach (GameObject gameObject16 in this.Goggles)
		{
			if (gameObject16 != null)
			{
				gameObject16.SetActive(false);
			}
		}
		foreach (GameObject gameObject17 in this.RedCloth)
		{
			if (gameObject17 != null)
			{
				gameObject17.SetActive(false);
			}
		}
		foreach (GameObject gameObject18 in this.Kerchiefs)
		{
			if (gameObject18 != null)
			{
				gameObject18.SetActive(false);
			}
		}
		foreach (GameObject gameObject19 in this.CatGifts)
		{
			if (gameObject19 != null)
			{
				gameObject19.SetActive(false);
			}
		}
		foreach (GameObject gameObject20 in this.PunkAccessories)
		{
			if (gameObject20 != null)
			{
				gameObject20.SetActive(false);
			}
		}
		foreach (GameObject gameObject21 in this.MusicNotes)
		{
			if (gameObject21 != null)
			{
				gameObject21.SetActive(false);
			}
		}
		bool flag2 = false;
		if (this.StudentID == 28)
		{
			flag2 = true;
		}
		if (flag2 && StudentGlobals.CustomSuitor && StudentGlobals.CustomSuitorEyewear > 0)
		{
			this.Eyewear[StudentGlobals.CustomSuitorEyewear].SetActive(true);
		}
		if (this.StudentID == 1 && SenpaiGlobals.CustomSenpai)
		{
			if (SenpaiGlobals.SenpaiEyeWear > 0)
			{
				this.Eyewear[SenpaiGlobals.SenpaiEyeWear].SetActive(true);
			}
			this.FacialHairstyle = SenpaiGlobals.SenpaiFacialHair;
			this.HairColor = SenpaiGlobals.SenpaiHairColor;
			this.EyeColor = SenpaiGlobals.SenpaiEyeColor;
			this.Hairstyle = SenpaiGlobals.SenpaiHairStyle;
		}
		if (!this.Male)
		{
			if (!this.Teacher)
			{
				this.FemaleHair[this.Hairstyle].SetActive(true);
				this.HairRenderer = this.FemaleHairRenderers[this.Hairstyle];
				this.SetFemaleUniform();
			}
			else
			{
				this.TeacherHair[this.Hairstyle].SetActive(true);
				this.HairRenderer = this.TeacherHairRenderers[this.Hairstyle];
				if (this.Club == ClubType.Teacher)
				{
					this.MyRenderer.materials[1].mainTexture = this.TeacherBodyTexture;
					this.MyRenderer.materials[2].mainTexture = this.DefaultFaceTexture;
					this.MyRenderer.materials[0].mainTexture = this.TeacherBodyTexture;
				}
				else if (this.Club == ClubType.GymTeacher)
				{
					if (StudentGlobals.GetStudentReplaced(this.StudentID))
					{
						this.MyRenderer.materials[2].mainTexture = this.DefaultFaceTexture;
						this.MyRenderer.materials[0].mainTexture = this.CoachPaleBodyTexture;
						this.MyRenderer.materials[1].mainTexture = this.CoachPaleBodyTexture;
					}
					else
					{
						this.MyRenderer.materials[2].mainTexture = this.CoachFaceTexture;
						this.MyRenderer.materials[0].mainTexture = this.CoachBodyTexture;
						this.MyRenderer.materials[1].mainTexture = this.CoachBodyTexture;
					}
				}
				else if (this.Club == ClubType.Nurse)
				{
					this.MyRenderer.materials = this.NurseMaterials;
				}
			}
		}
		else
		{
			if (this.Hairstyle > 0)
			{
				this.MaleHair[this.Hairstyle].SetActive(true);
				this.HairRenderer = this.MaleHairRenderers[this.Hairstyle];
			}
			if (this.FacialHairstyle > 0)
			{
				this.FacialHair[this.FacialHairstyle].SetActive(true);
				this.FacialHairRenderer = this.FacialHairRenderers[this.FacialHairstyle];
			}
			if (this.EyewearID > 0)
			{
				this.Eyewear[this.EyewearID].SetActive(true);
			}
			this.SetMaleUniform();
		}
		if (!this.Male)
		{
			if (!this.Teacher)
			{
				if (this.FemaleAccessories[this.Accessory] != null)
				{
					this.FemaleAccessories[this.Accessory].SetActive(true);
				}
			}
			else if (this.TeacherAccessories[this.Accessory] != null)
			{
				this.TeacherAccessories[this.Accessory].SetActive(true);
			}
		}
		else if (this.MaleAccessories[this.Accessory] != null)
		{
			this.MaleAccessories[this.Accessory].SetActive(true);
		}
		if (!this.Empty)
		{
			if (this.Club < ClubType.Gaming && this.ClubAccessories[(int)this.Club] != null && !ClubGlobals.GetClubClosed(this.Club) && this.StudentID != 26)
			{
				this.ClubAccessories[(int)this.Club].SetActive(true);
			}
			if (this.StudentID == 36)
			{
				this.ClubAccessories[(int)this.Club].SetActive(true);
			}
			if (this.Club == ClubType.Cooking)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.Kerchiefs[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Drama)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.Roses[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Art)
			{
				this.ClubAccessories[(int)this.Club].GetComponent<MeshFilter>().sharedMesh = this.Berets[this.StudentID];
			}
			else if (this.Club == ClubType.Science)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.Scanners[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.LightMusic)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.MusicNotes[this.StudentID - 50];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Sports)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.Goggles[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Gardening)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.Flowers[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Gaming)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.RedCloth[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club) && this.ClubAccessories[(int)this.Club] != null)
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
		}
		if (this.StudentID == 36 && TaskGlobals.GetTaskStatus(36) == 3)
		{
			this.ClubAccessories[(int)this.Club].SetActive(false);
		}
		if (!this.Male)
		{
			base.StartCoroutine(this.PutOnStockings());
		}
		if (!this.Randomize)
		{
			if (this.EyeColor != string.Empty)
			{
				if (this.EyeColor == "White")
				{
					this.CorrectColor = new Color(1f, 1f, 1f);
				}
				else if (this.EyeColor == "Black")
				{
					this.CorrectColor = new Color(0.5f, 0.5f, 0.5f);
				}
				else if (this.EyeColor == "Red")
				{
					this.CorrectColor = new Color(1f, 0f, 0f);
				}
				else if (this.EyeColor == "Yellow")
				{
					this.CorrectColor = new Color(1f, 1f, 0f);
				}
				else if (this.EyeColor == "Green")
				{
					this.CorrectColor = new Color(0f, 1f, 0f);
				}
				else if (this.EyeColor == "Cyan")
				{
					this.CorrectColor = new Color(0f, 1f, 1f);
				}
				else if (this.EyeColor == "Blue")
				{
					this.CorrectColor = new Color(0f, 0f, 1f);
				}
				else if (this.EyeColor == "Purple")
				{
					this.CorrectColor = new Color(1f, 0f, 1f);
				}
				else if (this.EyeColor == "Orange")
				{
					this.CorrectColor = new Color(1f, 0.5f, 0f);
				}
				else if (this.EyeColor == "Brown")
				{
					this.CorrectColor = new Color(0.5f, 0.25f, 0f);
				}
				else
				{
					this.CorrectColor = new Color(0f, 0f, 0f);
				}
				if (this.StudentID > 90 && this.StudentID < 97)
				{
					this.CorrectColor.r = this.CorrectColor.r * 0.5f;
					this.CorrectColor.g = this.CorrectColor.g * 0.5f;
					this.CorrectColor.b = this.CorrectColor.b * 0.5f;
				}
				if (this.CorrectColor != new Color(0f, 0f, 0f))
				{
					this.RightEyeRenderer.material.color = this.CorrectColor;
					this.LeftEyeRenderer.material.color = this.CorrectColor;
				}
			}
		}
		else
		{
			float r = UnityEngine.Random.Range(0f, 1f);
			float g = UnityEngine.Random.Range(0f, 1f);
			float b = UnityEngine.Random.Range(0f, 1f);
			this.RightEyeRenderer.material.color = new Color(r, g, b);
			this.LeftEyeRenderer.material.color = new Color(r, g, b);
		}
		if (!this.Randomize)
		{
			if (this.HairColor == "White")
			{
				this.ColorValue = new Color(1f, 1f, 1f);
			}
			else if (this.HairColor == "Black")
			{
				this.ColorValue = new Color(0.5f, 0.5f, 0.5f);
			}
			else if (this.HairColor == "SolidBlack")
			{
				this.ColorValue = new Color(0.0001f, 0.0001f, 0.0001f);
			}
			else if (this.HairColor == "Red")
			{
				this.ColorValue = new Color(1f, 0f, 0f);
			}
			else if (this.HairColor == "Yellow")
			{
				this.ColorValue = new Color(1f, 1f, 0f);
			}
			else if (this.HairColor == "Green")
			{
				this.ColorValue = new Color(0f, 1f, 0f);
			}
			else if (this.HairColor == "Cyan")
			{
				this.ColorValue = new Color(0f, 1f, 1f);
			}
			else if (this.HairColor == "Blue")
			{
				this.ColorValue = new Color(0f, 0f, 1f);
			}
			else if (this.HairColor == "Purple")
			{
				this.ColorValue = new Color(1f, 0f, 1f);
			}
			else if (this.HairColor == "Orange")
			{
				this.ColorValue = new Color(1f, 0.5f, 0f);
			}
			else if (this.HairColor == "Brown")
			{
				this.ColorValue = new Color(0.5f, 0.25f, 0f);
			}
			else
			{
				this.ColorValue = new Color(0f, 0f, 0f);
				this.RightIrisLight.SetActive(false);
				this.LeftIrisLight.SetActive(false);
			}
			if (this.StudentID > 90 && this.StudentID < 97)
			{
				this.ColorValue.r = this.ColorValue.r * 0.5f;
				this.ColorValue.g = this.ColorValue.g * 0.5f;
				this.ColorValue.b = this.ColorValue.b * 0.5f;
			}
			if (this.ColorValue == new Color(0f, 0f, 0f))
			{
				this.RightEyeRenderer.material.mainTexture = this.HairRenderer.material.mainTexture;
				this.LeftEyeRenderer.material.mainTexture = this.HairRenderer.material.mainTexture;
				if (!this.DoNotChangeFace)
				{
					this.FaceTexture = this.HairRenderer.material.mainTexture;
				}
				if (this.Empty)
				{
					this.FaceTexture = this.GrayFace;
				}
				this.CustomHair = true;
			}
			if (!this.CustomHair)
			{
				if (this.Hairstyle > 0)
				{
					if (GameGlobals.LoveSick)
					{
						this.HairRenderer.material.color = new Color(0.1f, 0.1f, 0.1f);
						if (this.HairRenderer.materials.Length > 1)
						{
							this.HairRenderer.materials[1].color = new Color(0.1f, 0.1f, 0.1f);
						}
					}
					else
					{
						this.HairRenderer.material.color = this.ColorValue;
					}
				}
			}
			else if (GameGlobals.LoveSick)
			{
				this.HairRenderer.material.color = new Color(0.1f, 0.1f, 0.1f);
				if (this.HairRenderer.materials.Length > 1)
				{
					this.HairRenderer.materials[1].color = new Color(0.1f, 0.1f, 0.1f);
				}
			}
			if (!this.Male)
			{
				if (this.StudentID == 25)
				{
					this.FemaleAccessories[6].GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
				}
				else if (this.StudentID == 30)
				{
					this.FemaleAccessories[6].GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
				}
			}
		}
		else
		{
			this.HairRenderer.material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		}
		if (!this.Teacher)
		{
			if (this.CustomHair)
			{
				if (!this.Male)
				{
					this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
				}
				else if (StudentGlobals.MaleUniform == 1)
				{
					this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
				}
				else if (StudentGlobals.MaleUniform < 4)
				{
					this.MyRenderer.materials[1].mainTexture = this.FaceTexture;
				}
				else
				{
					this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
				}
			}
		}
		else if (this.Teacher && StudentGlobals.GetStudentReplaced(this.StudentID))
		{
			Color studentColor = StudentGlobals.GetStudentColor(this.StudentID);
			Color studentEyeColor = StudentGlobals.GetStudentEyeColor(this.StudentID);
			this.HairRenderer.material.color = studentColor;
			this.RightEyeRenderer.material.color = studentEyeColor;
			this.LeftEyeRenderer.material.color = studentEyeColor;
		}
		if (this.Male)
		{
			if (this.Accessory == 2)
			{
				this.RightIrisLight.SetActive(false);
				this.LeftIrisLight.SetActive(false);
			}
			if (SceneManager.GetActiveScene().name == "PortraitScene")
			{
				this.Character.transform.localScale = new Vector3(0.93f, 0.93f, 0.93f);
			}
			if (this.FacialHairRenderer != null)
			{
				this.FacialHairRenderer.material.color = this.ColorValue;
				if (this.FacialHairRenderer.materials.Length > 1)
				{
					this.FacialHairRenderer.materials[1].color = this.ColorValue;
				}
			}
		}
		int studentID = this.StudentID;
		if (this.StudentID == 25 || this.StudentID == 30)
		{
			this.FemaleAccessories[6].SetActive(true);
			if ((float)StudentGlobals.GetStudentReputation(this.StudentID) < -33.33333f)
			{
				this.FemaleAccessories[6].SetActive(false);
			}
		}
		if (this.StudentID == 2)
		{
			if (SchemeGlobals.GetSchemeStage(2) == 2 || SchemeGlobals.GetSchemeStage(2) == 100)
			{
				this.FemaleAccessories[3].SetActive(false);
			}
		}
		else if (this.StudentID == 40)
		{
			if (base.transform.position != Vector3.zero)
			{
				this.RightEyeRenderer.material.mainTexture = this.DefaultFaceTexture;
				this.LeftEyeRenderer.material.mainTexture = this.DefaultFaceTexture;
				this.RightEyeRenderer.gameObject.GetComponent<RainbowScript>().enabled = true;
				this.LeftEyeRenderer.gameObject.GetComponent<RainbowScript>().enabled = true;
			}
		}
		else if (this.StudentID == 41)
		{
			this.CharacterAnimation["moodyEyes_00"].layer = 1;
			this.CharacterAnimation.Play("moodyEyes_00");
			this.CharacterAnimation["moodyEyes_00"].weight = 1f;
			this.CharacterAnimation.Play("moodyEyes_00");
		}
		else if (this.StudentID == 51)
		{
			if (!ClubGlobals.GetClubClosed(ClubType.LightMusic))
			{
				this.PunkAccessories[1].SetActive(true);
				this.PunkAccessories[2].SetActive(true);
				this.PunkAccessories[3].SetActive(true);
			}
		}
		else if (this.StudentID == 59)
		{
			this.ClubAccessories[7].transform.localPosition = new Vector3(0f, -1.04f, 0.5f);
			this.ClubAccessories[7].transform.localEulerAngles = new Vector3(-22.5f, 0f, 0f);
		}
		else if (this.StudentID == 60)
		{
			this.FemaleAccessories[13].SetActive(true);
		}
		if (this.Student != null && this.Student.AoT)
		{
			this.Student.AttackOnTitan();
		}
		if (this.HomeScene)
		{
			this.Student.CharacterAnimation["idle_00"].time = 9f;
			this.Student.CharacterAnimation["idle_00"].speed = 0f;
		}
		this.TaskCheck();
		this.TurnOnCheck();
		if (!this.Male)
		{
			this.EyeTypeCheck();
		}
		if (this.Kidnapped)
		{
			this.WearIndoorShoes();
		}
		if (!this.Male && (this.Hairstyle == 20 || this.Hairstyle == 21))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x00090608 File Offset: 0x0008E808
	public void SetMaleUniform()
	{
		if (this.StudentID == 1)
		{
			this.SkinColor = SenpaiGlobals.SenpaiSkinColor;
			this.FaceTexture = this.FaceTextures[this.SkinColor];
		}
		else
		{
			this.FaceTexture = (this.CustomHair ? this.HairRenderer.material.mainTexture : this.FaceTextures[this.SkinColor]);
			bool flag = false;
			if (this.StudentID == 28)
			{
				flag = true;
			}
			if (flag && StudentGlobals.CustomSuitor && StudentGlobals.CustomSuitorTan)
			{
				this.SkinColor = 6;
				this.DoNotChangeFace = true;
				this.FaceTexture = this.FaceTextures[6];
			}
		}
		this.MyRenderer.sharedMesh = this.MaleUniforms[StudentGlobals.MaleUniform];
		this.SchoolUniform = this.MaleUniforms[StudentGlobals.MaleUniform];
		this.UniformTexture = this.MaleUniformTextures[StudentGlobals.MaleUniform];
		this.CasualTexture = this.MaleCasualTextures[StudentGlobals.MaleUniform];
		this.SocksTexture = this.MaleSocksTextures[StudentGlobals.MaleUniform];
		if (StudentGlobals.MaleUniform == 1)
		{
			this.SkinID = 0;
			this.UniformID = 1;
			this.FaceID = 2;
		}
		else if (StudentGlobals.MaleUniform == 2)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (StudentGlobals.MaleUniform == 3)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (StudentGlobals.MaleUniform == 4)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (StudentGlobals.MaleUniform == 5)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (StudentGlobals.MaleUniform == 6)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		if (StudentGlobals.MaleUniform < 2 && this.Club == ClubType.Delinquent)
		{
			this.MyRenderer.sharedMesh = this.DelinquentMesh;
			if (this.StudentID == 76)
			{
				this.UniformTexture = this.EyeTextures[0];
				this.CasualTexture = this.EyeTextures[1];
				this.SocksTexture = this.EyeTextures[2];
			}
			else if (this.StudentID == 77)
			{
				this.UniformTexture = this.CheekTextures[0];
				this.CasualTexture = this.CheekTextures[1];
				this.SocksTexture = this.CheekTextures[2];
			}
			else if (this.StudentID == 78)
			{
				this.UniformTexture = this.ForeheadTextures[0];
				this.CasualTexture = this.ForeheadTextures[1];
				this.SocksTexture = this.ForeheadTextures[2];
			}
			else if (this.StudentID == 79)
			{
				this.UniformTexture = this.MouthTextures[0];
				this.CasualTexture = this.MouthTextures[1];
				this.SocksTexture = this.MouthTextures[2];
			}
			else if (this.StudentID == 80)
			{
				this.UniformTexture = this.NoseTextures[0];
				this.CasualTexture = this.NoseTextures[1];
				this.SocksTexture = this.NoseTextures[2];
			}
		}
		if (this.StudentID == 10)
		{
			this.Student.GymTexture = this.ObstacleGymTexture;
			this.Student.TowelTexture = this.ObstacleTowelTexture;
			this.Student.SwimsuitTexture = this.ObstacleSwimsuitTexture;
		}
		if (this.StudentID == 11)
		{
			this.Student.SwimsuitTexture = this.OsanaSwimsuitTexture;
		}
		if (this.StudentID == 58)
		{
			this.SkinColor = 8;
			this.Student.TowelTexture = this.TanTowelTexture;
			this.Student.SwimsuitTexture = this.TanSwimsuitTexture;
		}
		if (this.Empty)
		{
			this.UniformTexture = this.MaleUniformTextures[7];
			this.CasualTexture = this.MaleCasualTextures[7];
			this.SocksTexture = this.MaleSocksTextures[7];
			this.FaceTexture = this.GrayFace;
			this.SkinColor = 7;
		}
		if (!this.Student.Indoors)
		{
			this.MyRenderer.materials[this.FaceID].mainTexture = this.FaceTexture;
			this.MyRenderer.materials[this.SkinID].mainTexture = this.SkinTextures[this.SkinColor];
			this.MyRenderer.materials[this.UniformID].mainTexture = this.CasualTexture;
			return;
		}
		this.MyRenderer.materials[this.FaceID].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[this.SkinID].mainTexture = this.SkinTextures[this.SkinColor];
		this.MyRenderer.materials[this.UniformID].mainTexture = this.UniformTexture;
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x00090A98 File Offset: 0x0008EC98
	public void SetFemaleUniform()
	{
		if (this.Club != ClubType.Council)
		{
			this.MyRenderer.sharedMesh = this.FemaleUniforms[StudentGlobals.FemaleUniform];
			this.SchoolUniform = this.FemaleUniforms[StudentGlobals.FemaleUniform];
			if (this.Club == ClubType.Bully)
			{
				this.UniformTexture = this.GanguroUniformTextures[StudentGlobals.FemaleUniform];
				this.CasualTexture = this.GanguroCasualTextures[StudentGlobals.FemaleUniform];
				this.SocksTexture = this.GanguroSocksTextures[StudentGlobals.FemaleUniform];
			}
			else if (this.StudentID == 10)
			{
				this.UniformTexture = this.ObstacleUniformTextures[StudentGlobals.FemaleUniform];
				this.CasualTexture = this.ObstacleCasualTextures[StudentGlobals.FemaleUniform];
				this.SocksTexture = this.ObstacleSocksTextures[StudentGlobals.FemaleUniform];
			}
			else if (this.StudentID > 11 && this.StudentID < 21)
			{
				this.MysteriousObstacle = true;
				this.UniformTexture = this.BlackBody;
				this.CasualTexture = this.BlackBody;
				this.SocksTexture = this.BlackBody;
				this.HairRenderer.enabled = false;
				this.RightEyeRenderer.enabled = false;
				this.LeftEyeRenderer.enabled = false;
				this.RightIrisLight.SetActive(false);
				this.LeftIrisLight.SetActive(false);
			}
			else
			{
				this.UniformTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
				this.CasualTexture = this.FemaleCasualTextures[StudentGlobals.FemaleUniform];
				this.SocksTexture = this.FemaleSocksTextures[StudentGlobals.FemaleUniform];
			}
		}
		else
		{
			this.RightIrisLight.SetActive(false);
			this.LeftIrisLight.SetActive(false);
			this.MyRenderer.sharedMesh = this.FemaleUniforms[4];
			this.SchoolUniform = this.FemaleUniforms[4];
			this.UniformTexture = this.FemaleUniformTextures[7];
			this.CasualTexture = this.FemaleCasualTextures[7];
			this.SocksTexture = this.FemaleSocksTextures[7];
		}
		if (this.Empty)
		{
			this.UniformTexture = this.FemaleUniformTextures[8];
			this.CasualTexture = this.FemaleCasualTextures[8];
			this.SocksTexture = this.FemaleSocksTextures[8];
		}
		if (!this.Cutscene)
		{
			if (!this.Kidnapped)
			{
				if (!this.Student.Indoors)
				{
					this.MyRenderer.materials[0].mainTexture = this.CasualTexture;
					this.MyRenderer.materials[1].mainTexture = this.CasualTexture;
				}
				else
				{
					this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
					this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
				}
			}
			else
			{
				this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
				this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
			}
		}
		else
		{
			this.UniformTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
			this.FaceTexture = this.DefaultFaceTexture;
			this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
			this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
		}
		ClubType club = this.Club;
		if (this.MysteriousObstacle)
		{
			this.FaceTexture = this.BlackBody;
		}
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		if (!this.TakingPortrait && this.Student != null && this.Student.StudentManager != null && this.Student.StudentManager.Censor)
		{
			this.CensorPanties();
		}
		if (this.MyStockings != null)
		{
			base.StartCoroutine(this.PutOnStockings());
		}
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x00090E50 File Offset: 0x0008F050
	public void CensorPanties()
	{
		if (!this.Student.ClubAttire && this.Student.Schoolwear == 1)
		{
			this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 1f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 1f);
			return;
		}
		this.RemoveCensor();
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x00090EB7 File Offset: 0x0008F0B7
	public void RemoveCensor()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0f);
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x00090EF4 File Offset: 0x0008F0F4
	private void TaskCheck()
	{
		if (this.StudentID == 37)
		{
			if (TaskGlobals.GetTaskStatus(37) < 3)
			{
				if (!this.TakingPortrait)
				{
					this.MaleAccessories[1].SetActive(false);
					return;
				}
				this.MaleAccessories[1].SetActive(true);
				return;
			}
		}
		else if (this.StudentID == 11 && this.PhoneCharms.Length != 0)
		{
			if (TaskGlobals.GetTaskStatus(11) < 3)
			{
				this.PhoneCharms[11].SetActive(false);
				return;
			}
			this.PhoneCharms[11].SetActive(true);
		}
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x00090F78 File Offset: 0x0008F178
	private void TurnOnCheck()
	{
		if (!this.TurnedOn && !this.TakingPortrait && this.Male)
		{
			if (this.HairColor == "Purple")
			{
				this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
				this.LoveManager.TotalTargets++;
			}
			else if (this.Hairstyle == 30)
			{
				this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
				this.LoveManager.TotalTargets++;
			}
			else if ((this.Accessory > 1 && this.Accessory < 5) || this.Accessory == 13)
			{
				this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
				this.LoveManager.TotalTargets++;
			}
			else if (this.Student.Persona == PersonaType.TeachersPet)
			{
				this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
				this.LoveManager.TotalTargets++;
			}
			else if (this.EyewearID > 0)
			{
				this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
				this.LoveManager.TotalTargets++;
			}
		}
		this.TurnedOn = true;
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x00091118 File Offset: 0x0008F318
	private void DestroyUnneccessaryObjects()
	{
		foreach (GameObject gameObject in this.FemaleAccessories)
		{
			if (gameObject != null && !gameObject.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		foreach (GameObject gameObject2 in this.MaleAccessories)
		{
			if (gameObject2 != null && !gameObject2.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject2);
			}
		}
		foreach (GameObject gameObject3 in this.ClubAccessories)
		{
			if (gameObject3 != null && !gameObject3.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject3);
			}
		}
		foreach (GameObject gameObject4 in this.TeacherAccessories)
		{
			if (gameObject4 != null && !gameObject4.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject4);
			}
		}
		foreach (GameObject gameObject5 in this.TeacherHair)
		{
			if (gameObject5 != null && !gameObject5.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject5);
			}
		}
		foreach (GameObject gameObject6 in this.FemaleHair)
		{
			if (gameObject6 != null && !gameObject6.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject6);
			}
		}
		foreach (GameObject gameObject7 in this.MaleHair)
		{
			if (gameObject7 != null && !gameObject7.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject7);
			}
		}
		foreach (GameObject gameObject8 in this.FacialHair)
		{
			if (gameObject8 != null && !gameObject8.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject8);
			}
		}
		foreach (GameObject gameObject9 in this.Eyewear)
		{
			if (gameObject9 != null && !gameObject9.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject9);
			}
		}
		foreach (GameObject gameObject10 in this.RightStockings)
		{
			if (gameObject10 != null && !gameObject10.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject10);
			}
		}
		foreach (GameObject gameObject11 in this.LeftStockings)
		{
			if (gameObject11 != null && !gameObject11.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject11);
			}
		}
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x00091359 File Offset: 0x0008F559
	public IEnumerator PutOnStockings()
	{
		this.RightStockings[0].SetActive(false);
		this.LeftStockings[0].SetActive(false);
		if (this.Stockings == string.Empty)
		{
			this.MyStockings = null;
		}
		else if (this.Stockings == "Red")
		{
			this.MyStockings = this.RedStockings;
		}
		else if (this.Stockings == "Yellow")
		{
			this.MyStockings = this.YellowStockings;
		}
		else if (this.Stockings == "Green")
		{
			this.MyStockings = this.GreenStockings;
		}
		else if (this.Stockings == "Cyan")
		{
			this.MyStockings = this.CyanStockings;
		}
		else if (this.Stockings == "Blue")
		{
			this.MyStockings = this.BlueStockings;
		}
		else if (this.Stockings == "Purple")
		{
			this.MyStockings = this.PurpleStockings;
		}
		else if (this.Stockings == "ShortGreen")
		{
			this.MyStockings = this.GreenSocks;
		}
		else if (this.Stockings == "ShortBlack")
		{
			this.MyStockings = this.BlackKneeSocks;
		}
		else if (this.Stockings == "Black")
		{
			this.MyStockings = this.BlackStockings;
		}
		else if (this.Stockings == "Osana")
		{
			this.MyStockings = this.OsanaStockings;
		}
		else if (this.Stockings == "Kizana")
		{
			this.MyStockings = this.KizanaStockings;
		}
		else if (this.Stockings == "Council1")
		{
			this.MyStockings = this.TurtleStockings;
		}
		else if (this.Stockings == "Council2")
		{
			this.MyStockings = this.TigerStockings;
		}
		else if (this.Stockings == "Council3")
		{
			this.MyStockings = this.BirdStockings;
		}
		else if (this.Stockings == "Council4")
		{
			this.MyStockings = this.DragonStockings;
		}
		else if (this.Stockings == "Music1")
		{
			if (!ClubGlobals.GetClubClosed(ClubType.LightMusic))
			{
				this.MyStockings = this.MusicStockings[1];
			}
		}
		else if (this.Stockings == "Music2")
		{
			this.MyStockings = this.MusicStockings[2];
		}
		else if (this.Stockings == "Music3")
		{
			this.MyStockings = this.MusicStockings[3];
		}
		else if (this.Stockings == "Music4")
		{
			this.MyStockings = this.MusicStockings[4];
		}
		else if (this.Stockings == "Music5")
		{
			this.MyStockings = this.MusicStockings[5];
		}
		else if (this.Stockings == "Custom1")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings1.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[1] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[1];
			NewCustomStockings = null;
		}
		else if (this.Stockings == "Custom2")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings2.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[2] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[2];
			NewCustomStockings = null;
		}
		else if (this.Stockings == "Custom3")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings3.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[3] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[3];
			NewCustomStockings = null;
		}
		else if (this.Stockings == "Custom4")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings4.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[4] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[4];
			NewCustomStockings = null;
		}
		else if (this.Stockings == "Custom5")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings5.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[5] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[5];
			NewCustomStockings = null;
		}
		else if (this.Stockings == "Custom6")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings6.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[6] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[6];
			NewCustomStockings = null;
		}
		else if (this.Stockings == "Custom7")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings7.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[7] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[7];
			NewCustomStockings = null;
		}
		else if (this.Stockings == "Custom8")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings8.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[8] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[8];
			NewCustomStockings = null;
		}
		else if (this.Stockings == "Custom9")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings9.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[9] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[9];
			NewCustomStockings = null;
		}
		else if (this.Stockings == "Custom10")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings10.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[10] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[10];
			NewCustomStockings = null;
		}
		else if (this.Stockings == "Loose")
		{
			this.MyStockings = null;
			this.RightStockings[0].SetActive(true);
			this.LeftStockings[0].SetActive(true);
		}
		if (this.MyStockings != null)
		{
			this.MyRenderer.materials[0].SetTexture("_OverlayTex", this.MyStockings);
			this.MyRenderer.materials[1].SetTexture("_OverlayTex", this.MyStockings);
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 1f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 1f);
		}
		else
		{
			this.MyRenderer.materials[0].SetTexture("_OverlayTex", null);
			this.MyRenderer.materials[1].SetTexture("_OverlayTex", null);
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		}
		yield break;
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x00091368 File Offset: 0x0008F568
	public void WearIndoorShoes()
	{
		if (!this.Male)
		{
			this.MyRenderer.materials[0].mainTexture = this.CasualTexture;
			this.MyRenderer.materials[1].mainTexture = this.CasualTexture;
			return;
		}
		this.MyRenderer.materials[this.UniformID].mainTexture = this.CasualTexture;
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x000913CC File Offset: 0x0008F5CC
	public void WearOutdoorShoes()
	{
		if (!this.Male)
		{
			this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
			this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
			return;
		}
		this.MyRenderer.materials[this.UniformID].mainTexture = this.UniformTexture;
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x00091430 File Offset: 0x0008F630
	public void EyeTypeCheck()
	{
		int num = 0;
		if (this.EyeType == "Thin")
		{
			this.MyRenderer.SetBlendShapeWeight(8, 100f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.StudentManager.Thins++;
			num = this.StudentManager.Thins;
		}
		else if (this.EyeType == "Serious")
		{
			this.MyRenderer.SetBlendShapeWeight(5, 50f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.StudentManager.Seriouses++;
			num = this.StudentManager.Seriouses;
		}
		else if (this.EyeType == "Round")
		{
			this.MyRenderer.SetBlendShapeWeight(5, 15f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.StudentManager.Rounds++;
			num = this.StudentManager.Rounds;
		}
		else if (this.EyeType == "Sad")
		{
			this.MyRenderer.SetBlendShapeWeight(0, 50f);
			this.MyRenderer.SetBlendShapeWeight(5, 15f);
			this.MyRenderer.SetBlendShapeWeight(6, 50f);
			this.MyRenderer.SetBlendShapeWeight(8, 50f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.StudentManager.Sads++;
			num = this.StudentManager.Sads;
		}
		else if (this.EyeType == "Mean")
		{
			this.MyRenderer.SetBlendShapeWeight(10, 100f);
			this.StudentManager.Means++;
			num = this.StudentManager.Means;
		}
		else if (this.EyeType == "Smug")
		{
			this.MyRenderer.SetBlendShapeWeight(0, 50f);
			this.MyRenderer.SetBlendShapeWeight(5, 25f);
			this.StudentManager.Smugs++;
			num = this.StudentManager.Smugs;
		}
		else if (this.EyeType == "Gentle")
		{
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.MyRenderer.SetBlendShapeWeight(12, 100f);
			this.StudentManager.Gentles++;
			num = this.StudentManager.Gentles;
		}
		else if (this.EyeType == "MO")
		{
			this.MyRenderer.SetBlendShapeWeight(8, 50f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.MyRenderer.SetBlendShapeWeight(12, 100f);
			this.StudentManager.Gentles++;
			num = this.StudentManager.Gentles;
		}
		else if (this.EyeType == "Rival1")
		{
			this.MyRenderer.SetBlendShapeWeight(8, 5f);
			this.MyRenderer.SetBlendShapeWeight(9, 20f);
			this.MyRenderer.SetBlendShapeWeight(10, 50f);
			this.MyRenderer.SetBlendShapeWeight(11, 50f);
			this.MyRenderer.SetBlendShapeWeight(12, 10f);
			this.StudentManager.Rival1s++;
			num = this.StudentManager.Rival1s;
		}
		if (!this.Modified)
		{
			if ((this.EyeType == "Thin" && this.StudentManager.Thins > 1) || (this.EyeType == "Serious" && this.StudentManager.Seriouses > 1) || (this.EyeType == "Round" && this.StudentManager.Rounds > 1) || (this.EyeType == "Sad" && this.StudentManager.Sads > 1) || (this.EyeType == "Mean" && this.StudentManager.Means > 1) || (this.EyeType == "Smug" && this.StudentManager.Smugs > 1) || (this.EyeType == "Gentle" && this.StudentManager.Gentles > 1))
			{
				this.MyRenderer.SetBlendShapeWeight(8, this.MyRenderer.GetBlendShapeWeight(8) + (float)num);
				this.MyRenderer.SetBlendShapeWeight(9, this.MyRenderer.GetBlendShapeWeight(9) + (float)num);
				this.MyRenderer.SetBlendShapeWeight(10, this.MyRenderer.GetBlendShapeWeight(10) + (float)num);
				this.MyRenderer.SetBlendShapeWeight(12, this.MyRenderer.GetBlendShapeWeight(12) + (float)num);
			}
			this.Modified = true;
		}
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x00091938 File Offset: 0x0008FB38
	public void DeactivateBullyAccessories()
	{
		if (StudentGlobals.FemaleUniform < 2 || StudentGlobals.FemaleUniform == 3)
		{
			this.RightWristband.SetActive(false);
			this.LeftWristband.SetActive(false);
		}
		this.Bookbag.SetActive(false);
		this.Hoodie.SetActive(false);
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x00091988 File Offset: 0x0008FB88
	public void ActivateBullyAccessories()
	{
		if (StudentGlobals.FemaleUniform < 2 || StudentGlobals.FemaleUniform == 3)
		{
			this.RightWristband.SetActive(true);
			this.LeftWristband.SetActive(true);
		}
		this.Bookbag.SetActive(true);
		this.Hoodie.SetActive(true);
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x000919D8 File Offset: 0x0008FBD8
	public void LoadCosmeticSheet(StudentCosmeticSheet mySheet)
	{
		if (this.Male != mySheet.Male)
		{
			return;
		}
		this.Accessory = mySheet.Accessory;
		this.Hairstyle = mySheet.Hairstyle;
		this.Stockings = mySheet.Stockings;
		this.BreastSize = mySheet.BreastSize;
		this.Start();
		this.ColorValue = mySheet.HairColor;
		this.HairRenderer.material.color = this.ColorValue;
		if (mySheet.CustomHair)
		{
			this.RightEyeRenderer.material.mainTexture = this.HairRenderer.material.mainTexture;
			this.LeftEyeRenderer.material.mainTexture = this.HairRenderer.material.mainTexture;
			this.FaceTexture = this.HairRenderer.material.mainTexture;
			this.LeftIrisLight.SetActive(false);
			this.RightIrisLight.SetActive(false);
			this.CustomHair = true;
		}
		this.CorrectColor = mySheet.EyeColor;
		this.RightEyeRenderer.material.color = this.CorrectColor;
		this.LeftEyeRenderer.material.color = this.CorrectColor;
		this.Student.Schoolwear = mySheet.Schoolwear;
		this.Student.ChangeSchoolwear();
		if (mySheet.Bloody)
		{
			this.Student.LiquidProjector.material.mainTexture = this.Student.BloodTexture;
			this.Student.LiquidProjector.enabled = true;
		}
		if (!this.Male)
		{
			this.Stockings = mySheet.Stockings;
			base.StartCoroutine(this.Student.Cosmetic.PutOnStockings());
			for (int i = 0; i < this.MyRenderer.sharedMesh.blendShapeCount; i++)
			{
				this.MyRenderer.SetBlendShapeWeight(i, mySheet.Blendshapes[i]);
			}
		}
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x00091BB4 File Offset: 0x0008FDB4
	public StudentCosmeticSheet CosmeticSheet()
	{
		StudentCosmeticSheet studentCosmeticSheet = default(StudentCosmeticSheet);
		studentCosmeticSheet.Blendshapes = new List<float>();
		studentCosmeticSheet.Male = this.Male;
		studentCosmeticSheet.CustomHair = this.CustomHair;
		studentCosmeticSheet.Accessory = this.Accessory;
		studentCosmeticSheet.Hairstyle = this.Hairstyle;
		studentCosmeticSheet.Stockings = this.Stockings;
		studentCosmeticSheet.BreastSize = this.BreastSize;
		studentCosmeticSheet.CustomHair = this.CustomHair;
		studentCosmeticSheet.Schoolwear = this.Student.Schoolwear;
		studentCosmeticSheet.Bloody = (this.Student.LiquidProjector.enabled && this.Student.LiquidProjector.material.mainTexture == this.Student.BloodTexture);
		studentCosmeticSheet.HairColor = this.HairRenderer.material.color;
		studentCosmeticSheet.EyeColor = this.RightEyeRenderer.material.color;
		if (!this.Male)
		{
			for (int i = 0; i < this.MyRenderer.sharedMesh.blendShapeCount; i++)
			{
				studentCosmeticSheet.Blendshapes.Add(this.MyRenderer.GetBlendShapeWeight(i));
			}
		}
		return studentCosmeticSheet;
	}

	// Token: 0x040016F4 RID: 5876
	public StudentManagerScript StudentManager;

	// Token: 0x040016F5 RID: 5877
	public TextureManagerScript TextureManager;

	// Token: 0x040016F6 RID: 5878
	public SkinnedMeshUpdater SkinUpdater;

	// Token: 0x040016F7 RID: 5879
	public LoveManagerScript LoveManager;

	// Token: 0x040016F8 RID: 5880
	public Animation CharacterAnimation;

	// Token: 0x040016F9 RID: 5881
	public ModelSwapScript ModelSwap;

	// Token: 0x040016FA RID: 5882
	public StudentScript Student;

	// Token: 0x040016FB RID: 5883
	public JsonScript JSON;

	// Token: 0x040016FC RID: 5884
	public GameObject[] TeacherAccessories;

	// Token: 0x040016FD RID: 5885
	public GameObject[] FemaleAccessories;

	// Token: 0x040016FE RID: 5886
	public GameObject[] MaleAccessories;

	// Token: 0x040016FF RID: 5887
	public GameObject[] ClubAccessories;

	// Token: 0x04001700 RID: 5888
	public GameObject[] PunkAccessories;

	// Token: 0x04001701 RID: 5889
	public GameObject[] RightStockings;

	// Token: 0x04001702 RID: 5890
	public GameObject[] LeftStockings;

	// Token: 0x04001703 RID: 5891
	public GameObject[] PhoneCharms;

	// Token: 0x04001704 RID: 5892
	public GameObject[] TeacherHair;

	// Token: 0x04001705 RID: 5893
	public GameObject[] FacialHair;

	// Token: 0x04001706 RID: 5894
	public GameObject[] FemaleHair;

	// Token: 0x04001707 RID: 5895
	public GameObject[] MusicNotes;

	// Token: 0x04001708 RID: 5896
	public GameObject[] Kerchiefs;

	// Token: 0x04001709 RID: 5897
	public GameObject[] CatGifts;

	// Token: 0x0400170A RID: 5898
	public GameObject[] MaleHair;

	// Token: 0x0400170B RID: 5899
	public GameObject[] RedCloth;

	// Token: 0x0400170C RID: 5900
	public GameObject[] Scanners;

	// Token: 0x0400170D RID: 5901
	public GameObject[] Eyewear;

	// Token: 0x0400170E RID: 5902
	public GameObject[] Goggles;

	// Token: 0x0400170F RID: 5903
	public GameObject[] Flowers;

	// Token: 0x04001710 RID: 5904
	public GameObject[] Roses;

	// Token: 0x04001711 RID: 5905
	public Renderer[] TeacherHairRenderers;

	// Token: 0x04001712 RID: 5906
	public Renderer[] FacialHairRenderers;

	// Token: 0x04001713 RID: 5907
	public Renderer[] FemaleHairRenderers;

	// Token: 0x04001714 RID: 5908
	public Renderer[] MaleHairRenderers;

	// Token: 0x04001715 RID: 5909
	public Renderer[] Fingernails;

	// Token: 0x04001716 RID: 5910
	public Texture[] GanguroSwimsuitTextures;

	// Token: 0x04001717 RID: 5911
	public Texture[] GanguroUniformTextures;

	// Token: 0x04001718 RID: 5912
	public Texture[] GanguroCasualTextures;

	// Token: 0x04001719 RID: 5913
	public Texture[] GanguroSocksTextures;

	// Token: 0x0400171A RID: 5914
	public Texture[] ObstacleUniformTextures;

	// Token: 0x0400171B RID: 5915
	public Texture[] ObstacleCasualTextures;

	// Token: 0x0400171C RID: 5916
	public Texture[] ObstacleSocksTextures;

	// Token: 0x0400171D RID: 5917
	public Texture[] OccultUniformTextures;

	// Token: 0x0400171E RID: 5918
	public Texture[] OccultCasualTextures;

	// Token: 0x0400171F RID: 5919
	public Texture[] OccultSocksTextures;

	// Token: 0x04001720 RID: 5920
	public Texture[] FemaleUniformTextures;

	// Token: 0x04001721 RID: 5921
	public Texture[] FemaleCasualTextures;

	// Token: 0x04001722 RID: 5922
	public Texture[] FemaleSocksTextures;

	// Token: 0x04001723 RID: 5923
	public Texture[] MaleUniformTextures;

	// Token: 0x04001724 RID: 5924
	public Texture[] MaleCasualTextures;

	// Token: 0x04001725 RID: 5925
	public Texture[] MaleSocksTextures;

	// Token: 0x04001726 RID: 5926
	public Texture[] SmartphoneTextures;

	// Token: 0x04001727 RID: 5927
	public Texture[] HoodieTextures;

	// Token: 0x04001728 RID: 5928
	public Texture[] FaceTextures;

	// Token: 0x04001729 RID: 5929
	public Texture[] SkinTextures;

	// Token: 0x0400172A RID: 5930
	public Texture[] WristwearTextures;

	// Token: 0x0400172B RID: 5931
	public Texture[] CardiganTextures;

	// Token: 0x0400172C RID: 5932
	public Texture[] BookbagTextures;

	// Token: 0x0400172D RID: 5933
	public Texture[] EyeTextures;

	// Token: 0x0400172E RID: 5934
	public Texture[] CheekTextures;

	// Token: 0x0400172F RID: 5935
	public Texture[] ForeheadTextures;

	// Token: 0x04001730 RID: 5936
	public Texture[] MouthTextures;

	// Token: 0x04001731 RID: 5937
	public Texture[] NoseTextures;

	// Token: 0x04001732 RID: 5938
	public Texture[] ApronTextures;

	// Token: 0x04001733 RID: 5939
	public Texture[] CanTextures;

	// Token: 0x04001734 RID: 5940
	public Texture[] Trunks;

	// Token: 0x04001735 RID: 5941
	public Texture[] MusicStockings;

	// Token: 0x04001736 RID: 5942
	public Mesh[] FemaleUniforms;

	// Token: 0x04001737 RID: 5943
	public Mesh[] MaleUniforms;

	// Token: 0x04001738 RID: 5944
	public Mesh[] Berets;

	// Token: 0x04001739 RID: 5945
	public Color[] BullyColor;

	// Token: 0x0400173A RID: 5946
	public SkinnedMeshRenderer CardiganRenderer;

	// Token: 0x0400173B RID: 5947
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x0400173C RID: 5948
	public Renderer FacialHairRenderer;

	// Token: 0x0400173D RID: 5949
	public Renderer RightEyeRenderer;

	// Token: 0x0400173E RID: 5950
	public Renderer LeftEyeRenderer;

	// Token: 0x0400173F RID: 5951
	public Renderer HoodieRenderer;

	// Token: 0x04001740 RID: 5952
	public Renderer ScarfRenderer;

	// Token: 0x04001741 RID: 5953
	public Renderer HairRenderer;

	// Token: 0x04001742 RID: 5954
	public Renderer CanRenderer;

	// Token: 0x04001743 RID: 5955
	public Mesh DelinquentMesh;

	// Token: 0x04001744 RID: 5956
	public Mesh SchoolUniform;

	// Token: 0x04001745 RID: 5957
	public Texture DefaultFaceTexture;

	// Token: 0x04001746 RID: 5958
	public Texture TeacherBodyTexture;

	// Token: 0x04001747 RID: 5959
	public Texture CoachPaleBodyTexture;

	// Token: 0x04001748 RID: 5960
	public Texture CoachBodyTexture;

	// Token: 0x04001749 RID: 5961
	public Texture CoachFaceTexture;

	// Token: 0x0400174A RID: 5962
	public Texture UniformTexture;

	// Token: 0x0400174B RID: 5963
	public Texture CasualTexture;

	// Token: 0x0400174C RID: 5964
	public Texture SocksTexture;

	// Token: 0x0400174D RID: 5965
	public Texture FaceTexture;

	// Token: 0x0400174E RID: 5966
	public Texture PurpleStockings;

	// Token: 0x0400174F RID: 5967
	public Texture YellowStockings;

	// Token: 0x04001750 RID: 5968
	public Texture BlackStockings;

	// Token: 0x04001751 RID: 5969
	public Texture GreenStockings;

	// Token: 0x04001752 RID: 5970
	public Texture BlueStockings;

	// Token: 0x04001753 RID: 5971
	public Texture CyanStockings;

	// Token: 0x04001754 RID: 5972
	public Texture RedStockings;

	// Token: 0x04001755 RID: 5973
	public Texture BlackKneeSocks;

	// Token: 0x04001756 RID: 5974
	public Texture GreenSocks;

	// Token: 0x04001757 RID: 5975
	public Texture KizanaStockings;

	// Token: 0x04001758 RID: 5976
	public Texture OsanaStockings;

	// Token: 0x04001759 RID: 5977
	public Texture TurtleStockings;

	// Token: 0x0400175A RID: 5978
	public Texture TigerStockings;

	// Token: 0x0400175B RID: 5979
	public Texture BirdStockings;

	// Token: 0x0400175C RID: 5980
	public Texture DragonStockings;

	// Token: 0x0400175D RID: 5981
	public Texture[] CustomStockings;

	// Token: 0x0400175E RID: 5982
	public Texture MyStockings;

	// Token: 0x0400175F RID: 5983
	public Texture BlackBody;

	// Token: 0x04001760 RID: 5984
	public Texture BlackFace;

	// Token: 0x04001761 RID: 5985
	public Texture GrayFace;

	// Token: 0x04001762 RID: 5986
	public Texture DelinquentUniformTexture;

	// Token: 0x04001763 RID: 5987
	public Texture DelinquentCasualTexture;

	// Token: 0x04001764 RID: 5988
	public Texture DelinquentSocksTexture;

	// Token: 0x04001765 RID: 5989
	public Texture OsanaSwimsuitTexture;

	// Token: 0x04001766 RID: 5990
	public Texture ObstacleSwimsuitTexture;

	// Token: 0x04001767 RID: 5991
	public Texture ObstacleTowelTexture;

	// Token: 0x04001768 RID: 5992
	public Texture ObstacleGymTexture;

	// Token: 0x04001769 RID: 5993
	public Texture TanSwimsuitTexture;

	// Token: 0x0400176A RID: 5994
	public Texture TanTowelTexture;

	// Token: 0x0400176B RID: 5995
	public Texture TanGymTexture;

	// Token: 0x0400176C RID: 5996
	public GameObject RightIrisLight;

	// Token: 0x0400176D RID: 5997
	public GameObject LeftIrisLight;

	// Token: 0x0400176E RID: 5998
	public GameObject RightWristband;

	// Token: 0x0400176F RID: 5999
	public GameObject LeftWristband;

	// Token: 0x04001770 RID: 6000
	public GameObject Cardigan;

	// Token: 0x04001771 RID: 6001
	public GameObject Bookbag;

	// Token: 0x04001772 RID: 6002
	public GameObject ThickBrows;

	// Token: 0x04001773 RID: 6003
	public GameObject Character;

	// Token: 0x04001774 RID: 6004
	public GameObject RightShoe;

	// Token: 0x04001775 RID: 6005
	public GameObject LeftShoe;

	// Token: 0x04001776 RID: 6006
	public GameObject SadBrows;

	// Token: 0x04001777 RID: 6007
	public GameObject Armband;

	// Token: 0x04001778 RID: 6008
	public GameObject Hoodie;

	// Token: 0x04001779 RID: 6009
	public GameObject Tongue;

	// Token: 0x0400177A RID: 6010
	public Transform RightBreast;

	// Token: 0x0400177B RID: 6011
	public Transform LeftBreast;

	// Token: 0x0400177C RID: 6012
	public Transform RightTemple;

	// Token: 0x0400177D RID: 6013
	public Transform LeftTemple;

	// Token: 0x0400177E RID: 6014
	public Transform Head;

	// Token: 0x0400177F RID: 6015
	public Transform Neck;

	// Token: 0x04001780 RID: 6016
	public Color CorrectColor;

	// Token: 0x04001781 RID: 6017
	public Color ColorValue;

	// Token: 0x04001782 RID: 6018
	public Mesh TeacherMesh;

	// Token: 0x04001783 RID: 6019
	public Mesh CoachMesh;

	// Token: 0x04001784 RID: 6020
	public Mesh NurseMesh;

	// Token: 0x04001785 RID: 6021
	public bool MysteriousObstacle;

	// Token: 0x04001786 RID: 6022
	public bool DoNotChangeFace;

	// Token: 0x04001787 RID: 6023
	public bool TakingPortrait;

	// Token: 0x04001788 RID: 6024
	public bool Initialized;

	// Token: 0x04001789 RID: 6025
	public bool CustomEyes;

	// Token: 0x0400178A RID: 6026
	public bool CustomHair;

	// Token: 0x0400178B RID: 6027
	public bool LookCamera;

	// Token: 0x0400178C RID: 6028
	public bool HomeScene;

	// Token: 0x0400178D RID: 6029
	public bool Kidnapped;

	// Token: 0x0400178E RID: 6030
	public bool Randomize;

	// Token: 0x0400178F RID: 6031
	public bool Cutscene;

	// Token: 0x04001790 RID: 6032
	public bool Modified;

	// Token: 0x04001791 RID: 6033
	public bool TurnedOn;

	// Token: 0x04001792 RID: 6034
	public bool Teacher;

	// Token: 0x04001793 RID: 6035
	public bool Yandere;

	// Token: 0x04001794 RID: 6036
	public bool Empty;

	// Token: 0x04001795 RID: 6037
	public bool Male;

	// Token: 0x04001796 RID: 6038
	public float BreastSize;

	// Token: 0x04001797 RID: 6039
	public string OriginalStockings = string.Empty;

	// Token: 0x04001798 RID: 6040
	public string HairColor = string.Empty;

	// Token: 0x04001799 RID: 6041
	public string Stockings = string.Empty;

	// Token: 0x0400179A RID: 6042
	public string EyeColor = string.Empty;

	// Token: 0x0400179B RID: 6043
	public string EyeType = string.Empty;

	// Token: 0x0400179C RID: 6044
	public string Name = string.Empty;

	// Token: 0x0400179D RID: 6045
	public int FacialHairstyle;

	// Token: 0x0400179E RID: 6046
	public int Accessory;

	// Token: 0x0400179F RID: 6047
	public int Direction;

	// Token: 0x040017A0 RID: 6048
	public int Hairstyle;

	// Token: 0x040017A1 RID: 6049
	public int SkinColor;

	// Token: 0x040017A2 RID: 6050
	public int StudentID;

	// Token: 0x040017A3 RID: 6051
	public int EyewearID;

	// Token: 0x040017A4 RID: 6052
	public ClubType Club;

	// Token: 0x040017A5 RID: 6053
	public int ID;

	// Token: 0x040017A6 RID: 6054
	public GameObject[] GaloAccessories;

	// Token: 0x040017A7 RID: 6055
	public Material[] NurseMaterials;

	// Token: 0x040017A8 RID: 6056
	public GameObject CardiganPrefab;

	// Token: 0x040017A9 RID: 6057
	public int FaceID;

	// Token: 0x040017AA RID: 6058
	public int SkinID;

	// Token: 0x040017AB RID: 6059
	public int UniformID;
}

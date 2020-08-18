using System;
using UnityEngine;

// Token: 0x0200032E RID: 814
public class MemorialSceneScript : MonoBehaviour
{
	// Token: 0x0600181B RID: 6171 RVA: 0x000D7024 File Offset: 0x000D5224
	private void Start()
	{
		this.MemorialStudents = StudentGlobals.MemorialStudents;
		if (this.MemorialStudents % 2 == 0)
		{
			this.CanvasGroup.transform.localPosition = new Vector3(-0.5f, 0f, -2f);
		}
		int num = 0;
		int i;
		for (i = 1; i < 10; i++)
		{
			this.Canvases[i].SetActive(false);
		}
		i = 0;
		while (this.MemorialStudents > 0)
		{
			i++;
			this.Canvases[i].SetActive(true);
			if (this.MemorialStudents == 1)
			{
				num = StudentGlobals.MemorialStudent1;
			}
			else if (this.MemorialStudents == 2)
			{
				num = StudentGlobals.MemorialStudent2;
			}
			else if (this.MemorialStudents == 3)
			{
				num = StudentGlobals.MemorialStudent3;
			}
			else if (this.MemorialStudents == 4)
			{
				num = StudentGlobals.MemorialStudent4;
			}
			else if (this.MemorialStudents == 5)
			{
				num = StudentGlobals.MemorialStudent5;
			}
			else if (this.MemorialStudents == 6)
			{
				num = StudentGlobals.MemorialStudent6;
			}
			else if (this.MemorialStudents == 7)
			{
				num = StudentGlobals.MemorialStudent7;
			}
			else if (this.MemorialStudents == 8)
			{
				num = StudentGlobals.MemorialStudent8;
			}
			else if (this.MemorialStudents == 9)
			{
				num = StudentGlobals.MemorialStudent9;
			}
			WWW www = new WWW(string.Concat(new string[]
			{
				"file:///",
				Application.streamingAssetsPath,
				"/Portraits/Student_",
				num.ToString(),
				".png"
			}));
			this.Portraits[i].mainTexture = www.texture;
			this.MemorialStudents--;
		}
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x000D71A4 File Offset: 0x000D53A4
	private void Update()
	{
		this.Speed += Time.deltaTime;
		if (this.Speed > 1f)
		{
			if (!this.Eulogized)
			{
				this.StudentManager.Yandere.Subtitle.UpdateLabel(SubtitleType.Eulogy, 0, 8f);
				this.StudentManager.Yandere.PromptBar.Label[0].text = "Continue";
				this.StudentManager.Yandere.PromptBar.UpdateButtons();
				this.StudentManager.Yandere.PromptBar.Show = true;
				this.Eulogized = true;
			}
			this.StudentManager.MainCamera.position = Vector3.Lerp(this.StudentManager.MainCamera.position, new Vector3(38f, 4.125f, 68.825f), (this.Speed - 1f) * Time.deltaTime * 0.15f);
			if (Input.GetButtonDown("A"))
			{
				this.StudentManager.Yandere.PromptBar.Show = false;
				this.FadeOut = true;
			}
		}
		if (this.FadeOut)
		{
			this.StudentManager.Clock.BloomEffect.bloomIntensity += Time.deltaTime * 10f;
			if (this.StudentManager.Clock.BloomEffect.bloomIntensity > 10f)
			{
				this.StudentManager.Yandere.Casual = !this.StudentManager.Yandere.Casual;
				this.StudentManager.Yandere.ChangeSchoolwear();
				this.StudentManager.Yandere.transform.position = new Vector3(12f, 0f, 72f);
				this.StudentManager.Yandere.transform.eulerAngles = new Vector3(0f, -90f, 0f);
				this.StudentManager.Yandere.HeartCamera.enabled = true;
				this.StudentManager.Yandere.RPGCamera.enabled = true;
				this.StudentManager.Yandere.CanMove = true;
				this.StudentManager.Yandere.HUD.alpha = 1f;
				this.StudentManager.Clock.UpdateBloom = true;
				this.StudentManager.Clock.StopTime = false;
				this.StudentManager.Clock.PresentTime = 450f;
				this.StudentManager.Clock.HourTime = 7.5f;
				this.StudentManager.Unstop();
				this.StudentManager.SkipTo8();
				this.Headmaster.SetActive(false);
				this.Counselor.SetActive(false);
				base.enabled = false;
			}
		}
	}

	// Token: 0x040022E7 RID: 8935
	public StudentManagerScript StudentManager;

	// Token: 0x040022E8 RID: 8936
	public GameObject[] Canvases;

	// Token: 0x040022E9 RID: 8937
	public UITexture[] Portraits;

	// Token: 0x040022EA RID: 8938
	public GameObject CanvasGroup;

	// Token: 0x040022EB RID: 8939
	public GameObject Headmaster;

	// Token: 0x040022EC RID: 8940
	public GameObject Counselor;

	// Token: 0x040022ED RID: 8941
	public int MemorialStudents;

	// Token: 0x040022EE RID: 8942
	public float Speed;

	// Token: 0x040022EF RID: 8943
	public bool Eulogized;

	// Token: 0x040022F0 RID: 8944
	public bool FadeOut;
}

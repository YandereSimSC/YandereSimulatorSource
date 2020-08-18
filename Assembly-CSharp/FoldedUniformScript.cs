using System;
using UnityEngine;

// Token: 0x020002A3 RID: 675
public class FoldedUniformScript : MonoBehaviour
{
	// Token: 0x0600140B RID: 5131 RVA: 0x000AF750 File Offset: 0x000AD950
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		bool flag = false;
		if (this.Spare && !GameGlobals.SpareUniform)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			flag = true;
		}
		if (!flag && this.Clean && this.Prompt.Button[0] != null)
		{
			this.Prompt.HideButton[0] = true;
			this.Yandere.StudentManager.NewUniforms++;
			this.Yandere.StudentManager.UpdateStudents(0);
			this.Yandere.StudentManager.Uniforms[this.Yandere.StudentManager.NewUniforms] = base.transform;
			Debug.Log("A new uniform has appeared. There are now " + this.Yandere.StudentManager.NewUniforms + " new uniforms at school.");
		}
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x000AF840 File Offset: 0x000ADA40
	private void Update()
	{
		if (this.Clean)
		{
			this.InPosition = this.Yandere.StudentManager.LockerRoomArea.bounds.Contains(base.transform.position);
			if (this.Yandere.MyRenderer.sharedMesh == this.Yandere.Towel)
			{
				Debug.Log("Yandere-chan is wearing a towel.");
			}
			if (this.Yandere.Bloodiness == 0f)
			{
				Debug.Log("Yandere-chan is not bloody.");
			}
			if (this.InPosition)
			{
				Debug.Log("This uniform is in the locker room.");
			}
			if (this.Yandere.MyRenderer.sharedMesh != this.Yandere.Towel || this.Yandere.Bloodiness != 0f || !this.InPosition)
			{
				this.Prompt.HideButton[0] = true;
			}
			else
			{
				this.Prompt.HideButton[0] = false;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.SteamCloud, this.Yandere.transform.position + Vector3.up * 0.81f, Quaternion.identity);
				this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_stripping_00");
				this.Yandere.CurrentUniformOrigin = 2;
				this.Yandere.Stripping = true;
				this.Yandere.CanMove = false;
				this.Timer += Time.deltaTime;
			}
			if (this.Timer > 0f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 1.5f)
				{
					this.Yandere.Schoolwear = 1;
					this.Yandere.ChangeSchoolwear();
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x04001C34 RID: 7220
	public YandereScript Yandere;

	// Token: 0x04001C35 RID: 7221
	public PromptScript Prompt;

	// Token: 0x04001C36 RID: 7222
	public GameObject SteamCloud;

	// Token: 0x04001C37 RID: 7223
	public bool InPosition = true;

	// Token: 0x04001C38 RID: 7224
	public bool Clean;

	// Token: 0x04001C39 RID: 7225
	public bool Spare;

	// Token: 0x04001C3A RID: 7226
	public float Timer;

	// Token: 0x04001C3B RID: 7227
	public int Type;
}

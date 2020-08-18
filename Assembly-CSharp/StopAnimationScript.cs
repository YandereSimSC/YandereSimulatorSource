using System;
using UnityEngine;

// Token: 0x020003F8 RID: 1016
public class StopAnimationScript : MonoBehaviour
{
	// Token: 0x06001AEE RID: 6894 RVA: 0x0010EF60 File Offset: 0x0010D160
	private void Start()
	{
		this.StudentManager = GameObject.Find("StudentManager").GetComponent<StudentManagerScript>();
		this.Anim = base.GetComponent<Animation>();
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x0010EF84 File Offset: 0x0010D184
	private void Update()
	{
		if (this.StudentManager.DisableFarAnims)
		{
			if (Vector3.Distance(this.Yandere.position, base.transform.position) > 15f)
			{
				if (this.Anim.enabled)
				{
					this.Anim.enabled = false;
					return;
				}
			}
			else if (!this.Anim.enabled)
			{
				this.Anim.enabled = true;
				return;
			}
		}
		else if (!this.Anim.enabled)
		{
			this.Anim.enabled = true;
		}
	}

	// Token: 0x04002BB1 RID: 11185
	public StudentManagerScript StudentManager;

	// Token: 0x04002BB2 RID: 11186
	public Transform Yandere;

	// Token: 0x04002BB3 RID: 11187
	private Animation Anim;
}

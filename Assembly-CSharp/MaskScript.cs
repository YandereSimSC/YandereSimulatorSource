using System;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class MaskScript : MonoBehaviour
{
	// Token: 0x06001809 RID: 6153 RVA: 0x000D6234 File Offset: 0x000D4434
	private void Start()
	{
		if (GameGlobals.MasksBanned)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			this.MyFilter.mesh = this.Meshes[this.ID];
			this.MyRenderer.material.mainTexture = this.Textures[this.ID];
		}
		base.enabled = false;
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x000D6294 File Offset: 0x000D4494
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.StudentManager.CanAnyoneSeeYandere();
			if (!this.StudentManager.YandereVisible && !this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				Rigidbody component = base.GetComponent<Rigidbody>();
				component.useGravity = false;
				component.isKinematic = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Prompt.MyCollider.enabled = false;
				base.transform.parent = this.Yandere.Head;
				base.transform.localPosition = new Vector3(0f, 0.033333f, 0.1f);
				base.transform.localEulerAngles = Vector3.zero;
				this.Yandere.Mask = this;
				this.ClubManager.UpdateMasks();
				this.StudentManager.UpdateStudents(0);
				return;
			}
			this.Yandere.NotificationManager.CustomText = "Not now. Too suspicious.";
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
		}
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x000D63DC File Offset: 0x000D45DC
	public void Drop()
	{
		this.Prompt.MyCollider.isTrigger = false;
		this.Prompt.MyCollider.enabled = true;
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.useGravity = true;
		component.isKinematic = false;
		this.Prompt.enabled = true;
		base.transform.parent = null;
		this.Yandere.Mask = null;
		this.ClubManager.UpdateMasks();
		this.StudentManager.UpdateStudents(0);
	}

	// Token: 0x040022C1 RID: 8897
	public StudentManagerScript StudentManager;

	// Token: 0x040022C2 RID: 8898
	public ClubManagerScript ClubManager;

	// Token: 0x040022C3 RID: 8899
	public YandereScript Yandere;

	// Token: 0x040022C4 RID: 8900
	public PromptScript Prompt;

	// Token: 0x040022C5 RID: 8901
	public PickUpScript PickUp;

	// Token: 0x040022C6 RID: 8902
	public Projector Blood;

	// Token: 0x040022C7 RID: 8903
	public Renderer MyRenderer;

	// Token: 0x040022C8 RID: 8904
	public MeshFilter MyFilter;

	// Token: 0x040022C9 RID: 8905
	public Texture[] Textures;

	// Token: 0x040022CA RID: 8906
	public Mesh[] Meshes;

	// Token: 0x040022CB RID: 8907
	public int ID;
}

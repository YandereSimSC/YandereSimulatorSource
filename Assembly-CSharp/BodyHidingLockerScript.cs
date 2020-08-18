using System;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class BodyHidingLockerScript : MonoBehaviour
{
	// Token: 0x06000A64 RID: 2660 RVA: 0x000556E4 File Offset: 0x000538E4
	private void Update()
	{
		if (this.Rotation != 0f)
		{
			this.Speed += Time.deltaTime * 100f;
			this.Rotation = Mathf.MoveTowards(this.Rotation, 0f, this.Speed * Time.deltaTime);
			if (this.Rotation > -1f)
			{
				AudioSource.PlayClipAtPoint(this.LockerClose, this.Prompt.Yandere.MainCamera.transform.position);
				this.Corpse.gameObject.SetActive(false);
				base.enabled = false;
				this.Rotation = 0f;
				this.Speed = 0f;
			}
			this.Door.transform.localEulerAngles = new Vector3(0f, this.Rotation, 0f);
		}
		if (this.Prompt.Yandere.Carrying || this.Prompt.Yandere.Dragging)
		{
			this.Prompt.enabled = true;
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				AudioSource.PlayClipAtPoint(this.LockerOpen, this.Prompt.Yandere.MainCamera.transform.position);
				this.Corpse = this.Prompt.Yandere.CurrentRagdoll;
				this.Prompt.Yandere.EmptyHands();
				this.Prompt.Yandere.NearBodies = 0;
				this.Prompt.Yandere.NearestCorpseID = 0;
				this.Prompt.Yandere.CorpseWarning = false;
				this.Prompt.Yandere.StudentManager.UpdateStudents(0);
				this.Corpse.Student.CharacterAnimation.Play("f02_lockerPose_00");
				this.Corpse.transform.parent = base.transform;
				this.Corpse.transform.position = base.transform.position + new Vector3(0f, 0.1f, 0f);
				this.Corpse.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
				this.Corpse.DisableRigidbodies();
				this.Corpse.enabled = false;
				this.Corpse.Hidden = true;
				this.Rotation = -180f;
				return;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x04000AB2 RID: 2738
	public RagdollScript Corpse;

	// Token: 0x04000AB3 RID: 2739
	public PromptScript Prompt;

	// Token: 0x04000AB4 RID: 2740
	public AudioClip LockerClose;

	// Token: 0x04000AB5 RID: 2741
	public AudioClip LockerOpen;

	// Token: 0x04000AB6 RID: 2742
	public float Rotation;

	// Token: 0x04000AB7 RID: 2743
	public float Speed;

	// Token: 0x04000AB8 RID: 2744
	public Transform Door;
}

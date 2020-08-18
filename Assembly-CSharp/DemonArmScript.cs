using System;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class DemonArmScript : MonoBehaviour
{
	// Token: 0x06001329 RID: 4905 RVA: 0x0009F950 File Offset: 0x0009DB50
	private void Start()
	{
		this.MyAnimation = base.GetComponent<Animation>();
		if (!this.Rising)
		{
			this.MyAnimation[this.IdleAnim].speed = this.AnimSpeed * 0.5f;
		}
		this.MyAnimation[this.AttackAnim].speed = 0f;
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x0009F9B0 File Offset: 0x0009DBB0
	private void Update()
	{
		if (!this.Rising)
		{
			if (!this.Attacking)
			{
				this.MyAnimation.CrossFade(this.IdleAnim);
				return;
			}
			this.AnimTime += 0.016666668f;
			this.MyAnimation[this.AttackAnim].time = this.AnimTime;
			if (!this.Attacked)
			{
				if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length * 0.15f)
				{
					this.ClawCollider.enabled = true;
					this.Attacked = true;
					return;
				}
			}
			else
			{
				if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length * 0.4f)
				{
					this.ClawCollider.enabled = false;
				}
				if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length)
				{
					this.MyAnimation.CrossFade(this.IdleAnim);
					this.ClawCollider.enabled = false;
					this.Attacking = false;
					this.Attacked = false;
					this.AnimTime = 0f;
					return;
				}
			}
		}
		else if (this.MyAnimation[this.AttackAnim].time > this.MyAnimation[this.AttackAnim].length)
		{
			this.Rising = false;
		}
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x0009FB40 File Offset: 0x0009DD40
	private void OnTriggerEnter(Collider other)
	{
		StudentScript component = other.gameObject.GetComponent<StudentScript>();
		if (component != null && component.StudentID > 1)
		{
			AudioSource component2 = base.GetComponent<AudioSource>();
			component2.clip = this.Whoosh;
			component2.pitch = UnityEngine.Random.Range(-0.9f, 1.1f);
			component2.Play();
			base.GetComponent<Animation>().CrossFade(this.AttackAnim);
			this.Attacking = true;
		}
	}

	// Token: 0x040019C3 RID: 6595
	public GameObject DismembermentCollider;

	// Token: 0x040019C4 RID: 6596
	public Animation MyAnimation;

	// Token: 0x040019C5 RID: 6597
	public Collider ClawCollider;

	// Token: 0x040019C6 RID: 6598
	public bool Attacking;

	// Token: 0x040019C7 RID: 6599
	public bool Attacked;

	// Token: 0x040019C8 RID: 6600
	public bool Rising = true;

	// Token: 0x040019C9 RID: 6601
	public string IdleAnim = "DemonArmIdle";

	// Token: 0x040019CA RID: 6602
	public string AttackAnim = "DemonArmAttack";

	// Token: 0x040019CB RID: 6603
	public AudioClip Whoosh;

	// Token: 0x040019CC RID: 6604
	public float AnimSpeed = 1f;

	// Token: 0x040019CD RID: 6605
	public float AnimTime;
}

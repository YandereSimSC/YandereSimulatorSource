using System;
using UnityEngine;

// Token: 0x02000488 RID: 1160
public class YanvaniaWitchScript : MonoBehaviour
{
	// Token: 0x06001DD8 RID: 7640 RVA: 0x00174288 File Offset: 0x00172488
	private void Update()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (this.AttackTimer < 10f)
		{
			this.AttackTimer += Time.deltaTime;
			if (this.AttackTimer > 0.8f && !this.CastSpell)
			{
				this.CastSpell = true;
				UnityEngine.Object.Instantiate<GameObject>(this.BlackHole, base.transform.position + Vector3.up * 3f + Vector3.right * 6f, Quaternion.identity);
				UnityEngine.Object.Instantiate<GameObject>(this.GroundImpact, base.transform.position + Vector3.right * 1.15f, Quaternion.identity);
			}
			if (component["Staff Spell Ground"].time >= component["Staff Spell Ground"].length)
			{
				component.CrossFade("Staff Stance");
				this.Casting = false;
			}
		}
		else if (Vector3.Distance(base.transform.position, this.Yanmont.transform.position) < 5f)
		{
			this.AttackTimer = 0f;
			this.Casting = true;
			this.CastSpell = false;
			component["Staff Spell Ground"].time = 0f;
			component.CrossFade("Staff Spell Ground");
		}
		if (!this.Casting && component["Receive Damage"].time >= component["Receive Damage"].length)
		{
			component.CrossFade("Staff Stance");
		}
		this.HitReactTimer += Time.deltaTime * 10f;
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x0017443C File Offset: 0x0017263C
	private void OnTriggerEnter(Collider other)
	{
		if (this.HP > 0f)
		{
			if (other.gameObject.tag == "Player")
			{
				this.Yanmont.TakeDamage(5);
			}
			if (other.gameObject.name == "Heart")
			{
				Animation component = this.Character.GetComponent<Animation>();
				if (!this.Casting)
				{
					component["Receive Damage"].time = 0f;
					component.Play("Receive Damage");
				}
				if (this.HitReactTimer >= 1f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, other.transform.position, Quaternion.identity);
					this.HitReactTimer = 0f;
					this.HP -= 5f + ((float)this.Yanmont.Level * 5f - 5f);
					AudioSource component2 = base.GetComponent<AudioSource>();
					if (this.HP <= 0f)
					{
						component2.PlayOneShot(this.DeathScream);
						component.Play("Die 2");
						this.Yanmont.EXP += 100f;
						base.enabled = false;
						UnityEngine.Object.Destroy(this.Wall);
						return;
					}
					component2.PlayOneShot(this.HitSound);
				}
			}
		}
	}

	// Token: 0x04003B21 RID: 15137
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003B22 RID: 15138
	public GameObject GroundImpact;

	// Token: 0x04003B23 RID: 15139
	public GameObject BlackHole;

	// Token: 0x04003B24 RID: 15140
	public GameObject Character;

	// Token: 0x04003B25 RID: 15141
	public GameObject HitEffect;

	// Token: 0x04003B26 RID: 15142
	public GameObject Wall;

	// Token: 0x04003B27 RID: 15143
	public AudioClip DeathScream;

	// Token: 0x04003B28 RID: 15144
	public AudioClip HitSound;

	// Token: 0x04003B29 RID: 15145
	public float HitReactTimer;

	// Token: 0x04003B2A RID: 15146
	public float AttackTimer = 10f;

	// Token: 0x04003B2B RID: 15147
	public float HP = 100f;

	// Token: 0x04003B2C RID: 15148
	public bool CastSpell;

	// Token: 0x04003B2D RID: 15149
	public bool Casting;
}

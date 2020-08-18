using System;
using UnityEngine;

// Token: 0x0200048A RID: 1162
public class YanvaniaZombieScript : MonoBehaviour
{
	// Token: 0x06001DE9 RID: 7657 RVA: 0x00176448 File Offset: 0x00174648
	private void Start()
	{
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, (this.Yanmont.transform.position.x > base.transform.position.x) ? 90f : -90f, base.transform.eulerAngles.z);
		UnityEngine.Object.Instantiate<GameObject>(this.ZombieEffect, base.transform.position, Quaternion.identity);
		base.transform.position = new Vector3(base.transform.position.x, -0.63f, base.transform.position.z);
		Animation component = this.Character.GetComponent<Animation>();
		component["getup1"].speed = 2f;
		component.Play("getup1");
		base.GetComponent<AudioSource>().PlayOneShot(this.RisingSound);
		this.MyRenderer.material.mainTexture = this.Textures[UnityEngine.Random.Range(0, 22)];
		this.MyCollider.enabled = false;
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x00176570 File Offset: 0x00174770
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.Dying)
		{
			this.DeathTimer += Time.deltaTime;
			if (this.DeathTimer > 1f)
			{
				if (!this.EffectSpawned)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.ZombieEffect, base.transform.position, Quaternion.identity);
					component.PlayOneShot(this.SinkingSound);
					this.EffectSpawned = true;
				}
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - Time.deltaTime, base.transform.position.z);
				if (base.transform.position.y < -0.4f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
		else
		{
			Animation component2 = this.Character.GetComponent<Animation>();
			if (this.Sink)
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - Time.deltaTime * 0.74f, base.transform.position.z);
				if (base.transform.position.y < -0.63f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			else if (this.Walk)
			{
				this.WalkTimer += Time.deltaTime;
				if (this.WalkType == 1)
				{
					base.transform.Translate(Vector3.forward * Time.deltaTime * this.WalkSpeed1);
					component2.CrossFade("walk1");
				}
				else
				{
					base.transform.Translate(Vector3.forward * Time.deltaTime * this.WalkSpeed2);
					component2.CrossFade("walk2");
				}
				if (this.WalkTimer > 10f)
				{
					this.SinkNow();
				}
			}
			else
			{
				this.Timer += Time.deltaTime;
				if (base.transform.position.y < 0f)
				{
					base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime * 0.74f, base.transform.position.z);
					if (base.transform.position.y > 0f)
					{
						base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
					}
				}
				if (this.Timer > 0.85f)
				{
					this.Walk = true;
					this.MyCollider.enabled = true;
					this.WalkType = UnityEngine.Random.Range(1, 3);
				}
			}
			if (base.transform.position.x < this.LeftBoundary)
			{
				base.transform.position = new Vector3(this.LeftBoundary, base.transform.position.y, base.transform.position.z);
				this.SinkNow();
			}
			if (base.transform.position.x > this.RightBoundary)
			{
				base.transform.position = new Vector3(this.RightBoundary, base.transform.position.y, base.transform.position.z);
				this.SinkNow();
			}
			if (this.HP <= 0)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DeathEffect, new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z), Quaternion.identity);
				component2.Play("die");
				component.PlayOneShot(this.DeathSound);
				this.MyCollider.enabled = false;
				this.Yanmont.EXP += 10f;
				this.Dying = true;
			}
		}
		if (this.HitReactTimer < 1f)
		{
			this.MyRenderer.material.color = new Color(1f, this.HitReactTimer, this.HitReactTimer, 1f);
			this.HitReactTimer += Time.deltaTime * 10f;
			if (this.HitReactTimer >= 1f)
			{
				this.MyRenderer.material.color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x00176A40 File Offset: 0x00174C40
	private void SinkNow()
	{
		Animation component = this.Character.GetComponent<Animation>();
		component["getup1"].time = component["getup1"].length;
		component["getup1"].speed = -2f;
		component.Play("getup1");
		base.GetComponent<AudioSource>().PlayOneShot(this.SinkingSound);
		UnityEngine.Object.Instantiate<GameObject>(this.ZombieEffect, base.transform.position, Quaternion.identity);
		this.MyCollider.enabled = false;
		this.Sink = true;
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x00176ADC File Offset: 0x00174CDC
	private void OnTriggerEnter(Collider other)
	{
		if (!this.Dying)
		{
			if (other.gameObject.tag == "Player")
			{
				this.Yanmont.TakeDamage(5);
			}
			if (other.gameObject.name == "Heart" && this.HitReactTimer >= 1f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, other.transform.position, Quaternion.identity);
				base.GetComponent<AudioSource>().PlayOneShot(this.HitSound);
				this.HitReactTimer = 0f;
				this.HP -= 20 + (this.Yanmont.Level * 5 - 5);
			}
		}
	}

	// Token: 0x04003B83 RID: 15235
	public GameObject ZombieEffect;

	// Token: 0x04003B84 RID: 15236
	public GameObject BloodEffect;

	// Token: 0x04003B85 RID: 15237
	public GameObject DeathEffect;

	// Token: 0x04003B86 RID: 15238
	public GameObject HitEffect;

	// Token: 0x04003B87 RID: 15239
	public GameObject Character;

	// Token: 0x04003B88 RID: 15240
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003B89 RID: 15241
	public int HP;

	// Token: 0x04003B8A RID: 15242
	public float WalkSpeed1;

	// Token: 0x04003B8B RID: 15243
	public float WalkSpeed2;

	// Token: 0x04003B8C RID: 15244
	public float Damage;

	// Token: 0x04003B8D RID: 15245
	public float HitReactTimer;

	// Token: 0x04003B8E RID: 15246
	public float DeathTimer;

	// Token: 0x04003B8F RID: 15247
	public float WalkTimer;

	// Token: 0x04003B90 RID: 15248
	public float Timer;

	// Token: 0x04003B91 RID: 15249
	public int HitReactState;

	// Token: 0x04003B92 RID: 15250
	public int WalkType;

	// Token: 0x04003B93 RID: 15251
	public float LeftBoundary;

	// Token: 0x04003B94 RID: 15252
	public float RightBoundary;

	// Token: 0x04003B95 RID: 15253
	public bool EffectSpawned;

	// Token: 0x04003B96 RID: 15254
	public bool Dying;

	// Token: 0x04003B97 RID: 15255
	public bool Sink;

	// Token: 0x04003B98 RID: 15256
	public bool Walk;

	// Token: 0x04003B99 RID: 15257
	public Texture[] Textures;

	// Token: 0x04003B9A RID: 15258
	public Renderer MyRenderer;

	// Token: 0x04003B9B RID: 15259
	public Collider MyCollider;

	// Token: 0x04003B9C RID: 15260
	public AudioClip DeathSound;

	// Token: 0x04003B9D RID: 15261
	public AudioClip HitSound;

	// Token: 0x04003B9E RID: 15262
	public AudioClip RisingSound;

	// Token: 0x04003B9F RID: 15263
	public AudioClip SinkingSound;
}

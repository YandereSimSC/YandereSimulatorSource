using System;
using UnityEngine;

// Token: 0x020003DD RID: 989
public class SithBeamScript : MonoBehaviour
{
	// Token: 0x06001A93 RID: 6803 RVA: 0x00108A7C File Offset: 0x00106C7C
	private void Update()
	{
		if (this.Projectile)
		{
			base.transform.Translate(base.transform.forward * Time.deltaTime * 15f, Space.World);
		}
		this.Lifespan = Mathf.MoveTowards(this.Lifespan, 0f, Time.deltaTime);
		if (this.Lifespan == 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x00108AF0 File Offset: 0x00106CF0
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				AudioSource.PlayClipAtPoint(this.Hit, base.transform.position);
				this.RandomNumber = UnityEngine.Random.Range(0, 3);
				if (this.MalePain.Length != 0)
				{
					if (component.Male)
					{
						AudioSource.PlayClipAtPoint(this.MalePain[this.RandomNumber], base.transform.position);
					}
					else
					{
						AudioSource.PlayClipAtPoint(this.FemalePain[this.RandomNumber], base.transform.position);
					}
				}
				UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, component.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
				component.Health -= this.Damage;
				component.HealthBar.transform.parent.gameObject.SetActive(true);
				component.HealthBar.transform.localScale = new Vector3(component.Health / 100f, 1f, 1f);
				component.Character.transform.localScale = new Vector3(component.Character.transform.localScale.x * -1f, component.Character.transform.localScale.y, component.Character.transform.localScale.z);
				if (component.Health <= 0f)
				{
					component.DeathType = DeathType.EasterEgg;
					component.HealthBar.transform.parent.gameObject.SetActive(false);
					component.BecomeRagdoll();
					component.Ragdoll.AllRigidbodies[0].isKinematic = false;
				}
				else
				{
					component.CharacterAnimation[component.SithReactAnim].time = 0f;
					component.CharacterAnimation.Play(component.SithReactAnim);
					component.Pathfinding.canSearch = false;
					component.Pathfinding.canMove = false;
					component.HitReacting = true;
					component.Routine = false;
					component.Fleeing = false;
				}
				if (this.Projectile)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x04002A9E RID: 10910
	public GameObject BloodEffect;

	// Token: 0x04002A9F RID: 10911
	public Collider MyCollider;

	// Token: 0x04002AA0 RID: 10912
	public float Damage = 10f;

	// Token: 0x04002AA1 RID: 10913
	public float Lifespan;

	// Token: 0x04002AA2 RID: 10914
	public int RandomNumber;

	// Token: 0x04002AA3 RID: 10915
	public AudioClip Hit;

	// Token: 0x04002AA4 RID: 10916
	public AudioClip[] FemalePain;

	// Token: 0x04002AA5 RID: 10917
	public AudioClip[] MalePain;

	// Token: 0x04002AA6 RID: 10918
	public bool Projectile;
}

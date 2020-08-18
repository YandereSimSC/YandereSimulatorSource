using System;
using UnityEngine;

// Token: 0x02000263 RID: 611
public class DemonSlashScript : MonoBehaviour
{
	// Token: 0x06001331 RID: 4913 RVA: 0x000A0583 File Offset: 0x0009E783
	private void Start()
	{
		this.MyAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x000A0594 File Offset: 0x0009E794
	private void Update()
	{
		if (this.MyCollider.enabled)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 0.33333334f)
			{
				this.MyCollider.enabled = false;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x000A05E4 File Offset: 0x0009E7E4
	private void OnTriggerEnter(Collider other)
	{
		Transform root = other.gameObject.transform.root;
		StudentScript component = root.gameObject.GetComponent<StudentScript>();
		if (component != null && component.StudentID != 1 && component.Alive)
		{
			component.DeathType = DeathType.EasterEgg;
			if (!component.Male)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.FemaleBloodyScream, root.transform.position + Vector3.up, Quaternion.identity);
			}
			else
			{
				UnityEngine.Object.Instantiate<GameObject>(this.MaleBloodyScream, root.transform.position + Vector3.up, Quaternion.identity);
			}
			component.BecomeRagdoll();
			component.Ragdoll.Dismember();
			this.MyAudio.Play();
		}
	}

	// Token: 0x040019EB RID: 6635
	public GameObject FemaleBloodyScream;

	// Token: 0x040019EC RID: 6636
	public GameObject MaleBloodyScream;

	// Token: 0x040019ED RID: 6637
	public AudioSource MyAudio;

	// Token: 0x040019EE RID: 6638
	public Collider MyCollider;

	// Token: 0x040019EF RID: 6639
	public float Timer;
}

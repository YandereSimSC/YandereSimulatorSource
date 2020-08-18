using System;
using UnityEngine;

// Token: 0x020002A7 RID: 679
public class FootprintSpawnerScript : MonoBehaviour
{
	// Token: 0x06001414 RID: 5140 RVA: 0x000AFC44 File Offset: 0x000ADE44
	private void Start()
	{
		this.GardenArea = GameObject.Find("GardenArea").GetComponent<Collider>();
		this.PoolStairs = GameObject.Find("PoolStairs").GetComponent<Collider>();
		this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
		this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
		this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
		this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x000AFCD0 File Offset: 0x000ADED0
	private void Update()
	{
		if (this.Debugging)
		{
			Debug.Log(string.Concat(new string[]
			{
				"UpThreshold: ",
				(this.Yandere.transform.position.y + this.UpThreshold).ToString(),
				" | DownThreshold: ",
				(this.Yandere.transform.position.y + this.DownThreshold).ToString(),
				" | CurrentHeight: ",
				base.transform.position.y.ToString()
			}));
		}
		this.CanSpawn = (!this.GardenArea.bounds.Contains(base.transform.position) && !this.PoolStairs.bounds.Contains(base.transform.position) && !this.NEStairs.bounds.Contains(base.transform.position) && !this.NWStairs.bounds.Contains(base.transform.position) && !this.SEStairs.bounds.Contains(base.transform.position) && !this.SWStairs.bounds.Contains(base.transform.position));
		if (!this.FootUp)
		{
			if (base.transform.position.y > this.Yandere.transform.position.y + this.UpThreshold)
			{
				this.FootUp = true;
				return;
			}
		}
		else if (base.transform.position.y < this.Yandere.transform.position.y + this.DownThreshold)
		{
			if (this.Yandere.Stance.Current != StanceType.Crouching && this.Yandere.Stance.Current != StanceType.Crawling && this.Yandere.CanMove && !this.Yandere.NearSenpai && this.FootUp)
			{
				AudioSource component = base.GetComponent<AudioSource>();
				if (this.Yandere.Running)
				{
					component.clip = this.RunFootsteps[UnityEngine.Random.Range(0, this.RunFootsteps.Length)];
					component.volume = 0.15f;
					component.Play();
				}
				else
				{
					component.clip = this.WalkFootsteps[UnityEngine.Random.Range(0, this.WalkFootsteps.Length)];
					component.volume = 0.1f;
					component.Play();
				}
			}
			this.FootUp = false;
			if (this.CanSpawn && this.Bloodiness > 0)
			{
				if (base.transform.position.y > -1f && base.transform.position.y < 1f)
				{
					this.Height = 0f;
				}
				else if (base.transform.position.y > 3f && base.transform.position.y < 5f)
				{
					this.Height = 4f;
				}
				else if (base.transform.position.y > 7f && base.transform.position.y < 9f)
				{
					this.Height = 8f;
				}
				else if (base.transform.position.y > 11f && base.transform.position.y < 13f)
				{
					this.Height = 12f;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodyFootprint, new Vector3(base.transform.position.x, this.Height + 0.012f, base.transform.position.z), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, base.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
				gameObject.transform.GetChild(0).GetComponent<FootprintScript>().Yandere = this.Yandere;
				gameObject.transform.parent = this.BloodParent;
				this.Bloodiness--;
			}
		}
	}

	// Token: 0x04001C4A RID: 7242
	public YandereScript Yandere;

	// Token: 0x04001C4B RID: 7243
	public GameObject BloodyFootprint;

	// Token: 0x04001C4C RID: 7244
	public AudioClip[] WalkFootsteps;

	// Token: 0x04001C4D RID: 7245
	public AudioClip[] RunFootsteps;

	// Token: 0x04001C4E RID: 7246
	public Transform BloodParent;

	// Token: 0x04001C4F RID: 7247
	public Collider GardenArea;

	// Token: 0x04001C50 RID: 7248
	public Collider PoolStairs;

	// Token: 0x04001C51 RID: 7249
	public Collider NEStairs;

	// Token: 0x04001C52 RID: 7250
	public Collider NWStairs;

	// Token: 0x04001C53 RID: 7251
	public Collider SEStairs;

	// Token: 0x04001C54 RID: 7252
	public Collider SWStairs;

	// Token: 0x04001C55 RID: 7253
	public bool Debugging;

	// Token: 0x04001C56 RID: 7254
	public bool CanSpawn;

	// Token: 0x04001C57 RID: 7255
	public bool FootUp;

	// Token: 0x04001C58 RID: 7256
	public float DownThreshold;

	// Token: 0x04001C59 RID: 7257
	public float UpThreshold;

	// Token: 0x04001C5A RID: 7258
	public float Height;

	// Token: 0x04001C5B RID: 7259
	public int Bloodiness;

	// Token: 0x04001C5C RID: 7260
	public int Collisions;
}

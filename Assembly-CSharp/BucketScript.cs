using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class BucketScript : MonoBehaviour
{
	// Token: 0x06000A90 RID: 2704 RVA: 0x00056658 File Offset: 0x00054858
	private void Start()
	{
		this.Water.transform.localPosition = new Vector3(this.Water.transform.localPosition.x, 0f, this.Water.transform.localPosition.z);
		this.Water.transform.localScale = new Vector3(0.235f, 1f, 0.14f);
		this.Water.material.color = new Color(this.Water.material.color.r, this.Water.material.color.g, this.Water.material.color.b, 0f);
		this.Blood.material.color = new Color(this.Blood.material.color.r, this.Blood.material.color.g, this.Blood.material.color.b, 0f);
		this.Gas.transform.localPosition = new Vector3(this.Gas.transform.localPosition.x, 0f, this.Gas.transform.localPosition.z);
		this.Gas.transform.localScale = new Vector3(0.235f, 1f, 0.14f);
		this.Gas.material.color = new Color(this.Gas.material.color.r, this.Gas.material.color.g, this.Gas.material.color.b, 0f);
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x00056858 File Offset: 0x00054A58
	private void Update()
	{
		if (this.PickUp.Clock.Period == 5)
		{
			this.PickUp.Suspicious = false;
		}
		else
		{
			this.PickUp.Suspicious = true;
		}
		this.Distance = Vector3.Distance(base.transform.position, this.Yandere.transform.position);
		if (this.Distance < 1f)
		{
			if (this.Yandere.Bucket == null)
			{
				RaycastHit raycastHit;
				if (base.transform.position.y > this.Yandere.transform.position.y - 0.1f && base.transform.position.y < this.Yandere.transform.position.y + 0.1f && Physics.Linecast(base.transform.position, this.Yandere.transform.position + Vector3.up, out raycastHit) && raycastHit.collider.gameObject == this.Yandere.gameObject)
				{
					this.Yandere.Bucket = this;
				}
			}
			else
			{
				RaycastHit raycastHit;
				if (Physics.Linecast(base.transform.position, this.Yandere.transform.position + Vector3.up, out raycastHit) && raycastHit.collider.gameObject != this.Yandere.gameObject)
				{
					this.Yandere.Bucket = null;
				}
				if (base.transform.position.y < this.Yandere.transform.position.y - 0.1f || base.transform.position.y > this.Yandere.transform.position.y + 0.1f)
				{
					this.Yandere.Bucket = null;
				}
			}
		}
		else if (this.Yandere.Bucket == this)
		{
			this.Yandere.Bucket = null;
		}
		if (this.Yandere.Bucket == this && this.Yandere.Dipping)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.Yandere.transform.position + this.Yandere.transform.forward * 0.55f, Time.deltaTime * 10f);
			Quaternion b = Quaternion.LookRotation(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z) - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * 10f);
		}
		if (this.Yandere.PickUp != null)
		{
			if (this.Yandere.PickUp.JerryCan)
			{
				if (!this.Yandere.PickUp.Empty)
				{
					this.Prompt.Label[0].text = "     Pour Gasoline";
					this.Prompt.HideButton[0] = false;
				}
				else
				{
					this.Prompt.HideButton[0] = true;
				}
			}
			else if (this.Yandere.PickUp.Bleach)
			{
				if (this.Full && !this.Gasoline && !this.Bleached)
				{
					this.Prompt.Label[0].text = "     Pour Bleach";
					this.Prompt.HideButton[0] = false;
				}
				else
				{
					this.Prompt.HideButton[0] = true;
				}
			}
		}
		else if (this.Yandere.Equipped > 0)
		{
			if (!this.Full)
			{
				if (this.Yandere.EquippedWeapon.WeaponID == 12)
				{
					if (this.Dumbbells < 5)
					{
						this.Prompt.Label[0].text = "     Place Dumbbell";
						this.Prompt.HideButton[0] = false;
					}
					else
					{
						this.Prompt.HideButton[0] = true;
					}
				}
				else
				{
					this.Prompt.HideButton[0] = true;
				}
			}
			else
			{
				this.Prompt.HideButton[0] = true;
			}
		}
		else if (this.Dumbbells == 0)
		{
			this.Prompt.HideButton[0] = true;
		}
		else
		{
			this.Prompt.Label[0].text = "     Remove Dumbbell";
			this.Prompt.HideButton[0] = false;
		}
		if (this.Dumbbells > 0)
		{
			if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus == 0)
			{
				this.Prompt.Label[3].text = "     Physical Stat Too Low";
				this.Prompt.Circle[3].fillAmount = 1f;
			}
			else
			{
				this.Prompt.Label[3].text = "     Carry";
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Prompt.Label[0].text == "     Place Dumbbell")
			{
				this.Dumbbells++;
				this.Dumbbell[this.Dumbbells] = this.Yandere.EquippedWeapon.gameObject;
				this.Yandere.EmptyHands();
				this.Dumbbell[this.Dumbbells].GetComponent<WeaponScript>().enabled = false;
				this.Dumbbell[this.Dumbbells].GetComponent<PromptScript>().enabled = false;
				this.Dumbbell[this.Dumbbells].GetComponent<PromptScript>().Hide();
				this.Dumbbell[this.Dumbbells].GetComponent<Collider>().enabled = false;
				Rigidbody component = this.Dumbbell[this.Dumbbells].GetComponent<Rigidbody>();
				component.useGravity = false;
				component.isKinematic = true;
				this.Dumbbell[this.Dumbbells].transform.parent = base.transform;
				this.Dumbbell[this.Dumbbells].transform.localPosition = this.Positions[this.Dumbbells].localPosition;
				this.Dumbbell[this.Dumbbells].transform.localEulerAngles = new Vector3(90f, 0f, 0f);
			}
			else if (this.Prompt.Label[0].text == "     Remove Dumbbell")
			{
				this.Yandere.EmptyHands();
				this.Dumbbell[this.Dumbbells].GetComponent<WeaponScript>().enabled = true;
				this.Dumbbell[this.Dumbbells].GetComponent<PromptScript>().enabled = true;
				this.Dumbbell[this.Dumbbells].GetComponent<WeaponScript>().Prompt.Circle[3].fillAmount = 0f;
				this.Dumbbell[this.Dumbbells].GetComponent<Rigidbody>().isKinematic = false;
				this.Dumbbell[this.Dumbbells] = null;
				this.Dumbbells--;
			}
			else if (this.Prompt.Label[0].text == "     Pour Gasoline")
			{
				this.Gasoline = true;
				this.Fill();
			}
			else
			{
				this.Sparkles.Play();
				this.Bleached = true;
			}
		}
		if (this.UpdateAppearance)
		{
			if (this.Full)
			{
				if (!this.Gasoline)
				{
					this.Water.transform.localScale = Vector3.Lerp(this.Water.transform.localScale, new Vector3(0.285f, 1f, 0.17f), Time.deltaTime * 5f * this.FillSpeed);
					this.Water.transform.localPosition = new Vector3(this.Water.transform.localPosition.x, Mathf.Lerp(this.Water.transform.localPosition.y, 0.2f, Time.deltaTime * 5f * this.FillSpeed), this.Water.transform.localPosition.z);
					this.Water.material.color = new Color(this.Water.material.color.r, this.Water.material.color.g, this.Water.material.color.b, Mathf.Lerp(this.Water.material.color.a, 0.5f, Time.deltaTime * 5f));
				}
				else
				{
					this.Gas.transform.localScale = Vector3.Lerp(this.Gas.transform.localScale, new Vector3(0.285f, 1f, 0.17f), Time.deltaTime * 5f * this.FillSpeed);
					this.Gas.transform.localPosition = new Vector3(this.Gas.transform.localPosition.x, Mathf.Lerp(this.Gas.transform.localPosition.y, 0.2f, Time.deltaTime * 5f * this.FillSpeed), this.Gas.transform.localPosition.z);
					this.Gas.material.color = new Color(this.Gas.material.color.r, this.Gas.material.color.g, this.Gas.material.color.b, Mathf.Lerp(this.Gas.material.color.a, 0.5f, Time.deltaTime * 5f));
				}
			}
			else
			{
				this.Water.transform.localScale = Vector3.Lerp(this.Water.transform.localScale, new Vector3(0.235f, 1f, 0.14f), Time.deltaTime * 5f);
				this.Water.transform.localPosition = new Vector3(this.Water.transform.localPosition.x, Mathf.Lerp(this.Water.transform.localPosition.y, 0f, Time.deltaTime * 5f), this.Water.transform.localPosition.z);
				this.Water.material.color = new Color(this.Water.material.color.r, this.Water.material.color.g, this.Water.material.color.b, Mathf.Lerp(this.Water.material.color.a, 0f, Time.deltaTime * 5f));
				this.Gas.transform.localScale = Vector3.Lerp(this.Gas.transform.localScale, new Vector3(0.235f, 1f, 0.14f), Time.deltaTime * 5f);
				this.Gas.transform.localPosition = new Vector3(this.Gas.transform.localPosition.x, Mathf.Lerp(this.Gas.transform.localPosition.y, 0f, Time.deltaTime * 5f), this.Gas.transform.localPosition.z);
				this.Gas.material.color = new Color(this.Gas.material.color.r, this.Gas.material.color.g, this.Gas.material.color.b, Mathf.Lerp(this.Gas.material.color.a, 0f, Time.deltaTime * 5f));
			}
			this.Blood.material.color = new Color(this.Blood.material.color.r, this.Blood.material.color.g, this.Blood.material.color.b, Mathf.Lerp(this.Blood.material.color.a, this.Bloodiness / 100f, Time.deltaTime));
			this.Blood.transform.localPosition = new Vector3(this.Blood.transform.localPosition.x, this.Water.transform.localPosition.y + 0.001f, this.Blood.transform.localPosition.z);
			this.Blood.transform.localScale = this.Water.transform.localScale;
			this.Timer = Mathf.MoveTowards(this.Timer, 5f, Time.deltaTime);
			if (this.Timer == 5f)
			{
				this.UpdateAppearance = false;
				this.Timer = 0f;
			}
		}
		if (this.Yandere.PickUp != null)
		{
			if (this.Yandere.PickUp.Bucket == this)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				if (Input.GetKeyDown(KeyCode.B))
				{
					this.UpdateAppearance = true;
					if (this.Bloodiness == 0f)
					{
						this.Bloodiness = 100f;
						this.Gasoline = false;
					}
					else
					{
						this.Bloodiness = 0f;
						this.Gasoline = true;
					}
				}
			}
			else if (!this.Trap)
			{
				this.Prompt.enabled = true;
			}
		}
		else if (!this.Trap)
		{
			this.Prompt.enabled = true;
		}
		if (this.Fly)
		{
			if (this.Rotate < 360f)
			{
				if (this.Rotate == 0f)
				{
					if (this.Bloodiness < 50f)
					{
						if (!this.Gasoline)
						{
							this.Effect = UnityEngine.Object.Instantiate<GameObject>(this.SpillEffect, base.transform.position + base.transform.forward * 0.5f + base.transform.up * 0.5f, base.transform.rotation);
						}
						else
						{
							this.Effect = UnityEngine.Object.Instantiate<GameObject>(this.GasSpillEffect, base.transform.position + base.transform.forward * 0.5f + base.transform.up * 0.5f, base.transform.rotation);
							this.Gasoline = false;
						}
					}
					else
					{
						this.Effect = UnityEngine.Object.Instantiate<GameObject>(this.BloodSpillEffect, base.transform.position + base.transform.forward * 0.5f + base.transform.up * 0.5f, base.transform.rotation);
						this.Bloodiness = 0f;
					}
					if (this.Trap)
					{
						this.Effect.transform.LookAt(this.Effect.transform.position - Vector3.up);
					}
					else
					{
						Rigidbody component2 = base.GetComponent<Rigidbody>();
						component2.AddRelativeForce(Vector3.forward * 150f);
						component2.AddRelativeForce(Vector3.up * 250f);
						base.transform.Translate(Vector3.forward * 0.5f);
					}
				}
				this.Rotate += Time.deltaTime * 360f;
				base.transform.Rotate(Vector3.right * Time.deltaTime * 360f);
			}
			else
			{
				this.Sparkles.Stop();
				this.Rotate = 0f;
				this.Trap = false;
				this.Fly = false;
			}
		}
		if (this.Dropped && base.transform.position.y < 0.5f)
		{
			this.Dropped = false;
		}
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00057940 File Offset: 0x00055B40
	public void Empty()
	{
		if (SchemeGlobals.GetSchemeStage(1) == 2)
		{
			SchemeGlobals.SetSchemeStage(1, 1);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		this.UpdateAppearance = true;
		this.Bloodiness = 0f;
		this.Bleached = false;
		this.Gasoline = false;
		this.Sparkles.Stop();
		this.Full = false;
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x000579A4 File Offset: 0x00055BA4
	public void Fill()
	{
		if (SchemeGlobals.GetSchemeStage(1) == 1)
		{
			SchemeGlobals.SetSchemeStage(1, 2);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		this.UpdateAppearance = true;
		this.Full = true;
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x000579DC File Offset: 0x00055BDC
	private void OnCollisionEnter(Collision other)
	{
		if (this.Dropped)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				base.GetComponent<AudioSource>().Play();
				while (this.Dumbbells > 0)
				{
					this.Dumbbell[this.Dumbbells].GetComponent<WeaponScript>().enabled = true;
					this.Dumbbell[this.Dumbbells].GetComponent<PromptScript>().enabled = true;
					this.Dumbbell[this.Dumbbells].GetComponent<Collider>().enabled = true;
					Rigidbody component2 = this.Dumbbell[this.Dumbbells].GetComponent<Rigidbody>();
					component2.constraints = RigidbodyConstraints.None;
					component2.isKinematic = false;
					component2.useGravity = true;
					this.Dumbbell[this.Dumbbells].transform.parent = null;
					this.Dumbbell[this.Dumbbells] = null;
					this.Dumbbells--;
				}
				component.DeathType = DeathType.Weight;
				component.BecomeRagdoll();
				this.Dropped = false;
				GameObjectUtils.SetLayerRecursively(base.gameObject, 15);
				Debug.Log(component.Name + "'s ''Alive'' variable is: " + component.Alive.ToString());
			}
		}
	}

	// Token: 0x04000B0F RID: 2831
	public PhoneEventScript PhoneEvent;

	// Token: 0x04000B10 RID: 2832
	public ParticleSystem PourEffect;

	// Token: 0x04000B11 RID: 2833
	public ParticleSystem Sparkles;

	// Token: 0x04000B12 RID: 2834
	public YandereScript Yandere;

	// Token: 0x04000B13 RID: 2835
	public PickUpScript PickUp;

	// Token: 0x04000B14 RID: 2836
	public PromptScript Prompt;

	// Token: 0x04000B15 RID: 2837
	public GameObject WaterCollider;

	// Token: 0x04000B16 RID: 2838
	public GameObject BloodCollider;

	// Token: 0x04000B17 RID: 2839
	public GameObject GasCollider;

	// Token: 0x04000B18 RID: 2840
	[SerializeField]
	private GameObject BloodSpillEffect;

	// Token: 0x04000B19 RID: 2841
	[SerializeField]
	private GameObject GasSpillEffect;

	// Token: 0x04000B1A RID: 2842
	[SerializeField]
	private GameObject SpillEffect;

	// Token: 0x04000B1B RID: 2843
	[SerializeField]
	private GameObject Effect;

	// Token: 0x04000B1C RID: 2844
	[SerializeField]
	private GameObject[] Dumbbell;

	// Token: 0x04000B1D RID: 2845
	[SerializeField]
	private Transform[] Positions;

	// Token: 0x04000B1E RID: 2846
	[SerializeField]
	private Renderer Water;

	// Token: 0x04000B1F RID: 2847
	[SerializeField]
	private Renderer Blood;

	// Token: 0x04000B20 RID: 2848
	[SerializeField]
	private Renderer Gas;

	// Token: 0x04000B21 RID: 2849
	public float Bloodiness;

	// Token: 0x04000B22 RID: 2850
	public float FillSpeed = 1f;

	// Token: 0x04000B23 RID: 2851
	public float Timer;

	// Token: 0x04000B24 RID: 2852
	[SerializeField]
	private float Distance;

	// Token: 0x04000B25 RID: 2853
	[SerializeField]
	private float Rotate;

	// Token: 0x04000B26 RID: 2854
	public int Dumbbells;

	// Token: 0x04000B27 RID: 2855
	public bool UpdateAppearance;

	// Token: 0x04000B28 RID: 2856
	public bool Bleached;

	// Token: 0x04000B29 RID: 2857
	public bool Gasoline;

	// Token: 0x04000B2A RID: 2858
	public bool Dropped;

	// Token: 0x04000B2B RID: 2859
	public bool Poured;

	// Token: 0x04000B2C RID: 2860
	public bool Full;

	// Token: 0x04000B2D RID: 2861
	public bool Trap;

	// Token: 0x04000B2E RID: 2862
	public bool Fly;
}

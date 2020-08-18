using System;
using UnityEngine;

// Token: 0x0200038B RID: 907
public class RefrigeratorScript : MonoBehaviour
{
	// Token: 0x0600198E RID: 6542 RVA: 0x000F8C8C File Offset: 0x000F6E8C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.CookingEvent.EventCheck = false;
				this.Yandere.EmptyHands();
				this.Yandere.CanMove = false;
				this.Yandere.Cooking = true;
			}
		}
		if (this.Yandere.Cooking)
		{
			Quaternion b = Quaternion.LookRotation(new Vector3(this.Octodogs[1].transform.position.x, this.Yandere.transform.position.y, this.Octodogs[1].transform.position.z) - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, b, Time.deltaTime * 10f);
			this.Yandere.MoveTowardsTarget(this.CookingSpot.position);
			if (this.EventPhase == 0)
			{
				this.Yandere.Character.GetComponent<Animation>().Play("f02_prepareFood_00");
				this.Octodog.transform.parent = this.Yandere.RightHand;
				this.Octodog.transform.localPosition = new Vector3(0.0129f, -0.02475f, 0.0316f);
				this.Octodog.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
				this.Sausage.transform.parent = this.Yandere.RightHand;
				this.Sausage.transform.localPosition = new Vector3(0.013f, -0.038f, 0.015f);
				this.Sausage.transform.localEulerAngles = Vector3.zero;
				this.EventPhase++;
				return;
			}
			if (this.EventPhase == 1)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time > 1f)
				{
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 2)
			{
				this.Refrigerator.GetComponent<Animation>().Play("FridgeOpen");
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time > 3f)
				{
					this.Jar.parent = this.Yandere.RightHand;
					this.Jar.localPosition = new Vector3(0f, -0.033333335f, -0.14f);
					this.Jar.localEulerAngles = new Vector3(90f, 0f, 0f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 3)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time > 5f)
				{
					this.JarLid.transform.parent = this.Yandere.LeftHand;
					this.JarLid.localPosition = new Vector3(0.033333335f, 0f, 0f);
					this.JarLid.localEulerAngles = Vector3.zero;
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 4)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time > 6f)
				{
					this.JarLid.parent = this.CookingClub;
					this.JarLid.localPosition = new Vector3(0.334585f, 1f, -0.2528915f);
					this.JarLid.localEulerAngles = new Vector3(0f, 30f, 0f);
					this.Jar.parent = this.CookingClub;
					this.Jar.localPosition = new Vector3(0.29559f, 1f, 0.2029152f);
					this.Jar.localEulerAngles = new Vector3(0f, -150f, 0f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 5)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time > 7f)
				{
					this.Knife.GetComponent<WeaponScript>().FingerprintID = 100;
					this.Knife.parent = this.Yandere.LeftHand;
					this.Knife.localPosition = new Vector3(0f, -0.01f, 0f);
					this.Knife.localEulerAngles = new Vector3(0f, 0f, -90f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 6)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time >= this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_cutFood_00");
					this.Sausage.SetActive(true);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 7)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_cutFood_00"].time > 2.66666f)
				{
					this.Octodog.SetActive(true);
					this.Sausage.SetActive(false);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 8)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_cutFood_00"].time > 3f)
				{
					this.Rotation = Mathf.MoveTowards(this.Rotation, 90f, Time.deltaTime * 360f);
					this.Octodog.transform.localEulerAngles = new Vector3(this.Rotation, this.Octodog.transform.localEulerAngles.y, this.Octodog.transform.localEulerAngles.z);
					this.Octodog.transform.localPosition = new Vector3(this.Octodog.transform.localPosition.x, this.Octodog.transform.localPosition.y, Mathf.MoveTowards(this.Octodog.transform.localPosition.z, 0.012f, Time.deltaTime));
				}
				if (this.Yandere.Character.GetComponent<Animation>()["f02_cutFood_00"].time > 6f)
				{
					this.Octodog.SetActive(false);
					for (int i = 1; i < this.Octodogs.Length; i++)
					{
						this.Octodogs[i].SetActive(true);
					}
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 9)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_cutFood_00"].time >= this.Yandere.Character.GetComponent<Animation>()["f02_cutFood_00"].length)
				{
					this.Yandere.Character.GetComponent<Animation>().Play("f02_prepareFood_00");
					this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time = this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length;
					this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].speed = -1f;
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 10)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time < this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length - 1f)
				{
					this.Knife.parent = this.CookingClub;
					this.Knife.localPosition = new Vector3(0.197f, 1.1903f, -0.33333334f);
					this.Knife.localEulerAngles = new Vector3(45f, -90f, -90f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 11)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time < this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length - 2f)
				{
					this.JarLid.parent = this.Yandere.LeftHand;
					this.JarLid.localPosition = new Vector3(0.033333335f, 0f, 0f);
					this.JarLid.localEulerAngles = Vector3.zero;
					this.Jar.parent = this.Yandere.RightHand;
					this.Jar.localPosition = new Vector3(0f, -0.033333335f, -0.14f);
					this.Jar.localEulerAngles = new Vector3(90f, 0f, 0f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 12)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time < this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length - 3f)
				{
					this.JarLid.parent = this.Jar;
					this.JarLid.localPosition = new Vector3(0f, 0.175f, 0f);
					this.JarLid.localEulerAngles = Vector3.zero;
					this.Refrigerator.GetComponent<Animation>().Play("FridgeOpen");
					this.Refrigerator.GetComponent<Animation>()["FridgeOpen"].time = this.Refrigerator.GetComponent<Animation>()["FridgeOpen"].length;
					this.Refrigerator.GetComponent<Animation>()["FridgeOpen"].speed = -1f;
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 13)
			{
				if (this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time < this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].length - 5f)
				{
					this.Jar.parent = this.CookingClub;
					this.Jar.localPosition = new Vector3(0.1f, 0.941f, 0.75f);
					this.Jar.localEulerAngles = new Vector3(0f, 90f, 0f);
					this.EventPhase++;
					return;
				}
			}
			else if (this.EventPhase == 14 && this.Yandere.Character.GetComponent<Animation>()["f02_prepareFood_00"].time <= 0f)
			{
				this.PlateCollider.enabled = true;
				this.PlatePickUp.enabled = true;
				this.PlatePrompt.enabled = true;
				this.Yandere.Cooking = false;
				this.Yandere.CanMove = true;
				this.Empty = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.enabled = false;
			}
		}
	}

	// Token: 0x0400275F RID: 10079
	public CookingEventScript CookingEvent;

	// Token: 0x04002760 RID: 10080
	public YandereScript Yandere;

	// Token: 0x04002761 RID: 10081
	public PromptScript Prompt;

	// Token: 0x04002762 RID: 10082
	public PickUpScript PlatePickUp;

	// Token: 0x04002763 RID: 10083
	public PromptScript PlatePrompt;

	// Token: 0x04002764 RID: 10084
	public Collider PlateCollider;

	// Token: 0x04002765 RID: 10085
	public GameObject[] Octodogs;

	// Token: 0x04002766 RID: 10086
	public GameObject Refrigerator;

	// Token: 0x04002767 RID: 10087
	public GameObject Octodog;

	// Token: 0x04002768 RID: 10088
	public GameObject Sausage;

	// Token: 0x04002769 RID: 10089
	public Transform CookingSpot;

	// Token: 0x0400276A RID: 10090
	public Transform CookingClub;

	// Token: 0x0400276B RID: 10091
	public Transform JarLid;

	// Token: 0x0400276C RID: 10092
	public Transform Knife;

	// Token: 0x0400276D RID: 10093
	public Transform Jar;

	// Token: 0x0400276E RID: 10094
	public bool Empty;

	// Token: 0x0400276F RID: 10095
	public int EventPhase;

	// Token: 0x04002770 RID: 10096
	public float Rotation;
}

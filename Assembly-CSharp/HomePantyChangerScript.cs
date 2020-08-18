using System;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class HomePantyChangerScript : MonoBehaviour
{
	// Token: 0x06001730 RID: 5936 RVA: 0x000C62C8 File Offset: 0x000C44C8
	private void Start()
	{
		for (int i = 0; i < this.TotalPanties; i++)
		{
			this.NewPanties = UnityEngine.Object.Instantiate<GameObject>(this.PantyModels[i], new Vector3(base.transform.position.x, base.transform.position.y - 0.85f, base.transform.position.z - 1f), Quaternion.identity);
			this.NewPanties.transform.parent = this.PantyParent;
			this.NewPanties.GetComponent<HomePantiesScript>().PantyChanger = this;
			this.NewPanties.GetComponent<HomePantiesScript>().ID = i;
			this.PantyParent.transform.localEulerAngles = new Vector3(this.PantyParent.transform.localEulerAngles.x, this.PantyParent.transform.localEulerAngles.y + 360f / (float)this.TotalPanties, this.PantyParent.transform.localEulerAngles.z);
		}
		this.PantyParent.transform.localEulerAngles = new Vector3(this.PantyParent.transform.localEulerAngles.x, 0f, this.PantyParent.transform.localEulerAngles.z);
		this.PantyParent.transform.localPosition = new Vector3(this.PantyParent.transform.localPosition.x, this.PantyParent.transform.localPosition.y, 1.8f);
		this.UpdatePantyLabels();
		this.PantyParent.transform.localScale = Vector3.zero;
		this.PantyParent.gameObject.SetActive(false);
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x000C6494 File Offset: 0x000C4694
	private void Update()
	{
		if (this.HomeWindow.Show)
		{
			this.PantyParent.localScale = Vector3.Lerp(this.PantyParent.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			this.PantyParent.gameObject.SetActive(true);
			if (this.InputManager.TappedRight)
			{
				this.DestinationReached = false;
				this.TargetRotation += 360f / (float)this.TotalPanties;
				this.Selected++;
				if (this.Selected > this.TotalPanties - 1)
				{
					this.Selected = 0;
				}
				this.UpdatePantyLabels();
			}
			if (this.InputManager.TappedLeft)
			{
				this.DestinationReached = false;
				this.TargetRotation -= 360f / (float)this.TotalPanties;
				this.Selected--;
				if (this.Selected < 0)
				{
					this.Selected = this.TotalPanties - 1;
				}
				this.UpdatePantyLabels();
			}
			this.Rotation = Mathf.Lerp(this.Rotation, this.TargetRotation, Time.deltaTime * 10f);
			this.PantyParent.localEulerAngles = new Vector3(this.PantyParent.localEulerAngles.x, this.Rotation, this.PantyParent.localEulerAngles.z);
			if (Input.GetButtonDown("A"))
			{
				if (this.Selected == 0 || CollectibleGlobals.GetPantyPurchased(this.Selected))
				{
					PlayerGlobals.PantiesEquipped = this.Selected;
					Debug.Log("Yandere-chan should now be equipped with Panties #" + PlayerGlobals.PantiesEquipped);
				}
				else
				{
					Debug.Log("Yandere-chan doesn't own those panties.");
				}
				this.UpdatePantyLabels();
			}
			if (Input.GetButtonDown("B"))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeYandere.CanMove = true;
				this.HomeWindow.Show = false;
				return;
			}
		}
		else
		{
			this.PantyParent.localScale = Vector3.Lerp(this.PantyParent.localScale, Vector3.zero, Time.deltaTime * 10f);
			if (this.PantyParent.localScale.x < 0.01f)
			{
				this.PantyParent.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001732 RID: 5938 RVA: 0x000C6708 File Offset: 0x000C4908
	private void UpdatePantyLabels()
	{
		if (this.Selected == 0 || CollectibleGlobals.GetPantyPurchased(this.Selected))
		{
			this.PantyNameLabel.text = this.PantyNames[this.Selected];
			this.PantyDescLabel.text = this.PantyDescs[this.Selected];
			this.PantyBuffLabel.text = this.PantyBuffs[this.Selected];
		}
		else
		{
			this.PantyNameLabel.text = "?????";
			this.PantyBuffLabel.text = "?????";
			if (this.Selected < 11)
			{
				this.PantyDescLabel.text = "Unlock these panties by going shopping in town at night!";
			}
			else
			{
				this.PantyDescLabel.text = "Unlock these panties by locating them and picking them up!";
			}
		}
		if (this.Selected == 0 || CollectibleGlobals.GetPantyPurchased(this.Selected))
		{
			this.ButtonLabel.text = ((this.Selected == PlayerGlobals.PantiesEquipped) ? "Equipped" : "Wear");
			return;
		}
		this.ButtonLabel.text = "Unavailable";
	}

	// Token: 0x04001FB2 RID: 8114
	public InputManagerScript InputManager;

	// Token: 0x04001FB3 RID: 8115
	public HomeYandereScript HomeYandere;

	// Token: 0x04001FB4 RID: 8116
	public HomeCameraScript HomeCamera;

	// Token: 0x04001FB5 RID: 8117
	public HomeWindowScript HomeWindow;

	// Token: 0x04001FB6 RID: 8118
	private GameObject NewPanties;

	// Token: 0x04001FB7 RID: 8119
	public UILabel PantyNameLabel;

	// Token: 0x04001FB8 RID: 8120
	public UILabel PantyDescLabel;

	// Token: 0x04001FB9 RID: 8121
	public UILabel PantyBuffLabel;

	// Token: 0x04001FBA RID: 8122
	public UILabel ButtonLabel;

	// Token: 0x04001FBB RID: 8123
	public Transform PantyParent;

	// Token: 0x04001FBC RID: 8124
	public bool DestinationReached;

	// Token: 0x04001FBD RID: 8125
	public float TargetRotation;

	// Token: 0x04001FBE RID: 8126
	public float Rotation;

	// Token: 0x04001FBF RID: 8127
	public int TotalPanties;

	// Token: 0x04001FC0 RID: 8128
	public int Selected;

	// Token: 0x04001FC1 RID: 8129
	public GameObject[] PantyModels;

	// Token: 0x04001FC2 RID: 8130
	public string[] PantyNames;

	// Token: 0x04001FC3 RID: 8131
	public string[] PantyDescs;

	// Token: 0x04001FC4 RID: 8132
	public string[] PantyBuffs;
}

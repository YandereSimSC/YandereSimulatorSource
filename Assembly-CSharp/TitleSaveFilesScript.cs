using System;
using UnityEngine;

// Token: 0x02000425 RID: 1061
public class TitleSaveFilesScript : MonoBehaviour
{
	// Token: 0x06001C3F RID: 7231 RVA: 0x00151B7E File Offset: 0x0014FD7E
	private void Start()
	{
		base.transform.localPosition = new Vector3(1050f, base.transform.localPosition.y, base.transform.localPosition.z);
		this.UpdateHighlight();
	}

	// Token: 0x06001C40 RID: 7232 RVA: 0x00151BBC File Offset: 0x0014FDBC
	private void Update()
	{
		if (!this.Show)
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 1050f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			return;
		}
		base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
		if (!this.ConfirmationWindow.activeInHierarchy)
		{
			if (this.InputManager.TappedDown)
			{
				this.ID++;
				if (this.ID > 3)
				{
					this.ID = 1;
				}
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedUp)
			{
				this.ID--;
				if (this.ID < 1)
				{
					this.ID = 3;
				}
				this.UpdateHighlight();
			}
		}
		if (base.transform.localPosition.x < 50f)
		{
			if (!this.ConfirmationWindow.activeInHierarchy)
			{
				if (Input.GetButtonDown("A"))
				{
					GameGlobals.Profile = this.ID;
					Globals.DeleteAll();
					GameGlobals.Profile = this.ID;
					this.Menu.FadeOut = true;
					this.Menu.Fading = true;
					return;
				}
				if (Input.GetButtonDown("X"))
				{
					this.ConfirmationWindow.SetActive(true);
					return;
				}
			}
			else
			{
				if (Input.GetButtonDown("A"))
				{
					PlayerPrefs.SetInt("ProfileCreated_" + this.ID, 0);
					this.ConfirmationWindow.SetActive(false);
					this.SaveDatas[this.ID].Start();
					return;
				}
				if (Input.GetButtonDown("B"))
				{
					this.ConfirmationWindow.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06001C41 RID: 7233 RVA: 0x00151DC9 File Offset: 0x0014FFC9
	private void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(0f, 700f - 350f * (float)this.ID, 0f);
	}

	// Token: 0x040034FE RID: 13566
	public InputManagerScript InputManager;

	// Token: 0x040034FF RID: 13567
	public TitleSaveDataScript[] SaveDatas;

	// Token: 0x04003500 RID: 13568
	public GameObject ConfirmationWindow;

	// Token: 0x04003501 RID: 13569
	public TitleMenuScript Menu;

	// Token: 0x04003502 RID: 13570
	public Transform Highlight;

	// Token: 0x04003503 RID: 13571
	public bool Show;

	// Token: 0x04003504 RID: 13572
	public int ID = 1;
}

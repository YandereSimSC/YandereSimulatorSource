using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000069 RID: 105
[AddComponentMenu("NGUI/Interaction/Toggled Objects")]
public class UIToggledObjects : MonoBehaviour
{
	// Token: 0x0600032C RID: 812 RVA: 0x0001F3DC File Offset: 0x0001D5DC
	private void Awake()
	{
		if (this.target != null)
		{
			if (this.activate.Count == 0 && this.deactivate.Count == 0)
			{
				if (this.inverse)
				{
					this.deactivate.Add(this.target);
				}
				else
				{
					this.activate.Add(this.target);
				}
			}
			else
			{
				this.target = null;
			}
		}
		EventDelegate.Add(base.GetComponent<UIToggle>().onChange, new EventDelegate.Callback(this.Toggle));
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0001F464 File Offset: 0x0001D664
	public void Toggle()
	{
		bool value = UIToggle.current.value;
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				this.Set(this.activate[i], value);
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				this.Set(this.deactivate[j], !value);
			}
		}
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0001F4D9 File Offset: 0x0001D6D9
	private void Set(GameObject go, bool state)
	{
		if (go != null)
		{
			NGUITools.SetActive(go, state);
		}
	}

	// Token: 0x0400047D RID: 1149
	public List<GameObject> activate;

	// Token: 0x0400047E RID: 1150
	public List<GameObject> deactivate;

	// Token: 0x0400047F RID: 1151
	[HideInInspector]
	[SerializeField]
	private GameObject target;

	// Token: 0x04000480 RID: 1152
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}

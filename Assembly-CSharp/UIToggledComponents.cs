using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000068 RID: 104
[ExecuteInEditMode]
[RequireComponent(typeof(UIToggle))]
[AddComponentMenu("NGUI/Interaction/Toggled Components")]
public class UIToggledComponents : MonoBehaviour
{
	// Token: 0x06000329 RID: 809 RVA: 0x0001F2D8 File Offset: 0x0001D4D8
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

	// Token: 0x0600032A RID: 810 RVA: 0x0001F360 File Offset: 0x0001D560
	public void Toggle()
	{
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				this.activate[i].enabled = UIToggle.current.value;
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				this.deactivate[j].enabled = !UIToggle.current.value;
			}
		}
	}

	// Token: 0x04000479 RID: 1145
	public List<MonoBehaviour> activate;

	// Token: 0x0400047A RID: 1146
	public List<MonoBehaviour> deactivate;

	// Token: 0x0400047B RID: 1147
	[HideInInspector]
	[SerializeField]
	private MonoBehaviour target;

	// Token: 0x0400047C RID: 1148
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}

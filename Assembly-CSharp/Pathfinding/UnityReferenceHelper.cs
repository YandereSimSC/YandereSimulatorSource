using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020005A5 RID: 1445
	[ExecuteInEditMode]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_unity_reference_helper.php")]
	public class UnityReferenceHelper : MonoBehaviour
	{
		// Token: 0x0600275B RID: 10075 RVA: 0x001ADD8B File Offset: 0x001ABF8B
		public string GetGUID()
		{
			return this.guid;
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x001ADD93 File Offset: 0x001ABF93
		public void Awake()
		{
			this.Reset();
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x001ADD9C File Offset: 0x001ABF9C
		public void Reset()
		{
			if (string.IsNullOrEmpty(this.guid))
			{
				this.guid = Pathfinding.Util.Guid.NewGuid().ToString();
				Debug.Log("Created new GUID - " + this.guid);
				return;
			}
			foreach (UnityReferenceHelper unityReferenceHelper in UnityEngine.Object.FindObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[])
			{
				if (unityReferenceHelper != this && this.guid == unityReferenceHelper.guid)
				{
					this.guid = Pathfinding.Util.Guid.NewGuid().ToString();
					Debug.Log("Created new GUID - " + this.guid);
					return;
				}
			}
		}

		// Token: 0x040041CB RID: 16843
		[HideInInspector]
		[SerializeField]
		private string guid;
	}
}

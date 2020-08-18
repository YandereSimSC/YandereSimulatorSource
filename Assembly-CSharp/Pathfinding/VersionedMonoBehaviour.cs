using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000590 RID: 1424
	public abstract class VersionedMonoBehaviour : MonoBehaviour, ISerializationCallbackReceiver, IVersionedMonoBehaviourInternal
	{
		// Token: 0x060026B4 RID: 9908 RVA: 0x001A92E7 File Offset: 0x001A74E7
		protected virtual void Awake()
		{
			if (Application.isPlaying)
			{
				this.version = this.OnUpgradeSerializedData(int.MaxValue, true);
			}
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x001A9302 File Offset: 0x001A7502
		private void Reset()
		{
			this.version = this.OnUpgradeSerializedData(int.MaxValue, true);
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x00002ACE File Offset: 0x00000CCE
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x001A9316 File Offset: 0x001A7516
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.version = this.OnUpgradeSerializedData(this.version, false);
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x0002291C File Offset: 0x00020B1C
		protected virtual int OnUpgradeSerializedData(int version, bool unityThread)
		{
			return 1;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x001A932B File Offset: 0x001A752B
		int IVersionedMonoBehaviourInternal.OnUpgradeSerializedData(int version, bool unityThread)
		{
			return this.OnUpgradeSerializedData(version, unityThread);
		}

		// Token: 0x04004187 RID: 16775
		[SerializeField]
		[HideInInspector]
		private int version;
	}
}

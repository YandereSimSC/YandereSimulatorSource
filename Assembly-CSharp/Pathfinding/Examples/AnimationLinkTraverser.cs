using System;
using System.Collections;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005F8 RID: 1528
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_animation_link_traverser.php")]
	public class AnimationLinkTraverser : VersionedMonoBehaviour
	{
		// Token: 0x06002A03 RID: 10755 RVA: 0x001C2A1C File Offset: 0x001C0C1C
		private void OnEnable()
		{
			this.ai = base.GetComponent<RichAI>();
			if (this.ai != null)
			{
				RichAI richAI = this.ai;
				richAI.onTraverseOffMeshLink = (Func<RichSpecial, IEnumerator>)Delegate.Combine(richAI.onTraverseOffMeshLink, new Func<RichSpecial, IEnumerator>(this.TraverseOffMeshLink));
			}
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x001C2A6B File Offset: 0x001C0C6B
		private void OnDisable()
		{
			if (this.ai != null)
			{
				RichAI richAI = this.ai;
				richAI.onTraverseOffMeshLink = (Func<RichSpecial, IEnumerator>)Delegate.Remove(richAI.onTraverseOffMeshLink, new Func<RichSpecial, IEnumerator>(this.TraverseOffMeshLink));
			}
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x001C2AA3 File Offset: 0x001C0CA3
		protected virtual IEnumerator TraverseOffMeshLink(RichSpecial rs)
		{
			AnimationLink link = rs.nodeLink as AnimationLink;
			if (link == null)
			{
				Debug.LogError("Unhandled RichSpecial");
				yield break;
			}
			for (;;)
			{
				Quaternion rotation = this.ai.rotation;
				Quaternion quaternion = this.ai.SimulateRotationTowards(rs.first.forward, this.ai.rotationSpeed * Time.deltaTime);
				if (rotation == quaternion)
				{
					break;
				}
				this.ai.FinalizeMovement(this.ai.position, quaternion);
				yield return null;
			}
			base.transform.parent.position = base.transform.position;
			base.transform.parent.rotation = base.transform.rotation;
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			if (rs.reverse && link.reverseAnim)
			{
				this.anim[link.clip].speed = -link.animSpeed;
				this.anim[link.clip].normalizedTime = 1f;
				this.anim.Play(link.clip);
				this.anim.Sample();
			}
			else
			{
				this.anim[link.clip].speed = link.animSpeed;
				this.anim.Rewind(link.clip);
				this.anim.Play(link.clip);
			}
			base.transform.parent.position -= base.transform.position - base.transform.parent.position;
			yield return new WaitForSeconds(Mathf.Abs(this.anim[link.clip].length / link.animSpeed));
			yield break;
		}

		// Token: 0x040043D2 RID: 17362
		public Animation anim;

		// Token: 0x040043D3 RID: 17363
		private RichAI ai;
	}
}

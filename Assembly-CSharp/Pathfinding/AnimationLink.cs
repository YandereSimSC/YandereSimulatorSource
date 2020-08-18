using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000521 RID: 1313
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_animation_link.php")]
	public class AnimationLink : NodeLink2
	{
		// Token: 0x0600225F RID: 8799 RVA: 0x0018E9E4 File Offset: 0x0018CBE4
		private static Transform SearchRec(Transform tr, string name)
		{
			int childCount = tr.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = tr.GetChild(i);
				if (child.name == name)
				{
					return child;
				}
				Transform transform = AnimationLink.SearchRec(child, name);
				if (transform != null)
				{
					return transform;
				}
			}
			return null;
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0018EA30 File Offset: 0x0018CC30
		public void CalculateOffsets(List<Vector3> trace, out Vector3 endPosition)
		{
			endPosition = base.transform.position;
			if (this.referenceMesh == null)
			{
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.referenceMesh, base.transform.position, base.transform.rotation);
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			Transform transform = AnimationLink.SearchRec(gameObject.transform, this.boneRoot);
			if (transform == null)
			{
				throw new Exception("Could not find root transform");
			}
			Animation animation = gameObject.GetComponent<Animation>();
			if (animation == null)
			{
				animation = gameObject.AddComponent<Animation>();
			}
			for (int i = 0; i < this.sequence.Length; i++)
			{
				animation.AddClip(this.sequence[i].clip, this.sequence[i].clip.name);
			}
			Vector3 a = Vector3.zero;
			Vector3 vector = base.transform.position;
			Vector3 b = Vector3.zero;
			for (int j = 0; j < this.sequence.Length; j++)
			{
				AnimationLink.LinkClip linkClip = this.sequence[j];
				if (linkClip == null)
				{
					endPosition = vector;
					return;
				}
				animation[linkClip.clip.name].enabled = true;
				animation[linkClip.clip.name].weight = 1f;
				for (int k = 0; k < linkClip.loopCount; k++)
				{
					animation[linkClip.clip.name].normalizedTime = 0f;
					animation.Sample();
					Vector3 vector2 = transform.position - base.transform.position;
					if (j > 0)
					{
						vector += a - vector2;
					}
					else
					{
						b = vector2;
					}
					for (int l = 0; l <= 20; l++)
					{
						float num = (float)l / 20f;
						animation[linkClip.clip.name].normalizedTime = num;
						animation.Sample();
						Vector3 item = vector + (transform.position - base.transform.position) + linkClip.velocity * num * linkClip.clip.length;
						trace.Add(item);
					}
					vector += linkClip.velocity * 1f * linkClip.clip.length;
					animation[linkClip.clip.name].normalizedTime = 1f;
					animation.Sample();
					a = transform.position - base.transform.position;
				}
				animation[linkClip.clip.name].enabled = false;
				animation[linkClip.clip.name].weight = 0f;
			}
			vector += a - b;
			UnityEngine.Object.DestroyImmediate(gameObject);
			endPosition = vector;
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0018ED38 File Offset: 0x0018CF38
		public override void OnDrawGizmosSelected()
		{
			base.OnDrawGizmosSelected();
			List<Vector3> list = ListPool<Vector3>.Claim();
			Vector3 zero = Vector3.zero;
			this.CalculateOffsets(list, out zero);
			Gizmos.color = Color.blue;
			for (int i = 0; i < list.Count - 1; i++)
			{
				Gizmos.DrawLine(list[i], list[i + 1]);
			}
		}

		// Token: 0x04003ECD RID: 16077
		public string clip;

		// Token: 0x04003ECE RID: 16078
		public float animSpeed = 1f;

		// Token: 0x04003ECF RID: 16079
		public bool reverseAnim = true;

		// Token: 0x04003ED0 RID: 16080
		public GameObject referenceMesh;

		// Token: 0x04003ED1 RID: 16081
		public AnimationLink.LinkClip[] sequence;

		// Token: 0x04003ED2 RID: 16082
		public string boneRoot = "bn_COG_Root";

		// Token: 0x02000713 RID: 1811
		[Serializable]
		public class LinkClip
		{
			// Token: 0x17000672 RID: 1650
			// (get) Token: 0x06002C7D RID: 11389 RVA: 0x001C9EA3 File Offset: 0x001C80A3
			public string name
			{
				get
				{
					if (!(this.clip != null))
					{
						return "";
					}
					return this.clip.name;
				}
			}

			// Token: 0x040048D4 RID: 18644
			public AnimationClip clip;

			// Token: 0x040048D5 RID: 18645
			public Vector3 velocity;

			// Token: 0x040048D6 RID: 18646
			public int loopCount = 1;
		}
	}
}

using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000537 RID: 1335
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_animation_link.php")]
	public class AnimationLink : NodeLink2
	{
		// Token: 0x060022B5 RID: 8885 RVA: 0x001945D0 File Offset: 0x001927D0
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

		// Token: 0x060022B6 RID: 8886 RVA: 0x0019461C File Offset: 0x0019281C
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

		// Token: 0x060022B7 RID: 8887 RVA: 0x00194924 File Offset: 0x00192B24
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

		// Token: 0x04003FAB RID: 16299
		public string clip;

		// Token: 0x04003FAC RID: 16300
		public float animSpeed = 1f;

		// Token: 0x04003FAD RID: 16301
		public bool reverseAnim = true;

		// Token: 0x04003FAE RID: 16302
		public GameObject referenceMesh;

		// Token: 0x04003FAF RID: 16303
		public AnimationLink.LinkClip[] sequence;

		// Token: 0x04003FB0 RID: 16304
		public string boneRoot = "bn_COG_Root";

		// Token: 0x0200072D RID: 1837
		[Serializable]
		public class LinkClip
		{
			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x001CFB4B File Offset: 0x001CDD4B
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

			// Token: 0x040049BF RID: 18879
			public AnimationClip clip;

			// Token: 0x040049C0 RID: 18880
			public Vector3 velocity;

			// Token: 0x040049C1 RID: 18881
			public int loopCount = 1;
		}
	}
}

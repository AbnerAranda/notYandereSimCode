using System;
using System.Collections.Generic;
using Pathfinding.RVO;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005FD RID: 1533
	[RequireComponent(typeof(RVOController))]
	[RequireComponent(typeof(Seeker))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_r_v_o_example_agent.php")]
	public class RVOExampleAgent : MonoBehaviour
	{
		// Token: 0x06002A06 RID: 10758 RVA: 0x001C6464 File Offset: 0x001C4664
		public void Awake()
		{
			this.seeker = base.GetComponent<Seeker>();
			this.controller = base.GetComponent<RVOController>();
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x001C647E File Offset: 0x001C467E
		public void SetTarget(Vector3 target)
		{
			this.target = target;
			this.RecalculatePath();
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x001C6490 File Offset: 0x001C4690
		public void SetColor(Color color)
		{
			if (this.rends == null)
			{
				this.rends = base.GetComponentsInChildren<MeshRenderer>();
			}
			foreach (MeshRenderer meshRenderer in this.rends)
			{
				Color color2 = meshRenderer.material.GetColor("_TintColor");
				AnimationCurve curve = AnimationCurve.Linear(0f, color2.r, 1f, color.r);
				AnimationCurve curve2 = AnimationCurve.Linear(0f, color2.g, 1f, color.g);
				AnimationCurve curve3 = AnimationCurve.Linear(0f, color2.b, 1f, color.b);
				AnimationClip animationClip = new AnimationClip();
				animationClip.legacy = true;
				animationClip.SetCurve("", typeof(Material), "_TintColor.r", curve);
				animationClip.SetCurve("", typeof(Material), "_TintColor.g", curve2);
				animationClip.SetCurve("", typeof(Material), "_TintColor.b", curve3);
				Animation animation = meshRenderer.gameObject.GetComponent<Animation>();
				if (animation == null)
				{
					animation = meshRenderer.gameObject.AddComponent<Animation>();
				}
				animationClip.wrapMode = WrapMode.Once;
				animation.AddClip(animationClip, "ColorAnim");
				animation.Play("ColorAnim");
			}
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x001C65E8 File Offset: 0x001C47E8
		public void RecalculatePath()
		{
			this.canSearchAgain = false;
			this.nextRepath = Time.time + this.repathRate * (UnityEngine.Random.value + 0.5f);
			this.seeker.StartPath(base.transform.position, this.target, new OnPathDelegate(this.OnPathComplete));
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x001C6644 File Offset: 0x001C4844
		public void OnPathComplete(Path _p)
		{
			ABPath abpath = _p as ABPath;
			this.canSearchAgain = true;
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = abpath;
			abpath.Claim(this);
			if (abpath.error)
			{
				this.wp = 0;
				this.vectorPath = null;
				return;
			}
			Vector3 originalStartPoint = abpath.originalStartPoint;
			Vector3 position = base.transform.position;
			originalStartPoint.y = position.y;
			float magnitude = (position - originalStartPoint).magnitude;
			this.wp = 0;
			this.vectorPath = abpath.vectorPath;
			if (this.moveNextDist > 0f)
			{
				for (float num = 0f; num <= magnitude; num += this.moveNextDist * 0.6f)
				{
					this.wp--;
					Vector3 a = originalStartPoint + (position - originalStartPoint) * num;
					Vector3 b;
					do
					{
						this.wp++;
						b = this.vectorPath[this.wp];
					}
					while (this.controller.To2D(a - b).sqrMagnitude < this.moveNextDist * this.moveNextDist && this.wp != this.vectorPath.Count - 1);
				}
			}
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x001C6798 File Offset: 0x001C4998
		public void Update()
		{
			if (Time.time >= this.nextRepath && this.canSearchAgain)
			{
				this.RecalculatePath();
			}
			Vector3 vector = base.transform.position;
			if (this.vectorPath != null && this.vectorPath.Count != 0)
			{
				while ((this.controller.To2D(vector - this.vectorPath[this.wp]).sqrMagnitude < this.moveNextDist * this.moveNextDist && this.wp != this.vectorPath.Count - 1) || this.wp == 0)
				{
					this.wp++;
				}
				Vector3 vector2 = this.vectorPath[this.wp - 1];
				Vector3 vector3 = this.vectorPath[this.wp];
				float num = VectorMath.LineCircleIntersectionFactor(this.controller.To2D(base.transform.position), this.controller.To2D(vector2), this.controller.To2D(vector3), this.moveNextDist);
				num = Mathf.Clamp01(num);
				Vector3 vector4 = Vector3.Lerp(vector2, vector3, num);
				float num2 = this.controller.To2D(vector4 - vector).magnitude + this.controller.To2D(vector4 - vector3).magnitude;
				for (int i = this.wp; i < this.vectorPath.Count - 1; i++)
				{
					num2 += this.controller.To2D(this.vectorPath[i + 1] - this.vectorPath[i]).magnitude;
				}
				Vector3 pos = (vector4 - vector).normalized * num2 + vector;
				float speed = Mathf.Clamp01(num2 / this.slowdownDistance) * this.maxSpeed;
				Debug.DrawLine(base.transform.position, vector4, Color.red);
				this.controller.SetTarget(pos, speed, this.maxSpeed);
			}
			else
			{
				this.controller.SetTarget(vector, this.maxSpeed, this.maxSpeed);
			}
			Vector3 vector5 = this.controller.CalculateMovementDelta(Time.deltaTime);
			vector += vector5;
			if (Time.deltaTime > 0f && vector5.magnitude / Time.deltaTime > 0.01f)
			{
				Quaternion rotation = base.transform.rotation;
				Quaternion quaternion = Quaternion.LookRotation(vector5, this.controller.To3D(Vector2.zero, 1f));
				if (this.controller.movementPlane == MovementPlane.XY)
				{
					quaternion *= Quaternion.Euler(-90f, 180f, 0f);
				}
				base.transform.rotation = Quaternion.Slerp(rotation, quaternion, Time.deltaTime * 5f);
			}
			RaycastHit raycastHit;
			if (this.controller.movementPlane == MovementPlane.XZ && Physics.Raycast(vector + Vector3.up, Vector3.down, out raycastHit, 2f, this.groundMask))
			{
				vector.y = raycastHit.point.y;
			}
			base.transform.position = vector;
		}

		// Token: 0x04004451 RID: 17489
		public float repathRate = 1f;

		// Token: 0x04004452 RID: 17490
		private float nextRepath;

		// Token: 0x04004453 RID: 17491
		private Vector3 target;

		// Token: 0x04004454 RID: 17492
		private bool canSearchAgain = true;

		// Token: 0x04004455 RID: 17493
		private RVOController controller;

		// Token: 0x04004456 RID: 17494
		public float maxSpeed = 10f;

		// Token: 0x04004457 RID: 17495
		private Path path;

		// Token: 0x04004458 RID: 17496
		private List<Vector3> vectorPath;

		// Token: 0x04004459 RID: 17497
		private int wp;

		// Token: 0x0400445A RID: 17498
		public float moveNextDist = 1f;

		// Token: 0x0400445B RID: 17499
		public float slowdownDistance = 1f;

		// Token: 0x0400445C RID: 17500
		public LayerMask groundMask;

		// Token: 0x0400445D RID: 17501
		private Seeker seeker;

		// Token: 0x0400445E RID: 17502
		private MeshRenderer[] rends;
	}
}

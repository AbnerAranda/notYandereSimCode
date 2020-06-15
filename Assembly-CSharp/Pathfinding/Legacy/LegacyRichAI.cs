﻿using System;
using UnityEngine;

namespace Pathfinding.Legacy
{
	// Token: 0x020005BE RID: 1470
	[RequireComponent(typeof(Seeker))]
	[AddComponentMenu("Pathfinding/Legacy/AI/Legacy RichAI (3D, for navmesh)")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_legacy_1_1_legacy_rich_a_i.php")]
	public class LegacyRichAI : RichAI
	{
		// Token: 0x060027BF RID: 10175 RVA: 0x001B40C3 File Offset: 0x001B22C3
		protected override void Awake()
		{
			base.Awake();
			if (this.rvoController != null)
			{
				if (this.rvoController is LegacyRVOController)
				{
					(this.rvoController as LegacyRVOController).enableRotation = false;
					return;
				}
				Debug.LogError("The LegacyRichAI component only works with the legacy RVOController, not the latest one. Please upgrade this component", this);
			}
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x001B4104 File Offset: 0x001B2304
		protected override void Update()
		{
			LegacyRichAI.deltaTime = Mathf.Min(Time.smoothDeltaTime * 2f, Time.deltaTime);
			if (this.richPath != null)
			{
				RichPathPart currentPart = this.richPath.GetCurrentPart();
				RichFunnel richFunnel = currentPart as RichFunnel;
				if (richFunnel != null)
				{
					Vector3 vector = this.UpdateTarget(richFunnel);
					if (Time.frameCount % 5 == 0 && this.wallForce > 0f && this.wallDist > 0f)
					{
						this.wallBuffer.Clear();
						richFunnel.FindWalls(this.wallBuffer, this.wallDist);
					}
					int num = 0;
					Vector3 vector2 = this.nextCorners[num];
					Vector3 vector3 = vector2 - vector;
					vector3.y = 0f;
					if (Vector3.Dot(vector3, this.currentTargetDirection) < 0f && this.nextCorners.Count - num > 1)
					{
						num++;
						vector2 = this.nextCorners[num];
					}
					if (vector2 != this.lastTargetPoint)
					{
						this.currentTargetDirection = vector2 - vector;
						this.currentTargetDirection.y = 0f;
						this.currentTargetDirection.Normalize();
						this.lastTargetPoint = vector2;
					}
					vector3 = vector2 - vector;
					vector3.y = 0f;
					float magnitude = vector3.magnitude;
					this.distanceToSteeringTarget = magnitude;
					vector3 = ((magnitude == 0f) ? Vector3.zero : (vector3 / magnitude));
					Vector3 lhs = vector3;
					Vector3 a = Vector3.zero;
					if (this.wallForce > 0f && this.wallDist > 0f)
					{
						float num2 = 0f;
						float num3 = 0f;
						for (int i = 0; i < this.wallBuffer.Count; i += 2)
						{
							float sqrMagnitude = (VectorMath.ClosestPointOnSegment(this.wallBuffer[i], this.wallBuffer[i + 1], this.tr.position) - vector).sqrMagnitude;
							if (sqrMagnitude <= this.wallDist * this.wallDist)
							{
								Vector3 normalized = (this.wallBuffer[i + 1] - this.wallBuffer[i]).normalized;
								float num4 = Vector3.Dot(vector3, normalized) * (1f - Math.Max(0f, 2f * (sqrMagnitude / (this.wallDist * this.wallDist)) - 1f));
								if (num4 > 0f)
								{
									num3 = Math.Max(num3, num4);
								}
								else
								{
									num2 = Math.Max(num2, -num4);
								}
							}
						}
						a = Vector3.Cross(Vector3.up, vector3) * (num3 - num2);
					}
					bool flag = this.lastCorner && this.nextCorners.Count - num == 1;
					if (flag)
					{
						if (this.slowdownTime < 0.001f)
						{
							this.slowdownTime = 0.001f;
						}
						Vector3 a2 = vector2 - vector;
						a2.y = 0f;
						if (this.preciseSlowdown)
						{
							vector3 = (6f * a2 - 4f * this.slowdownTime * this.velocity) / (this.slowdownTime * this.slowdownTime);
						}
						else
						{
							vector3 = 2f * (a2 - this.slowdownTime * this.velocity) / (this.slowdownTime * this.slowdownTime);
						}
						vector3 = Vector3.ClampMagnitude(vector3, this.acceleration);
						a *= Math.Min(magnitude / 0.5f, 1f);
						if (magnitude < this.endReachedDistance)
						{
							base.NextPart();
						}
					}
					else
					{
						vector3 *= this.acceleration;
					}
					this.velocity += (vector3 + a * this.wallForce) * LegacyRichAI.deltaTime;
					if (this.slowWhenNotFacingTarget)
					{
						float a3 = (Vector3.Dot(lhs, this.tr.forward) + 0.5f) * 0.6666667f;
						float a4 = Mathf.Sqrt(this.velocity.x * this.velocity.x + this.velocity.z * this.velocity.z);
						float y = this.velocity.y;
						this.velocity.y = 0f;
						float d = Mathf.Min(a4, this.maxSpeed * Mathf.Max(a3, 0.2f));
						this.velocity = Vector3.Lerp(this.tr.forward * d, this.velocity.normalized * d, Mathf.Clamp(flag ? (magnitude * 2f) : 0f, 0.5f, 1f));
						this.velocity.y = y;
					}
					else
					{
						float num5 = Mathf.Sqrt(this.velocity.x * this.velocity.x + this.velocity.z * this.velocity.z);
						num5 = this.maxSpeed / num5;
						if (num5 < 1f)
						{
							this.velocity.x = this.velocity.x * num5;
							this.velocity.z = this.velocity.z * num5;
						}
					}
					if (flag)
					{
						Vector3 trotdir = Vector3.Lerp(this.velocity, this.currentTargetDirection, Math.Max(1f - magnitude * 2f, 0f));
						this.RotateTowards(trotdir);
					}
					else
					{
						this.RotateTowards(this.velocity);
					}
					this.velocity += LegacyRichAI.deltaTime * this.gravity;
					if (this.rvoController != null && this.rvoController.enabled)
					{
						this.tr.position = vector;
						this.rvoController.Move(this.velocity);
					}
					else if (this.controller != null && this.controller.enabled)
					{
						this.tr.position = vector;
						this.controller.Move(this.velocity * LegacyRichAI.deltaTime);
					}
					else
					{
						float y2 = vector.y;
						vector += this.velocity * LegacyRichAI.deltaTime;
						vector = this.RaycastPosition(vector, y2);
						this.tr.position = vector;
					}
				}
				else if (this.rvoController != null && this.rvoController.enabled)
				{
					this.rvoController.Move(Vector3.zero);
				}
				if (currentPart is RichSpecial && !base.traversingOffMeshLink)
				{
					base.StartCoroutine(this.TraverseSpecial(currentPart as RichSpecial));
				}
			}
			else if (this.rvoController != null && this.rvoController.enabled)
			{
				this.rvoController.Move(Vector3.zero);
			}
			else if (!(this.controller != null) || !this.controller.enabled)
			{
				this.tr.position = this.RaycastPosition(this.tr.position, this.tr.position.y);
			}
			base.UpdateVelocity();
			this.lastDeltaTime = Time.deltaTime;
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x001B4868 File Offset: 0x001B2A68
		private new Vector3 RaycastPosition(Vector3 position, float lasty)
		{
			if (this.raycastingForGroundPlacement)
			{
				float num = Mathf.Max(this.centerOffset, lasty - position.y + this.centerOffset);
				RaycastHit raycastHit;
				if (Physics.Raycast(position + Vector3.up * num, Vector3.down, out raycastHit, num, this.groundMask) && raycastHit.distance < num)
				{
					position = raycastHit.point;
					this.velocity.y = 0f;
				}
			}
			return position;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x001B48E8 File Offset: 0x001B2AE8
		private bool RotateTowards(Vector3 trotdir)
		{
			trotdir.y = 0f;
			if (trotdir != Vector3.zero)
			{
				Quaternion rotation = this.tr.rotation;
				Vector3 eulerAngles = Quaternion.LookRotation(trotdir).eulerAngles;
				Vector3 eulerAngles2 = rotation.eulerAngles;
				eulerAngles2.y = Mathf.MoveTowardsAngle(eulerAngles2.y, eulerAngles.y, this.rotationSpeed * LegacyRichAI.deltaTime);
				this.tr.rotation = Quaternion.Euler(eulerAngles2);
				return Mathf.Abs(eulerAngles2.y - eulerAngles.y) < 5f;
			}
			return false;
		}

		// Token: 0x040042B4 RID: 17076
		public bool preciseSlowdown = true;

		// Token: 0x040042B5 RID: 17077
		public bool raycastingForGroundPlacement;

		// Token: 0x040042B6 RID: 17078
		private new Vector3 velocity;

		// Token: 0x040042B7 RID: 17079
		private Vector3 lastTargetPoint;

		// Token: 0x040042B8 RID: 17080
		private Vector3 currentTargetDirection;

		// Token: 0x040042B9 RID: 17081
		private static float deltaTime;
	}
}

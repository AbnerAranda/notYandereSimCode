using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pathfinding.Examples
{
	// Token: 0x02000604 RID: 1540
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_turn_based_manager.php")]
	public class TurnBasedManager : MonoBehaviour
	{
		// Token: 0x06002A28 RID: 10792 RVA: 0x001C724A File Offset: 0x001C544A
		private void Awake()
		{
			this.eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x001C7258 File Offset: 0x001C5458
		private void Update()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (this.eventSystem.IsPointerOverGameObject())
			{
				return;
			}
			if (this.state == TurnBasedManager.State.SelectTarget)
			{
				this.HandleButtonUnderRay(ray);
			}
			if ((this.state == TurnBasedManager.State.SelectUnit || this.state == TurnBasedManager.State.SelectTarget) && Input.GetKeyDown(KeyCode.Mouse0))
			{
				TurnBasedAI byRay = this.GetByRay<TurnBasedAI>(ray);
				if (byRay != null)
				{
					this.Select(byRay);
					this.DestroyPossibleMoves();
					this.GeneratePossibleMoves(this.selected);
					this.state = TurnBasedManager.State.SelectTarget;
				}
			}
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x001C72E4 File Offset: 0x001C54E4
		private void HandleButtonUnderRay(Ray ray)
		{
			Astar3DButton byRay = this.GetByRay<Astar3DButton>(ray);
			if (byRay != null && Input.GetKeyDown(KeyCode.Mouse0))
			{
				byRay.OnClick();
				this.DestroyPossibleMoves();
				this.state = TurnBasedManager.State.Move;
				base.StartCoroutine(this.MoveToNode(this.selected, byRay.node));
			}
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x001C733C File Offset: 0x001C553C
		private T GetByRay<T>(Ray ray) where T : class
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity, this.layerMask))
			{
				return raycastHit.transform.GetComponentInParent<T>();
			}
			return default(T);
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x001C7379 File Offset: 0x001C5579
		private void Select(TurnBasedAI unit)
		{
			this.selected = unit;
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x001C7382 File Offset: 0x001C5582
		private IEnumerator MoveToNode(TurnBasedAI unit, GraphNode node)
		{
			ABPath path = ABPath.Construct(unit.transform.position, (Vector3)node.position, null);
			path.traversalProvider = unit.traversalProvider;
			AstarPath.StartPath(path, false);
			yield return base.StartCoroutine(path.WaitForPath());
			if (path.error)
			{
				Debug.LogError("Path failed:\n" + path.errorLog);
				this.state = TurnBasedManager.State.SelectTarget;
				this.GeneratePossibleMoves(this.selected);
				yield break;
			}
			unit.targetNode = path.path[path.path.Count - 1];
			yield return base.StartCoroutine(TurnBasedManager.MoveAlongPath(unit, path, this.movementSpeed));
			unit.blocker.BlockAtCurrentPosition();
			this.state = TurnBasedManager.State.SelectUnit;
			yield break;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x001C739F File Offset: 0x001C559F
		private static IEnumerator MoveAlongPath(TurnBasedAI unit, ABPath path, float speed)
		{
			if (path.error || path.vectorPath.Count == 0)
			{
				throw new ArgumentException("Cannot follow an empty path");
			}
			float distanceAlongSegment = 0f;
			int num;
			for (int i = 0; i < path.vectorPath.Count - 1; i = num + 1)
			{
				Vector3 p0 = path.vectorPath[Mathf.Max(i - 1, 0)];
				Vector3 p = path.vectorPath[i];
				Vector3 p2 = path.vectorPath[i + 1];
				Vector3 p3 = path.vectorPath[Mathf.Min(i + 2, path.vectorPath.Count - 1)];
				float segmentLength = Vector3.Distance(p, p2);
				while (distanceAlongSegment < segmentLength)
				{
					Vector3 position = AstarSplines.CatmullRom(p0, p, p2, p3, distanceAlongSegment / segmentLength);
					unit.transform.position = position;
					yield return null;
					distanceAlongSegment += Time.deltaTime * speed;
				}
				distanceAlongSegment -= segmentLength;
				p0 = default(Vector3);
				p = default(Vector3);
				p2 = default(Vector3);
				p3 = default(Vector3);
				num = i;
			}
			unit.transform.position = path.vectorPath[path.vectorPath.Count - 1];
			yield break;
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x001C73BC File Offset: 0x001C55BC
		private void DestroyPossibleMoves()
		{
			foreach (GameObject obj in this.possibleMoves)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.possibleMoves.Clear();
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x001C7418 File Offset: 0x001C5618
		private void GeneratePossibleMoves(TurnBasedAI unit)
		{
			ConstantPath constantPath = ConstantPath.Construct(unit.transform.position, unit.movementPoints * 1000 + 1, null);
			constantPath.traversalProvider = unit.traversalProvider;
			AstarPath.StartPath(constantPath, false);
			constantPath.BlockUntilCalculated();
			foreach (GraphNode graphNode in constantPath.allNodes)
			{
				if (graphNode != constantPath.startNode)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.nodePrefab, (Vector3)graphNode.position, Quaternion.identity);
					this.possibleMoves.Add(gameObject);
					gameObject.GetComponent<Astar3DButton>().node = graphNode;
				}
			}
		}

		// Token: 0x04004473 RID: 17523
		private TurnBasedAI selected;

		// Token: 0x04004474 RID: 17524
		public float movementSpeed;

		// Token: 0x04004475 RID: 17525
		public GameObject nodePrefab;

		// Token: 0x04004476 RID: 17526
		public LayerMask layerMask;

		// Token: 0x04004477 RID: 17527
		private List<GameObject> possibleMoves = new List<GameObject>();

		// Token: 0x04004478 RID: 17528
		private EventSystem eventSystem;

		// Token: 0x04004479 RID: 17529
		public TurnBasedManager.State state;

		// Token: 0x0200079E RID: 1950
		public enum State
		{
			// Token: 0x04004BB1 RID: 19377
			SelectUnit,
			// Token: 0x04004BB2 RID: 19378
			SelectTarget,
			// Token: 0x04004BB3 RID: 19379
			Move
		}
	}
}

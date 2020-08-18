using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pathfinding.Examples
{
	// Token: 0x020005EE RID: 1518
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_turn_based_manager.php")]
	public class TurnBasedManager : MonoBehaviour
	{
		// Token: 0x060029D2 RID: 10706 RVA: 0x001C165E File Offset: 0x001BF85E
		private void Awake()
		{
			this.eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x001C166C File Offset: 0x001BF86C
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

		// Token: 0x060029D4 RID: 10708 RVA: 0x001C16F8 File Offset: 0x001BF8F8
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

		// Token: 0x060029D5 RID: 10709 RVA: 0x001C1750 File Offset: 0x001BF950
		private T GetByRay<T>(Ray ray) where T : class
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity, this.layerMask))
			{
				return raycastHit.transform.GetComponentInParent<T>();
			}
			return default(T);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x001C178D File Offset: 0x001BF98D
		private void Select(TurnBasedAI unit)
		{
			this.selected = unit;
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x001C1796 File Offset: 0x001BF996
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

		// Token: 0x060029D8 RID: 10712 RVA: 0x001C17B3 File Offset: 0x001BF9B3
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

		// Token: 0x060029D9 RID: 10713 RVA: 0x001C17D0 File Offset: 0x001BF9D0
		private void DestroyPossibleMoves()
		{
			foreach (GameObject obj in this.possibleMoves)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.possibleMoves.Clear();
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x001C182C File Offset: 0x001BFA2C
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

		// Token: 0x04004395 RID: 17301
		private TurnBasedAI selected;

		// Token: 0x04004396 RID: 17302
		public float movementSpeed;

		// Token: 0x04004397 RID: 17303
		public GameObject nodePrefab;

		// Token: 0x04004398 RID: 17304
		public LayerMask layerMask;

		// Token: 0x04004399 RID: 17305
		private List<GameObject> possibleMoves = new List<GameObject>();

		// Token: 0x0400439A RID: 17306
		private EventSystem eventSystem;

		// Token: 0x0400439B RID: 17307
		public TurnBasedManager.State state;

		// Token: 0x02000784 RID: 1924
		public enum State
		{
			// Token: 0x04004AC6 RID: 19142
			SelectUnit,
			// Token: 0x04004AC7 RID: 19143
			SelectTarget,
			// Token: 0x04004AC8 RID: 19144
			Move
		}
	}
}

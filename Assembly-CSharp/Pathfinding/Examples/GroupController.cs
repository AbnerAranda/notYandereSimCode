using System;
using System.Collections.Generic;
using Pathfinding.RVO;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005FA RID: 1530
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_group_controller.php")]
	public class GroupController : MonoBehaviour
	{
		// Token: 0x060029F5 RID: 10741 RVA: 0x001C5014 File Offset: 0x001C3214
		public void Start()
		{
			this.cam = Camera.main;
			RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
			if (rvosimulator == null)
			{
				base.enabled = false;
				throw new Exception("No RVOSimulator in the scene. Please add one");
			}
			this.sim = rvosimulator.GetSimulator();
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x001C5068 File Offset: 0x001C3268
		public void Update()
		{
			if (Screen.fullScreen && Screen.width != Screen.resolutions[Screen.resolutions.Length - 1].width)
			{
				Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length - 1].width, Screen.resolutions[Screen.resolutions.Length - 1].height, true);
			}
			if (this.adjustCamera)
			{
				List<Agent> agents = this.sim.GetAgents();
				float num = 0f;
				for (int i = 0; i < agents.Count; i++)
				{
					float num2 = Mathf.Max(Mathf.Abs(agents[i].Position.x), Mathf.Abs(agents[i].Position.y));
					if (num2 > num)
					{
						num = num2;
					}
				}
				float a = num / Mathf.Tan(this.cam.fieldOfView * 0.0174532924f / 2f);
				float b = num / Mathf.Tan(Mathf.Atan(Mathf.Tan(this.cam.fieldOfView * 0.0174532924f / 2f) * this.cam.aspect));
				float num3 = Mathf.Max(a, b) * 1.1f;
				num3 = Mathf.Max(num3, 20f);
				this.cam.transform.position = Vector3.Lerp(this.cam.transform.position, new Vector3(0f, num3, 0f), Time.smoothDeltaTime * 2f);
			}
			if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Mouse0))
			{
				this.Order();
			}
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x001C5208 File Offset: 0x001C3408
		private void OnGUI()
		{
			if (Event.current.type == EventType.MouseUp && Event.current.button == 0 && !Input.GetKey(KeyCode.A))
			{
				this.Select(this.start, this.end);
				this.wasDown = false;
			}
			if (Event.current.type == EventType.MouseDrag && Event.current.button == 0)
			{
				this.end = Event.current.mousePosition;
				if (!this.wasDown)
				{
					this.start = this.end;
					this.wasDown = true;
				}
			}
			if (Input.GetKey(KeyCode.A))
			{
				this.wasDown = false;
			}
			if (this.wasDown)
			{
				Rect position = Rect.MinMaxRect(Mathf.Min(this.start.x, this.end.x), Mathf.Min(this.start.y, this.end.y), Mathf.Max(this.start.x, this.end.x), Mathf.Max(this.start.y, this.end.y));
				if (position.width > 4f && position.height > 4f)
				{
					GUI.Box(position, "", this.selectionBox);
				}
			}
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x001C5350 File Offset: 0x001C3550
		public void Order()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out raycastHit))
			{
				float num = 0f;
				for (int i = 0; i < this.selection.Count; i++)
				{
					num += this.selection[i].GetComponent<RVOController>().radius;
				}
				float num2 = num / 3.14159274f;
				num2 *= 2f;
				for (int j = 0; j < this.selection.Count; j++)
				{
					float num3 = 6.28318548f * (float)j / (float)this.selection.Count;
					Vector3 target = raycastHit.point + new Vector3(Mathf.Cos(num3), 0f, Mathf.Sin(num3)) * num2;
					this.selection[j].SetTarget(target);
					this.selection[j].SetColor(this.GetColor(num3));
					this.selection[j].RecalculatePath();
				}
			}
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x001C5464 File Offset: 0x001C3664
		public void Select(Vector2 _start, Vector2 _end)
		{
			_start.y = (float)Screen.height - _start.y;
			_end.y = (float)Screen.height - _end.y;
			Vector2 vector = Vector2.Min(_start, _end);
			Vector2 vector2 = Vector2.Max(_start, _end);
			if ((vector2 - vector).sqrMagnitude < 16f)
			{
				return;
			}
			this.selection.Clear();
			RVOExampleAgent[] array = UnityEngine.Object.FindObjectsOfType(typeof(RVOExampleAgent)) as RVOExampleAgent[];
			for (int i = 0; i < array.Length; i++)
			{
				Vector2 vector3 = this.cam.WorldToScreenPoint(array[i].transform.position);
				if (vector3.x > vector.x && vector3.y > vector.y && vector3.x < vector2.x && vector3.y < vector2.y)
				{
					this.selection.Add(array[i]);
				}
			}
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x001C555D File Offset: 0x001C375D
		public Color GetColor(float angle)
		{
			return AstarMath.HSVToRGB(angle * 57.2957764f, 0.8f, 0.6f);
		}

		// Token: 0x0400442C RID: 17452
		public GUIStyle selectionBox;

		// Token: 0x0400442D RID: 17453
		public bool adjustCamera = true;

		// Token: 0x0400442E RID: 17454
		private Vector2 start;

		// Token: 0x0400442F RID: 17455
		private Vector2 end;

		// Token: 0x04004430 RID: 17456
		private bool wasDown;

		// Token: 0x04004431 RID: 17457
		private List<RVOExampleAgent> selection = new List<RVOExampleAgent>();

		// Token: 0x04004432 RID: 17458
		private Simulator sim;

		// Token: 0x04004433 RID: 17459
		private Camera cam;

		// Token: 0x04004434 RID: 17460
		private const float rad2Deg = 57.2957764f;
	}
}

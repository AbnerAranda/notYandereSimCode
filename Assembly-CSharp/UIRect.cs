using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
public abstract class UIRect : MonoBehaviour
{
	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06000522 RID: 1314 RVA: 0x000313D1 File Offset: 0x0002F5D1
	public GameObject cachedGameObject
	{
		get
		{
			if (this.mGo == null)
			{
				this.mGo = base.gameObject;
			}
			return this.mGo;
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000523 RID: 1315 RVA: 0x000313F3 File Offset: 0x0002F5F3
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x06000524 RID: 1316 RVA: 0x00031415 File Offset: 0x0002F615
	public Camera anchorCamera
	{
		get
		{
			if (!this.mCam || !this.mAnchorsCached)
			{
				this.ResetAnchors();
			}
			return this.mCam;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x06000525 RID: 1317 RVA: 0x00031438 File Offset: 0x0002F638
	public bool isFullyAnchored
	{
		get
		{
			return this.leftAnchor.target && this.rightAnchor.target && this.topAnchor.target && this.bottomAnchor.target;
		}
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x06000526 RID: 1318 RVA: 0x0003148D File Offset: 0x0002F68D
	public virtual bool isAnchoredHorizontally
	{
		get
		{
			return this.leftAnchor.target || this.rightAnchor.target;
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000527 RID: 1319 RVA: 0x000314B3 File Offset: 0x0002F6B3
	public virtual bool isAnchoredVertically
	{
		get
		{
			return this.bottomAnchor.target || this.topAnchor.target;
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x06000528 RID: 1320 RVA: 0x00022944 File Offset: 0x00020B44
	public virtual bool canBeAnchored
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x06000529 RID: 1321 RVA: 0x000314D9 File Offset: 0x0002F6D9
	public UIRect parent
	{
		get
		{
			if (!this.mParentFound)
			{
				this.mParentFound = true;
				this.mParent = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
			}
			return this.mParent;
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x0600052A RID: 1322 RVA: 0x00031508 File Offset: 0x0002F708
	public UIRoot root
	{
		get
		{
			if (this.parent != null)
			{
				return this.mParent.root;
			}
			if (!this.mRootSet)
			{
				this.mRootSet = true;
				this.mRoot = NGUITools.FindInParents<UIRoot>(this.cachedTransform);
			}
			return this.mRoot;
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x0600052B RID: 1323 RVA: 0x00031558 File Offset: 0x0002F758
	public bool isAnchored
	{
		get
		{
			return (this.leftAnchor.target || this.rightAnchor.target || this.topAnchor.target || this.bottomAnchor.target) && this.canBeAnchored;
		}
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x0600052C RID: 1324
	// (set) Token: 0x0600052D RID: 1325
	public abstract float alpha { get; set; }

	// Token: 0x0600052E RID: 1326
	public abstract float CalculateFinalAlpha(int frameID);

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x0600052F RID: 1327
	public abstract Vector3[] localCorners { get; }

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000530 RID: 1328
	public abstract Vector3[] worldCorners { get; }

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06000531 RID: 1329 RVA: 0x000315B8 File Offset: 0x0002F7B8
	protected float cameraRayDistance
	{
		get
		{
			if (this.anchorCamera == null)
			{
				return 0f;
			}
			if (!this.mCam.orthographic)
			{
				Transform cachedTransform = this.cachedTransform;
				Transform transform = this.mCam.transform;
				Plane plane = new Plane(cachedTransform.rotation * Vector3.back, cachedTransform.position);
				Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);
				float result;
				if (plane.Raycast(ray, out result))
				{
					return result;
				}
			}
			return Mathf.Lerp(this.mCam.nearClipPlane, this.mCam.farClipPlane, 0.5f);
		}
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00031664 File Offset: 0x0002F864
	public virtual void Invalidate(bool includeChildren)
	{
		this.mChanged = true;
		if (includeChildren)
		{
			for (int i = 0; i < this.mChildren.size; i++)
			{
				this.mChildren.buffer[i].Invalidate(true);
			}
		}
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x000316A4 File Offset: 0x0002F8A4
	public virtual Vector3[] GetSides(Transform relativeTo)
	{
		if (this.anchorCamera != null)
		{
			return this.mCam.GetSides(this.cameraRayDistance, relativeTo);
		}
		Vector3 position = this.cachedTransform.position;
		for (int i = 0; i < 4; i++)
		{
			UIRect.mSides[i] = position;
		}
		if (relativeTo != null)
		{
			for (int j = 0; j < 4; j++)
			{
				UIRect.mSides[j] = relativeTo.InverseTransformPoint(UIRect.mSides[j]);
			}
		}
		return UIRect.mSides;
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0003172C File Offset: 0x0002F92C
	protected Vector3 GetLocalPos(UIRect.AnchorPoint ac, Transform trans)
	{
		if (ac.targetCam == null)
		{
			this.FindCameraFor(ac);
		}
		if (this.anchorCamera == null || ac.targetCam == null)
		{
			return this.cachedTransform.localPosition;
		}
		Rect rect = ac.targetCam.rect;
		Vector3 vector = ac.targetCam.WorldToViewportPoint(ac.target.position);
		Vector3 vector2 = new Vector3(vector.x * rect.width + rect.x, vector.y * rect.height + rect.y, vector.z);
		vector2 = this.mCam.ViewportToWorldPoint(vector2);
		if (trans != null)
		{
			vector2 = trans.InverseTransformPoint(vector2);
		}
		vector2.x = Mathf.Floor(vector2.x + 0.5f);
		vector2.y = Mathf.Floor(vector2.y + 0.5f);
		return vector2;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00031821 File Offset: 0x0002FA21
	protected virtual void OnEnable()
	{
		this.mUpdateFrame = -1;
		if (this.updateAnchors == UIRect.AnchorUpdate.OnEnable)
		{
			this.mAnchorsCached = false;
			this.mUpdateAnchors = true;
		}
		if (this.mStarted)
		{
			this.OnInit();
		}
		this.mUpdateFrame = -1;
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00031855 File Offset: 0x0002FA55
	protected virtual void OnInit()
	{
		this.mChanged = true;
		this.mRootSet = false;
		this.mParentFound = false;
		if (this.parent != null)
		{
			this.mParent.mChildren.Add(this);
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x0003188B File Offset: 0x0002FA8B
	protected virtual void OnDisable()
	{
		if (this.mParent)
		{
			this.mParent.mChildren.Remove(this);
		}
		this.mParent = null;
		this.mRoot = null;
		this.mRootSet = false;
		this.mParentFound = false;
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x000318C8 File Offset: 0x0002FAC8
	protected virtual void Awake()
	{
		NGUITools.CheckForPrefabStage(base.gameObject);
		this.mStarted = false;
		this.mGo = base.gameObject;
		this.mTrans = base.transform;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x000318F4 File Offset: 0x0002FAF4
	protected void Start()
	{
		this.mStarted = true;
		this.OnInit();
		this.OnStart();
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x0003190C File Offset: 0x0002FB0C
	public void Update()
	{
		if (!this.mCam)
		{
			this.ResetAndUpdateAnchors();
			this.mUpdateFrame = -1;
		}
		else if (!this.mAnchorsCached)
		{
			this.ResetAnchors();
		}
		int frameCount = Time.frameCount;
		if (this.mUpdateFrame != frameCount)
		{
			if (this.updateAnchors == UIRect.AnchorUpdate.OnUpdate || this.mUpdateAnchors)
			{
				this.UpdateAnchorsInternal(frameCount);
			}
			this.OnUpdate();
		}
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00031970 File Offset: 0x0002FB70
	protected void UpdateAnchorsInternal(int frame)
	{
		this.mUpdateFrame = frame;
		this.mUpdateAnchors = false;
		bool flag = false;
		if (this.leftAnchor.target)
		{
			flag = true;
			if (this.leftAnchor.rect != null && this.leftAnchor.rect.mUpdateFrame != frame)
			{
				this.leftAnchor.rect.Update();
			}
		}
		if (this.bottomAnchor.target)
		{
			flag = true;
			if (this.bottomAnchor.rect != null && this.bottomAnchor.rect.mUpdateFrame != frame)
			{
				this.bottomAnchor.rect.Update();
			}
		}
		if (this.rightAnchor.target)
		{
			flag = true;
			if (this.rightAnchor.rect != null && this.rightAnchor.rect.mUpdateFrame != frame)
			{
				this.rightAnchor.rect.Update();
			}
		}
		if (this.topAnchor.target)
		{
			flag = true;
			if (this.topAnchor.rect != null && this.topAnchor.rect.mUpdateFrame != frame)
			{
				this.topAnchor.rect.Update();
			}
		}
		if (flag)
		{
			this.OnAnchor();
		}
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x00031ABE File Offset: 0x0002FCBE
	public void UpdateAnchors()
	{
		if (this.isAnchored)
		{
			this.mUpdateFrame = -1;
			this.mUpdateAnchors = true;
			this.UpdateAnchorsInternal(Time.frameCount);
		}
	}

	// Token: 0x0600053D RID: 1341
	protected abstract void OnAnchor();

	// Token: 0x0600053E RID: 1342 RVA: 0x00031AE1 File Offset: 0x0002FCE1
	public void SetAnchor(Transform t)
	{
		this.leftAnchor.target = t;
		this.rightAnchor.target = t;
		this.topAnchor.target = t;
		this.bottomAnchor.target = t;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00031B20 File Offset: 0x0002FD20
	public void SetAnchor(GameObject go)
	{
		Transform target = (go != null) ? go.transform : null;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00031B7C File Offset: 0x0002FD7C
	public void SetAnchor(GameObject go, int left, int bottom, int right, int top)
	{
		Transform target = (go != null) ? go.transform : null;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = 0f;
		this.rightAnchor.relative = 1f;
		this.bottomAnchor.relative = 0f;
		this.topAnchor.relative = 1f;
		this.leftAnchor.absolute = left;
		this.rightAnchor.absolute = right;
		this.bottomAnchor.absolute = bottom;
		this.topAnchor.absolute = top;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00031C4C File Offset: 0x0002FE4C
	public void SetAnchor(GameObject go, float left, float bottom, float right, float top)
	{
		Transform target = (go != null) ? go.transform : null;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = left;
		this.rightAnchor.relative = right;
		this.bottomAnchor.relative = bottom;
		this.topAnchor.relative = top;
		this.leftAnchor.absolute = 0;
		this.rightAnchor.absolute = 0;
		this.bottomAnchor.absolute = 0;
		this.topAnchor.absolute = 0;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00031D0C File Offset: 0x0002FF0C
	public void SetAnchor(GameObject go, float left, int leftOffset, float bottom, int bottomOffset, float right, int rightOffset, float top, int topOffset)
	{
		Transform target = (go != null) ? go.transform : null;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = left;
		this.rightAnchor.relative = right;
		this.bottomAnchor.relative = bottom;
		this.topAnchor.relative = top;
		this.leftAnchor.absolute = leftOffset;
		this.rightAnchor.absolute = rightOffset;
		this.bottomAnchor.absolute = bottomOffset;
		this.topAnchor.absolute = topOffset;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00031DD0 File Offset: 0x0002FFD0
	public void SetAnchor(float left, int leftOffset, float bottom, int bottomOffset, float right, int rightOffset, float top, int topOffset)
	{
		Transform parent = this.cachedTransform.parent;
		this.leftAnchor.target = parent;
		this.rightAnchor.target = parent;
		this.topAnchor.target = parent;
		this.bottomAnchor.target = parent;
		this.leftAnchor.relative = left;
		this.rightAnchor.relative = right;
		this.bottomAnchor.relative = bottom;
		this.topAnchor.relative = top;
		this.leftAnchor.absolute = leftOffset;
		this.rightAnchor.absolute = rightOffset;
		this.bottomAnchor.absolute = bottomOffset;
		this.topAnchor.absolute = topOffset;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00031E8C File Offset: 0x0003008C
	public void SetScreenRect(int left, int top, int width, int height)
	{
		this.SetAnchor(0f, left, 1f, -top - height, 0f, left + width, 1f, -top);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00031EC0 File Offset: 0x000300C0
	public void ResetAnchors()
	{
		this.mAnchorsCached = true;
		this.leftAnchor.rect = (this.leftAnchor.target ? this.leftAnchor.target.GetComponent<UIRect>() : null);
		this.bottomAnchor.rect = (this.bottomAnchor.target ? this.bottomAnchor.target.GetComponent<UIRect>() : null);
		this.rightAnchor.rect = (this.rightAnchor.target ? this.rightAnchor.target.GetComponent<UIRect>() : null);
		this.topAnchor.rect = (this.topAnchor.target ? this.topAnchor.target.GetComponent<UIRect>() : null);
		this.mCam = NGUITools.FindCameraForLayer(this.cachedGameObject.layer);
		this.FindCameraFor(this.leftAnchor);
		this.FindCameraFor(this.bottomAnchor);
		this.FindCameraFor(this.rightAnchor);
		this.FindCameraFor(this.topAnchor);
		this.mUpdateAnchors = true;
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00031FE1 File Offset: 0x000301E1
	public void ResetAndUpdateAnchors()
	{
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x06000547 RID: 1351
	public abstract void SetRect(float x, float y, float width, float height);

	// Token: 0x06000548 RID: 1352 RVA: 0x00031FF0 File Offset: 0x000301F0
	private void FindCameraFor(UIRect.AnchorPoint ap)
	{
		if (ap.target == null || ap.rect != null)
		{
			ap.targetCam = null;
			return;
		}
		ap.targetCam = NGUITools.FindCameraForLayer(ap.target.gameObject.layer);
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0003203C File Offset: 0x0003023C
	public virtual void ParentHasChanged()
	{
		this.mParentFound = false;
		UIRect y = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
		if (this.mParent != y)
		{
			if (this.mParent)
			{
				this.mParent.mChildren.Remove(this);
			}
			this.mParent = y;
			if (this.mParent)
			{
				this.mParent.mChildren.Add(this);
			}
			this.mRootSet = false;
		}
	}

	// Token: 0x0600054A RID: 1354
	protected abstract void OnStart();

	// Token: 0x0600054B RID: 1355 RVA: 0x00002ACE File Offset: 0x00000CCE
	protected virtual void OnUpdate()
	{
	}

	// Token: 0x0400056E RID: 1390
	public UIRect.AnchorPoint leftAnchor = new UIRect.AnchorPoint();

	// Token: 0x0400056F RID: 1391
	public UIRect.AnchorPoint rightAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x04000570 RID: 1392
	public UIRect.AnchorPoint bottomAnchor = new UIRect.AnchorPoint();

	// Token: 0x04000571 RID: 1393
	public UIRect.AnchorPoint topAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x04000572 RID: 1394
	public UIRect.AnchorUpdate updateAnchors = UIRect.AnchorUpdate.OnUpdate;

	// Token: 0x04000573 RID: 1395
	[NonSerialized]
	protected GameObject mGo;

	// Token: 0x04000574 RID: 1396
	[NonSerialized]
	protected Transform mTrans;

	// Token: 0x04000575 RID: 1397
	[NonSerialized]
	protected BetterList<UIRect> mChildren = new BetterList<UIRect>();

	// Token: 0x04000576 RID: 1398
	[NonSerialized]
	protected bool mChanged = true;

	// Token: 0x04000577 RID: 1399
	[NonSerialized]
	protected bool mParentFound;

	// Token: 0x04000578 RID: 1400
	[NonSerialized]
	private bool mUpdateAnchors = true;

	// Token: 0x04000579 RID: 1401
	[NonSerialized]
	private int mUpdateFrame = -1;

	// Token: 0x0400057A RID: 1402
	[NonSerialized]
	private bool mAnchorsCached;

	// Token: 0x0400057B RID: 1403
	[NonSerialized]
	private UIRoot mRoot;

	// Token: 0x0400057C RID: 1404
	[NonSerialized]
	private UIRect mParent;

	// Token: 0x0400057D RID: 1405
	[NonSerialized]
	private bool mRootSet;

	// Token: 0x0400057E RID: 1406
	[NonSerialized]
	protected Camera mCam;

	// Token: 0x0400057F RID: 1407
	protected bool mStarted;

	// Token: 0x04000580 RID: 1408
	[NonSerialized]
	public float finalAlpha = 1f;

	// Token: 0x04000581 RID: 1409
	protected static Vector3[] mSides = new Vector3[4];

	// Token: 0x02000661 RID: 1633
	[Serializable]
	public class AnchorPoint
	{
		// Token: 0x06002B21 RID: 11041 RVA: 0x000045DB File Offset: 0x000027DB
		public AnchorPoint()
		{
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x001C9D58 File Offset: 0x001C7F58
		public AnchorPoint(float relative)
		{
			this.relative = relative;
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x001C9D67 File Offset: 0x001C7F67
		public void Set(float relative, float absolute)
		{
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x001C9D82 File Offset: 0x001C7F82
		public void Set(Transform target, float relative, float absolute)
		{
			this.target = target;
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x001C9DA4 File Offset: 0x001C7FA4
		public void SetToNearest(float abs0, float abs1, float abs2)
		{
			this.SetToNearest(0f, 0.5f, 1f, abs0, abs1, abs2);
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x001C9DC0 File Offset: 0x001C7FC0
		public void SetToNearest(float rel0, float rel1, float rel2, float abs0, float abs1, float abs2)
		{
			float num = Mathf.Abs(abs0);
			float num2 = Mathf.Abs(abs1);
			float num3 = Mathf.Abs(abs2);
			if (num < num2 && num < num3)
			{
				this.Set(rel0, abs0);
				return;
			}
			if (num2 < num && num2 < num3)
			{
				this.Set(rel1, abs1);
				return;
			}
			this.Set(rel2, abs2);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x001C9E14 File Offset: 0x001C8014
		public void SetHorizontal(Transform parent, float localPos)
		{
			if (this.rect)
			{
				Vector3[] sides = this.rect.GetSides(parent);
				float num = Mathf.Lerp(sides[0].x, sides[2].x, this.relative);
				this.absolute = Mathf.FloorToInt(localPos - num + 0.5f);
				return;
			}
			Vector3 vector = this.target.position;
			if (parent != null)
			{
				vector = parent.InverseTransformPoint(vector);
			}
			this.absolute = Mathf.FloorToInt(localPos - vector.x + 0.5f);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x001C9EAC File Offset: 0x001C80AC
		public void SetVertical(Transform parent, float localPos)
		{
			if (this.rect)
			{
				Vector3[] sides = this.rect.GetSides(parent);
				float num = Mathf.Lerp(sides[3].y, sides[1].y, this.relative);
				this.absolute = Mathf.FloorToInt(localPos - num + 0.5f);
				return;
			}
			Vector3 vector = this.target.position;
			if (parent != null)
			{
				vector = parent.InverseTransformPoint(vector);
			}
			this.absolute = Mathf.FloorToInt(localPos - vector.y + 0.5f);
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x001C9F44 File Offset: 0x001C8144
		public Vector3[] GetSides(Transform relativeTo)
		{
			if (this.target != null)
			{
				if (this.rect != null)
				{
					return this.rect.GetSides(relativeTo);
				}
				Camera component = this.target.GetComponent<Camera>();
				if (component != null)
				{
					return component.GetSides(relativeTo);
				}
			}
			return null;
		}

		// Token: 0x0400460E RID: 17934
		public Transform target;

		// Token: 0x0400460F RID: 17935
		public float relative;

		// Token: 0x04004610 RID: 17936
		public int absolute;

		// Token: 0x04004611 RID: 17937
		[NonSerialized]
		public UIRect rect;

		// Token: 0x04004612 RID: 17938
		[NonSerialized]
		public Camera targetCam;
	}

	// Token: 0x02000662 RID: 1634
	[DoNotObfuscateNGUI]
	public enum AnchorUpdate
	{
		// Token: 0x04004614 RID: 17940
		OnEnable,
		// Token: 0x04004615 RID: 17941
		OnUpdate,
		// Token: 0x04004616 RID: 17942
		OnStart
	}
}

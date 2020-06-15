using System;
using UnityEngine;

namespace Aura2API
{
	// Token: 0x02000521 RID: 1313
	public class LightFlicker : MonoBehaviour
	{
		// Token: 0x060020B8 RID: 8376 RVA: 0x0018BC63 File Offset: 0x00189E63
		private void Start()
		{
			UnityEngine.Random.InitState((int)base.transform.position.x + (int)base.transform.position.y);
			this._initialFactor = base.GetComponent<Light>().intensity;
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0018BC9E File Offset: 0x00189E9E
		private void OnEnable()
		{
			this._initPos = base.transform.localPosition;
			this._currentPos = this._initPos;
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x0018BCBD File Offset: 0x00189EBD
		private void OnDisable()
		{
			base.transform.localPosition = this._initPos;
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0018BCD0 File Offset: 0x00189ED0
		private void Update()
		{
			this._deltaTime = Time.deltaTime;
			if (this._timeLeft <= this._deltaTime)
			{
				this._targetFactor = UnityEngine.Random.Range(this.minFactor, this.maxFactor);
				this._targetPos = this._initPos + UnityEngine.Random.insideUnitSphere * this.moveRange;
				this._timeLeft = this.speed;
				return;
			}
			float t = this._deltaTime / this._timeLeft;
			this._currentFactor = Mathf.Lerp(this._currentFactor, this._targetFactor, t);
			base.GetComponent<Light>().intensity = this._initialFactor * this._currentFactor;
			this._currentPos = Vector3.Lerp(this._currentPos, this._targetPos, t);
			base.transform.localPosition = this._currentPos;
			this._timeLeft -= this._deltaTime;
		}

		// Token: 0x04003ED2 RID: 16082
		public float maxFactor = 1.2f;

		// Token: 0x04003ED3 RID: 16083
		public float minFactor = 1f;

		// Token: 0x04003ED4 RID: 16084
		public float moveRange = 0.1f;

		// Token: 0x04003ED5 RID: 16085
		public float speed = 0.1f;

		// Token: 0x04003ED6 RID: 16086
		private float _currentFactor = 1f;

		// Token: 0x04003ED7 RID: 16087
		private Vector3 _currentPos;

		// Token: 0x04003ED8 RID: 16088
		private float _deltaTime;

		// Token: 0x04003ED9 RID: 16089
		private Vector3 _initPos;

		// Token: 0x04003EDA RID: 16090
		private float _targetFactor;

		// Token: 0x04003EDB RID: 16091
		private Vector3 _targetPos;

		// Token: 0x04003EDC RID: 16092
		private float _initialFactor;

		// Token: 0x04003EDD RID: 16093
		private float _time;

		// Token: 0x04003EDE RID: 16094
		private float _timeLeft;
	}
}

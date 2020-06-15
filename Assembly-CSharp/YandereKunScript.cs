using System;
using UnityEngine;

// Token: 0x0200046F RID: 1135
public class YandereKunScript : MonoBehaviour
{
	// Token: 0x06001D57 RID: 7511 RVA: 0x0016071C File Offset: 0x0015E91C
	private void Start()
	{
		if (!this.Kizuna)
		{
			if (this.KunHips != null)
			{
				this.KunHips.parent = this.ChanHips;
			}
			if (this.KunSpine != null)
			{
				this.KunSpine.parent = this.ChanSpine;
			}
			if (this.KunSpine1 != null)
			{
				this.KunSpine1.parent = this.ChanSpine1;
			}
			if (this.KunSpine2 != null)
			{
				this.KunSpine2.parent = this.ChanSpine2;
			}
			if (this.KunSpine3 != null)
			{
				this.KunSpine3.parent = this.ChanSpine3;
			}
			if (this.KunNeck != null)
			{
				this.KunNeck.parent = this.ChanNeck;
			}
			if (this.KunHead != null)
			{
				this.KunHead.parent = this.ChanHead;
			}
			this.KunRightUpLeg.parent = this.ChanRightUpLeg;
			this.KunRightLeg.parent = this.ChanRightLeg;
			this.KunRightFoot.parent = this.ChanRightFoot;
			this.KunRightToes.parent = this.ChanRightToes;
			this.KunLeftUpLeg.parent = this.ChanLeftUpLeg;
			this.KunLeftLeg.parent = this.ChanLeftLeg;
			this.KunLeftFoot.parent = this.ChanLeftFoot;
			this.KunLeftToes.parent = this.ChanLeftToes;
			this.KunRightShoulder.parent = this.ChanRightShoulder;
			this.KunRightArm.parent = this.ChanRightArm;
			if (this.KunRightArmRoll != null)
			{
				this.KunRightArmRoll.parent = this.ChanRightArmRoll;
			}
			this.KunRightForeArm.parent = this.ChanRightForeArm;
			if (this.KunRightForeArmRoll != null)
			{
				this.KunRightForeArmRoll.parent = this.ChanRightForeArmRoll;
			}
			this.KunRightHand.parent = this.ChanRightHand;
			this.KunLeftShoulder.parent = this.ChanLeftShoulder;
			this.KunLeftArm.parent = this.ChanLeftArm;
			if (this.KunLeftArmRoll != null)
			{
				this.KunLeftArmRoll.parent = this.ChanLeftArmRoll;
			}
			this.KunLeftForeArm.parent = this.ChanLeftForeArm;
			if (this.KunLeftForeArmRoll != null)
			{
				this.KunLeftForeArmRoll.parent = this.ChanLeftForeArmRoll;
			}
			this.KunLeftHand.parent = this.ChanLeftHand;
			if (!this.Man)
			{
				this.KunLeftHandPinky1.parent = this.ChanLeftHandPinky1;
				this.KunLeftHandPinky2.parent = this.ChanLeftHandPinky2;
				this.KunLeftHandPinky3.parent = this.ChanLeftHandPinky3;
				this.KunLeftHandRing1.parent = this.ChanLeftHandRing1;
				this.KunLeftHandRing2.parent = this.ChanLeftHandRing2;
				this.KunLeftHandRing3.parent = this.ChanLeftHandRing3;
				this.KunLeftHandMiddle1.parent = this.ChanLeftHandMiddle1;
				this.KunLeftHandMiddle2.parent = this.ChanLeftHandMiddle2;
				this.KunLeftHandMiddle3.parent = this.ChanLeftHandMiddle3;
				this.KunLeftHandIndex1.parent = this.ChanLeftHandIndex1;
				this.KunLeftHandIndex2.parent = this.ChanLeftHandIndex2;
				this.KunLeftHandIndex3.parent = this.ChanLeftHandIndex3;
				this.KunLeftHandThumb1.parent = this.ChanLeftHandThumb1;
				this.KunLeftHandThumb2.parent = this.ChanLeftHandThumb2;
				this.KunLeftHandThumb3.parent = this.ChanLeftHandThumb3;
				this.KunRightHandPinky1.parent = this.ChanRightHandPinky1;
				this.KunRightHandPinky2.parent = this.ChanRightHandPinky2;
				this.KunRightHandPinky3.parent = this.ChanRightHandPinky3;
				this.KunRightHandRing1.parent = this.ChanRightHandRing1;
				this.KunRightHandRing2.parent = this.ChanRightHandRing2;
				this.KunRightHandRing3.parent = this.ChanRightHandRing3;
				this.KunRightHandMiddle1.parent = this.ChanRightHandMiddle1;
				this.KunRightHandMiddle2.parent = this.ChanRightHandMiddle2;
				this.KunRightHandMiddle3.parent = this.ChanRightHandMiddle3;
				this.KunRightHandIndex1.parent = this.ChanRightHandIndex1;
				this.KunRightHandIndex2.parent = this.ChanRightHandIndex2;
				this.KunRightHandIndex3.parent = this.ChanRightHandIndex3;
				this.KunRightHandThumb1.parent = this.ChanRightHandThumb1;
				this.KunRightHandThumb2.parent = this.ChanRightHandThumb2;
				this.KunRightHandThumb3.parent = this.ChanRightHandThumb3;
			}
		}
		if (this.MyRenderer != null)
		{
			this.MyRenderer.enabled = true;
		}
		if (this.SecondRenderer != null)
		{
			this.SecondRenderer.enabled = true;
		}
		if (this.ThirdRenderer != null)
		{
			this.ThirdRenderer.enabled = true;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001D58 RID: 7512 RVA: 0x00160BFC File Offset: 0x0015EDFC
	private void LateUpdate()
	{
		if (this.Man)
		{
			this.ChanItemParent.position = this.KunItemParent.position;
			if (!this.Adjusted)
			{
				this.KunRightShoulder.position += new Vector3(0f, 0.1f, 0f);
				this.KunRightArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunRightForeArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunRightHand.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftShoulder.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftForeArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftHand.position += new Vector3(0f, 0.1f, 0f);
				this.Adjusted = true;
			}
		}
		if (this.Kizuna)
		{
			this.KunItemParent.localPosition = new Vector3(0.066666f, -0.033333f, 0.02f);
			this.ChanItemParent.position = this.KunItemParent.position;
			this.KunHips.localPosition = this.ChanHips.localPosition;
			if (this.KunHips != null)
			{
				this.KunHips.eulerAngles = this.ChanHips.eulerAngles;
			}
			if (this.KunSpine != null)
			{
				this.KunSpine.eulerAngles = this.ChanSpine.eulerAngles;
			}
			if (this.KunSpine1 != null)
			{
				this.KunSpine1.eulerAngles = this.ChanSpine1.eulerAngles;
			}
			if (this.KunSpine2 != null)
			{
				this.KunSpine2.eulerAngles = this.ChanSpine2.eulerAngles;
			}
			if (this.KunSpine3 != null)
			{
				this.KunSpine3.eulerAngles = this.ChanSpine3.eulerAngles;
			}
			if (this.KunNeck != null)
			{
				this.KunNeck.eulerAngles = this.ChanNeck.eulerAngles;
			}
			if (this.KunHead != null)
			{
				this.KunHead.eulerAngles = this.ChanHead.eulerAngles;
			}
			this.KunRightUpLeg.eulerAngles = this.ChanRightUpLeg.eulerAngles;
			this.KunRightLeg.eulerAngles = this.ChanRightLeg.eulerAngles;
			this.KunRightFoot.eulerAngles = this.ChanRightFoot.eulerAngles;
			this.KunRightToes.eulerAngles = this.ChanRightToes.eulerAngles;
			this.KunLeftUpLeg.eulerAngles = this.ChanLeftUpLeg.eulerAngles;
			this.KunLeftLeg.eulerAngles = this.ChanLeftLeg.eulerAngles;
			this.KunLeftFoot.eulerAngles = this.ChanLeftFoot.eulerAngles;
			this.KunLeftToes.eulerAngles = this.ChanLeftToes.eulerAngles;
			this.KunRightShoulder.eulerAngles = this.ChanRightShoulder.eulerAngles;
			this.KunRightArm.eulerAngles = this.ChanRightArm.eulerAngles;
			if (this.KunLeftArmRoll != null)
			{
				this.KunRightArmRoll.eulerAngles = this.ChanRightArmRoll.eulerAngles;
			}
			this.KunRightForeArm.eulerAngles = this.ChanRightForeArm.eulerAngles;
			if (this.KunLeftArmRoll != null)
			{
				this.KunRightForeArmRoll.eulerAngles = this.ChanRightForeArmRoll.eulerAngles;
			}
			this.KunRightHand.eulerAngles = this.ChanRightHand.eulerAngles;
			this.KunLeftShoulder.eulerAngles = this.ChanLeftShoulder.eulerAngles;
			this.KunLeftArm.eulerAngles = this.ChanLeftArm.eulerAngles;
			if (this.KunLeftArmRoll != null)
			{
				this.KunLeftArmRoll.eulerAngles = this.ChanLeftArmRoll.eulerAngles;
			}
			this.KunLeftForeArm.eulerAngles = this.ChanLeftForeArm.eulerAngles;
			if (this.KunLeftForeArmRoll != null)
			{
				this.KunLeftForeArmRoll.eulerAngles = this.ChanLeftForeArmRoll.eulerAngles;
			}
			this.KunLeftHand.eulerAngles = this.ChanLeftHand.eulerAngles;
			this.KunLeftHandPinky1.eulerAngles = this.ChanLeftHandPinky1.eulerAngles;
			this.KunLeftHandPinky2.eulerAngles = this.ChanLeftHandPinky2.eulerAngles;
			this.KunLeftHandPinky3.eulerAngles = this.ChanLeftHandPinky3.eulerAngles;
			this.KunLeftHandRing1.eulerAngles = this.ChanLeftHandRing1.eulerAngles;
			this.KunLeftHandRing2.eulerAngles = this.ChanLeftHandRing2.eulerAngles;
			this.KunLeftHandRing3.eulerAngles = this.ChanLeftHandRing3.eulerAngles;
			this.KunLeftHandMiddle1.eulerAngles = this.ChanLeftHandMiddle1.eulerAngles;
			this.KunLeftHandMiddle2.eulerAngles = this.ChanLeftHandMiddle2.eulerAngles;
			this.KunLeftHandMiddle3.eulerAngles = this.ChanLeftHandMiddle3.eulerAngles;
			this.KunLeftHandIndex1.eulerAngles = this.ChanLeftHandIndex1.eulerAngles;
			this.KunLeftHandIndex2.eulerAngles = this.ChanLeftHandIndex2.eulerAngles;
			this.KunLeftHandIndex3.eulerAngles = this.ChanLeftHandIndex3.eulerAngles;
			this.KunLeftHandThumb1.eulerAngles = this.ChanLeftHandThumb1.eulerAngles;
			this.KunLeftHandThumb2.eulerAngles = this.ChanLeftHandThumb2.eulerAngles;
			this.KunLeftHandThumb3.eulerAngles = this.ChanLeftHandThumb3.eulerAngles;
			this.KunRightHandPinky1.eulerAngles = this.ChanRightHandPinky1.eulerAngles;
			this.KunRightHandPinky2.eulerAngles = this.ChanRightHandPinky2.eulerAngles;
			this.KunRightHandPinky3.eulerAngles = this.ChanRightHandPinky3.eulerAngles;
			this.KunRightHandRing1.eulerAngles = this.ChanRightHandRing1.eulerAngles;
			this.KunRightHandRing2.eulerAngles = this.ChanRightHandRing2.eulerAngles;
			this.KunRightHandRing3.eulerAngles = this.ChanRightHandRing3.eulerAngles;
			this.KunRightHandMiddle1.eulerAngles = this.ChanRightHandMiddle1.eulerAngles;
			this.KunRightHandMiddle2.eulerAngles = this.ChanRightHandMiddle2.eulerAngles;
			this.KunRightHandMiddle3.eulerAngles = this.ChanRightHandMiddle3.eulerAngles;
			this.KunRightHandIndex1.eulerAngles = this.ChanRightHandIndex1.eulerAngles;
			this.KunRightHandIndex2.eulerAngles = this.ChanRightHandIndex2.eulerAngles;
			this.KunRightHandIndex3.eulerAngles = this.ChanRightHandIndex3.eulerAngles;
			this.KunRightHandThumb1.eulerAngles = this.ChanRightHandThumb1.eulerAngles;
			this.KunRightHandThumb2.eulerAngles = this.ChanRightHandThumb2.eulerAngles;
			this.KunRightHandThumb3.eulerAngles = this.ChanRightHandThumb3.eulerAngles;
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (this.ID > -1)
				{
					for (int i = 0; i < 32; i++)
					{
						this.SecondRenderer.SetBlendShapeWeight(i, 0f);
					}
					if (this.ID > 32)
					{
						this.ID = 0;
					}
					this.SecondRenderer.SetBlendShapeWeight(this.ID, 100f);
				}
				this.ID++;
			}
		}
	}

	// Token: 0x04003790 RID: 14224
	public Transform ChanItemParent;

	// Token: 0x04003791 RID: 14225
	public Transform KunItemParent;

	// Token: 0x04003792 RID: 14226
	public Transform ChanHips;

	// Token: 0x04003793 RID: 14227
	public Transform ChanSpine;

	// Token: 0x04003794 RID: 14228
	public Transform ChanSpine1;

	// Token: 0x04003795 RID: 14229
	public Transform ChanSpine2;

	// Token: 0x04003796 RID: 14230
	public Transform ChanSpine3;

	// Token: 0x04003797 RID: 14231
	public Transform ChanNeck;

	// Token: 0x04003798 RID: 14232
	public Transform ChanHead;

	// Token: 0x04003799 RID: 14233
	public Transform ChanRightUpLeg;

	// Token: 0x0400379A RID: 14234
	public Transform ChanRightLeg;

	// Token: 0x0400379B RID: 14235
	public Transform ChanRightFoot;

	// Token: 0x0400379C RID: 14236
	public Transform ChanRightToes;

	// Token: 0x0400379D RID: 14237
	public Transform ChanLeftUpLeg;

	// Token: 0x0400379E RID: 14238
	public Transform ChanLeftLeg;

	// Token: 0x0400379F RID: 14239
	public Transform ChanLeftFoot;

	// Token: 0x040037A0 RID: 14240
	public Transform ChanLeftToes;

	// Token: 0x040037A1 RID: 14241
	public Transform ChanRightShoulder;

	// Token: 0x040037A2 RID: 14242
	public Transform ChanRightArm;

	// Token: 0x040037A3 RID: 14243
	public Transform ChanRightArmRoll;

	// Token: 0x040037A4 RID: 14244
	public Transform ChanRightForeArm;

	// Token: 0x040037A5 RID: 14245
	public Transform ChanRightForeArmRoll;

	// Token: 0x040037A6 RID: 14246
	public Transform ChanRightHand;

	// Token: 0x040037A7 RID: 14247
	public Transform ChanLeftShoulder;

	// Token: 0x040037A8 RID: 14248
	public Transform ChanLeftArm;

	// Token: 0x040037A9 RID: 14249
	public Transform ChanLeftArmRoll;

	// Token: 0x040037AA RID: 14250
	public Transform ChanLeftForeArm;

	// Token: 0x040037AB RID: 14251
	public Transform ChanLeftForeArmRoll;

	// Token: 0x040037AC RID: 14252
	public Transform ChanLeftHand;

	// Token: 0x040037AD RID: 14253
	public Transform ChanLeftHandPinky1;

	// Token: 0x040037AE RID: 14254
	public Transform ChanLeftHandPinky2;

	// Token: 0x040037AF RID: 14255
	public Transform ChanLeftHandPinky3;

	// Token: 0x040037B0 RID: 14256
	public Transform ChanLeftHandRing1;

	// Token: 0x040037B1 RID: 14257
	public Transform ChanLeftHandRing2;

	// Token: 0x040037B2 RID: 14258
	public Transform ChanLeftHandRing3;

	// Token: 0x040037B3 RID: 14259
	public Transform ChanLeftHandMiddle1;

	// Token: 0x040037B4 RID: 14260
	public Transform ChanLeftHandMiddle2;

	// Token: 0x040037B5 RID: 14261
	public Transform ChanLeftHandMiddle3;

	// Token: 0x040037B6 RID: 14262
	public Transform ChanLeftHandIndex1;

	// Token: 0x040037B7 RID: 14263
	public Transform ChanLeftHandIndex2;

	// Token: 0x040037B8 RID: 14264
	public Transform ChanLeftHandIndex3;

	// Token: 0x040037B9 RID: 14265
	public Transform ChanLeftHandThumb1;

	// Token: 0x040037BA RID: 14266
	public Transform ChanLeftHandThumb2;

	// Token: 0x040037BB RID: 14267
	public Transform ChanLeftHandThumb3;

	// Token: 0x040037BC RID: 14268
	public Transform ChanRightHandPinky1;

	// Token: 0x040037BD RID: 14269
	public Transform ChanRightHandPinky2;

	// Token: 0x040037BE RID: 14270
	public Transform ChanRightHandPinky3;

	// Token: 0x040037BF RID: 14271
	public Transform ChanRightHandRing1;

	// Token: 0x040037C0 RID: 14272
	public Transform ChanRightHandRing2;

	// Token: 0x040037C1 RID: 14273
	public Transform ChanRightHandRing3;

	// Token: 0x040037C2 RID: 14274
	public Transform ChanRightHandMiddle1;

	// Token: 0x040037C3 RID: 14275
	public Transform ChanRightHandMiddle2;

	// Token: 0x040037C4 RID: 14276
	public Transform ChanRightHandMiddle3;

	// Token: 0x040037C5 RID: 14277
	public Transform ChanRightHandIndex1;

	// Token: 0x040037C6 RID: 14278
	public Transform ChanRightHandIndex2;

	// Token: 0x040037C7 RID: 14279
	public Transform ChanRightHandIndex3;

	// Token: 0x040037C8 RID: 14280
	public Transform ChanRightHandThumb1;

	// Token: 0x040037C9 RID: 14281
	public Transform ChanRightHandThumb2;

	// Token: 0x040037CA RID: 14282
	public Transform ChanRightHandThumb3;

	// Token: 0x040037CB RID: 14283
	public Transform KunHips;

	// Token: 0x040037CC RID: 14284
	public Transform KunSpine;

	// Token: 0x040037CD RID: 14285
	public Transform KunSpine1;

	// Token: 0x040037CE RID: 14286
	public Transform KunSpine2;

	// Token: 0x040037CF RID: 14287
	public Transform KunSpine3;

	// Token: 0x040037D0 RID: 14288
	public Transform KunNeck;

	// Token: 0x040037D1 RID: 14289
	public Transform KunHead;

	// Token: 0x040037D2 RID: 14290
	public Transform KunRightUpLeg;

	// Token: 0x040037D3 RID: 14291
	public Transform KunRightLeg;

	// Token: 0x040037D4 RID: 14292
	public Transform KunRightFoot;

	// Token: 0x040037D5 RID: 14293
	public Transform KunRightToes;

	// Token: 0x040037D6 RID: 14294
	public Transform KunLeftUpLeg;

	// Token: 0x040037D7 RID: 14295
	public Transform KunLeftLeg;

	// Token: 0x040037D8 RID: 14296
	public Transform KunLeftFoot;

	// Token: 0x040037D9 RID: 14297
	public Transform KunLeftToes;

	// Token: 0x040037DA RID: 14298
	public Transform KunRightShoulder;

	// Token: 0x040037DB RID: 14299
	public Transform KunRightArm;

	// Token: 0x040037DC RID: 14300
	public Transform KunRightArmRoll;

	// Token: 0x040037DD RID: 14301
	public Transform KunRightForeArm;

	// Token: 0x040037DE RID: 14302
	public Transform KunRightForeArmRoll;

	// Token: 0x040037DF RID: 14303
	public Transform KunRightHand;

	// Token: 0x040037E0 RID: 14304
	public Transform KunLeftShoulder;

	// Token: 0x040037E1 RID: 14305
	public Transform KunLeftArm;

	// Token: 0x040037E2 RID: 14306
	public Transform KunLeftArmRoll;

	// Token: 0x040037E3 RID: 14307
	public Transform KunLeftForeArm;

	// Token: 0x040037E4 RID: 14308
	public Transform KunLeftForeArmRoll;

	// Token: 0x040037E5 RID: 14309
	public Transform KunLeftHand;

	// Token: 0x040037E6 RID: 14310
	public Transform KunLeftHandPinky1;

	// Token: 0x040037E7 RID: 14311
	public Transform KunLeftHandPinky2;

	// Token: 0x040037E8 RID: 14312
	public Transform KunLeftHandPinky3;

	// Token: 0x040037E9 RID: 14313
	public Transform KunLeftHandRing1;

	// Token: 0x040037EA RID: 14314
	public Transform KunLeftHandRing2;

	// Token: 0x040037EB RID: 14315
	public Transform KunLeftHandRing3;

	// Token: 0x040037EC RID: 14316
	public Transform KunLeftHandMiddle1;

	// Token: 0x040037ED RID: 14317
	public Transform KunLeftHandMiddle2;

	// Token: 0x040037EE RID: 14318
	public Transform KunLeftHandMiddle3;

	// Token: 0x040037EF RID: 14319
	public Transform KunLeftHandIndex1;

	// Token: 0x040037F0 RID: 14320
	public Transform KunLeftHandIndex2;

	// Token: 0x040037F1 RID: 14321
	public Transform KunLeftHandIndex3;

	// Token: 0x040037F2 RID: 14322
	public Transform KunLeftHandThumb1;

	// Token: 0x040037F3 RID: 14323
	public Transform KunLeftHandThumb2;

	// Token: 0x040037F4 RID: 14324
	public Transform KunLeftHandThumb3;

	// Token: 0x040037F5 RID: 14325
	public Transform KunRightHandPinky1;

	// Token: 0x040037F6 RID: 14326
	public Transform KunRightHandPinky2;

	// Token: 0x040037F7 RID: 14327
	public Transform KunRightHandPinky3;

	// Token: 0x040037F8 RID: 14328
	public Transform KunRightHandRing1;

	// Token: 0x040037F9 RID: 14329
	public Transform KunRightHandRing2;

	// Token: 0x040037FA RID: 14330
	public Transform KunRightHandRing3;

	// Token: 0x040037FB RID: 14331
	public Transform KunRightHandMiddle1;

	// Token: 0x040037FC RID: 14332
	public Transform KunRightHandMiddle2;

	// Token: 0x040037FD RID: 14333
	public Transform KunRightHandMiddle3;

	// Token: 0x040037FE RID: 14334
	public Transform KunRightHandIndex1;

	// Token: 0x040037FF RID: 14335
	public Transform KunRightHandIndex2;

	// Token: 0x04003800 RID: 14336
	public Transform KunRightHandIndex3;

	// Token: 0x04003801 RID: 14337
	public Transform KunRightHandThumb1;

	// Token: 0x04003802 RID: 14338
	public Transform KunRightHandThumb2;

	// Token: 0x04003803 RID: 14339
	public Transform KunRightHandThumb3;

	// Token: 0x04003804 RID: 14340
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04003805 RID: 14341
	public SkinnedMeshRenderer SecondRenderer;

	// Token: 0x04003806 RID: 14342
	public SkinnedMeshRenderer ThirdRenderer;

	// Token: 0x04003807 RID: 14343
	public bool Kizuna;

	// Token: 0x04003808 RID: 14344
	public bool Man;

	// Token: 0x04003809 RID: 14345
	public int ID;

	// Token: 0x0400380A RID: 14346
	private bool Adjusted;
}

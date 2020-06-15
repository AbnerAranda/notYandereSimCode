using System;
using System.Collections.Generic;

// Token: 0x020003C4 RID: 964
[Serializable]
public class TaskSaveData
{
	// Token: 0x06001A3D RID: 6717 RVA: 0x00101894 File Offset: 0x000FFA94
	public static TaskSaveData ReadFromGlobals()
	{
		TaskSaveData taskSaveData = new TaskSaveData();
		foreach (int num in TaskGlobals.KeysOfGuitarPhoto())
		{
			if (TaskGlobals.GetGuitarPhoto(num))
			{
				taskSaveData.guitarPhoto.Add(num);
			}
		}
		foreach (int num2 in TaskGlobals.KeysOfKittenPhoto())
		{
			if (TaskGlobals.GetKittenPhoto(num2))
			{
				taskSaveData.kittenPhoto.Add(num2);
			}
		}
		foreach (int num3 in TaskGlobals.KeysOfHorudaPhoto())
		{
			if (TaskGlobals.GetHorudaPhoto(num3))
			{
				taskSaveData.horudaPhoto.Add(num3);
			}
		}
		foreach (int num4 in TaskGlobals.KeysOfTaskStatus())
		{
			taskSaveData.taskStatus.Add(num4, TaskGlobals.GetTaskStatus(num4));
		}
		return taskSaveData;
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x00101964 File Offset: 0x000FFB64
	public static void WriteToGlobals(TaskSaveData data)
	{
		foreach (int photoID in data.kittenPhoto)
		{
			TaskGlobals.SetKittenPhoto(photoID, true);
		}
		foreach (int photoID2 in data.guitarPhoto)
		{
			TaskGlobals.SetGuitarPhoto(photoID2, true);
		}
		foreach (KeyValuePair<int, int> keyValuePair in data.taskStatus)
		{
			TaskGlobals.SetTaskStatus(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x04002970 RID: 10608
	public IntHashSet guitarPhoto = new IntHashSet();

	// Token: 0x04002971 RID: 10609
	public IntHashSet kittenPhoto = new IntHashSet();

	// Token: 0x04002972 RID: 10610
	public IntHashSet horudaPhoto = new IntHashSet();

	// Token: 0x04002973 RID: 10611
	public IntAndIntDictionary taskStatus = new IntAndIntDictionary();
}

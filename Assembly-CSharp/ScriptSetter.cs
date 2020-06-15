using System;
using System.Reflection;
using UnityEngine;

// Token: 0x0200049F RID: 1183
public class ScriptSetter : MonoBehaviour
{
	// Token: 0x06001E44 RID: 7748 RVA: 0x0017C558 File Offset: 0x0017A758
	private void Start()
	{
		foreach (Component component in base.GetComponents(typeof(Component)))
		{
			Debug.Log(string.Concat(new object[]
			{
				"name ",
				component.name,
				" type ",
				component.GetType(),
				" basetype ",
				component.GetType().BaseType
			}));
			foreach (FieldInfo fieldInfo in component.GetType().GetFields())
			{
				object obj = component;
				Debug.Log(fieldInfo.Name + " value is: " + fieldInfo.GetValue(obj));
			}
		}
	}

	// Token: 0x04003C76 RID: 15478
	public StudentScript OldStudent;

	// Token: 0x04003C77 RID: 15479
	public StudentScript NewStudent;
}

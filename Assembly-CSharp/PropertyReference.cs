using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

// Token: 0x0200007B RID: 123
[Serializable]
public class PropertyReference
{
	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600049E RID: 1182 RVA: 0x0002C68C File Offset: 0x0002A88C
	// (set) Token: 0x0600049F RID: 1183 RVA: 0x0002C694 File Offset: 0x0002A894
	public Component target
	{
		get
		{
			return this.mTarget;
		}
		set
		{
			this.mTarget = value;
			this.mProperty = null;
			this.mField = null;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0002C6AB File Offset: 0x0002A8AB
	// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0002C6B3 File Offset: 0x0002A8B3
	public string name
	{
		get
		{
			return this.mName;
		}
		set
		{
			this.mName = value;
			this.mProperty = null;
			this.mField = null;
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0002C6CA File Offset: 0x0002A8CA
	public bool isValid
	{
		get
		{
			return this.mTarget != null && !string.IsNullOrEmpty(this.mName);
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0002C6EC File Offset: 0x0002A8EC
	public bool isEnabled
	{
		get
		{
			if (this.mTarget == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = this.mTarget as MonoBehaviour;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x000045DB File Offset: 0x000027DB
	public PropertyReference()
	{
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0002C726 File Offset: 0x0002A926
	public PropertyReference(Component target, string fieldName)
	{
		this.mTarget = target;
		this.mName = fieldName;
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0002C73C File Offset: 0x0002A93C
	public Type GetPropertyType()
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty != null)
		{
			return this.mProperty.PropertyType;
		}
		if (this.mField != null)
		{
			return this.mField.FieldType;
		}
		return typeof(void);
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0002C7B4 File Offset: 0x0002A9B4
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !this.isValid;
		}
		if (obj is PropertyReference)
		{
			PropertyReference propertyReference = obj as PropertyReference;
			return this.mTarget == propertyReference.mTarget && string.Equals(this.mName, propertyReference.mName);
		}
		return false;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0002C805 File Offset: 0x0002AA05
	public override int GetHashCode()
	{
		return PropertyReference.s_Hash;
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0002C80C File Offset: 0x0002AA0C
	public void Set(Component target, string methodName)
	{
		this.mTarget = target;
		this.mName = methodName;
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0002C81C File Offset: 0x0002AA1C
	public void Clear()
	{
		this.mTarget = null;
		this.mName = null;
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0002C82C File Offset: 0x0002AA2C
	public void Reset()
	{
		this.mField = null;
		this.mProperty = null;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0002C83C File Offset: 0x0002AA3C
	public override string ToString()
	{
		return PropertyReference.ToString(this.mTarget, this.name);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0002C850 File Offset: 0x0002AA50
	public static string ToString(Component comp, string property)
	{
		if (!(comp != null))
		{
			return null;
		}
		string text = comp.GetType().ToString();
		int num = text.LastIndexOf('.');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		if (!string.IsNullOrEmpty(property))
		{
			return text + "." + property;
		}
		return text + ".[property]";
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0002C8AC File Offset: 0x0002AAAC
	[DebuggerHidden]
	[DebuggerStepThrough]
	public object Get()
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty != null)
		{
			if (this.mProperty.CanRead)
			{
				return this.mProperty.GetValue(this.mTarget, null);
			}
		}
		else if (this.mField != null)
		{
			return this.mField.GetValue(this.mTarget);
		}
		return null;
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0002C934 File Offset: 0x0002AB34
	[DebuggerHidden]
	[DebuggerStepThrough]
	public bool Set(object value)
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty == null && this.mField == null)
		{
			return false;
		}
		if (value == null)
		{
			try
			{
				if (!(this.mProperty != null))
				{
					this.mField.SetValue(this.mTarget, null);
					return true;
				}
				if (this.mProperty.CanWrite)
				{
					this.mProperty.SetValue(this.mTarget, null, null);
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
		if (!this.Convert(ref value))
		{
			if (Application.isPlaying)
			{
				UnityEngine.Debug.LogError(string.Concat(new object[]
				{
					"Unable to convert ",
					value.GetType(),
					" to ",
					this.GetPropertyType()
				}));
			}
		}
		else
		{
			if (this.mField != null)
			{
				this.mField.SetValue(this.mTarget, value);
				return true;
			}
			if (this.mProperty.CanWrite)
			{
				this.mProperty.SetValue(this.mTarget, value, null);
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0002CA84 File Offset: 0x0002AC84
	[DebuggerHidden]
	[DebuggerStepThrough]
	private bool Cache()
	{
		if (this.mTarget != null && !string.IsNullOrEmpty(this.mName))
		{
			Type type = this.mTarget.GetType();
			this.mField = type.GetField(this.mName);
			this.mProperty = type.GetProperty(this.mName);
		}
		else
		{
			this.mField = null;
			this.mProperty = null;
		}
		return this.mField != null || this.mProperty != null;
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0002CB08 File Offset: 0x0002AD08
	private bool Convert(ref object value)
	{
		if (this.mTarget == null)
		{
			return false;
		}
		Type propertyType = this.GetPropertyType();
		Type from;
		if (value == null)
		{
			if (!propertyType.IsClass)
			{
				return false;
			}
			from = propertyType;
		}
		else
		{
			from = value.GetType();
		}
		return PropertyReference.Convert(ref value, from, propertyType);
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0002CB50 File Offset: 0x0002AD50
	public static bool Convert(Type from, Type to)
	{
		object obj = null;
		return PropertyReference.Convert(ref obj, from, to);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0002CB68 File Offset: 0x0002AD68
	public static bool Convert(object value, Type to)
	{
		if (value == null)
		{
			value = null;
			return PropertyReference.Convert(ref value, to, to);
		}
		return PropertyReference.Convert(ref value, value.GetType(), to);
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x0002CB88 File Offset: 0x0002AD88
	public static bool Convert(ref object value, Type from, Type to)
	{
		if (to.IsAssignableFrom(from))
		{
			return true;
		}
		if (to == typeof(string))
		{
			value = ((value != null) ? value.ToString() : "null");
			return true;
		}
		if (value == null)
		{
			return false;
		}
		if (to == typeof(int))
		{
			if (from == typeof(string))
			{
				int num;
				if (int.TryParse((string)value, out num))
				{
					value = num;
					return true;
				}
			}
			else
			{
				if (from == typeof(float))
				{
					value = Mathf.RoundToInt((float)value);
					return true;
				}
				if (from == typeof(double))
				{
					value = (int)Math.Round((double)value);
				}
			}
		}
		else if (to == typeof(float))
		{
			if (from == typeof(string))
			{
				float num2;
				if (float.TryParse((string)value, out num2))
				{
					value = num2;
					return true;
				}
			}
			else if (from == typeof(double))
			{
				value = (float)((double)value);
			}
			else if (from == typeof(int))
			{
				value = (float)((int)value);
			}
		}
		else if (to == typeof(double))
		{
			if (from == typeof(string))
			{
				double num3;
				if (double.TryParse((string)value, out num3))
				{
					value = num3;
					return true;
				}
			}
			else if (from == typeof(float))
			{
				value = (double)((float)value);
			}
			else if (from == typeof(int))
			{
				value = (double)((int)value);
			}
		}
		return false;
	}

	// Token: 0x0400050C RID: 1292
	[SerializeField]
	private Component mTarget;

	// Token: 0x0400050D RID: 1293
	[SerializeField]
	private string mName;

	// Token: 0x0400050E RID: 1294
	private FieldInfo mField;

	// Token: 0x0400050F RID: 1295
	private PropertyInfo mProperty;

	// Token: 0x04000510 RID: 1296
	private static int s_Hash = "PropertyBinding".GetHashCode();
}

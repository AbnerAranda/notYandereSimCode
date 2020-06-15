using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004A1 RID: 1185
public static class YanSave
{
	// Token: 0x17000494 RID: 1172
	// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0017CD17 File Offset: 0x0017AF17
	public static string SaveDataPath
	{
		get
		{
			return Path.Combine(Application.streamingAssetsPath, "SaveFiles");
		}
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x0017CD28 File Offset: 0x0017AF28
	public static void SaveData(string targetSave)
	{
		YanSaveIdentifier[] array = Resources.FindObjectsOfTypeAll<YanSaveIdentifier>();
		List<SerializedGameObject> list = new List<SerializedGameObject>();
		foreach (YanSaveIdentifier yanSaveIdentifier in array)
		{
			List<SerializedComponent> list2 = new List<SerializedComponent>();
			foreach (Component component in yanSaveIdentifier.gameObject.GetComponents(typeof(Component)))
			{
				if (yanSaveIdentifier.EnabledComponents.Contains(component))
				{
					SerializedComponent serializedComponent = default(SerializedComponent);
					serializedComponent.TypePath = component.GetType().AssemblyQualifiedName;
					serializedComponent.PropertyReferences = new ReferenceDict();
					serializedComponent.PropertyValues = new ValueDict();
					serializedComponent.FieldReferences = new ReferenceDict();
					serializedComponent.FieldValues = new ValueDict();
					serializedComponent.FieldReferenceArrays = new ReferenceArrayDict();
					serializedComponent.PropertyReferenceArrays = new ReferenceArrayDict();
					if (typeof(MonoBehaviour).IsAssignableFrom(component.GetType()))
					{
						serializedComponent.IsMonoBehaviour = true;
						serializedComponent.IsEnabled = ((MonoBehaviour)component).enabled;
					}
					Type type = component.GetType();
					foreach (PropertyInfo propertyInfo in YanSave.GetCachedProperties(type))
					{
						if (propertyInfo.CanWrite && !propertyInfo.IsDefined(typeof(ObsoleteAttribute), true))
						{
							bool flag = false;
							foreach (DisabledYanSaveMember disabledYanSaveMember in yanSaveIdentifier.DisabledProperties)
							{
								if (disabledYanSaveMember.Component == component && disabledYanSaveMember.Name == propertyInfo.Name)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								object value = propertyInfo.GetValue(component);
								bool flag2 = typeof(Component).IsAssignableFrom(propertyInfo.PropertyType);
								bool flag3 = propertyInfo.PropertyType == typeof(GameObject);
								bool isArray = propertyInfo.PropertyType.IsArray;
								bool flag4 = typeof(Component[]).IsAssignableFrom(propertyInfo.PropertyType);
								bool flag5 = typeof(GameObject[]).IsAssignableFrom(propertyInfo.PropertyType);
								if (value != null)
								{
									try
									{
										if (!flag2 && !flag3)
										{
											serializedComponent.PropertyValues.Add(propertyInfo.Name, value);
										}
										else if (isArray)
										{
											List<string> list3 = new List<string>();
											if (flag4)
											{
												list3.AddRange(((Component[])value).Select(delegate(Component x)
												{
													if (!(x.GetComponent<YanSaveIdentifier>() != null))
													{
														return string.Empty;
													}
													return x.GetComponent<YanSaveIdentifier>().ObjectID;
												}));
											}
											else if (flag5)
											{
												list3.AddRange(((GameObject[])value).Select(delegate(GameObject x)
												{
													if (!(x.GetComponent<YanSaveIdentifier>() != null))
													{
														return string.Empty;
													}
													return x.GetComponent<YanSaveIdentifier>().ObjectID;
												}));
											}
											serializedComponent.PropertyReferenceArrays.Add(propertyInfo.Name, list3);
										}
										else
										{
											YanSaveIdentifier yanSaveIdentifier2 = flag2 ? ((Component)value).gameObject.GetComponent<YanSaveIdentifier>() : (flag3 ? ((GameObject)value).GetComponent<YanSaveIdentifier>() : null);
											if (yanSaveIdentifier2 != null)
											{
												serializedComponent.PropertyReferences.Add(propertyInfo.Name, yanSaveIdentifier2.ObjectID);
											}
											else
											{
												serializedComponent.PropertyReferences.Add(propertyInfo.Name, null);
											}
										}
									}
									catch
									{
									}
								}
							}
						}
					}
					foreach (FieldInfo fieldInfo in YanSave.GetCachedFields(type))
					{
						if (!fieldInfo.IsLiteral && !fieldInfo.IsDefined(typeof(ObsoleteAttribute), true))
						{
							bool flag6 = false;
							foreach (DisabledYanSaveMember disabledYanSaveMember2 in yanSaveIdentifier.DisabledFields)
							{
								if (disabledYanSaveMember2.Component == component && disabledYanSaveMember2.Name == fieldInfo.Name)
								{
									flag6 = true;
									break;
								}
							}
							if (!flag6)
							{
								object value2 = fieldInfo.GetValue(component);
								bool flag7 = typeof(Component).IsAssignableFrom(fieldInfo.FieldType);
								bool flag8 = fieldInfo.FieldType == typeof(GameObject);
								bool isArray2 = fieldInfo.FieldType.IsArray;
								bool flag9 = typeof(Component[]).IsAssignableFrom(fieldInfo.FieldType);
								bool flag10 = typeof(GameObject[]).IsAssignableFrom(fieldInfo.FieldType);
								try
								{
									if (!flag7 && !flag8 && !flag9 && !flag10)
									{
										serializedComponent.FieldValues.Add(fieldInfo.Name, value2);
									}
									else if (isArray2)
									{
										List<string> list4 = new List<string>();
										if (flag9)
										{
											list4.AddRange(((Component[])value2).Select(delegate(Component x)
											{
												if (!(x.GetComponent<YanSaveIdentifier>() != null))
												{
													return string.Empty;
												}
												return x.GetComponent<YanSaveIdentifier>().ObjectID;
											}));
										}
										else if (flag10)
										{
											list4.AddRange(((GameObject[])value2).Select(delegate(GameObject x)
											{
												if (!(x.GetComponent<YanSaveIdentifier>() != null))
												{
													return string.Empty;
												}
												return x.GetComponent<YanSaveIdentifier>().ObjectID;
											}));
										}
										serializedComponent.FieldReferenceArrays.Add(fieldInfo.Name, list4);
									}
									else
									{
										YanSaveIdentifier yanSaveIdentifier3 = flag7 ? ((Component)value2).gameObject.GetComponent<YanSaveIdentifier>() : (flag8 ? ((GameObject)value2).GetComponent<YanSaveIdentifier>() : null);
										if (yanSaveIdentifier3 != null)
										{
											serializedComponent.FieldReferences.Add(fieldInfo.Name, yanSaveIdentifier3.ObjectID);
										}
										else
										{
											serializedComponent.FieldReferences.Add(fieldInfo.Name, null);
										}
									}
								}
								catch
								{
								}
							}
						}
					}
					serializedComponent.OwnerID = yanSaveIdentifier.ObjectID;
					list2.Add(serializedComponent);
				}
			}
			SerializedGameObject item = new SerializedGameObject
			{
				ActiveInHierarchy = yanSaveIdentifier.gameObject.activeInHierarchy,
				ActiveSelf = yanSaveIdentifier.gameObject.activeSelf,
				IsStatic = yanSaveIdentifier.gameObject.isStatic,
				Layer = yanSaveIdentifier.gameObject.layer,
				Tag = yanSaveIdentifier.gameObject.tag,
				Name = yanSaveIdentifier.gameObject.name,
				SerializedComponents = list2.ToArray(),
				ObjectID = yanSaveIdentifier.ObjectID
			};
			list.Add(item);
		}
		object value3 = new YanSaveData
		{
			LoadedLevelName = SceneManager.GetActiveScene().name,
			SerializedGameObjects = list.ToArray()
		};
		JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
		jsonSerializerSettings.ContractResolver = new YanSaveResolver();
		jsonSerializerSettings.Error = delegate(object s, Newtonsoft.Json.Serialization.ErrorEventArgs e)
		{
			e.ErrorContext.Handled = true;
		};
		string contents = JsonConvert.SerializeObject(value3, jsonSerializerSettings);
		if (!Directory.Exists(YanSave.SaveDataPath))
		{
			Directory.CreateDirectory(YanSave.SaveDataPath);
		}
		File.WriteAllText(Path.Combine(YanSave.SaveDataPath, targetSave + ".yansave"), contents);
		Action onSave = YanSave.OnSave;
		if (onSave == null)
		{
			return;
		}
		onSave();
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x0017D4BC File Offset: 0x0017B6BC
	public static void LoadData(string targetSave, bool recreateMissing = false)
	{
		if (!File.Exists(Path.Combine(YanSave.SaveDataPath, targetSave + ".yansave")))
		{
			return;
		}
		YanSaveData yanSaveData = JsonConvert.DeserializeObject<YanSaveData>(File.ReadAllText(Path.Combine(YanSave.SaveDataPath, targetSave + ".yansave")));
		if (SceneManager.GetActiveScene().name != yanSaveData.LoadedLevelName)
		{
			SceneManager.LoadScene(yanSaveData.LoadedLevelName);
		}
		SerializedGameObject[] serializedGameObjects = yanSaveData.SerializedGameObjects;
		int i = 0;
		while (i < serializedGameObjects.Length)
		{
			SerializedGameObject serializedGameObject = serializedGameObjects[i];
			GameObject gameObject = YanSaveIdentifier.GetObject(serializedGameObject);
			if (!(gameObject == null))
			{
				goto IL_BA;
			}
			if (recreateMissing)
			{
				gameObject = new GameObject();
				gameObject.AddComponent<YanSaveIdentifier>().ObjectID = serializedGameObject.ObjectID;
				gameObject.SetActive(serializedGameObject.ActiveSelf);
				goto IL_BA;
			}
			IL_15D:
			i++;
			continue;
			IL_BA:
			gameObject.isStatic = serializedGameObject.IsStatic;
			gameObject.layer = serializedGameObject.Layer;
			gameObject.tag = serializedGameObject.Tag;
			gameObject.name = serializedGameObject.Name;
			gameObject.SetActive(serializedGameObject.ActiveSelf);
			foreach (SerializedComponent serializedComponent in serializedGameObject.SerializedComponents)
			{
				if (gameObject != null)
				{
					Type type = YanSave.GetType(serializedComponent.TypePath);
					if (recreateMissing && gameObject.GetComponent(type) == null)
					{
						gameObject.AddComponent(type);
					}
				}
			}
			goto IL_15D;
		}
		foreach (SerializedGameObject serializedGameObject2 in yanSaveData.SerializedGameObjects)
		{
			GameObject @object = YanSaveIdentifier.GetObject(serializedGameObject2);
			if (!(@object == null))
			{
				foreach (SerializedComponent serializedComponent2 in serializedGameObject2.SerializedComponents)
				{
					Type type2 = YanSave.GetType(serializedComponent2.TypePath);
					Component component = @object.GetComponent(type2);
					@object.GetComponent<YanSaveIdentifier>();
					if (!(component == null))
					{
						if (serializedComponent2.IsMonoBehaviour)
						{
							((MonoBehaviour)component).enabled = serializedComponent2.IsEnabled;
						}
						foreach (PropertyInfo propertyInfo in YanSave.GetCachedProperties(type2))
						{
							if (propertyInfo.CanWrite)
							{
								bool flag = typeof(Component).IsAssignableFrom(propertyInfo.PropertyType);
								if (!flag && propertyInfo.PropertyType != typeof(GameObject))
								{
									if (serializedComponent2.PropertyValues.ContainsKey(propertyInfo.Name))
									{
										object obj = serializedComponent2.PropertyValues[propertyInfo.Name];
										if (obj == null)
										{
											propertyInfo.SetValue(component, null);
										}
										else
										{
											if (obj.GetType() == typeof(JObject))
											{
												try
												{
													propertyInfo.SetValue(component, ((JObject)obj).ToObject(propertyInfo.PropertyType));
													goto IL_522;
												}
												catch
												{
													goto IL_522;
												}
											}
											if (obj.GetType() == typeof(JArray))
											{
												try
												{
													propertyInfo.SetValue(component, ((JArray)obj).ToObject(propertyInfo.PropertyType));
													goto IL_522;
												}
												catch
												{
													goto IL_522;
												}
											}
											bool isEnum = propertyInfo.PropertyType.IsEnum;
											bool flag2 = typeof(IConvertible).IsAssignableFrom(obj.GetType());
											propertyInfo.SetValue(component, isEnum ? Enum.ToObject(propertyInfo.PropertyType, obj) : (flag2 ? Convert.ChangeType(obj, propertyInfo.PropertyType) : obj));
										}
									}
								}
								else if (serializedComponent2.PropertyReferences.ContainsKey(propertyInfo.Name))
								{
									bool flag3 = propertyInfo.PropertyType == typeof(GameObject);
									GameObject object2 = YanSaveIdentifier.GetObject(serializedComponent2.FieldReferences[propertyInfo.Name]);
									if (!(object2 == null))
									{
										if (flag)
										{
											propertyInfo.SetValue(component, object2.GetComponent(propertyInfo.PropertyType));
										}
										else if (flag3)
										{
											propertyInfo.SetValue(component, object2);
										}
									}
								}
								else if (serializedComponent2.PropertyReferenceArrays.ContainsKey(propertyInfo.Name))
								{
									bool flag4 = typeof(Component[]).IsAssignableFrom(propertyInfo.PropertyType);
									bool flag5 = typeof(GameObject[]).IsAssignableFrom(propertyInfo.PropertyType);
									List<string> list = serializedComponent2.PropertyReferenceArrays[propertyInfo.Name];
									Type elementType = propertyInfo.PropertyType.GetElementType();
									if (flag4)
									{
										IList list2 = Array.CreateInstance(elementType, list.Count);
										for (int l = 0; l < list.Count; l++)
										{
											YanSaveIdentifier.GetObject(list[l]);
											Component value = (@object != null) ? @object.GetComponent(elementType) : null;
											list2[l] = value;
										}
										propertyInfo.SetValue(component, list2);
									}
									else if (flag5)
									{
										IList list3 = Array.CreateInstance(elementType, list.Count);
										for (int m = 0; m < list.Count; m++)
										{
											GameObject object3 = YanSaveIdentifier.GetObject(list[m]);
											list3[m] = object3;
										}
										propertyInfo.SetValue(component, list3);
									}
								}
							}
							IL_522:;
						}
						foreach (FieldInfo fieldInfo in YanSave.GetCachedFields(type2))
						{
							bool flag6 = typeof(Component).IsAssignableFrom(fieldInfo.FieldType);
							bool flag7 = typeof(Component[]).IsAssignableFrom(fieldInfo.FieldType);
							bool flag8 = typeof(GameObject[]).IsAssignableFrom(fieldInfo.FieldType);
							if (!flag7 && !flag8 && !flag6 && fieldInfo.FieldType != typeof(GameObject))
							{
								if (serializedComponent2.FieldValues.ContainsKey(fieldInfo.Name))
								{
									object obj2 = serializedComponent2.FieldValues[fieldInfo.Name];
									if (obj2 == null)
									{
										fieldInfo.SetValue(component, null);
									}
									else
									{
										if (obj2.GetType() == typeof(JObject))
										{
											try
											{
												fieldInfo.SetValue(component, ((JObject)obj2).ToObject(fieldInfo.FieldType));
												goto IL_85E;
											}
											catch
											{
												goto IL_85E;
											}
										}
										if (obj2.GetType() == typeof(JArray))
										{
											try
											{
												fieldInfo.SetValue(component, ((JArray)obj2).ToObject(fieldInfo.FieldType));
												goto IL_85E;
											}
											catch
											{
												goto IL_85E;
											}
										}
										bool isEnum2 = fieldInfo.FieldType.IsEnum;
										bool flag9 = typeof(IConvertible).IsAssignableFrom(obj2.GetType());
										fieldInfo.SetValue(component, isEnum2 ? Enum.ToObject(fieldInfo.FieldType, obj2) : (flag9 ? Convert.ChangeType(obj2, fieldInfo.FieldType) : obj2));
									}
								}
							}
							else if (serializedComponent2.FieldReferences.ContainsKey(fieldInfo.Name))
							{
								bool flag10 = fieldInfo.FieldType == typeof(GameObject);
								GameObject object4 = YanSaveIdentifier.GetObject(serializedComponent2.FieldReferences[fieldInfo.Name]);
								if (!(object4 == null))
								{
									if (flag6)
									{
										fieldInfo.SetValue(component, object4.GetComponent(fieldInfo.FieldType));
									}
									else if (flag10)
									{
										fieldInfo.SetValue(component, object4);
									}
								}
							}
							else if (serializedComponent2.FieldReferenceArrays.ContainsKey(fieldInfo.Name))
							{
								List<string> list4 = serializedComponent2.FieldReferenceArrays[fieldInfo.Name];
								Type elementType2 = fieldInfo.FieldType.GetElementType();
								if (flag7)
								{
									IList list5 = Array.CreateInstance(elementType2, list4.Count);
									for (int n = 0; n < list4.Count; n++)
									{
										YanSaveIdentifier.GetObject(list4[n]);
										Component value2 = (@object != null) ? @object.GetComponent(elementType2) : null;
										list5[n] = value2;
									}
									fieldInfo.SetValue(component, list5);
								}
								else if (flag8)
								{
									IList list6 = Array.CreateInstance(elementType2, list4.Count);
									for (int num = 0; num < list4.Count; num++)
									{
										GameObject object5 = YanSaveIdentifier.GetObject(list4[num]);
										list6[num] = object5;
									}
									fieldInfo.SetValue(component, list6);
								}
							}
							IL_85E:;
						}
					}
				}
			}
		}
		Action onLoad = YanSave.OnLoad;
		if (onLoad == null)
		{
			return;
		}
		onLoad();
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x0017DD9C File Offset: 0x0017BF9C
	public static void RemoveData(string targetSave)
	{
		string path = Path.Combine(YanSave.SaveDataPath, targetSave + ".yansave");
		try
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
		catch
		{
		}
	}

	// Token: 0x06001E4E RID: 7758 RVA: 0x0017DDE4 File Offset: 0x0017BFE4
	private static PropertyInfo[] GetCachedProperties(Type type)
	{
		if (YanSave.PropertyCache.ContainsKey(type))
		{
			return YanSave.PropertyCache[type];
		}
		YanSave.PropertyCache.Add(type, type.GetProperties());
		return YanSave.PropertyCache[type];
	}

	// Token: 0x06001E4F RID: 7759 RVA: 0x0017DE1C File Offset: 0x0017C01C
	private static FieldInfo[] GetCachedFields(Type type)
	{
		if (YanSave.FieldCache.ContainsKey(type))
		{
			return YanSave.FieldCache[type];
		}
		FieldInfo[] fields = type.GetFields();
		YanSave.FieldCache.Add(type, fields);
		return fields;
	}

	// Token: 0x06001E50 RID: 7760 RVA: 0x0017DE58 File Offset: 0x0017C058
	private static Type GetType(string typeName)
	{
		Type type = Type.GetType(typeName);
		if (type != null)
		{
			return type;
		}
		Assembly assembly = Assembly.Load(typeName.Substring(0, typeName.IndexOf('.')));
		if (assembly == null)
		{
			return null;
		}
		return assembly.GetType(typeName);
	}

	// Token: 0x04003C7E RID: 15486
	public const string SAVE_EXTENSION = "yansave";

	// Token: 0x04003C7F RID: 15487
	public static Action OnLoad;

	// Token: 0x04003C80 RID: 15488
	public static Action OnSave;

	// Token: 0x04003C81 RID: 15489
	private static Dictionary<Type, PropertyInfo[]> PropertyCache = new Dictionary<Type, PropertyInfo[]>();

	// Token: 0x04003C82 RID: 15490
	private static Dictionary<Type, FieldInfo[]> FieldCache = new Dictionary<Type, FieldInfo[]>();
}

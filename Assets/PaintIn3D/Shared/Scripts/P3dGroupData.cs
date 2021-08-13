using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PaintIn3D
{
	/// <summary>This object allows you to define information about a paint group like its name, which can then be selected using the <b>P3dGroup</b> setting on components like <b>P3dPaintableTexture</b> and <b>P3dPaintDecal</b>.</summary>
	public class P3dGroupData : ScriptableObject
	{
		[System.Serializable]
		public class TextureData
		{
			public string Name;

			public P3dBlendMode BlendMode = P3dBlendMode.AlphaBlend(Vector4.one);
		}

		class Entry
		{
			public string Path;

			public string Property;
		}

		/// <summary>This allows you to set the ID of this group (e.g. 100).
		/// NOTE: This number should be unique, and not shared by any other <b>P3dGroupData</b>.</summary>
		public int Index { set { index = value; } get { return index; } } [SerializeField] private int index;

		/// <summary>This allows you to specify the way each channel of this group's pixels are mapped to textures. This is mainly used by the in-editor painting mateiral builder tool.</summary>
		public List<TextureData> TextureDatas { get { if (textureDatas == null) textureDatas = new List<TextureData>(); return textureDatas; } } [SerializeField] private List<TextureData> textureDatas;

		/// <summary>This allows you to specify which shaders and their proprties are associated with this group.</summary>
		public string ShaderData { set { shaderData = value; } get { return shaderData; } } [SerializeField] [Multiline(10)] private string shaderData;

		private static List<P3dGroupData> cachedInstances = new List<P3dGroupData>();

		private static bool cachedInstancesSet;

		private List<Entry> entries = new List<Entry>();

		public void TryGetShaderSlotName(string shaderPath, ref string propertyName)
		{
			if (entries.Count == 0 && shaderData != null)
			{
				var lines = shaderData.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

				foreach (var line in lines)
				{
					var divider = line.IndexOf("@");

					if (divider > 0)
					{
						var entry = new Entry();

						entry.Property = line.Substring(0, divider);
						entry.Path     = line.Substring(divider + 1);

						entries.Add(entry);
					}
				}
			}

			foreach (var entry in entries)
			{
				if (entry.Path == shaderPath)
				{
					propertyName = entry.Property;

					return;
				}
			}
		}

		public bool Supports(Shader shader)
		{
			return shaderData != null && shaderData.Contains("@" + shader.name) == true;
		}

		/// <summary>This method allows you to get the <b>name</b> of the current group, with an optional prefix of the <b>Index</b> (e.g. "100: Albedo").</summary>
		public string GetName(bool prefixNumber)
		{
			if (prefixNumber == true)
			{
				return index + ": " + name;
			}

			return name;
		}

		/// <summary>This static method forces the cached instance list to update.
		/// NOTE: This does nothing in-game.</summary>
		public static void UpdateCachedInstances()
		{
			cachedInstancesSet = true;
#if UNITY_EDITOR
			cachedInstances.Clear();

			foreach (var guid in AssetDatabase.FindAssets("t:P3dGroupData"))
			{
				var groupName = AssetDatabase.LoadAssetAtPath<P3dGroupData>(AssetDatabase.GUIDToAssetPath(guid));

				cachedInstances.Add(groupName);
			}
#endif
		}

		/// <summary>This static property returns a list of all cached <b>P3dGroupData</b> instances.
		/// NOTE: This will be empty in-game.</summary>
		public static List<P3dGroupData> CachedInstances
		{
			get
			{
				if (cachedInstancesSet == false)
				{
					UpdateCachedInstances();
				}

				return cachedInstances;
			}
		}

		/// <summary>This static method calls <b>GetAlias</b> on the <b>P3dGroupData</b> with the specified <b>Index</b> setting, or null.</summary>
		public static string GetGroupName(int index, bool prefixNumber)
		{
			var groupData = GetGroupData(index);

			return groupData != null ? groupData.GetName(prefixNumber) : null;
		}

		/// <summary>This static method returns the <b>P3dGroupData</b> with the specified <b>Index</b> setting, or null.</summary>
		public static P3dGroupData GetGroupData(int index)
		{
			foreach (var cachedGroupName in CachedInstances)
			{
				if (cachedGroupName != null && cachedGroupName.index == index)
				{
					return cachedGroupName;
				}
			}

			return null;
		}

#if UNITY_EDITOR
		[MenuItem("Assets/Create/Paint in 3D/Group Data")]
		private static void CreateAsset()
		{
			var asset = CreateInstance<P3dGroupData>();
			var guids = Selection.assetGUIDs;
			var path  = guids.Length > 0 ? AssetDatabase.GUIDToAssetPath(guids[0]) : null;

			if (string.IsNullOrEmpty(path) == true)
			{
				path = "Assets";
			}
			else if (AssetDatabase.IsValidFolder(path) == false)
			{
				path = System.IO.Path.GetDirectoryName(path);
			}

			var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + typeof(P3dGroupData).ToString() + ".asset");

			AssetDatabase.CreateAsset(asset, assetPathAndName);

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			EditorUtility.FocusProjectWindow();

			Selection.activeObject = asset; EditorGUIUtility.PingObject(asset);

			cachedInstances.Add(asset);
		}
#endif
	}
}

#if UNITY_EDITOR
namespace PaintIn3D.Examples
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(P3dGroupData))]
	public class P3dGroupName_Editor : P3dEditor<P3dGroupData>
	{
		class Entry
		{
			public string Path;

			public List<P3dHelper.TexEnv> TexEnvs = new List<P3dHelper.TexEnv>();
		}

		private string filter;

		private bool clean;

		private List<Entry> entries = new List<Entry>();

		protected virtual void OnEnable()
		{
			P3dGroupData.UpdateCachedInstances();
		}

		protected override void OnInspector()
		{
			var clashes = P3dGroupData.CachedInstances.Where(d => d.Index == Target.Index);

			BeginError(clashes.Count() > 1);
				Draw("index", "This allows you to set the ID of this group (e.g. 100).\n\nNOTE: This number should be unique, and not shared by any other <b>P3dGroupData</b>.");
			EndError();
			Draw("textureDatas", "This allows you to specify the way each channel of this group's pixels are mapped to textures. This is mainly used by the in-editor painting mateiral builder tool.");
			Draw("shaderData", "This allows you to specify which shaders and their proprties are associated with this group.");

			Separator();

			EditorGUILayout.LabelField("Current Groups", EditorStyles.boldLabel);

			var groupDatas = P3dGroupData.CachedInstances.OrderBy(d => d.Index);

			EditorGUI.BeginDisabledGroup(true);
				foreach (var groupData in groupDatas)
				{
					if (groupData != null)
					{
						EditorGUILayout.BeginHorizontal();
							EditorGUILayout.LabelField(groupData.name);
							EditorGUILayout.IntField(groupData.Index);
						EditorGUILayout.EndHorizontal();
					}
				}
			EditorGUI.EndDisabledGroup();

			Separator();

			EditorGUILayout.LabelField("Shader Properties", EditorStyles.boldLabel);

			filter = EditorGUILayout.TextField("Filter", filter);
			clean  = EditorGUILayout.Toggle("Clean", clean);

			var text = "";

			if (string.IsNullOrEmpty(filter) == false)
			{
				var tokens = filter.Split(' ');

				if (entries.Count == 0)
				{
					foreach (var shaderInfo in ShaderUtil.GetAllShaderInfo())
					{
						var entry = new Entry();

						entry.Path    = shaderInfo.name;
						entry.TexEnvs.AddRange(P3dHelper.GetTexEnvs(Shader.Find(shaderInfo.name)));

						entries.Add(entry);
					}
				}

				foreach (var entry in entries)
				{
					foreach (var texEnv in entry.TexEnvs)
					{
						foreach (var token in tokens)
						{
							if (string.IsNullOrEmpty(token) == false)
							{
								if (Contains(texEnv.Name, token) == true || Contains(texEnv.Desc, token) == true || Contains(entry.Path, token) == true)
								{
									var line = "";

									if (clean == true)
									{
										line += texEnv.Name + "@" + entry.Path + "\n";
									}
									else
									{
										line += texEnv.Name + " - " + texEnv.Desc + " - " + entry.Path + "\n";
									}

									if (text.Contains(line) == false)
									{
										text += line;
									}

									continue;
								}
							}
						}
					}
				}

				EditorGUILayout.TextArea(text, GUILayout.ExpandHeight(true));
			}
		}

		private bool Contains(string paragraph, string word)
		{
			return System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(paragraph, word,  System.Globalization.CompareOptions.IgnoreCase) >= 0;
		}
	}
}
#endif
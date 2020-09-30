using System.IO;
using UnityEditor;
using UnityEngine;

namespace DesignPatterns.Singleton
{
	public partial class CustomTypeGenerator : EditorWindow
	{
		private string _name = string.Empty;
		private string _path = string.Empty;
		private string _namespace = string.Empty;
#if CODE_GENERATOR_PRESENT
		[MenuItem("Window/Utilities/TypeGenerator")]
		static void Init()
		{
			CustomTypeGenerator window = (CustomTypeGenerator)GetWindow(typeof(CustomTypeGenerator));
			window.minSize = window.maxSize = new Vector2(300, EditorGUIUtility.singleLineHeight * 3 + 22);
			window.SelectPath();
			window.Show();
		}
#endif
		private void SelectPath()
		{
			if (Selection.activeObject != null)
			{
				_path = AssetDatabase.GetAssetPath(Selection.activeObject);
				if (!string.IsNullOrEmpty(Path.GetExtension(_path)))
				{
					string fileName = Path.GetFileName(_path);
					_path = _path.Replace("/" + fileName, "");
				}
			}
			else
				_path = Application.dataPath;
		}

		private void OnSelectionChange()
		{
			SelectPath();
			Repaint();
		}

		private void OnGUI()
		{
			_name = EditorGUILayout.TextField("Custom type name: ", _name);
			_namespace = EditorGUILayout.TextField("Namespace: ", _namespace);
			EditorGUILayout.LabelField(string.Format("Path: {0}", _path));
#if CODE_GENERATOR_PRESENT
			if (GUILayout.Button("Generate"))
				new CustomTypeClassGenerator(_path, _name, _namespace).Generate();
#endif
		}
	}
}
using System.IO;
using UnityEditor;
using UnityEngine;
using BaseGameLogic.Utilities.Editor;

namespace BaseGameLogic.Singleton
{
	public class SingletonTypeGenerator : EditorWindow
	{
		public class CreateAssetMenu : CSharpFileGenerator.Attribute
		{
			public override string Name => "CreateAssetMenu";
			public override string Parameters => string.Format("fileName = \"{0}.asset\", menuName = \"Custom Type/{0}\"", FileName);
			public string FileName { get; private set; }

			public CreateAssetMenu(string fileName)
			{
				FileName = fileName;
			}
		}

		private const string Usings = "using UnityEngine;\r\nusing Mechanic.BaseClasses;\r\n\r\n";
		private const string Mechanic_Class_Heder = "public class {0} : MechanicWithSettings<{0}Settings>";
		private const string Create_Asset_Menu = "[CreateAssetMenu(fileName = \"{0}.asset\", menuName = \"Custom Type/{0}\")]";

		private const string Mechanic_Settings_Class_Heder = "[CreateAssetMenu(fileName = \"{0} Settings.asset\", menuName = \"Settings/{0} Settings\")]\r\npublic class {0}Settings : ScriptableObject";
		private const string Class_Body = "\r\n{\r\n}\r\n";

		private string _name = string.Empty;
		private string _path = string.Empty;

		[MenuItem("Window/Utilities/TypeGenerator")]
		static void Init()
		{
			SingletonTypeGenerator window = (SingletonTypeGenerator)GetWindow(typeof(SingletonTypeGenerator));
			window.minSize = window.maxSize = new Vector2(300, 50);
			window.SelectPath();
			window.Show();
		}

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
			_name = EditorGUILayout.TextField("Mechanic script name: ", _name);
			EditorGUILayout.LabelField(string.Format("Path: {0}", _path));
			if (GUILayout.Button("Generate"))
				GenerateMechanicScipts();
		}

		private void GenerateMechanicScipts()
		{
			CSharpFileGenerator generator = new CSharpFileGenerator();
			generator.Usings.Add(new CSharpFileGenerator.Using("UnityEngine"));
			generator.ClassAttributes.Add(new CreateAssetMenu("Test"));
			generator.DerivedFrom += typeof(ScriptableObject).Name;
			generator.Save(Application.dataPath);
			
			//string data = Usings + string.Format(Mechanic_Class_Heder, _name) + Class_Body;
			//File.WriteAllText(string.Format("{0}/{1}.cs", _path, _name), data);
			//data = Usings + string.Format(Mechanic_Settings_Class_Heder, _name) + Class_Body;
			//File.WriteAllText(string.Format("{0}/{1}Settings.cs", _path, _name), data);
			AssetDatabase.Refresh();
		}
	}
}
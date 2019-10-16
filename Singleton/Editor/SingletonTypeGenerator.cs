using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Utilities.Editor;

namespace BaseGameLogic.Singleton
{
	public partial class SingletonTypeGenerator : EditorWindow
	{
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
			string name = "SuperKlasa";

			CSharpFile customTypeManagerGenerator = new CSharpFile();
			customTypeManagerGenerator.Name = "SuperKlasaTypeManager";

			customTypeManagerGenerator.cSharpFileElements.Add(new UsingElement(typeof(SingletonTypeGenerator)));
			customTypeManagerGenerator.cSharpFileElements.Add(new UsingElement(typeof(CreateAssetMenuAttribute)));
			customTypeManagerGenerator.cSharpFileElements.Add(new UsingElement(typeof(SerializableAttribute)));
			customTypeManagerGenerator.cSharpFileElements.Add(new NewLineElement());

			NamespaceElement namespaceElement = new NamespaceElement("CustomType");
			customTypeManagerGenerator.cSharpFileElements.Add(namespaceElement);

			ClassElement managerClass = new ClassElement(string.Format("{0}TypeManager", name));
			managerClass.ClassAttributes.Add(new CreateAssetMenuElement(name));
			managerClass.DerivedFrom = new SingletonTypeManagerElement(string.Format("{0}TypeManager", name));

			namespaceElement.cSharpFileElements.Add(managerClass);

			ClassElement typeClass = new ClassElement(name);
			typeClass.ClassAttributes.Add(new AttributeElement(typeof(SerializableAttribute)));
			typeClass.DerivedFrom = new CustomTypeEelemnt();
			namespaceElement.cSharpFileElements.Add(typeClass);


			customTypeManagerGenerator.Save(Application.dataPath + "/Test");

			CSharpFile propertyGenerator = new CSharpFile();

			propertyGenerator.cSharpFileElements.Add(new UsingElement(typeof(CustomPropertyDrawer)));
			propertyGenerator.cSharpFileElements.Add(new UsingElement(typeof(SingletonTypeGenerator)));
			propertyGenerator.cSharpFileElements.Add(new UsingElement(typeof(CreateAssetMenuAttribute)));
			propertyGenerator.cSharpFileElements.Add(new UsingElement(typeof(CustomPropertyDrawer)));


			propertyGenerator.cSharpFileElements.Add(namespaceElement);
			ClassElement propertyClass = new ClassElement(string.Format("{0}PropertyDrower", name));
			propertyGenerator.Name = propertyClass.Name;
			propertyClass.DerivedFrom = new TypePropertyDrowerElement(managerClass.Name);
			propertyClass.ClassAttributes.Add(new WeaponModeTypePropertyDrowerAttributeElement(typeClass.Name));
			namespaceElement.cSharpFileElements.Clear();
			namespaceElement.cSharpFileElements.Add(propertyClass);


			propertyGenerator.Save(Application.dataPath + "/Test/Editor");

			AssetDatabase.Refresh();
		}
	}

	internal class SingletonTypeManagerElement : InheritanceElement
	{
		public SingletonTypeManagerElement(string name) : base(string.Format("SingletonTypeManager<{0}>", name)) { }
	}

	internal class TypePropertyDrowerElement : InheritanceElement
	{
		public TypePropertyDrowerElement(string name) : base(string.Format("TypePropertyDrower<{0}>", name)) { }
	}

	internal class CustomTypeEelemnt : InheritanceElement
	{
		public CustomTypeEelemnt() : base("BaseGameLogic.Singleton.Type") { }
	}

	internal class WeaponModeTypePropertyDrowerAttributeElement : AttributeElement
	{
		public WeaponModeTypePropertyDrowerAttributeElement(string name) : base(string.Format("CustomPropertyDrawer(typeof({0}), true)", name)) { }
	}

	internal class CreateAssetMenuElement : AttributeElement
	{
		public override string Parameters => string.Format("fileName = \"{0}TypeManager.asset\", menuName = \"Custom Type/{0}\"", FileName);
		public string FileName { get; private set; }

		public CreateAssetMenuElement(string fileName) : base("CreateAssetMenu")
		{
			FileName = fileName;
		}
	}
}
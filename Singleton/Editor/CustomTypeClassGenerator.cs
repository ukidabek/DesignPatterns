using System;
using UnityEditor;
using UnityEngine;

#if CODE_GENERATOR_PRESENT
using Utilities.Editor.CodeGeneration;

namespace DesignPatterns.Singleton
{
	internal class CustomTypeClassGenerator
	{
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

		private string path = string.Empty, name = string.Empty, @namespace = string.Empty;

		public CustomTypeClassGenerator(string path, string name, string @namespace)
		{
			this.path = path;
			this.name = name;
			this.@namespace = @namespace;
		}

		public void Generate()
		{
			CSharpFile customTypeManagerGenerator = new CSharpFile();
			customTypeManagerGenerator.Name = string.Format("{0}TypeManager", name);

			customTypeManagerGenerator.cSharpFileElements.Add(new UsingElement(typeof(CustomTypeGenerator)));
			customTypeManagerGenerator.cSharpFileElements.Add(new UsingElement(typeof(CreateAssetMenuAttribute)));
			customTypeManagerGenerator.cSharpFileElements.Add(new UsingElement(typeof(SerializableAttribute)));
			customTypeManagerGenerator.cSharpFileElements.Add(new NewLineElement());

			NamespaceElement namespaceElement = new NamespaceElement(string.IsNullOrEmpty(@namespace) ? "CustomType" : @namespace);
			customTypeManagerGenerator.cSharpFileElements.Add(namespaceElement);

			ClassElement managerClass = new ClassElement(string.Format("{0}TypeManager", name));
			managerClass.ClassAttributes.Add(new CreateAssetMenuElement(name));
			managerClass.DerivedFrom = new SingletonTypeManagerElement(string.Format("{0}TypeManager", name));

			namespaceElement.cSharpFileElements.Add(managerClass);

			ClassElement typeClass = new ClassElement(name);
			typeClass.ClassAttributes.Add(new AttributeElement(typeof(SerializableAttribute)));
			typeClass.DerivedFrom = new CustomTypeEelemnt();
			namespaceElement.cSharpFileElements.Add(typeClass);


			customTypeManagerGenerator.Save(string.Format("{0}/{1}", path,name));

			CSharpFile propertyGenerator = new CSharpFile();

			propertyGenerator.cSharpFileElements.Add(new UsingElement(typeof(CustomPropertyDrawer)));
			propertyGenerator.cSharpFileElements.Add(new UsingElement(typeof(CustomTypeGenerator)));
			propertyGenerator.cSharpFileElements.Add(new UsingElement(typeof(CreateAssetMenuAttribute)));


			propertyGenerator.cSharpFileElements.Add(namespaceElement);
			ClassElement propertyClass = new ClassElement(string.Format("{0}PropertyDrower", name));
			propertyGenerator.Name = propertyClass.Name;
			propertyClass.DerivedFrom = new TypePropertyDrowerElement(managerClass.Name);
			propertyClass.ClassAttributes.Add(new WeaponModeTypePropertyDrowerAttributeElement(typeClass.Name));
			namespaceElement.cSharpFileElements.Clear();
			namespaceElement.cSharpFileElements.Add(propertyClass);


			propertyGenerator.Save(string.Format("{0}/{1}/Editor", path, name));

			AssetDatabase.Refresh();
		}
	}
}
#endif
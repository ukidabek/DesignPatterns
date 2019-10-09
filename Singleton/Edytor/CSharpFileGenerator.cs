using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BaseGameLogic.Utilities.Editor
{
	public class CSharpFileGenerator
	{
		public class Using
		{
			public Using(string usingPath)
			{
				UsingPath = usingPath;
			}

			public string UsingPath { get; private set; }
			public override string ToString()
			{
				return string.Format("using {0};\r\n", UsingPath);
			}
		}
		public abstract class Attribute
		{
			public abstract string Name { get; }
			public virtual string Parameters { get => string.Empty; }
			public override string ToString()
			{
				string parameters = string.IsNullOrEmpty(Parameters) ? string.Empty : string.Format("({0})", Parameters);
				return string.Format("[{0}{1}]\r\n", Name, parameters);
			}
		}

		public enum FileType { Class, Struct, Interface }
		public enum FileAccessModifier { Public, Private, Protected }

		public FileAccessModifier AccessModifier = FileAccessModifier.Public;
		public FileType Type = FileType.Class;

		public List<Using> Usings = new List<Using>();
		public List<Attribute> ClassAttributes = new List<Attribute>();

		public string Name = "CSharpFile";
		public string DerivedFrom = string.Empty;

		private string WriteObjects(IList objects)
		{
			string content = string.Empty;
			foreach (var item in objects)
				content += item.ToString();
			return content;
		}

		private string Generate()
		{
			string content = string.Empty;

			content += WriteObjects(Usings);
			content += "\r\n";
			content += WriteObjects(ClassAttributes);

			content += string.Format(
				"{0} {1} {2}",
				AccessModifier.ToString().ToLower(),
				Type.ToString().ToString().ToLower(),
				Name);

			if (!string.IsNullOrEmpty(DerivedFrom))
				content += string.Format(" : {0}", DerivedFrom);

			content += "\r\n";
			content += "{\r\n";
			content += "}\r\n";

			return content;
		}

		public virtual void Save(string path)
		{
			File.WriteAllText(string.Format("{0}/{1}.cs", path, Name), Generate());
		}
	}
}

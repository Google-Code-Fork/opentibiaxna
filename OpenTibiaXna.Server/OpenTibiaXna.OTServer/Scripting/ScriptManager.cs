﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Scripting
{
    public class ScriptManager
    {
        private static CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
        private static VBCodeProvider vBCodeProvider = new VBCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
        private static List<IScript> loadedScripts = new List<IScript>();

        private static StringBuilder errorLog;

        public static string LoadAllScripts(GameObject game)
        {
            errorLog = new StringBuilder();

            foreach (string path in Directory.GetFiles(@"Data\Scripts"))
            {
                if (!File.Exists(path)) continue;
                LoadScript(game, path);
            }

            return errorLog.ToString();
        }

        public static void ReloadAllScripts(GameObject game)
        {
            UnloadAllScripts();
            LoadAllScripts(game);
        }

        public static void UnloadAllScripts()
        {
            foreach (IScript script in loadedScripts)
            {
                script.Stop();
            }
            loadedScripts.Clear();
        }

        public static void LoadScript(GameObject game, string path)
        {
            Assembly assembly = null;
            switch (Path.GetExtension(path))
            {
                case ".dll":
                    assembly = LoadDll(path);
                    break;
                case ".cs":
                    assembly = CompileScript(path, cSharpCodeProvider);
                    break;
                case ".vb":
                    assembly = CompileScript(path, vBCodeProvider);
                    break;
            }

            if (assembly != null)
            {
                foreach (IScript script in FindScripts(assembly))
                {
                    loadedScripts.Add(script);
                    script.Start(game);
                }
            }
        }

        public static Assembly CompileScript(string path, CodeDomProvider provider)
        {
            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.GenerateExecutable = false;
            compilerParameters.GenerateInMemory = true;
            compilerParameters.IncludeDebugInformation = false;
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add(System.Reflection.Assembly.GetExecutingAssembly().Location);
            CompilerResults results = provider.CompileAssemblyFromFile(compilerParameters, path);
            if (!results.Errors.HasErrors)
            {
                return results.CompiledAssembly;
            }
            else
            {
                foreach (CompilerError error in results.Errors)
                    errorLog.AppendLine(error.ToString());
            }
            return null;
        }

        public static IEnumerable<IScript> FindScripts(Assembly assembly)
        {
            foreach (Type t in assembly.GetTypes())
            {
                if (t.GetInterface("IScript", true) != null)
                {
                    yield return (IScript)assembly.CreateInstance(t.FullName);
                }
            }
        }

        public static Assembly LoadDll(string path)
        {
            return Assembly.LoadFile(path);
        }
    }
}

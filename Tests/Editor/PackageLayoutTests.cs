using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;


namespace ScriptableObjectArchitecture.Tests
{
    /// <summary>
    ///     Guards against packaging regressions: editor-only code leaking into the runtime assembly
    ///     (player builds fail with CS0246), and sample paths in package.json that don't exist.
    /// </summary>
    public class PackageLayoutTests
    {
        private static readonly string[] NonRuntimeFolders = {"Editor", "Tests", "Samples", "Samples~", ".git"};


        [Test]
        public void RuntimeScripts_GuardEveryUsingUnityEditor()
        {
            var root = GetPackageRoot();
            var offenders = new List<string>();

            foreach (var file in Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories))
            {
                var relative = file.Substring(root.Length).TrimStart('/', '\\');
                var firstFolder = relative.Split('/', '\\')[0];

                if (NonRuntimeFolders.Contains(firstFolder))
                {
                    continue;
                }

                offenders.AddRange(FindUnguardedEditorUsings(File.ReadAllLines(file)).Select(line => relative + ":" + line));
            }

            Assert.That(offenders, Is.Empty, "Unguarded 'using UnityEditor' in runtime code breaks player builds.");
        }


        [Test]
        public void FindUnguardedEditorUsings_DetectsGuardedAndUnguarded()
        {
            var lines = new[]
            {
                "using UnityEditor;",
                "#if UNITY_EDITOR",
                "using UnityEditor;",
                "#if FOO",
                "using UnityEditor.SceneManagement;",
                "#endif",
                "#endif",
                "using UnityEditor.Callbacks;"
            };

            Assert.That(FindUnguardedEditorUsings(lines), Is.EqualTo(new[] {1, 8}));
        }


        [Test]
        public void PackageJson_SamplePathsExistAndAreVisible()
        {
            var root = GetPackageRoot();
            var json = File.ReadAllText(Path.Combine(root, "package.json"));
            var manifest = JsonUtility.FromJson<Manifest>(json);

            Assert.That(manifest.samples, Is.Not.Null.And.Not.Empty);

            foreach (var sample in manifest.samples)
            {
                StringAssert.DoesNotContain("~", sample.path, "Samples live in visible Samples/, not Samples~/.");
                Assert.That(Directory.Exists(Path.Combine(root, sample.path)), Is.True, "Missing sample folder: " + sample.path);
            }
        }


        /// <summary>Returns 1-based line numbers of <c>using UnityEditor...;</c> lines that are not inside an <c>#if UNITY_EDITOR</c> block.</summary>
        private static List<int> FindUnguardedEditorUsings(IReadOnlyList<string> lines)
        {
            var result = new List<int>();
            var stack = new Stack<bool>();

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Trim().TrimStart('﻿');

                if (line.StartsWith("#if", StringComparison.Ordinal))
                {
                    stack.Push(line.Contains("UNITY_EDITOR") && !line.Contains("!UNITY_EDITOR"));
                }
                else if (line.StartsWith("#endif", StringComparison.Ordinal))
                {
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                    }
                }
                else if (line.StartsWith("#else", StringComparison.Ordinal) || line.StartsWith("#elif", StringComparison.Ordinal))
                {
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                        stack.Push(line.StartsWith("#elif", StringComparison.Ordinal) && line.Contains("UNITY_EDITOR") && !line.Contains("!UNITY_EDITOR"));
                    }
                }
                else if (line.StartsWith("using UnityEditor", StringComparison.Ordinal) && !stack.Contains(true))
                {
                    result.Add(i + 1);
                }
            }

            return result;
        }


        private static string GetPackageRoot()
        {
            var info = UnityEditor.PackageManager.PackageInfo.FindForAssembly(typeof(GameEventBase).Assembly);

            if (info != null)
            {
                return info.resolvedPath;
            }

            var guid = AssetDatabase.FindAssets("ScriptableObject-Architecture t:AssemblyDefinitionAsset")
                                    .FirstOrDefault(g => Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(g)) == "ScriptableObject-Architecture");

            Assert.That(guid, Is.Not.Null, "Could not find ScriptableObject-Architecture.asmdef");

            return Path.GetFullPath(Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(guid)));
        }


        [Serializable]
        private class Manifest
        {
            public Sample[] samples;
        }


        [Serializable]
        private class Sample
        {
            public string path;
        }
    }
}

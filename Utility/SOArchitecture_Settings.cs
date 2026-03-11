using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace ScriptableObjectArchitecture
{
    /// <summary>
    ///     Project-scoped settings asset for SO Architecture.
    ///     Stores the code generation output directory, the overwrite flag, and the default create-asset-menu order.
    ///     Accessed as a singleton via <see cref="Instance" />; automatically created at the project root when missing.
    /// </summary>

    public class SOArchitecture_Settings : ScriptableObject
    {
        [SerializeField] private string _codeGenerationTargetDirectory = "CODE_GENERATION";

        [SerializeField] [Tooltip("Allow newly generated code files to overwrite existing ones")]
        private bool _codeGenerationAllowOverwrite;

        [SerializeField] private int _defualtCreateAssetMenuOrder = 120;

        /// <summary>Relative path under <c>Assets/</c> where generated code files are written.</summary>
        public string CodeGenerationTargetDirectory
        {
            get => _codeGenerationTargetDirectory;
            set => _codeGenerationTargetDirectory = value;
        }

        /// <summary>When <c>true</c>, the code generator may overwrite existing files at the target path.</summary>
        public bool CodeGenerationAllowOverwrite
        {
            get => _codeGenerationAllowOverwrite;
            set => _codeGenerationAllowOverwrite = value;
        }

        /// <summary>The default order value placed in <c>[CreateAssetMenu]</c> attributes on generated types.</summary>
        public int DefaultCreateAssetMenuOrder
        {
            get => _defualtCreateAssetMenuOrder;
            set => _defualtCreateAssetMenuOrder = value;
        }

        #region Singleton

        /// <summary>
        ///     Singleton accessor. Returns the existing project asset if found; otherwise auto-creates one at the project root.
        ///     Always returns <c>null</c> in non-editor builds.
        /// </summary>
        public static SOArchitecture_Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GetInstance();
                }

                return _instance;
            }
        }

        private static SOArchitecture_Settings _instance;


        private static SOArchitecture_Settings GetInstance()
        {
            #if UNITY_EDITOR
            var instance = FindInstanceInProject();

            if (instance == null)
            {
                return CreateInstance();
            }

            return instance;
            #else
            return null;
            #endif
        }


        private static SOArchitecture_Settings FindInstanceInProject()
        {
            #if UNITY_EDITOR
            var settingsGUIDs = AssetDatabase.FindAssets(AssetDatabaseSearchString);

            if (settingsGUIDs.Length == 0)
            {
                return null;
            }

            if (settingsGUIDs.Length > 1)
            {
                Debug.LogWarning("Found more than one instance of SOArchitecture_Settings, you've probably created several SOA settings objects." +
                                 $"\nTo find all instances, type {AssetDatabaseSearchString} into the project view search bar");

                return null;
            }

            var settingsPath = AssetDatabase.GUIDToAssetPath(settingsGUIDs[0]);

            return AssetDatabase.LoadAssetAtPath<SOArchitecture_Settings>(settingsPath);
            #else
            throw new System.NullReferenceException();
            #endif
        }


        private static SOArchitecture_Settings CreateInstance()
        {
            #if UNITY_EDITOR
            var newSettings = CreateInstance<SOArchitecture_Settings>();

            AssetDatabase.CreateAsset(newSettings, DefaultNewSettingsLocation + DefaultNewSettingsName);
            AssetDatabase.SaveAssets();

            Selection.activeObject = newSettings;

            Debug.LogWarning("No SOArchitecture_Settings asset found! " +
                             "Created new one at asset root, feel free to move it wherever you please in your project.", newSettings);

            return newSettings;
            #else
        throw new System.NullReferenceException();
            #endif
        }


        private const string AssetDatabaseSearchString = "t:SOArchitecture_Settings";
        private const string DefaultNewSettingsLocation = "Assets\\";
        private const string DefaultNewSettingsName = "SOArchitecture_Settings.asset";

        #endregion
    }
}
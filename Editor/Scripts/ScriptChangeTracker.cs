using System.IO;
using UnityEditor;
using UnityEditor.Scripting.LifecycleManagement;
using UnityEditor.Toolbars;
using Unity.Scripting.LifecycleManagement;
using UnityEngine;

namespace Core.Editor.Toolbar
{
    [InitializeOnLoad]
    public static partial class ScriptChangeTracker
    {
        private static FileSystemWatcher watcher;
        private static bool doReload = false;
        private static int pendingChangesCount = 0;
        private static readonly object countLock = new object();

        public static int PendingChangesCount
        {
            get
            {
                lock (countLock)
                {
                    return pendingChangesCount;
                }
            }
        }

        public static void ResetCount()
        {
            lock (countLock)
            {
                pendingChangesCount = 0;
                doReload = false;
            }
            MainToolbar.Refresh("AssetControls/PendingChangesCount");
        }

        static ScriptChangeTracker()
        {
            watcher = new FileSystemWatcher(Path.GetFullPath(Application.dataPath))
            {
                Filter = "*.cs",
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };

            watcher.Changed += OnAssetsChanged;
            watcher.Created += OnAssetsChanged;
            watcher.Deleted += OnAssetsChanged;
            watcher.Renamed += OnAssetsChanged;
        }

        private static void OnAssetsChanged(object sender, FileSystemEventArgs e)
        {
            doReload = true;
            lock (countLock)
            {
                pendingChangesCount++;
            }

            EditorApplication.delayCall += () =>
            {
                MainToolbar.Refresh("AssetControls/PendingChangesCount");
            };
        }

        [OnCodeUnloading]
        private static void CleanUp()
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                watcher = null;
            }
        }

        [OnExitingEditMode]
        private static void OnExitingEditMode()
        {
            if (doReload)
            {
                Debug.Log("[ScriptChangeTracker] Pending changes detected — triggering AssetDatabase refresh before Play Mode.");
                AssetDatabase.Refresh();
                ResetCount();
            }
        }
    } 
}
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Toolbar
{
    public class MainToolbarSceneControls
    {
        #region Start Button
        private const string k_playFirstIcon = "SceneSet Icon";

        [MainToolbarElement("SceneControls/PlayFirstScene", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement PlayFirstSceneButton()
        {
            var icon = EditorGUIUtility.IconContent(k_playFirstIcon).image as Texture2D;
            var content = new MainToolbarContent(icon, "Play First Scene in Build Settings");

            var button = new MainToolbarButton(content, () =>
            {
                if (EditorApplication.isPlaying)
                {
                    EditorApplication.isPlaying = false;
                    return;
                }

                if (EditorBuildSettings.scenes.Length == 0)
                {
                    Debug.LogWarning("No scenes found in Build Settings!");
                    return;
                }

                var firstScene = EditorBuildSettings.scenes[0];
                if (!firstScene.enabled)
                {
                    Debug.LogWarning("The first scene in Build Settings is disabled!");
                    return;
                }

                // Prompt to save modified scenes before switching
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(firstScene.path);
                    EditorApplication.isPlaying = true;
                }
            });

            // Apply compact styling
            MainToolbarElementStyler.StyleElement<EditorToolbarButton>("SceneControls/PlayFirstScene", element =>
            {
                const float k_padding = 6.0f;
                const float k_dimension = 16.0f;
                Color k_background = new Color(0.3f, 0.3f, 0.3f);

                element.style.paddingLeft = k_padding;
                element.style.paddingRight = k_padding;
                element.style.backgroundColor = k_background;

                var image = element.Q<Image>();
                if (image != null)
                {
                    image.style.width = k_dimension;
                    image.style.height = k_dimension;
                }
            });

            return button;
        }
        #endregion

        #region Scene Dropdown
        private const string k_sceneDropdownIcon = "d_SceneList Icon";

        [MainToolbarElement("SceneControls/SceneDropdown", defaultDockPosition = MainToolbarDockPosition.Left)]
        public static MainToolbarElement SceneDropdownMenu()
        {
            var icon = EditorGUIUtility.IconContent(k_sceneDropdownIcon, "Start game with default scene.").image as Texture2D;
            var content = new MainToolbarContent("Scenes", icon, "Open Scene");

            var dropdown = new MainToolbarDropdown(content, (evt) =>
            {
                var menu = new GenericMenu();

                // Section 1: Build Settings Scenes
                menu.AddDisabledItem(new GUIContent("Build Settings Scenes"));
                for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
                {
                    var sceneOption = EditorBuildSettings.scenes[i];
                    if (string.IsNullOrEmpty(sceneOption.path)) continue;

                    string sceneName = Path.GetFileNameWithoutExtension(sceneOption.path);
                    string path = sceneOption.path;
                    bool isCurrent = EditorSceneManager.GetActiveScene().path == path;

                    menu.AddItem(new GUIContent($"Build Scenes/{i}: {sceneName}"), isCurrent, () => OpenScene(path));
                }

                menu.AddSeparator("");

                // Section 2: All Project Scenes
                menu.AddDisabledItem(new GUIContent("All Project Scenes"));
                string[] allSceneGuids = AssetDatabase.FindAssets("t:Scene");
                foreach (var guid in allSceneGuids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    if (path.StartsWith("Packages/")) continue; // Exclude package scenes

                    string sceneName = Path.GetFileNameWithoutExtension(path);
                    bool isCurrent = EditorSceneManager.GetActiveScene().path == path;

                    // Structure submenus using folder structure
                    string folderPath = Path.GetDirectoryName(path)?.Replace("Assets/", "") ?? "Root";
                    string menuPath = $"All Scenes/{folderPath}/{sceneName}";

                    menu.AddItem(new GUIContent(menuPath), isCurrent, () => OpenScene(path));
                }

                menu.DropDown(evt);
            });

            MainToolbarElementStyler.StyleElement<VisualElement>("SceneControls/SceneDropdown", element =>
            {
                const float margin = 6.0f;
                Color k_background = new Color(0.3f, 0.3f, 0.3f);

                element.style.marginLeft = margin;
                element.style.marginRight = margin * 2.0f;

                var dropdown = element.Q<EditorToolbarDropdown>();
                if (dropdown != null)
                    dropdown.style.backgroundColor = k_background;
            });

            return dropdown;
        }

        private static void OpenScene(string scenePath)
        {
            if (EditorApplication.isPlaying)
            {
                Debug.LogWarning("Cannot switch scenes while in Play Mode.");
                return;
            }

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }
        #endregion
    } 
}
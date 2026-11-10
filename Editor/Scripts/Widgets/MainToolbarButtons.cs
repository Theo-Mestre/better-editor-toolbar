using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Toolbar
{
    public class MainToolbarButtons
    {
        #region Reset Timescale Button
        [MainToolbarElement("Timescale/Reset", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement ResetTimescaleButton()
        {
            const float k_defaultTimescaleValue = 1.0f;

            var icon = EditorGUIUtility.IconContent("Refresh").image as Texture2D;
            var content = new MainToolbarContent(icon, "Reset");
            var button = new MainToolbarButton(content, () =>
            {
                Time.timeScale = k_defaultTimescaleValue;
                MainToolbar.Refresh("Timescale/Slider");
            });

            MainToolbarElementStyler.StyleElement<EditorToolbarButton>("Timescale/Reset", element =>
            {
                const float padding = 0.0f;
                const float marging = 20.0f;
                const float width = 20.0f;
                const float imageDimension = 16.0f;

                element.style.paddingRight = padding;
                element.style.paddingLeft = padding;
                element.style.marginLeft = marging;
                element.style.minWidth = width;
                element.style.maxWidth = width;

                var image = element.Q<Image>();
                if (image != null)
                {
                    image.style.width = imageDimension;
                    image.style.height = imageDimension;
                }
            });

            return button;
        }
        #endregion

        #region Refresh Assets Button
        private const string k_refreshIcon = "Refresh";

        [MainToolbarElement("AssetControls/RefreshAssets", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement RefreshAssetsButton()
        {
            var icon = EditorGUIUtility.IconContent(k_refreshIcon).image as Texture2D;
            var content = new MainToolbarContent(icon, "Refresh Asset Database (Reimport changed assets)");

            var button = new MainToolbarButton(content, () =>
            {
                AssetDatabase.Refresh();
                Debug.Log("[Toolbar] Asset Database refreshed.");
            });

            MainToolbarElementStyler.StyleElement<EditorToolbarButton>("AssetControls/RefreshAssets", element =>
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

        #region Compile Scripts Button
        private const string k_compileIcon = "cs Script Icon";

        [MainToolbarElement("AssetControls/CompileScripts", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement CompileScriptsButton()
        {
            var icon = EditorGUIUtility.IconContent(k_compileIcon).image as Texture2D;
            var content = new MainToolbarContent(icon, "Force Recompile Scripts");

            var button = new MainToolbarButton(content, () =>
            {
                CompilationPipeline.RequestScriptCompilation();
                Debug.Log("[Toolbar] Requested script compilation.");
            });

            MainToolbarElementStyler.StyleElement<EditorToolbarButton>("AssetControls/CompileScripts", element =>
            {
                const float k_padding = 6.0f;
                const float k_dimension = 16.0f;
                const float k_margin = 2.0f;
                Color k_background = new Color(0.3f, 0.3f, 0.3f);

                element.style.paddingLeft = k_padding;
                element.style.paddingRight = k_padding;
                element.style.marginLeft = k_margin;
                element.style.marginRight = k_margin;
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
    }
}
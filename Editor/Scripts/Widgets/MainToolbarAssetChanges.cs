using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;
using Core.Utilities;

namespace Core.Editor.Toolbar
{
    public class MainToolbarAssetChanges
    {
        #region Pending Changes Count Indicator
        [MainToolbarElement("AssetControls/PendingChangesCount", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement PendingChangesCountElement()
        {
            int count = ScriptChangeTracker.PendingChangesCount;
            var content = new MainToolbarContent($"{count} Script Change{(count == 1 ? "" : "s")}", "Pending C# file changes since last refresh");

            var labelElement = new MainToolbarLabel(content);

            MainToolbarElementStyler.StyleElement<VisualElement>("AssetControls/PendingChangesCount", element =>
            {
                const float k_containerPadding = 0.0f;
                const float k_paddingLeftRight = 6.0f;
                const float k_margin = 2.0f;
                const float k_fontSize = 11.0f;
                const FontStyle fontType = FontStyle.Bold;
                Color textColor = Color.white;
                Color highlightedTextColor = new Color(0.7f, 0.7f, 0.7f);
                Color k_background = count > 0 ? new Color(0.8f, 0.45f, 0.1f) : new Color(0.25f, 0.25f, 0.25f);

                element.style.paddingLeft = k_paddingLeftRight;
                element.style.paddingRight = k_paddingLeftRight;
                element.style.marginLeft = k_margin;
                element.style.marginRight = k_margin;

                var label = element.Q<Label>();
                if (label != null)
                {
                    label.style.fontSize = k_fontSize;
                    label.style.unityFontStyleAndWeight = fontType;
                    label.style.color = count > 0 ? textColor : highlightedTextColor;
                }

                var container = element.FindElementByName("unity-overlay");
                container.style.backgroundColor = k_background;
                container.style.paddingTop = k_containerPadding;
                container.style.paddingBottom = k_containerPadding;
            });

            return labelElement;
        }
        #endregion
    }

}
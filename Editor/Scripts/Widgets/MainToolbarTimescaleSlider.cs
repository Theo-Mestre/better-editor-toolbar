using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Toolbar
{
    public class MainToolbarTimescaleSlider
    {
        const float k_minTimescaleValue = 0f;
        const float k_defaultTimescaleValue = 0f;
        const float k_maxTimescaleValue = 10f;

        [MainToolbarElement("Timescale/Slider", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarElement TimescaleSlider()
        {
            var content = new MainToolbarContent("Time Scale", "Time Scale");
            var slider = new MainToolbarSlider(content, Time.timeScale, k_minTimescaleValue, k_maxTimescaleValue, OnSliderValueChanged)
            {
                populateContextMenu = (menu) =>
                {
                    menu.AppendAction("Reset", _ =>
                    {
                        Time.timeScale = k_defaultTimescaleValue;
                        MainToolbar.Refresh("Timescale/Slider");

                    });
                }
            };

            MainToolbarElementStyler.StyleElement<VisualElement>("Timescale/Slider", (element) =>
            {
                const float padding = 10.0f;

                element.style.paddingLeft = padding;
                element.style.paddingRight = padding;
            });


            return slider;
        }

        private static void OnSliderValueChanged(float newValue)
        {
            Time.timeScale = newValue;
        }
    }

}
using Core.Utilities;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Toolbar
{
    public class MainToolbarElementStyler
    {
        public static void StyleElement<T>(string elementName, Action<T> styleCallback)
            where T : VisualElement
        {
            EditorApplication.delayCall += () =>
            {
                ApplyStyle(elementName, (element) =>
                {
                    T target = null;

                    if (element is T typedElement) target = typedElement;
                    else target = element.Query<T>().First();

                    if (target != null)
                        styleCallback?.Invoke(target);
                });
            };
        }

        private static void ApplyStyle(string elementName, Action<VisualElement> styleCallback)
        {
            var element = FindElementByName(elementName);

            if (element == null) return;

            styleCallback?.Invoke(element);
        }

        private static VisualElement FindElementByName(string name)
        {
            var windows = Resources.FindObjectsOfTypeAll<EditorWindow>();

            foreach (var window in windows)
            {
                var root = window.rootVisualElement;
                if (root == null) continue;

                VisualElement element = null;
                if ((element = root.FindElementByName(name)) != null) return element;
                if ((element = root.FindElementByTooltip(name)) != null) return element;
            }

            return null;
        }
    }
}
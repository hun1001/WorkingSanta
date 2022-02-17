using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class ButtonManager : MonoSingleton<ButtonManager>
    {
        private List<Button> handledButtonList = new List<Button>();

        [System.Serializable]
        public class ButtonInfo
        {
            public string name;
            public Button button;
        }

        [SerializeField] List<ButtonInfo> _buttonInfos;

        public void AddHandledButton(Button button)
        {
            if (!handledButtonList.Contains(button))
            {
                handledButtonList.Add(button);
            }
        }

        public void RemoveHandledButton(Button button)
        {
            if (handledButtonList.Contains(button))
            {
                handledButtonList.Remove(button);
            }
        }

        private void Start()
        {
            if (_buttonInfos == null)
            {
                return;
            }

            var buttons = FindObjectsOfType<Button>();
            foreach (var button in buttons)
            {
                var buttonInfo = _buttonInfos.Find(x => x.button == button);
                if (buttonInfo == null)
                {
                    if (!handledButtonList.Contains(button))
                    {
                        Debug.LogError("등록되어있지 않은 버튼이 있습니다: " + GetGameObjectPath(button.gameObject));
                    }
                }
            }
        }

        public void AddHandler<T>(T handler)
        {
            foreach (var buttonInfo in _buttonInfos)
            {
                MethodInfo methodInfo = handler.GetType().GetMethod("On" + buttonInfo.name, BindingFlags.NonPublic | BindingFlags.Instance);
                if (methodInfo != null)
                {
                    buttonInfo.button.onClick.AddListener(() => methodInfo.Invoke(handler, null));
                }
            }
        }

        private string GetGameObjectPath(GameObject obj)
        {
            string path = "/" + obj.name;
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = "/" + obj.name + path;
            }
            return path;
        }
    }
}

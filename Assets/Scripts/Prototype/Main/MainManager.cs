using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype;
using UnityEngine.SceneManagement;

namespace Prototype_Main
{
    public class MainManager : MonoBehaviour
    {
        private void Awake()
        {
            ButtonManager.Instance.AddHandler(this);
        }

        private void OnDeliveryStart()
        {
            //road other scene
            SceneManager.LoadScene("Prototype");
        }
    }
}

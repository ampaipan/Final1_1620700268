using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test
{
    public class TestButton : MonoBehaviour
    {
        public void Back()
        {
            SceneManager.LoadScene("Game");
        }
    }
}

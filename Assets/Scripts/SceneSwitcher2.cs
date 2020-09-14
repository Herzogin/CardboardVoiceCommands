using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher2 : MonoBehaviour
{
    public void switchToMuseum()
    {
        SceneManager.LoadScene("Exhibition");
    }
}

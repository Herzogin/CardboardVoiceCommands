using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void switchToScene0()
	{
		SceneManager.LoadScene("0_Home");
	}
	public void switchToScene1()
    {
        SceneManager.LoadScene("1_Positioning");
    }
	public void switchToScene2()
	{
		SceneManager.LoadScene("2_ObjectIdentification");
	}
	public void switchToScene3()
	{
		SceneManager.LoadScene("3_InformationMapping");
	}
	public void switchToScene4()
	{
		SceneManager.LoadScene("4_Disambiguation");
	}
}

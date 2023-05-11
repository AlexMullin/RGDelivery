using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextScene = "TitleScreen";

    public void ChangeScene ()
    {
        SceneManager.LoadScene (nextScene);
    }

    public void QuitGame ()
    {
        Application.Quit ();
    }
}

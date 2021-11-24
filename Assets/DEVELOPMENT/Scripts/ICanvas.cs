using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICanvas : MonoBehaviour
{
    public void GoToScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(1);
}

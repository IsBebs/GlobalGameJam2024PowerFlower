using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    [SerializeField]
    int LevelBuildIndex = 0;
    // Start is called before the first frame update
    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelBuildIndex, LoadSceneMode.Single);
    }
}

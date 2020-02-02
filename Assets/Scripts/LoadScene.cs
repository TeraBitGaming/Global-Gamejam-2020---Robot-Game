using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void loadScene(int sceneSelect){
        SceneManager.LoadScene(sceneSelect);
    }

    public IEnumerator ReloadScene(float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }
}

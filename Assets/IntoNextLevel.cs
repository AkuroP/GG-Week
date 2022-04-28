using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntoNextLevel : MonoBehaviour
{
    public string nextScene;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}

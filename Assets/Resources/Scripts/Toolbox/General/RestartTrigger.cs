using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Custom/General/RestartTrigger")]
public class RestartTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).path);
        }
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			SceneManager.LoadScene(SceneManager.GetSceneAt(0).path);
		}
	}
}


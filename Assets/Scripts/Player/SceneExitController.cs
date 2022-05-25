using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExitController : MonoBehaviour
{
    public string nextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().PlayeriHareketsizYap();
            other.GetComponent<PlayerController>().enabled = false;

            FadeController.instance.SeffafdanMataGec();

            StartCoroutine(SwitchToNextScene());
        }
    }

    IEnumerator SwitchToNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
    }
}

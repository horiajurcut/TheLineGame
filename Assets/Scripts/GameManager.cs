using System.Collections; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public void EndGame()
    {
        StartCoroutine(PlayEndGameAnimation());
    }

    private static IEnumerator PlayEndGameAnimation()
    {
        Debug.Log("Game Over");
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

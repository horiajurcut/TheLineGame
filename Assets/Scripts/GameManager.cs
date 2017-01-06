using System.Collections; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public void EndGame()
    {
        StartCoroutine(PlayEndGameAnimation());
    }

    private IEnumerator PlayEndGameAnimation()
    {
        Debug.Log("Game Over");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

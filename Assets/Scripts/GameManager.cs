using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private List<Vector2> _screenBoundsPoints;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _screenBoundsPoints = new List<Vector2>();

        var topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        _screenBoundsPoints.Add(new Vector2(topLeft.x, topLeft.y));

        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f));
        _screenBoundsPoints.Add(new Vector2(topRight.x, topRight.y));

        var bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        _screenBoundsPoints.Add(new Vector2(bottomRight.x, bottomRight.y));

        var bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f));
        _screenBoundsPoints.Add(new Vector2(bottomLeft.x, bottomLeft.y));

        // Connect the last point with the first one in the EdgeCollider2D
        _screenBoundsPoints.Add(new Vector2(topLeft.x, topLeft.y));

        var screenBounds = gameObject.AddComponent<EdgeCollider2D>();

        screenBounds.tag = "instantDeath";
        screenBounds.isTrigger = true;
        screenBounds.points = _screenBoundsPoints.ToArray<Vector2>();

    }

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

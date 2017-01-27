using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public Vector3 InitialVectorBottomLeft { get; private set; }
    public Vector3 InitialVectorTopRight { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitialVectorBottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        InitialVectorTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
    }

    private void Start()
    {
        CalculateScreenEdgeCollider();
    }

    private void Update()
    {
        if (HasScreenSizeChanged())
        {
            CalculateScreenEdgeCollider();
        }
    }

    private void ToggleScreenEdgeCollider()
    {
        var screenBounds = gameObject.GetComponent<EdgeCollider2D>();

        if (screenBounds == null)
        {
            return;
        }

        screenBounds.enabled = !screenBounds.enabled;
    }

    private void CalculateScreenEdgeCollider()
    {
        var screenBoundsPoints = new List<Vector2>();

        var topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        screenBoundsPoints.Add(new Vector2(topLeft.x, topLeft.y));

        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f));
        screenBoundsPoints.Add(new Vector2(topRight.x, topRight.y));

        var bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        screenBoundsPoints.Add(new Vector2(bottomRight.x, bottomRight.y));

        var bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f));
        screenBoundsPoints.Add(new Vector2(bottomLeft.x, bottomLeft.y));

        // Connect the last point with the first one in the EdgeCollider2D
        screenBoundsPoints.Add(new Vector2(topLeft.x, topLeft.y));

        var screenBounds = gameObject.GetComponent<EdgeCollider2D>();

        if (screenBounds == null)
        {
            screenBounds = gameObject.AddComponent<EdgeCollider2D>();
        }

        screenBounds.tag = "instantDeath";
        screenBounds.isTrigger = true;
        screenBounds.points = screenBoundsPoints.ToArray<Vector2>();
    }

    public bool HasScreenSizeChanged()
    {
        var updatedVectorBottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var updatedVectorTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        var hasScreenSizeChanged = (InitialVectorBottomLeft != updatedVectorBottomLeft) ||
                                    (InitialVectorTopRight != updatedVectorTopRight);

        if (!hasScreenSizeChanged) return false;

        InitialVectorBottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        InitialVectorTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        return true;
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

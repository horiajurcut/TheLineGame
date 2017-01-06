using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof (LineRenderer))]
[RequireComponent (typeof (EdgeCollider2D))]
public class Tail : MonoBehaviour {

    public Transform myHead;
    public float tailPointSpacing = .1f;

    List<Vector2> tailPoints;

    LineRenderer myLineRenderer;
    EdgeCollider2D myEdgeCollider2D;

    private void Start()
    {
        myLineRenderer = GetComponent<LineRenderer>();
        myEdgeCollider2D = GetComponent<EdgeCollider2D>();

        tailPoints = new List<Vector2>();

        SetTailPoint();
    }

    private void Update()
    {
        if (Vector3.Distance(tailPoints.Last(), myHead.position) > tailPointSpacing)
        {
            SetTailPoint();
        }
    }

    private void SetTailPoint()
    {
        if (tailPoints.Count > 1)
        {
            myEdgeCollider2D.points = tailPoints.ToArray<Vector2>();
        }

        tailPoints.Add(myHead.position);

        myLineRenderer.numPositions = tailPoints.Count;
        myLineRenderer.SetPosition(tailPoints.Count - 1, myHead.position);
    }
}

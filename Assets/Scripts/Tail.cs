using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Tail : MonoBehaviour {

    public Transform MyHead;

    [Header("Tail Settings")]
    public float TailPointSpacing = .1f;
    public Material TailMaterial;

    public Color TailColorStart;
    public Color TailColorEnd;

    private List<List<Vector2>> _segmentTailPoints;
    private List<GameObject> _lineSegments;

    private const float TimeToGap = .15f;
    private const float TimeBetweenGaps = 2.5f;
    private float _timeSinceLastGap;

    private void Start()
    {
        _lineSegments = new List<GameObject>();
        _segmentTailPoints = new List<List<Vector2>>();

        CreateNewLineSegment();
        _timeSinceLastGap = Time.time;

        SetTailPoint();
    }

    private void Update()
    {
        if (!(Vector3.Distance(_segmentTailPoints.Last().Last(), MyHead.position) > TailPointSpacing)) return;

        if (Time.time >= _timeSinceLastGap + TimeBetweenGaps &&
            Time.time < _timeSinceLastGap + TimeBetweenGaps + TimeToGap)
        {
            return;
        }

        if (Time.time >= _timeSinceLastGap + TimeBetweenGaps + TimeToGap)
        {
            AddLastPointToEdgeCollider();
            CreateNewLineSegment();
            _timeSinceLastGap = Time.time + TimeToGap;
        }

        SetTailPoint();
    }

    private void SetTailPoint()
    {
        var currentLineRenderer = _lineSegments.Last().GetComponent<LineRenderer>();
        var currentEdgeCollider2D = _lineSegments.Last().GetComponent<EdgeCollider2D>();

        // EdgeCollider2D needs to have at least 2 points
        if (_segmentTailPoints.Last().Count == 2)
        {
            currentEdgeCollider2D.enabled = true;
        }

        if (_segmentTailPoints.Last().Count > 1)
        {
            currentEdgeCollider2D.points = _segmentTailPoints.Last().ToArray<Vector2>();
        }

        _segmentTailPoints.Last().Add(MyHead.position);

        currentLineRenderer.numPositions = _segmentTailPoints.Last().Count;
        currentLineRenderer.SetPosition(_segmentTailPoints.Last().Count - 1, MyHead.position);
    }

    private void CreateNewLineSegment()
    {
        var newLineSegmentGameObject = new GameObject("Line Segment " + _lineSegments.Count);
        var newLineRenderer = newLineSegmentGameObject.AddComponent<LineRenderer>();
        var newEdgeCollider2D = newLineSegmentGameObject.AddComponent<EdgeCollider2D>();

        _segmentTailPoints.Add(new List<Vector2>());

        newLineRenderer.receiveShadows = false;
        newLineRenderer.shadowCastingMode = ShadowCastingMode.Off;

        const float alpha = 1.0f;
        var gradient = new Gradient();
        gradient.SetKeys(
            new[] { new GradientColorKey(TailColorStart, 0.0f), new GradientColorKey(TailColorEnd, 1.0f) },
            new[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        newLineRenderer.material = TailMaterial;
        newLineRenderer.colorGradient = gradient;

        newLineRenderer.useWorldSpace = true;
        newLineRenderer.widthCurve = AnimationCurve.Linear(0f, .08f, 1f, .08f);
        newLineRenderer.startWidth = .08f;
        newLineRenderer.endWidth = .08f;

        newLineRenderer.numCornerVertices = 5;
        newLineRenderer.numCapVertices = 5;

        newEdgeCollider2D.enabled = false;

        newLineSegmentGameObject.transform.parent = transform;
        newLineSegmentGameObject.tag = "instantDeath";

        _lineSegments.Add(newLineSegmentGameObject);
    }

    private void AddLastPointToEdgeCollider()
    {
        _lineSegments.Last().GetComponent<EdgeCollider2D>().points = _segmentTailPoints.Last().ToArray<Vector2>();
    }
}

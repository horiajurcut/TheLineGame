using UnityEngine;

public class Head : MonoBehaviour {

    public float Speed = 3f;
    public float RotationSpeed = 200f;
    public bool ReversedControls = false;

    private float _horizontalAxis = 0f;
    private float _myDirection = 0f;

    private void Update()
    {
        _horizontalAxis = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        _myDirection = -_horizontalAxis * (ReversedControls ? -1 : 1);

        transform.Translate(Vector2.up * Speed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate(Vector3.forward * _myDirection * RotationSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("instantDeath")) return;

        Debug.Log(collision.name);

        Speed = 0f;
        RotationSpeed = 0f;

        // Replace with singleton
        GameObject.FindObjectOfType<GameManager>().EndGame();
    }
}

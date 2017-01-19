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

        Speed = 0f;
        RotationSpeed = 0f;

        GameManager.Instance.EndGame();
    }

    public void OnPickUp(PowerUp.PowerUpType powerUpType)
    {
        Debug.Log("Picked up " + powerUpType);
    }
}

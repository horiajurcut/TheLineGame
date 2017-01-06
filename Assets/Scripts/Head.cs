using UnityEngine;

public class Head : MonoBehaviour {

    public float speed = 3f;
    public float rotationSpeed = 200f;
    public bool reversedControls = false;

    float horizontalAxis = 0f;
    float myDirection = 0f;

    private void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        myDirection = -horizontalAxis * (reversedControls ? -1 : 1);

        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate(Vector3.forward * myDirection * rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (collision.tag == "instantDeath")
        {
            speed = 0f;
            rotationSpeed = 0f;

            // Replace with singleton
            GameObject.FindObjectOfType<GameManager>().EndGame();
        }
    }
}

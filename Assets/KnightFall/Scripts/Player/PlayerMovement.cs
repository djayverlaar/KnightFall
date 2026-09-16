using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");

        // Check if the player is moving left or right and flip the sprite accordingly
        if (horizontalinput > 0 && transform.localScale.x <0 || horizontalinput < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        anim.SetFloat("horizontal", Mathf.Abs(horizontalinput));
        anim.SetFloat("vertical", Mathf.Abs(verticalinput));

        rb.linearVelocity = new Vector2(horizontalinput, verticalinput) * moveSpeed;
      
    }
    // Flips the player sprite horizontally
    void Flip()
    {
       facingDirection *= -1;
       transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}

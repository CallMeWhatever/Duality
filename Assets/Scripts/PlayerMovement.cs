using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;



    private void Awake(){
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update(){
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed ,body.velocity.y);

        if(Input.GetKey(KeyCode.Space) && isGrounded()  && !touchesWall()){
            Jump();
        }
    }

    private void Jump(){
        body.velocity = new Vector2(body.velocity.x, speed);
    }

    private void OnCollisionEnter2D(Collision2D Collision){
    }

    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool touchesWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}

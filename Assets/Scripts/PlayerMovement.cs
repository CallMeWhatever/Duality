using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private bool flipCooldwonActive = false;
    private float cooldown;
    private float worldUp = 1;



    private void Awake(){
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update(){
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed ,body.velocity.y);

        if(Input.GetKey(KeyCode.Space) && isGrounded()  && !touchesWall()){
            Jump();
        }
        if(Input.GetKey(KeyCode.X) && !flipCooldwonActive){
            FlipWorld();
            flipCooldwonActive = true;
            cooldown = 1;
        }

        if(cooldown <= 0){flipCooldwonActive = false;}
        
        cooldown -= Time.deltaTime;
    }

    private void Jump(){
        body.velocity = new Vector2(body.velocity.x, speed * worldUp);
    }

    private void OnCollisionEnter2D(Collision2D Collision){
    }

    private bool isGrounded(){
        RaycastHit2D raycastHit = (worldUp > 0) ? Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer)
        : Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.up, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool touchesWall(){
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, groundLayer);
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, groundLayer);
        return (raycastHitRight.collider != null) || (raycastHitLeft.collider != null) ;
    }


    private void FlipWorld(){
        body.transform.position = new Vector2(body.position.x, -body.position.y);
        body.gravityScale *= -1;
        worldUp *= -1;
    }
}

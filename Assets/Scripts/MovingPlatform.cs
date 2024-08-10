using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform plaform;
    public Transform startPoint;
    public Transform endPoint; 
    public Vector2 mvDirection; 
    PlayerMovement player; 
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D boxCollider;
    [SerializeField] public float platformSpeed;
    [SerializeField] int direction;
   
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); 
    }
    void Awake(){
        boxCollider = plaform.GetComponent<BoxCollider2D>();
    }
    void Update(){
        Vector2 target = currentMovementTarget();
        Vector2 mvDirection = new Vector2(target.x - plaform.position.x, target.y - plaform.position.y);
        float distance = mvDirection.magnitude;
        mvDirection.Normalize();
        plaform.position = new Vector2(plaform.position.x + mvDirection.x*platformSpeed*0.001f , plaform.position.y + mvDirection.y*platformSpeed*0.001f);

        touchesPlayer();

        if (distance <= 0.01f){
            direction = direction * (-1);
        } 
    }

    Vector2 currentMovementTarget(){
        if (direction > 0){
            return(new Vector2(startPoint.position.x, startPoint.position.y));
        }
        else{
            return(new Vector2(endPoint.position.x, endPoint.position.y));
        }
    }
    private void OnDrawGizmos(){
        if(plaform!=null && startPoint != null && endPoint != null){
            Gizmos.DrawLine(plaform.transform.position, startPoint.position);
            Gizmos.DrawLine(plaform.transform.position, endPoint.position);
        }
    }

    private void touchesPlayer(){
        
        RaycastHit2D raycastHitUp = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.up, 0.1f, groundLayer);
         if (raycastHitUp.collider != null)
            {
                Debug.Log("called touches player");
                if (raycastHitUp.collider.gameObject.CompareTag("Player")){
                    Debug.Log("called move player");
                    player.MovePlayer(mvDirection.x*platformSpeed*0.001f, mvDirection.y*platformSpeed*0.001f);
                }
            }
    }
}

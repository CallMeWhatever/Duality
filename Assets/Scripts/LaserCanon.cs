using UnityEngine;

public class LaserCanon : MonoBehaviour
{
    public Transform plaform;
    public Transform startPoint;
    public Transform endPoint; 
    public Vector2 mvDirection; 
    PlayerMovement player; 
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D boxCollider;
    [SerializeField] public float platformSpeed;
   
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); 
    }
    void Awake(){
        boxCollider = plaform.GetComponent<BoxCollider2D>();
    }
    void FixedUpdate(){
        Vector2 target = new Vector2(endPoint.position.x,endPoint.position.y);
        Vector2 mvDirection = new Vector2(target.x - plaform.position.x, target.y - plaform.position.y);
        float distance = mvDirection.magnitude;
        mvDirection.Normalize();
        plaform.position = new Vector2(plaform.position.x + mvDirection.x*platformSpeed*0.001f , plaform.position.y + mvDirection.y*platformSpeed*0.001f);

    
        if (distance <= 0.1f){
            plaform.position = startPoint.position;
        } 
    }

   
    private void OnDrawGizmos(){
        if(plaform!=null && startPoint != null && endPoint != null){
            Gizmos.DrawLine(plaform.transform.position, startPoint.position);
            Gizmos.DrawLine(plaform.transform.position, endPoint.position);
        }
    }
}

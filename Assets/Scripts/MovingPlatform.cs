using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform plaform;
    public Transform startPoint;
    public Transform endPoint;  
    [SerializeField] private float speed;
    [SerializeField] int direction;
   
    void Update(){
        Vector2 target = currentMovementTarget();
        Vector2 mvDirection = new Vector2(target.x - plaform.position.x, target.y - plaform.position.y);
        float distance = mvDirection.magnitude;
        mvDirection.Normalize();
        plaform.position = new Vector2(plaform.position.x + mvDirection.x*speed*0.001f , plaform.position.y + mvDirection.y*speed*0.001f);


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
}

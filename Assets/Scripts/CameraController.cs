using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosx;
    private float currentPosy;
    private Vector3 velocity = Vector3.zero;

    //folowo player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update(){
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosx,transform.position.y, transform.position.z), 
        //    ref velocity, speed * Time.deltaTime);
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
}

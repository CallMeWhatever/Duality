using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos,startpos_y;
    public GameObject cam;
    [SerializeField] public float parallaxEffect;
    
    void Start(){
        startpos = transform.position.x;
        startpos_y = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate(){
        float temp = (cam.transform.position.x * (1-parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, startpos_y-1.0f, transform.position.z);
        //if (temp > startpos + length) startpos += length;
        //else if (temp < startpos - length) startpos -= length;
    }
}

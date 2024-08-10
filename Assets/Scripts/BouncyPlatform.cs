using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    [SerializeField] private float bouncyness;

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player"){
            collision.GetComponent<PlayerMovement>().bounce(bouncyness);
        }
    }
}

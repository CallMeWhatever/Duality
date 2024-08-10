using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool doDamageRecoil;

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player"){
            collision.GetComponent<Health>().TakeDamage(damage);
            if (doDamageRecoil){
                collision.GetComponent<PlayerMovement>().Damage_Recoil(damage);
            }
            
        }
    }
}

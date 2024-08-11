using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private Animator anim;

    private void Awake(){
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage){
        if(GetComponent<PlayerMovement>().worldUp < 0 && _damage > 0){
            currentHealth = 0;
        }
        else{
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        }

        if(currentHealth <= 0){
            //GetComponent<PlayerMovement>().enable = false;
            anim.SetTrigger("Death");
            Respawn();
        }
        else{
            anim.SetTrigger("hurt");
        }
    
    }
    public void AddHealth(float _health){
        currentHealth = Mathf.Clamp(currentHealth + _health, 0, startingHealth);
    }

    public void Respawn(){
        AddHealth(startingHealth);
        //anim.ResetTrigger()
        anim.Play("Idle");
        GetComponent<PlayerMovement>().enable = true;
        GetComponent<PlayerMovement>().Respawn();
    }
}

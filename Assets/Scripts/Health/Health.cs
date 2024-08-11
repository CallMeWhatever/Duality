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
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
    
        if(currentHealth <= 0){
            //GetComponent<PlayerMovement>().enable = false;
            Respawn();
        }
    
    }
    public void AddHealth(float _health){
        currentHealth = Mathf.Clamp(currentHealth + _health, 0, startingHealth);
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.E)){
            TakeDamage(0.5f);
        }
    }

    public void Respawn(){
        AddHealth(startingHealth);
        //anim.ResetTrigger()
        anim.Play("Idle");
        GetComponent<PlayerMovement>().enable = true;
        GetComponent<PlayerMovement>().Respawn();
    }
}

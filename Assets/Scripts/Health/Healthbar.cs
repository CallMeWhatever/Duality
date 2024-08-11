using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image downHealthBar;
    private float totalHealth;
    
    private void Start(){
        totalHealthBar.fillAmount = playerHealth.currentHealth /10;
        totalHealth = playerHealth.currentHealth;
    }
    private void Update(){
        
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().worldUp < 0){
            Debug.Log(totalHealth);
            currentHealthBar.fillAmount = 0;
            totalHealthBar.fillAmount = 0;
            downHealthBar.fillAmount = playerHealth.currentHealth /10;
            
        }
        else{
            currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
            totalHealthBar.fillAmount = totalHealth /10;
            downHealthBar.fillAmount = 0;
        }
    }

}

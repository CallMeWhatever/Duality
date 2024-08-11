using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject gameOverScreen;
   [SerializeField] private GameObject LostScreen;
   [SerializeField] private GameObject gameUI;
   [SerializeField] private GameObject gameUI_2;

    private void Awake(){
        gameOverScreen.SetActive(false);
        LostScreen.SetActive(false);
        gameUI.SetActive(true);
        gameUI_2.SetActive(true);
    }

   public void GameOver(){
        gameOverScreen.SetActive(true);
        gameUI.SetActive(false);
        gameUI_2.SetActive(false);
   }
   public void Lost(){
        LostScreen.SetActive(true);
        gameUI.SetActive(false);
        gameUI_2.SetActive(false);
   }
   public void NewGame(){
        gameOverScreen.SetActive(false);
        LostScreen.SetActive(false);
        gameUI.SetActive(true);
        gameUI_2.SetActive(true);
   }
}

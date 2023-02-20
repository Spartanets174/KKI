using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject warningText;
    public PlayerManager1 playerManager;
    public void Exit()
    {
        Debug.Log("out");
        Application.Quit();
    }
    public void toGame()
    {
        if (playerManager.deckUserCharCards.Count<5||playerManager.deckUserSupportCards.Count<7)
        {
            warningText.SetActive(true);
            Invoke("SetActive",5);
        }
        else
        {
            SceneManager.LoadScene("game");
        }
        
    }
    public void toMenu()
    {
        SceneManager.LoadScene("menu");
    }
    private void SetActive()
    {
        warningText.SetActive(false);
    }
}

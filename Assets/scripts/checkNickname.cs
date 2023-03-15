using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class checkNickname : MonoBehaviour
{
    public PlayerData playerData;
    [SerializeField] private InputField Nick;
    [SerializeField] private Text warningText;
    [SerializeField] DbManager DB;
    private void Awake()
    {
        if (playerData.Name!="")
        {
            SceneManager.LoadScene("menu");
        }
    }
    public void checkNick()
    {
        Nick.text = Nick.text.Trim();
        if (Nick.text == "")
        {
            warningText.text = "Вы ничего не ввели!";
            StartCoroutine(onWarningText());
            StopCoroutine(onWarningText());
        }
        else
        {
            if (Nick.text.Length<=15)
            {
                if (Nick.text.Length >=4)
                {
                    bool hasName=false;
                    List<string> nickList = DB.SelectFromPlayers();
                    for (int i = 0; i < nickList.Count; i++)
                    {
                        if (nickList[i]==Nick.text)
                        {
                            hasName = true;
                        }
                    }
                    if (nickList.Count==0||!hasName)
                    {
                        DB.InsertToPlayers(Nick.text, 1000);
                        playerData.Name = Nick.text;
                        SceneManager.LoadScene("menu");
                    }
                    else
                    {
                        warningText.text = "Данное имя уже существует!";
                        StartCoroutine(onWarningText());
                        StopCoroutine(onWarningText());
                    }
                }
                else
                {
                    warningText.text = "Имя слишком маленькое (минимум 4 символа)!";
                    StartCoroutine(onWarningText());
                    StopCoroutine(onWarningText());
                }               
            }
            else
            {
                warningText.text = "Имя слишком большое (минимум 15 символов)!";
                StartCoroutine(onWarningText());
                StopCoroutine(onWarningText());
            }
        }
    }
    IEnumerator onWarningText()
    {
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        warningText.gameObject.SetActive(false);
    }
}

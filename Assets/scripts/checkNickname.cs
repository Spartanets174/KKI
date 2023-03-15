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
            warningText.text = "�� ������ �� �����!";
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
                        warningText.text = "������ ��� ��� ����������!";
                        StartCoroutine(onWarningText());
                        StopCoroutine(onWarningText());
                    }
                }
                else
                {
                    warningText.text = "��� ������� ��������� (������� 4 �������)!";
                    StartCoroutine(onWarningText());
                    StopCoroutine(onWarningText());
                }               
            }
            else
            {
                warningText.text = "��� ������� ������� (������� 15 ��������)!";
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

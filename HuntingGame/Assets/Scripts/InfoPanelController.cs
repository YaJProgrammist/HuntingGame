using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelController : MonoBehaviour
{
    [SerializeField] private Hunter hunter;

    [SerializeField] private Text bulletsLeft;
    [SerializeField] private GameObject youDiedMessage;

    [SerializeField] private float timeToShowMessage = 1f;

    private void Awake()
    {
        bulletsLeft.text = "";

        hunter.OnBulletsQuantityChanged += ShowCurrentBulletValue;
        hunter.OnPlayerDied += ShowYouDiedMessage;

        youDiedMessage.SetActive(false);

    }

    private void ShowYouDiedMessage()
    {
        if (!youDiedMessage.activeSelf)
            StartCoroutine(ShowYouDiedMessageDuringTime());
    }

    private IEnumerator ShowYouDiedMessageDuringTime()
    {
        youDiedMessage.SetActive(true);
        yield return new WaitForSeconds(timeToShowMessage);
        youDiedMessage.SetActive(false);

    }

    private void ShowCurrentBulletValue(int newValue)
    {
        bulletsLeft.text = newValue.ToString();
    }
}

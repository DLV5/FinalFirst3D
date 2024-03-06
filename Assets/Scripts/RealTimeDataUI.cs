using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RealTimeDataUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _dataText;

    private DateTime currentTime = DateTime.Now;

    private void Start()
    {
        StartCoroutine(UpdateTime());

        _dataText.text = $"{currentTime.Day}.{currentTime.Month}.{currentTime.Year}"; ;
    }

    IEnumerator UpdateTime()
    {
        while(true)
        {
            _timeText.text = $"{currentTime.Hour}:{currentTime.Minute}";
            yield return new WaitForSeconds(1f);
        }
    }
}

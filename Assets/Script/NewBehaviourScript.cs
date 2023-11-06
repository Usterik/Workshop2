using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class NewBehaviourScript : MonoBehaviour
{
    public AudioClip goodSpeak;
    public AudioClip normalSpeak;
    public AudioClip badSpeak;
    private AudioSource selectAudio;
    private Dictionary<string, float> dataSet = new Dictionary<string, float>();
    private bool statusStart = false;
    private int i = 1;

    private float capsCount = 0;
    private float defaultEquipmentPrice = 500f;
    private float bestEquipmentPrice = 1500f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoogleSheets());
    }

    // Update is called once per frame
    void Update()
    {
        if (dataSet.Count == 0)
            return;

        if (statusStart == false & i != dataSet.Count)
            capsCount += dataSet["Mon_" + i.ToString()];

        if (capsCount >= bestEquipmentPrice & statusStart == false & i != dataSet.Count)
        {
            Debug.Log(dataSet["Mon_" + i.ToString()] + " " + capsCount);
            StartCoroutine(PlaySelectAudioGood());
            capsCount -= bestEquipmentPrice;
        }

        if (capsCount >= defaultEquipmentPrice & capsCount < bestEquipmentPrice & statusStart == false & i != dataSet.Count)
        {

            Debug.Log(dataSet["Mon_" + i.ToString()] + " " + capsCount);
            StartCoroutine(PlaySelectAudioNormal());
            capsCount -= defaultEquipmentPrice;
        }

        if (capsCount < 500 & statusStart == false & i != dataSet.Count)
        {
            Debug.Log(dataSet["Mon_" + i.ToString()] + " " + capsCount);
            StartCoroutine(PlaySelectAudioBad());
        }
    }

    IEnumerator GoogleSheets()
    {
        UnityWebRequest curentResp = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1fVoFcLqFc4vLt7U1LIjmfcmPifRoVaZT4FWdrR-lh9I/values/%D0%9B%D0%B8%D1%81%D1%821?key=AIzaSyDTxntYTtwKvFRWyY5sTOI4rVKrkByi9Bk");
        yield return curentResp.SendWebRequest();
        string rawResp = curentResp.downloadHandler.text;
        var rawJson = JSON.Parse(rawResp);
        foreach (var itemRawJson in rawJson["values"])
        {
            var parseJson = JSON.Parse(itemRawJson.ToString());
            var selectRow = parseJson[0].AsStringList;
            dataSet.Add(("Mon_" + selectRow[0]), float.Parse(selectRow[1]));
        }
    }

    IEnumerator PlaySelectAudioGood()
    {
        statusStart = true;
        selectAudio = GetComponent<AudioSource>();
        selectAudio.clip = goodSpeak;
        selectAudio.Play();
        yield return new WaitForSeconds(3);
        statusStart = false;
        i++;
    }
    IEnumerator PlaySelectAudioNormal()
    {
        statusStart = true;
        selectAudio = GetComponent<AudioSource>();
        selectAudio.clip = normalSpeak;
        selectAudio.Play();
        yield return new WaitForSeconds(3);
        statusStart = false;
        i++;
    }
    IEnumerator PlaySelectAudioBad()
    {
        statusStart = true;
        selectAudio = GetComponent<AudioSource>();
        selectAudio.clip = badSpeak;
        selectAudio.Play();
        yield return new WaitForSeconds(4);
        statusStart = false;
        i++;
    }
}

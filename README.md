# АНАЛИЗ ДАННЫХ И ИСКУССТВЕННЫЙ ИНТЕЛЛЕКТ [in GameDev]
Отчет по лабораторной работе #2 выполнил(а):
- Устинов Эрик Константинович
- ФО-220007
Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.
- ✨Magic ✨

## Цель работы
Научиться передавать в Unity данные из Google Sheets с помощью Python.

## Задание 1
### Выберите одну из компьютерных игр, приведите скриншот её геймплея и краткое описание концепта игры. Выберите одну из игровых переменных в игре (ресурсы, внутри игровая валюта, здоровье персонажей и т.д.), опишите её роль в игре, условия изменения / появления и диапазон допустимых значений. Постройте схему экономической модели в игре и укажите место выбранного ресурса в ней.

В Fallout Shelter одной из ключевых игровых переменных является ресурс "крышки". Роль этого ресурса в игре заключается в том, чтобы обеспечить возможность игроку управлять и развивать своим убежищем.

Крышки выполняют несколько важных функций:
1. Они используются для строительства новых помещений в убежище, таких как жилые кварталы, энергетические блоки, столовые и другие.
2. Крышки необходимы для найма новых жителей в убежище, которые будут выполнять разные задачи, такие как добыча ресурсов, исследования или защита убежища от врагов. 
Кроме того, крышки можно потратить на улучшение навыков жителей, приобретение нового снаряжения или на различные приобретаемые предметы.

Крышки можно заработать различными способами. Они поступают в убежище с новыми жителями, от выполнения заданий и от сбора их в пустоши. Кроме того, игрок может получать крышки в виде награды за выполнение достижений и других игровых событий.

Диапазон допустимых значений для крышек в Fallout Shelter зависит от лимитов, установленных в игре. Обычно у игрока есть возможность накопить большое количество крышек, но важно умело расходовать этот ресурс, чтобы обеспечить потребности убежища.

Схема экономической модели в игре Fallout Shelter включает в себя взаимосвязь между различными ресурсами, потребностями жителей и развитием убежища. Крышки играют роль основной валюты и будут преобразовываться в различные ресурсы и услуги, необходимые для функционирования и процветания убежища.

## Задание 2
### С помощью скрипта на языке Python заполните google-таблицу данными, описывающими выбранную игровую переменную в выбранной игре (в качестве таких переменных может выступать игровая валюта, ресурсы, здоровье и т.д.). Средствами google-sheets визуализируйте данные в google-таблице (постройте график, диаграмму и пр.) для наглядного представления выбранной игровой величины.

В Fallout Shelter можно отправлять поселенцев в пустошь для поиска полезный материалов и крышек. Однако, для этого нужно потратить крышки на снаряжение для поселенца.
 
Программа в Python будет рандомно генерировать количество крышек, найденных в пустоши. Для упрощения программы, в пустоши можно будет найти только крышки.

```py

import gspread
import numpy as np
gc = gspread.service_account(filename='workshop2-404304-346a1788aeca.json')
sh = gc.open("Workshop2")
caps = np.random.randint(100, 2000, 11)
mon = list(range(1,11))
i = 0
while i <= len(mon):
    i += 1
    if i == 0:
        continue
    else:
        sh.sheet1.update(('A' + str(i)), str(i))
        sh.sheet1.update(('B' + str(i)), str(caps[i-1]))
        print(str(i), str(caps[i-1]))

```

## Задание 3
### Настройте на сцене Unity воспроизведение звуковых файлов, описывающих динамику изменения выбранной переменной. Например, если выбрано здоровье главного персонажа вы можете выводить сообщения, связанные с его состоянием.

Пусть для отправки поселенца в пустошь необходимо 500 крышек.
Для отправления поселенца с лучшим снаряжением - 1500 крышек.

Программа в Unity будет рассчитывать общее количество крышек, включая траты на отправку поселнца в пустошь. И от этого включать звуковой сигнал:
- Если не хватает крышек для отправки поселенца - badSpeak
- Если возможно отправить только поселенца со стандартным снаряжением - normalSpeak
- Если возможно отправить поселенца с лучшим снаряжением - goodSpeak

```cs

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

```

## Выводы

Я описал экономическую модель своей любимой игры, а также реализовал её. Для этого я использовал Google Cloud Console, Jupyter Notebook и Unity.

| Plugin | README |
| ------ | ------ |
| Dropbox | [plugins/dropbox/README.md][PlDb] |
| GitHub | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
| Medium | [plugins/medium/README.md][PlMe] |
| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |

## Powered by

**BigDigital Team: Denisov | Fadeev | Panov**
using UnityEngine;
using UnityEngine.EventSystems;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;
using System;

public class LabelMe : MonoBehaviour
{
    [SerializeField]
    Text uiText;
    bool gazedAt = false;
    const string LANG_CODE = "en-US";

    
    void Start()
    {
        SetupLanguage(LANG_CODE);
        CheckMicPermission();

        EventTrigger trigger = GetComponent<EventTrigger>();

        //ReticlePointer enters trigger
        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { ReticlePointerEnters((PointerEventData)data); });

        //ReticlePointer exits trigger
        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { ReticlePointerExits((PointerEventData)data); });

        trigger.triggers.Add(enterEntry);
        trigger.triggers.Add(exitEntry);
    }

    void Update()
    {
        if (gazedAt)
        {
            SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
            StartListening();
        }
    }

    void OnFinalSpeechResult(string result)
    {
        //separates the first two words
        string[] words = result.Split(' ');
        string keywords = words.GetValue(0).ToString()+" "+ words.GetValue(1).ToString();
        Array.Clear(words, 0, 2);
        string label = " "; 

        foreach (string i in words)
        {
            label += i+" ";
        }

        if (keywords.Equals("that is"))
        {
            uiText.text = label;
        }
    }

    private void ReticlePointerEnters(PointerEventData data)
    {
        gazedAt = true;
    }

    private void ReticlePointerExits(PointerEventData data)
    {
        gazedAt = false;
    }

    public void StartListening()
    {
        SpeechToText.instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    void CheckMicPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }

    void SetupLanguage(string languageCode)
    {
        SpeechToText.instance.Setting(languageCode);
    }
}

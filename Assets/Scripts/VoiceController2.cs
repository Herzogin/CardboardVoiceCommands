using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;

public class VoiceController2 : MonoBehaviour
{
    const string LANG_CODE = "en-US";
    [SerializeField]
    Text uiText;
    private void Start()
    {
        Setup(LANG_CODE);


        SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult;
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        CheckPermission();
    }

    void CheckPermission()
    {
       if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
          {
            Permission.RequestUserPermission(Permission.Microphone);
          }
    }

    public void StartListening()
    {
        SpeechToText.instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    void OnFinalSpeechResult(string result)
    {
        uiText.text = result;
    }

    void OnPartialSpeechResult(string result)
    {
        uiText.text = result;
    }

    void Setup(string code)
    {
        SpeechToText.instance.Setting(code);
    }
}

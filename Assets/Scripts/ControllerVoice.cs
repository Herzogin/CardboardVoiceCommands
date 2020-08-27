using UnityEngine;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;

public class ControllerVoice : MonoBehaviour
{
    const string LANG_CODE = "en-US";
    [SerializeField]
    Text uiText;
    [SerializeField]
    GameObject obj;
    private void Start()
    {
        Setup(LANG_CODE);
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
        if (result.Equals("down"))
        {
            uiText.text = "runter";
            obj.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (Equals(result, "small"))
        {
            uiText.text = "kleiner";
            obj.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Setup(string code)
    {
        SpeechToText.instance.Setting(code);
    }
}

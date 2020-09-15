using TextSpeech;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    //[SerializeField]
    //Text UItext;
    const string LANG_CODE = "en-US";

    void Start()
    {


        SetupLanguage(LANG_CODE);
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        CheckMicPermission();
    }

    void Update()
    {

        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        StartListening();

        
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
       // UItext.text = result;
        if (result.Equals("home"))
        {
            SceneManager.LoadScene("Home");
        }      
        else if (result.Equals("Museum"))
        {
            SceneManager.LoadScene("Exhibition");
        }
        else if (result.Equals("blue"))
        {
            SceneManager.LoadScene("FloatingSpheres");
        }
    }

    void CheckMicPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }

    void SetupLanguage(string code)
    {
        SpeechToText.instance.Setting(code);
    }
}

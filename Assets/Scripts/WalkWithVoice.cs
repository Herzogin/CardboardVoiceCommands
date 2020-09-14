using TextSpeech;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class WalkWithVoice : MonoBehaviour
{
    public Transform cameraTransform;
    public float toggleAngle = 10.0F;
    public float speed = 3.0F;
    public bool moveForward = false;
    const string LANG_CODE = "en-US";
    private CharacterController characterController;

    void Start()
    {


        SetupLanguage(LANG_CODE);
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        CheckMicPermission();
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        
    }

    void Update()
    {
        
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        StartListening();
        
        if (moveForward)
        {
            // Find the forward direction
            Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
            characterController.SimpleMove(forward * speed * toggleAngle / 5);
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
        if (result.Equals("stop"))
        {
            moveForward = false;
        }
        else if (result.Equals("go"))
        {
            moveForward = true;
        }
        else if (result.Equals("fast"))
        {
            speed += 1.0f;
        }
        else if (result.Equals("slow"))
        {
            speed -= 1.0f;
        }
        else if (result.Equals("blue"))
        {
            SceneManager.LoadScene("Museum");
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

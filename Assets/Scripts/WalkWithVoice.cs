using TextSpeech;
using UnityEngine;
using UnityEngine.Android;
using System;

public class WalkWithVoice : MonoBehaviour
{
    public Transform cameraTransform;
    public float speed = 3.0F;
    public bool moveForward = true;
    const string LANG_CODE = "en-US";
    private CharacterController characterController;
    public GameObject blueTarget;
    public GameObject redTarget;
    Vector3 target;
    

    void Start()
    {
         SetupLanguage(LANG_CODE);
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        CheckMicPermission();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        StartListening();
        
        if (moveForward)
        {
            characterController.SimpleMove(target * speed);

            Vector3 playerPosition = transform.position;
            if (Math.Abs(playerPosition.x) > Math.Abs(target.x)-10)
            {
                moveForward = false;
            }
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
        if (result.Equals("blue"))
        {
            target = blueTarget.transform.position;
            moveForward = true;
        }
        else if (result.Equals("red"))
        {
            target = redTarget.transform.position;
            moveForward = true;
            
        }
        else if (result.Equals("stop"))
        {
            moveForward = false;
        }
        else if (result.Equals("fast"))
        {
            speed += 1.0f;
        }
        else if (result.Equals("slow"))
        {
            speed -= 1.0f;
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

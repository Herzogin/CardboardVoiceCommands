using UnityEngine;
using UnityEngine.EventSystems;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;

[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(Rigidbody))]


public class WalkToObject : MonoBehaviour
{
    bool gazedAt = false;
    const string LANG_CODE = "en-US";

    public float speed = 2.0F;

    public CharacterController characterController;
    private Transform cameraTransform;

    [SerializeField]
    GameObject targetGameObject;

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
        if (result.Equals("go"))
        {
            characterController = GetComponent<CharacterController>();
            cameraTransform = Camera.main.transform;
            //Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
            Vector3 target = targetGameObject.transform.localPosition;
            characterController.SimpleMove(target * speed);
        }
        else if (Equals(result, "You go there"))
        {
            //
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

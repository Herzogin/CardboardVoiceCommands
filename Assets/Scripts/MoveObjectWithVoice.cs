using UnityEngine;
using UnityEngine.EventSystems;
using TextSpeech;
using UnityEngine.Android;

[RequireComponent(typeof(EventTrigger))]
//[RequireComponent(typeof(Rigidbody))]
public class MoveObjectWithVoice : MonoBehaviour
{
    bool gazedAt = false;
    //public float speed = 1.0F;
    const string LANG_CODE = "en-US";
    public GameObject movingGameObject;
    public GameObject blueTarget;
    public GameObject redTarget;
    Vector3 target = new Vector3(0, 9, 30);
    bool moving = false;

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
        
        if (moving)
        {
            movingGameObject.transform.position = Vector3.MoveTowards(movingGameObject.transform.position, target, Time.deltaTime*4);
        }
    }

    void OnFinalSpeechResult(string result)
    {
        if (result.Equals("right"))
        {
            target = blueTarget.transform.position;
            moving = true;
        }
        else if (result.Equals("left"))
        {
            target = redTarget.transform.position;
            moving = true;
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

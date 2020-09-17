using UnityEngine;
using UnityEngine.EventSystems;
using TextSpeech;
using UnityEngine.Android;

[RequireComponent(typeof(EventTrigger))]
public class GoThere : MonoBehaviour
{
    bool gazedAt = false;
    public int speed = 4;
    const string LANG_CODE = "en-US";
    public GameObject movingGameObject;
    public GameObject targetGameObject;
    Vector3 target;
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
            movingGameObject.transform.position = Vector3.MoveTowards(movingGameObject.transform.position, target, Time.deltaTime * speed);
        }
    }

    void OnFinalSpeechResult(string result)
    {
        if (result.Equals("go"))
        {
            target = targetGameObject.transform.position;
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

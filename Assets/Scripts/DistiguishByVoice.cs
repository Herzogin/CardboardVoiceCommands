using UnityEngine;
using UnityEngine.EventSystems;
using TextSpeech;
using UnityEngine.Android;
using System;
using System.Linq;
using UnityEngine.UI;

public class DistiguishByVoice : MonoBehaviour
{
    bool gazedAt = false;
    const string LANG_CODE = "en-US";
    //public GameObject statue;
    public GameObject statueGroup;
    public GameObject player;
    int farthest;
    Transform farthestStatue;
    Transform nearestStatue;
    [SerializeField]
    Text uiText;

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

        getStatuePositions();
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
    {   uiText.text = result;
        if (result.Equals("blue"))
        {
            farthestStatue.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (result.Equals("yellow"))
        {
             nearestStatue.transform.GetChild(1).gameObject.SetActive(false);
        }
        //else if (result.Equals("delete right"))
        //{
        //    //
        //}
        //else if (result.Equals("delete left"))
        //{
        //    //
        //}
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

    private void getStatuePositions()
    {
        Transform statue0 = statueGroup.transform.GetChild(0);
        Transform statue1 = statueGroup.transform.GetChild(1);
        Transform statue2 = statueGroup.transform.GetChild(2);
        Transform statue3 = statueGroup.transform.GetChild(3);
        Transform statue4 = statueGroup.transform.GetChild(4);



        float dist0 = Vector3.Distance(statue0.position, player.transform.position);
        float dist1 = Vector3.Distance(statue1.position, player.transform.position);
        float dist2 = Vector3.Distance(statue2.position, player.transform.position);
        float dist3 = Vector3.Distance(statue3.position, player.transform.position);
        float dist4 = Vector3.Distance(statue4.position, player.transform.position);

        float[] array = new float[] { dist0, dist1, dist2, dist3, dist4 };


        // Finding maximum
        float m = array.Max();
        farthest = Array.IndexOf(array, m);
        farthestStatue = statueGroup.transform.GetChild(farthest);

        //Finding minimum
        float n = array.Min();
        int nearest = Array.IndexOf(array, n);
        nearestStatue = statueGroup.transform.GetChild(nearest);
    }

}

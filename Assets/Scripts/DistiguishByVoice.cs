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
    public GameObject statueGroup;
    public GameObject player;
    Transform farthestStatue;
    Transform nearestStatue;
    Transform rightStatue;
    Transform leftStatue;
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
        if (result.Equals("far"))
        {
            farthestStatue.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (result.Equals("near"))
        {
             nearestStatue.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (result.Equals("right"))
        {
            rightStatue.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (result.Equals("left"))
        {
            leftStatue.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (result.Equals("delete"))
        {
            statueGroup.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            statueGroup.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
            statueGroup.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            statueGroup.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            statueGroup.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(false);
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

    private void getStatuePositions()
    {
        //gets the separate statues out off the StatueGroup
        Transform statue0 = statueGroup.transform.GetChild(0);
        Transform statue1 = statueGroup.transform.GetChild(1);
        Transform statue2 = statueGroup.transform.GetChild(2);
        Transform statue3 = statueGroup.transform.GetChild(3);
        Transform statue4 = statueGroup.transform.GetChild(4);

        #region get nearest and farthest statue

        //distance to player
        float dist0 = Vector3.Distance(statue0.position, player.transform.position);
        float dist1 = Vector3.Distance(statue1.position, player.transform.position);
        float dist2 = Vector3.Distance(statue2.position, player.transform.position);
        float dist3 = Vector3.Distance(statue3.position, player.transform.position);
        float dist4 = Vector3.Distance(statue4.position, player.transform.position);

        float[] distanceArray = new float[] { dist0, dist1, dist2, dist3, dist4 };

        // Finding maximum
        float m = distanceArray.Max();
        int farthest = Array.IndexOf(distanceArray, m);
        farthestStatue = statueGroup.transform.GetChild(farthest);

        //Finding minimum
        float n = distanceArray.Min();
        int nearest = Array.IndexOf(distanceArray, n);
        nearestStatue = statueGroup.transform.GetChild(nearest);
        #endregion

        #region get statue on the far left or right
        
        //   right < point on X-axis > left

        //get value of x per statue
        float getX0 = statue0.transform.position.x;
        float getX1 = statue1.transform.position.x;
        float getX2 = statue2.transform.position.x;
        float getX3 = statue3.transform.position.x;
        float getX4 = statue4.transform.position.x;

        float[] xAxisArray = new float[] { getX0, getX1, getX2, getX3, getX4 };

        // Finding maximum
        float p = xAxisArray.Max();
        int highestZ = Array.IndexOf(xAxisArray, p);
        rightStatue = statueGroup.transform.GetChild(highestZ);

        //Finding minimum
        float q = xAxisArray.Min();
        int lowestZ = Array.IndexOf(xAxisArray, q);
        leftStatue = statueGroup.transform.GetChild(lowestZ);
        #endregion
    }

}

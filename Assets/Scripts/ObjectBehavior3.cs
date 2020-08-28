using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;

[RequireComponent(typeof(EventTrigger))]
public class ObjectBehavior3 : MonoBehaviour
{
	//public UnityEvent GazeEvent;
	bool m_targeted = false;
	const string LANG_CODE = "en-US";
	[SerializeField]
	Text uiText;
	[SerializeField]
	GameObject obj;

	// Use this for initialization
	void Start()
	{
		// Add triggers.
		EventTrigger trigger = GetComponent<EventTrigger>();

		// EventTrigger entry for PointerEnter.
		EventTrigger.Entry enterEntry = new EventTrigger.Entry();
		enterEntry.eventID = EventTriggerType.PointerEnter;
		enterEntry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });

		// EventTrigger entry for PointerExit.
		EventTrigger.Entry exitEntry = new EventTrigger.Entry();
		exitEntry.eventID = EventTriggerType.PointerExit;
		exitEntry.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });

		// Add the entries to the Event Trigger.
		trigger.triggers.Add(enterEntry);
		trigger.triggers.Add(exitEntry);


		Setup(LANG_CODE);
		
		CheckPermission();

	}

	// Update is called once per frame
	void Update()
	{
		if (m_targeted)
		{
			SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
			StartListening();
			
		}
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
	private void OnPointerEnter(PointerEventData data)
	{
		m_targeted = true;
	}

	private void OnPointerExit(PointerEventData data)
	{
		m_targeted = false;
	}

	public void StartListening()
	{
		SpeechToText.instance.StartRecording();
	}

	public void StopListening()
	{
		SpeechToText.instance.StopRecording();
	}
	void CheckPermission()
	{
		if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
		{
			Permission.RequestUserPermission(Permission.Microphone);
		}
	}

	void Setup(string code)
	{
		SpeechToText.instance.Setting(code);
	}
}
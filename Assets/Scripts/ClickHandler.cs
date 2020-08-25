using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ClickHandler : MonoBehaviour
{
    public UnityEvent UpEvent;
    public UnityEvent DownEvent;
    void OnMouseDown()
    {
        Debug.Log("Mouse down");
        DownEvent?.Invoke();
    }

    void OnMouseUp()
    {
        Debug.Log("Mouse up");
        UpEvent?.Invoke();
    }
}

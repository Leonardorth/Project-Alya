using UnityEngine;

public class CursorHideAndLock : MonoBehaviour
{
    [SerializeField] bool lockCursor;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked; //Locks cursor in place
            Cursor.visible = false;
        }
    }
}

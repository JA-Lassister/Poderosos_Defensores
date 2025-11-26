using UnityEngine;

public class Menu : MonoBehaviour
{
    void Awake() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
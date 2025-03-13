using UnityEngine;
using UnityEngine.InputSystem;

public class QuitAplication : MonoBehaviour
{
    private void Update()
    {
        if(Keyboard.current.escapeKey.isPressed)
        {
            Debug.Log("You pressed exit");
            Application.Quit();
        }
    }
}

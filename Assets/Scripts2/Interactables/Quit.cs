using UnityEngine;

public class Quit : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Application.Quit();
    }
}

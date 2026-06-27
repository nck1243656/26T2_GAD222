using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Rigidbody playerRigidbody;

    Keybinds keybinds;

    [SerializeField] Toggle invertYToggle;
    [SerializeField] Slider mouseSenstivity;

    [SerializeField] TMP_InputField interactKeybind;

    private bool isOptionsActive = false;

    void Awake()
    {
        keybinds = GameObject.Find("GameManager").GetComponent<Keybinds>();

        UpdateOptions();
    }

    public void Interact()
    {
        if(!isOptionsActive)
        {
            optionsPanel.SetActive(true);
            isOptionsActive = true;

            playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        isOptionsActive = false;

        playerRigidbody.constraints = RigidbodyConstraints.None;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UpdateOptions()
    {
        invertYToggle.isOn = keybinds.mouseYInvert;
        mouseSenstivity.value = keybinds.cameraSenstivity;

        //interactKeybind.text = keybinds.interactKey.ToString();
    }

    public void SetMouseYInvert(bool state)
    {
        keybinds.mouseYInvert = state;
    }

    public void SetCameraSenstivity(float amount)
    {
        keybinds.cameraSenstivity = amount;
    }
}

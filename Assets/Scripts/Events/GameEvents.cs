using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public int currentEvent = 0;

    [SerializeField] private PlayerCollection playerCollection;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            changeEvent();
        }
    }

    public void changeEvent()
    {
        currentEvent = 1;
    }
}

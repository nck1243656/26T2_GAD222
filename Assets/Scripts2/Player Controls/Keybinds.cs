using UnityEngine;

[CreateAssetMenu(fileName = "Keybinds", menuName = "Input/Keybinds")]
public class Keybinds : ScriptableObject
{
    public KeyCode jumpKey = KeyCode.Space;

    public KeyCode interactKey = KeyCode.E;
    public KeyCode collectKey = KeyCode.Q;

    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
}

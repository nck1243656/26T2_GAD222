using UnityEngine;

public class Keybinds : MonoBehaviour
{
    public KeyCode jumpKey = KeyCode.Space;

    public KeyCode pauseKey = KeyCode.L;

    public KeyCode interactKey = KeyCode.E;
    public KeyCode collectKey = KeyCode.Q;

    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    public bool mouseYInvert;

    [Range(1f, 600f)]
    public float cameraSenstivity = 100f;
}

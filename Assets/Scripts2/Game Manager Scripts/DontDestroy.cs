using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject[] persistentObjects = new GameObject[5];

    [SerializeField, Range(0, 5)] public int obbjectIndex;

    void Awake()
    {
        if (persistentObjects[obbjectIndex] == null)
        {
            persistentObjects[obbjectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }

        else if (persistentObjects[obbjectIndex] != gameObject)
        {
            Destroy(gameObject);
        }
    }
}

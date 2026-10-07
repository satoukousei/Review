using UnityEngine;
using UnityEngine.InputSystem;

public class Initializer : MonoBehaviour
{ 
    private void Awake()
    {
        InputInit init = new InputInit();
        init.Init();
        Destroy(gameObject);
    }
}
class InputInit
{
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public void Init()
    {
        InputSystem.settings.maxEventBytesPerUpdate = 100000;
    }
}

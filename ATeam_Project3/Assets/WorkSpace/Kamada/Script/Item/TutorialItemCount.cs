using UnityEngine;

public class TutorialItemCount : MonoBehaviour
{
    private int count = 0;
    private void Start()
    {
        count = 0;
    }

    public void PlusCount()
    {
        if(count < 5)
        {
            count++;
        }
    }
    public int GetItemCount() { return count; }
}

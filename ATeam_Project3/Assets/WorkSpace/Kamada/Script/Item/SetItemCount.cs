using UnityEngine;

public class SetItemCount : MonoBehaviour
{
    [SerializeField] private ItemDestroy[] itemDestroy = null;

    public void SetItem(float time)
    {
        for(int i= 0; i < itemDestroy.Length; i++)
        {
            itemDestroy[i].PlayTimeCount(time);
        }
    }

    private void Update()
    {
        for(int i = 0; i < itemDestroy.Length; i++)
        {
            if (itemDestroy[i].GetIsEnd())
            {
                Destroy(this.gameObject);
            }
        }
    }
}
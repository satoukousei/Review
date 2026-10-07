using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    //private ItemObjectPool pool;
    [SerializeField]
    private GetItemManager itemManager;
    [SerializeField]
    private ItemTracking itemTracking;
    [SerializeField]
    private EffectManager effect;
    [SerializeField]
    private int number = 0;
    [SerializeField]
    private SEManager seManager;
    private void Start()
    {
        //ui = FindAnyObjectByType<UIScript>();
        //pool = FindAnyObjectByType<ItemObjectPool>();
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (itemManager.GetPoint(number) > itemManager.GetMaxPoint())
            {
                return;
            }
            itemManager.PointPlus(number);
            itemTracking.SetItemPos(collision.gameObject.transform.position);
            effect.CreateEffect(4, collision.gameObject.transform.position, 1);
            seManager.PlayHeartCatchSE();
            Destroy(collision.gameObject);

            //ui.UpdateScore(1);
            //hp.PlusHp(10);
            //pool.Despawn(gameObject); // é©ï™é©êgÇîjâÛ
        }
    }
}
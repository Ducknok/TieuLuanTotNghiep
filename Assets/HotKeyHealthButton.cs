//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class HotKeyHealthButton : HotKeyButton
//{
    

//    protected override void OnDisable()
//    {
//        base.OnDisable();
//    }

//    protected override void OnEnable()
//    {
//        base.OnEnable();
//    }
//    public override void UseItem()
//    {
//        base.UseItem();
//        foreach (GameObject slot in inventory.slots)
//        {
//            Debug.Log(slot);
//            if (slot.GetComponent<SlotsScript>().isUsed && slot.GetComponentInChildren<HealthUse>().itemSO.itemName == itemName)
//            {
//                healthUse = slot.GetComponentInChildren<HealthUse>();
//                healthUse.UseButton();
//                onItemUsed?.Invoke();
//                break;
//            }
//        }
//    }

    
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class HotKeyManaButton : HotKeyButton
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
//            if (!slot.GetComponent<SlotsScript>().isUsed)
//            {
//                continue;
                
//            }
//            if (slot.GetComponent<SlotsScript>().isUsed && slot.GetComponentInChildren<ManaUse>().itemSO.itemName == itemName)
//            {
                
//                manaUse = slot.GetComponentInChildren<ManaUse>();
//                manaUse.UseButton();
//                onItemUsed?.Invoke();
//                break;
//            }
//        }
//    }


//}

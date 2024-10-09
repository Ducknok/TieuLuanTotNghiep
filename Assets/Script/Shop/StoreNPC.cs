using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : MonoBehaviour
{
    [SerializeField] protected GameObject[] itemInStore;
    [SerializeField] protected Inventory inventory;
    [SerializeField] protected Animator anim;

    protected virtual void Start()
    {
        this.inventory = GetComponent<Inventory>();
        this.SetUpStore();
    }
    protected virtual void SetUpStore()
    {
        for(int i = 0;i < this.itemInStore.Length; i++)
        {
            GameObject itemToSell = Instantiate(this.itemInStore[i], this.inventory.slots[i].transform.position, Quaternion.identity);        
            itemToSell.transform.SetParent(this.inventory.slots[i].transform);
            itemToSell.transform.localPosition = new Vector3(0, 0, 0);
            itemToSell.transform.localScale = new Vector3(1f, 1f, 0f);
            itemToSell.name = itemToSell.name.Replace("(Clone)", "");

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Da vao cua hang");
        this.anim.SetBool("showStore", true);
    }
    public void ExitStore()
    {
        this.anim.SetBool("showStore", false);
        StartCoroutine(DeactivateStore());
    }
    IEnumerator DeactivateStore()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}

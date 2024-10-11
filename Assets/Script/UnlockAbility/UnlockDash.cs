using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDash : MonoBehaviour
{
    [SerializeField] private PlayerController instance;
    public PlayerController Instance => instance;
    [SerializeField] protected GameObject canvasUI;
    [SerializeField] protected bool used;
    // Start is called before the first frame update
    void Start()
    {
        this.LoadPlayerController();
    }
    protected virtual void LoadPlayerController()
    {
        if (this.instance != null) return;
        this.instance = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !this.used)
        {
            this.used = true;          
            StartCoroutine(ShowUI());
        }
    }
    IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(0.5f);
        this.canvasUI.SetActive(true);
        yield return new WaitForSeconds(4f);
        this.instance.PlayerMove.unlockedDash = true;
        GameData.Instance.saveData.playerUnlockDash = this.instance.PlayerMove.unlockedDash;
        GameData.Instance.Save();
        canvasUI.SetActive(false);
    }
}

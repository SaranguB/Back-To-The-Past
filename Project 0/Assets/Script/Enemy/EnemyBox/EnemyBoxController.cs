using Enemy;
using Main;
using Player;
using System.Collections;
using UnityEngine;

public class EnemyBoxController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Animator boxAnimator;
    [SerializeField] private GameObject Box;
    [SerializeField] private float animationLength;

    private bool hasTriggered = false;
    private EnemyView enemyView;
    private bool isEnemyInPresent;

    private void Start()
    {
        enemyView = enemy.GetComponent<EnemyView>();

        enemyView.WasActiveInitially = false;
        GameManager.Instance.eventService.OnTimeSwitchWithBoolParam.AddListener(TimeSwitched);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.TryGetComponent<PlayerView>(out var player))
        {
            hasTriggered = true;
            boxAnimator.enabled = true;
            StartCoroutine(DestroyBox());
        }
    }

    public IEnumerator DestroyBox()
    {
        yield return new WaitForSeconds(animationLength);

        enemyView.SetOriginallyActive();
        enemy.gameObject.SetActive(true);
        Box.gameObject.SetActive(false);
    }

    private void TimeSwitched(bool value)
    {
        isEnemyInPresent = value;

        if (isEnemyInPresent)
        {
           this.gameObject.SetActive(true);
        }
        else
        {
           this.gameObject.SetActive(false);
        }
    }
}

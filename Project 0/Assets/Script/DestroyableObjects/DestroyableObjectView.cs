using Objects.Destroyable;
using System;
using UnityEngine;
using Wepons.Bomb;

public class DestroyableObjectView : MonoBehaviour, IDamagableFromBomb
{
    private DestroyableObjectController destroyableObjectController;

    [SerializeField] public GameObject presentWall;
    [SerializeField] public GameObject pastWall;
    [SerializeField] public BoxCollider2D boxCollider;

    public void SetController(DestroyableObjectController destroyableObjectController)
    {
        this.destroyableObjectController = destroyableObjectController;
    }

    public void TimeSwitchedToPast()
    {
        DisablePresentWall();
        EnablePastWall();
    }
    
    public void TimeSwitchedToPresent()
    {
        DisablePastWall();
        EnablePresentWall();
    }

    private void EnablePresentWall()
    {
        if (presentWall != null)
            presentWall.SetActive(true);
    }

    private void EnablePastWall()
    {
        if (pastWall != null)
            pastWall.SetActive(true);
    }

    private void DisablePresentWall()
    {
        if (presentWall != null)
            presentWall.SetActive(false);
    }

    public void DisablePastWall()
    {
        if (presentWall != null)
            pastWall.SetActive(false);
    }

    public void TakeDamageFromBomb(int damage)
    {
        if (!destroyableObjectController.GetIsPresentTime())
        {
            boxCollider.enabled = false;
            Destroy(pastWall.gameObject);
            Destroy(presentWall.gameObject);
        }

    }
}

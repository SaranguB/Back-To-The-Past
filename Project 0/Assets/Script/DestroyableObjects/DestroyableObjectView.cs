using Main;
using Objects.Destroyable;
using System;
using UnityEngine;
using Wepons.Bomb;

public class DestroyableObjectView : MonoBehaviour, IDamagableFromBomb
{
    private DestroyableObjectController destroyableObjectController;

    public GameObject presentObject;
    public GameObject pastObject;
    public BoxCollider2D boxCollider;
    public bool CanDestroyInPast;
    public Transform particleEffect;
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
        if (presentObject != null)
            presentObject.SetActive(true);
    }

    private void EnablePastWall()
    {
        if (pastObject != null)
            pastObject.SetActive(true);
    }

    private void DisablePresentWall()
    {
        if (presentObject != null)
            presentObject.SetActive(false);
    }

    public void DisablePastWall()
    {
        if (presentObject != null)
            pastObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {

        if (!destroyableObjectController.GetIsPresentTime() && CanDestroyInPast)
        {
            boxCollider.enabled = false;
            Destroy(pastObject.gameObject);
            Destroy(presentObject.gameObject);
            GameManager.Instance.vfxService.PlayVFXAtPosition(VFX.VFXType.DestroyableObjectExplosion, particleEffect.position);
        }     
        
        if (destroyableObjectController.GetIsPresentTime() && !CanDestroyInPast)
        {
            Debug.Log("Destroy in present");
            boxCollider.enabled = false;
            Destroy(pastObject.gameObject);
            Destroy(presentObject.gameObject);
            GameManager.Instance.vfxService.PlayVFXAtPosition(VFX.VFXType.DestroyableObjectExplosion, particleEffect.position);
        }

    }
}

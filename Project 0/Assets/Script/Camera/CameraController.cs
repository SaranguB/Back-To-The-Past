using Unity.Cinemachine;

public class CameraController
{
    private float shakeForce = 1f;
    private bool hasShaken = false;

    public void UpdateCameraShake(bool isTabHeld, CinemachineImpulseSource impulseSource)
    {
        if (impulseSource == null)
            return;

        if (isTabHeld && !hasShaken)
        {
            impulseSource.GenerateImpulseWithForce(shakeForce);
            hasShaken = true; 
        }
        else if (!isTabHeld)
        {
            hasShaken = false; 
        }
    }

    
}

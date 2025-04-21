using UnityEngine;

namespace VFX
{
    public class VFXService
    {
        private VFXPool vfxPool;

        public VFXService(VFXView vfxPrefab)
        {
            vfxPool = new VFXPool(vfxPrefab);
        }

        public void PlayVFXAtPosition(VFXType vfxType, Vector2 position)
        {
            VFXController vfxToPlay = vfxPool.GetVFX();

            vfxToPlay.ConfigureVFX(vfxType, position);
        }

        public void ReturnVFXToPool(VFXController vfxToReturn) => vfxPool.ReturnItem(vfxToReturn);

    }
}

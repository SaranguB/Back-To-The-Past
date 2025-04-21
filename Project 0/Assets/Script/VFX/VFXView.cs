using System;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class VFXView : MonoBehaviour
    {
        private VFXController vfxController;
        [SerializeField] private List<VFXData> particleSystemMap;
        private ParticleSystem currentPlayingVFX;

        public void SetController(VFXController vfxController)
        {
            this.vfxController = vfxController;
        }

        public void ConfigureVFX(VFXType vfxType, Vector2 positionToSet)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = positionToSet;


            foreach (VFXData item in particleSystemMap)
            {
                if (item.vfxType == vfxType)
                {
                    item.particleSystem.gameObject.SetActive(true);
                    currentPlayingVFX = item.particleSystem;
                }
                else
                {
                    item.particleSystem.gameObject.SetActive(false);
                }
            }

        }
        private void Update()
        {
            StopNonLoopingVFX();
        }

        private void StopNonLoopingVFX()
        {
            if (currentPlayingVFX != null)
            {
                if (currentPlayingVFX.isStopped)
                {
                    currentPlayingVFX.gameObject.SetActive(false);
                    currentPlayingVFX = null;
                    vfxController.OnParticleEffectCompleted();
                    gameObject.SetActive(false);
                }
            }
        }
    }

    [Serializable]
    public struct VFXData
    {
        public VFXType vfxType;
        public ParticleSystem particleSystem;
    }
}

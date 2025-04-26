using Main;

namespace Trap
{
    public class TrapController
    {
        private TrapView trapView;

        public TrapController(TrapView trapView)
        {
            this.trapView = trapView;
            this.trapView.SetController(this);
        }

        public void ActivateTrap()
        {
            switch (trapView.trapData.trapType)
            {
                case TrapType.FallingPlatform:
                  trapView.ActivateFallingPlatform();
                    break;

                case TrapType.SwingingSpike:
                    GameManager.Instance.eventService.OnPlayerGotDamaged.InvokeEvent(trapView.trapData.damage);
                    break;
            }
        }
    }
}

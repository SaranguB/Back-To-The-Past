using System;
using UnityEngine;

namespace Trap
{
    public class TrapService
    {
        private TrapViewCollection trapViewCollection;
        private TrapController trapController;
        public TrapService(TrapViewCollection trapViewCollection)
        {
            this.trapViewCollection = trapViewCollection;

            CreateTrapController();
        }

        private void CreateTrapController()
        {
            foreach (TrapView trapView in trapViewCollection.trapView)
            {
                trapController = new TrapController(trapView);
            }
        }
    }
}

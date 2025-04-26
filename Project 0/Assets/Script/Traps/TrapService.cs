namespace Trap
{
    public class TrapService
    {
        private TrapViewCollection trapViewCollection;

        public TrapService(TrapViewCollection trapViewCollection)
        {
            this.trapViewCollection = trapViewCollection;
            CreateTrapController();
        }

        private void CreateTrapController()
        {
            foreach (TrapView trapView in trapViewCollection.trapView)
            {
                TrapController trapController = new TrapController(trapView);
            }
        }
    }
}

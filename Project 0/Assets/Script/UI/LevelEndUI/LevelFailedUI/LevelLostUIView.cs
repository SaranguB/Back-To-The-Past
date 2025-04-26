namespace UI
{
    public class LevelLostUIView : BaseLevelEndUI
    {
        private LevelLostUIController levelLostUIController;

        public void SetController(LevelLostUIController levelLostUIController)
        {
            this.levelLostUIController = levelLostUIController;
        }
    }
}

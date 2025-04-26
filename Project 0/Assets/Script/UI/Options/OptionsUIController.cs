using Audio;
using Main;

namespace UI
{
    public class OptionsUIController
    {
        private OptionsUIView optionsUIView;

        public OptionsUIController(OptionsUIView optionsUIView)
        {
            this.optionsUIView = optionsUIView;
            optionsUIView.SetController(this);
        }

        public void PlayButtonSound()
            =>GameManager.Instance.soundService.PlaySoundEffects(SoundType.ButtonSound);
    }
}

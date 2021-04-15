using Core.Game;

namespace Interactors
{
    public class PauseInteractor
    {
        ITimeController timeController;
        IInputController inputController;


        public PauseInteractor(ITimeController timeController, IInputController inputController)
        {
            this.timeController = timeController;
            this.inputController = inputController;
        }

        public void Pause()
        {
            timeController.Lock();
            inputController.Lock();
        }

        public void Resume()
        {
            timeController.Unlock();
            inputController.Unlock();
        }
    }
}

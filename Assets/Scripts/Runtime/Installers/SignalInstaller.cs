using Runtime.Signals;
using Signals;
using Zenject;

namespace Runtime.Installers
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<SignalGameInit>();
            Container.DeclareSignal<SignalMakeHudButtonsVisible>();
            Container.DeclareSignal<SignalBlackScreen>();
            Container.DeclareSignal<SignalQuestionChoice>();
            Container.DeclareSignal<SignalJoystickUpdate>();
            Container.DeclareSignal<SignalHpBarUpdate>();
            Container.DeclareSignal<SignalScoreUpdate>();
            Container.DeclareSignal<SignalLifeCountUpdate>();
            Container.DeclareSignal<SignalLevelEnd>();
            Container.DeclareSignal<SignalUpdateCurrency>();
            Container.DeclareSignal<SignalBeforeAttack>();
            Container.DeclareSignal<SignalAfterAttack>();
            Container.DeclareSignal<SignalLevelState>();
            Container.DeclareSignal<SignalCriticalDmg>();
        }
    }
}
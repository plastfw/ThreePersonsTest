using R3;

namespace Source.Scripts.Core
{
    public class TickableService

    {
        private Observable<Unit> _update;
        public Observable<Unit> Update => _update ??= CreateUpdate();

        private Observable<Unit> CreateUpdate()
        {
            return Observable
                .EveryUpdate();
        }
    }
}
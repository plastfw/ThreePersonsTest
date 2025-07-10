using Source.Scripts.Ads;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using Source.Scripts.Player;

namespace Source.Scripts.UI
{
    public class AdsModel
    {
        private SwitchModelObserver _switchModelObserver;
        private LevelManager _levelManager;
        private IRewardedAds _rewardedAds;
        private SavesManager _savesManager;
        private IAnalytic _analytic;
        private IInterstitialAds _interstitialAds;

        public AdsModel(SwitchModelObserver switchModel, LevelManager levelManager,
            IRewardedAds rewardedAds, SavesManager saves, IAnalytic analytic, IInterstitialAds interstitial)
        {
            _switchModelObserver = switchModel;
            _levelManager = levelManager;
            _rewardedAds = rewardedAds;
            _savesManager = saves;
            _analytic = analytic;
            _interstitialAds = interstitial;
        }

        public void ConfirmClickedEvent()
        {
            ConfirmEvent();
        }

        public void RejectClicked()
        {
            RejectEvent();
        }

        private void ConfirmEvent()
        {
            _switchModelObserver.SaveTempPositions();
            _levelManager.LoadGameScene();
        }

        private void RejectEvent()
        {
            _analytic.LoseLevel();
            _levelManager.LoadMenuScene();
            _savesManager.DeleteAll();
        }
    }
}
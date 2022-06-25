using System;
using System.Diagnostics.CodeAnalysis;
using CustomSelectables;
using DG.Tweening;
using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.Ui.Windows.LevelComplete 
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LevelCompleteView : UiView 
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private TMP_Text _levelN;
        
        [SerializeField] public CustomButton NextLevel;

        [SerializeField] public TMP_Text Currency;
        [SerializeField] public TMP_Text RewardValue;
        [SerializeField] public TMP_Text Coins;
        [SerializeField] private RectTransform _currencyIcon;
        [SerializeField] private RectTransform _rewardIcon;
        
        [SerializeField] public Image Back;
        [SerializeField] public RectTransform Top;
        [SerializeField] public RectTransform Bottom;
        
        
        public void Show(EScene currentLevel)
        {
            gameObject.SetActive(true);
            _levelN.text = Enum.GetName(typeof(EScene), currentLevel)?.Replace("_", " ");
            var color = Back.color;
            Back.color = new Color(0, 0, 0, 0);
            var oldPosTop = Top.anchoredPosition;
            Top.anchoredPosition = new Vector2(0, 650f);
            var oldPosBottom = Bottom.anchoredPosition;
            Bottom.anchoredPosition = new Vector2(0, -800f);
            Back.DOColor(color, 1.2f).OnComplete(() =>
            {
                Top.DOAnchorPos(oldPosTop, 0.7f);
                Bottom.DOAnchorPos(oldPosBottom, 0.7f);
            });
        }

        public void GetReward(int result)
        {
            var newIcon = Instantiate(_currencyIcon, _rewardIcon.TransformPoint(_rewardIcon.rect.center), Quaternion.identity,  _container);
            newIcon.DOMove(_currencyIcon.TransformPoint(_currencyIcon.rect.center), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Currency.text = result.ToString();
                newIcon.gameObject.SetActive(false);
            });
        }

        public void UpdateCurrency(ref int value)
        {
            Currency.text = value.ToString();
        }
    }
}
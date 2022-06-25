using System.Diagnostics.CodeAnalysis;
using CustomSelectables;
using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.Ui.Windows.InGameButtons
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "RedundantDefaultMemberInitializer")]
    public class InGameButtonsView : UiView
    {
        public CustomButton InGameMenuBtn;
        public CustomButton StartToPlayBtn; 
        public CustomButton DropProgressButton;
        public CustomButton RollbackBtn;
        public RectTransform UiBox;
        
        public TMP_Text LevelN;
        public TMP_Text Coins;
        
        public TMP_Text Score;
        public TMP_Text HighScore;
        public TMP_Text Currency;

        public RectTransform Vignette;
        public float VignetteDuration = 0.3f;
        public int MaxHp = 100;
        public ProgressBar HpBar;
        
        public RectTransform TipText;
        public Image TipImage;
        public RectTransform TipFinger;
        

        // public void UpdateCurrency(ref int value)
        // {
        //     _currency.text = value.ToString();
        // }
        
        // public void SetHightScore(int value) => _highScore.text = value.ToString();
        
        // public void UpdateScore(ref SignalScoreUpdate signal) => _score.text = signal.Value.ToString();

        // public void SetFinishBtn(ref bool value) => FinishBtn.gameObject.SetActive(value);

        // public void UpdateHpBar(ref SignalHpBarUpdate signal)
        // {
        //     if (signal.Hp < _lastHp)
        //     {
        //         Vignette.DOKill();
        //         Vignette.gameObject.SetActive(true);
        //         Vignette.DOScale(Vector3.one * 3, _vignetteDuration).SetEase(Ease.Linear);
        //         Vignette.DOScale(Vector3.one * 15, _vignetteDuration).SetEase(Ease.Linear).SetDelay(_vignetteDuration)
        //             .OnComplete(() => Vignette.gameObject.SetActive(false));
        //     }
        //
        //     if (signal.Hp > 70)
        //         _currentColor = _color1;
        //     else if (signal.Hp < 30)
        //         _currentColor = _color3;
        //     else
        //         _currentColor = _color2;
        //
        //     _hpBar.Repaint(signal.Hp.Remap01(_maxHp), _currentColor);
        //     // _hpBarRect.anchoredPosition = signal.Position;
        //     _lastHp = signal.Hp;
        // }

        // public void InitTip1()
        // {
            // Tip1.DOKill();
            // Finger1.DOKill();
            // Tip1.gameObject.SetActive(true);
            // _tipColor = Tip1.color;
            // Tip1.color = new Color(0, 0, 0, 0);
            // var fingerPos = Finger1.anchoredPosition;
            // Tip1.DOColor(_tipColor, 0.6f).OnComplete(() =>
            // {
            //     Finger1.gameObject.SetActive(true);
            //     Tip1.rectTransform.DOScale(Vector3.one * 0.85f, 1.2f);
            //     Finger1.DOAnchorPos(Vector2.up * 100, 1.3f).SetRelative(true);
            // });
            // Tip1.DOFade(1, 0.1f).SetDelay(3f).OnComplete(() =>
            // {
            //     Tip1.gameObject.SetActive(false);
            //     Tip1.rectTransform.DOScale(Vector3.one , 0.1f);
            //     Finger1.DOKill();
            //     Finger1.gameObject.SetActive(false);
            //     Finger1.anchoredPosition = fingerPos;
            // });
        // }
        
        // public void InitPerfectParry()
        // {
            // PerfectParry.gameObject.SetActive(true);
            //
            // // var rotation = PerfectParry.rotation;
            // PerfectParry.rotation = Quaternion.Euler(0,0,-45f);
            // PerfectParry.DORotateQuaternion(Quaternion.identity, 0.4f);
            // PerfectParry.DORotateQuaternion(Quaternion.Euler(0,0,45f), 0.4f).SetDelay(0.75f);
            //
            // var scale = PerfectParry.transform.localScale;
            // PerfectParry.transform.localScale = Vector3.zero;
            // PerfectParry.transform.DOScale(scale, 0.4f);
            // PerfectParry.transform.DOScale(Vector3.zero, 0.4f).SetDelay(0.75f).OnComplete(() =>
            // {
            //     PerfectParry.transform.localScale = scale;
            //     PerfectParry.rotation = Quaternion.identity;
            //     PerfectParry.gameObject.SetActive(false);
            // });

        // }
    }
}
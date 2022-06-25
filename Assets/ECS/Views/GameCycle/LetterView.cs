using ECS.Views.General;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace ECS.Views.GameCycle
{
    public class LetterView : LinkableView
    {
        [SerializeField] public char Letter;
        [SerializeField] private TMP_Text LetterText;

        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            if (Letter != 0)
                Entity.Get<LetterComponent>().Value = Letter;
            InitText();
        }

        public void InitText() => LetterText.text = Entity.Get<LetterComponent>().Value.ToString();
    }

    public struct LetterComponent
    {
        public char Value;
    }
}
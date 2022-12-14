using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Battle.Views
{
    public class HealthPanel : MonoBehaviour
    {
        [SerializeField] private OpponentId _opponent;
        [SerializeField] private Slider _hpSlider;
        [SerializeField] private Slider _shieldSlider;

        public OpponentId OpponentId => _opponent;

        public void SetCurrentHp(float currentHp) 
            => _hpSlider.value = ModifyToSliderValue(currentHp);

        public void SetMaxHp(float maxHp)
            => _hpSlider.maxValue = ModifyToSliderValue(maxHp);

        public void SetCurrentShield(float currentShield)
            => _shieldSlider.value = ModifyToSliderValue(currentShield);

        public void SetMaxShield(float maxHp)
            => _shieldSlider.maxValue = ModifyToSliderValue(maxHp);

        private int ModifyToSliderValue(float value)
        {
            if (value < 0)
                value = 0;
            return (int)Math.Ceiling(value);
        }
    }
}
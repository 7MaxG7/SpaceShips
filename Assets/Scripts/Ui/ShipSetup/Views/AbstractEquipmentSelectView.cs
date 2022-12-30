using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abstractions.Services;
using DG.Tweening;
using Enums;
using Infrastructure;
using Ui.ShipSetup.Data;
using UnityEngine;

namespace Ui.ShipSetup
{
    public abstract class AbstractEquipmentSelectView<T> : MonoBehaviour, ICleanable where T : Enum
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _equipmentsContent;
        [SerializeField] private OpponentAnchor[] _opponentAnchors;
        [SerializeField] private CanvasGroup _canvasGroup;

        public event Action<OpponentId, int, T> OnEquipmentSelect;

        protected Transform EquipmentsContent => _equipmentsContent;
        protected readonly List<SlotUiView> EquipmentsSlots = new();

        protected IAssetsProvider AssetsProvider;
        private ICoroutineRunner _coroutineRunner;
        private OpponentId _opponentId;
        private int _slotIndex;
        private float _fadeAnimDuration;


        public void Init(IAssetsProvider assetsProvider, ICoroutineRunner coroutineRunner, float fadeAnimDuration)
        {
            _fadeAnimDuration = fadeAnimDuration;
            _coroutineRunner = coroutineRunner;
            AssetsProvider = assetsProvider;
        }

        public void Setup(OpponentId opponentId, int slotIndex)
        {
            _slotIndex = slotIndex;
            _opponentId = opponentId;
            var anchors = _opponentAnchors.FirstOrDefault(data => data.OpponentId == opponentId);
            if (anchors != null)
            {
                _rectTransform.anchorMin = anchors.Min;
                _rectTransform.anchorMax = anchors.Max;
                _rectTransform.pivot = anchors.Pivot;
            }
        }

        public void Show(bool isAnimated = true)
        {
            _canvasGroup.DOKill();
            gameObject.SetActive(true);
            if (isAnimated)
                _canvasGroup.DOFade(1, _fadeAnimDuration);
            else
                _canvasGroup.alpha = 1;
        }

        public void Hide(bool isAnimated = true)
        {
            _canvasGroup.DOKill();
            if (isAnimated)
                _canvasGroup.DOFade(0, _fadeAnimDuration)
                    .OnComplete(() => gameObject.SetActive(false));
            else
            {
                _canvasGroup.alpha = 0;
                gameObject.SetActive(false);
            }
        }

        public void CleanUp()
        {
            foreach (var slot in EquipmentsSlots)
            {
                slot.SelectButton.onClick.RemoveAllListeners();
                if (slot != null && slot.gameObject != null)
                    Destroy(slot.gameObject);
            }
            EquipmentsSlots.Clear();
        }

        protected void InvokeEquipmentSelect(T equipmentType)
            => OnEquipmentSelect?.Invoke(_opponentId, _slotIndex, equipmentType);

        protected void AjustSize() 
            => _coroutineRunner.StartCoroutine(AjustSizeCoroutine());

        private IEnumerator AjustSizeCoroutine()
        {
            _canvasGroup.alpha = 0;
            yield return new WaitForEndOfFrame();
            _rectTransform.sizeDelta = _equipmentsContent.sizeDelta;
            Hide(false);
        }
    }
}
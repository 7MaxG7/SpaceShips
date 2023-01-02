using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Services;
using DG.Tweening;
using Enums;
using Infrastructure;
using Ui.ShipSetup.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.ShipSetup
{
    public abstract class AbstractEquipmentSelectView<TType> : MonoBehaviour, ICleanable where TType : Enum
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _equipmentsContent;
        [SerializeField] private OpponentAnchor[] _opponentAnchors;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        protected IUiFactory UiFactory;
        protected Transform EquipmentsContent => _equipmentsContent;
        private ICoroutineRunner _coroutineRunner;
        
        private readonly List<SlotUiView> _equipmentsSlots = new();
        private float _fadeAnimDuration;


        public void CleanUp()
        {
            foreach (var slot in _equipmentsSlots)
            {
                slot.SelectButton.onClick.RemoveAllListeners();
                if (slot != null && slot.gameObject != null)
                    Destroy(slot.gameObject);
            }
            _equipmentsSlots.Clear();
        }

        public void Init(IUiFactory uiFactory, ICoroutineRunner coroutineRunner, float fadeAnimDuration)
        {
            UiFactory = uiFactory;
            _coroutineRunner = coroutineRunner;
            _fadeAnimDuration = fadeAnimDuration;
        }

        public void Locate(OpponentId opponentId, Vector3 position)
        {
            var anchors = _opponentAnchors.FirstOrDefault(data => data.OpponentId == opponentId);
            if (anchors != null)
            {
                _rectTransform.anchorMin = anchors.Min;
                _rectTransform.anchorMax = anchors.Max;
                _rectTransform.pivot = anchors.Pivot;
            }

            _rectTransform.position = position;
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

        public async Task<Button> AddEquipmentSelectSlot(TType equipmentType)
        {
            var selectUiSlot = await CreateSelectUiSlot(equipmentType);
            _equipmentsSlots.Add(selectUiSlot);
            return selectUiSlot.SelectButton;
        }

        public void AjustSize() 
            => _coroutineRunner.StartCoroutine(AjustSizeCoroutine());

        protected abstract Task<SlotUiView> CreateSelectUiSlot(TType equipmentType);

        private IEnumerator AjustSizeCoroutine()
        {
            _canvasGroup.alpha = 0;
            yield return new WaitForEndOfFrame();
            _rectTransform.sizeDelta = _equipmentsContent.sizeDelta;
            Hide(false);
        }
    }
}
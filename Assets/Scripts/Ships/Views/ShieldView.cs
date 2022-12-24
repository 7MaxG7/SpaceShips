using UnityEngine;

namespace Ships.Views
{
    public class ShieldView : MonoBehaviour
    {
        private const float VISIBLE_SHIELD_TRESHOLD = .25f;

    
        public void UpdatePower(float currentShield, float maxShield)
        {
            gameObject.SetActive(maxShield > 0 && currentShield / maxShield > VISIBLE_SHIELD_TRESHOLD);
        }
    }
}
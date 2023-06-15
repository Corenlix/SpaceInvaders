using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.View
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _shieldBar;

        public void UpdateHealth(float amount, float max)
        {
            _healthBar.fillAmount = amount / max;
        }

        public void UpdateShield(float amount, float max)
        {
            _shieldBar.fillAmount = amount / max;
        }
    }
}

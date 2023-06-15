using TMPro;
using UnityEngine;

namespace Code.UI.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void UpdateScore(int amount)
        {
            _scoreText.text = amount.ToString();
        }
    }
}

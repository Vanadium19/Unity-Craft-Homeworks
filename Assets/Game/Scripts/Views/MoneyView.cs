using TMPro;
using UnityEngine;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _money;

        public void SetMoney(int money)
        {
            _money.text = money.ToString();
        }
    }
}
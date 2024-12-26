using TMPro;
using UnityEngine;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _money;
        [SerializeField] private Transform _coin;
        
        public Vector3 CoinPosition => _coin.position;

        public void SetMoney(int money)
        {
            _money.text = money.ToString();
        }
    }
}
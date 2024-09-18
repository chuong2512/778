using System;
using TMPro;
using UnityEngine;

namespace ABCBoard.Scripts.UI
{
    public class CoinText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmp;

        private void OnValidate()
        {
            _tmp = GetComponentInChildren<TextMeshProUGUI>();
        }

        void Start()
        {
        }


        private void Update()
        {
            _tmp.SetText($"{GameManager.instance.getcoin().ToString()}");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trafficlight {
    // Класс меняет цвета светофора
    public class TrafficlightController : MonoBehaviour
    {

        [SerializeField] private MeshRenderer _light_red;
        [SerializeField] private MeshRenderer _light_yellow;
        [SerializeField] private MeshRenderer _light_green;

        [SerializeField] private TrafficlightData _data;

        private void Awake()
        {
            SetAllToDefault();
        }

        private void Start()
        {
            StartCoroutine(Work());
        }

        private void OnDestroy()
        {
            StopCoroutine(Work());
        }

        private IEnumerator Work()
        {
            while(true)
            {
                SetRed();
                yield return new WaitForSeconds(_data.delay_red_to_yellow);
                SetYellow();
                yield return new WaitForSeconds(_data.delay_yellow_to_green);
                SetGreen();
                yield return new WaitForSeconds(_data.delay_green_to_red);
            }
        }

        private void SetAllToDefault()
        {
            _light_red.material = _data.material_default;
            _light_yellow.material = _data.material_default;
            _light_green.material = _data.material_default;
        }

        private void SetRed()
        {
            SetAllToDefault();
            _light_red.material = _data.material_red;
            EventFather.RedLight();
        }

        private void SetYellow()
        {
            SetAllToDefault();
            _light_yellow.material = _data.material_yellow;
        }

        private void SetGreen()
        {
            SetAllToDefault();
            _light_green.material = _data.material_green;
            EventFather.GreenLight();
        }
    }
}
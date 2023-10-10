using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trafficlight
{
    [CreateAssetMenu(menuName = "Trafficlight")]
    internal class TrafficlightData : ScriptableObject
    {
        [SerializeField] internal Material material_default;
        [SerializeField] internal Material material_red;
        [SerializeField] internal Material material_yellow;
        [SerializeField] internal Material material_green;

        [SerializeField, Min(0)] internal float delay_red_to_yellow;
        [SerializeField, Min(0)] internal float delay_yellow_to_green;
        [SerializeField, Min(0)] internal float delay_green_to_red;
        

    }
}
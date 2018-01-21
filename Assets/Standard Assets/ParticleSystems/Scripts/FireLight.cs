using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Effects
{
    public class FireLight : MonoBehaviour
    {
        private float m_Rnd;
        private bool m_Burning = true;
        private Light m_Light;
        private Vector3 Offset;


        private void Start()
        {
            Offset = transform.position - transform.parent.position;
            m_Rnd = Random.value*10000;
            m_Light = GetComponent<Light>();
        }


        private void Update()
        {
            if (m_Burning)
            {
                m_Light.intensity = Random.Range(10f, 12f);
                m_Light.range = Random.Range(.62f, .7f);

                if (m_Light.intensity < 1)
                {
                    m_Light.intensity = 0;
                }

                Vector3 rotationAmount = Random.insideUnitSphere * 0.1f;

                rotationAmount.z = transform.localPosition.z;

                //rotationAmount.y -= 0.2f;

                transform.localPosition = rotationAmount;

                //float x = Mathf.PerlinNoise(m_Rnd + 0 + Time.time*2, m_Rnd + 1 + Time.time*2) - 0.5f;
                //float y = Mathf.PerlinNoise(m_Rnd + 2 + Time.time*2, m_Rnd + 3 + Time.time*2) - 0.5f;
                //float z = Mathf.PerlinNoise(m_Rnd + 4 + Time.time*2, m_Rnd + 5 + Time.time*2) - 0.5f;
                //transform.localPosition = Vector3.up + new Vector3(x, y, z)*1;
            }
        }


        public void Extinguish()
        {
            m_Burning = false;
            m_Light.enabled = false;
        }
    }
}

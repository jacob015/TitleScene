using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFarm
{
    [System.Serializable]
    public class SeedPointList
    {
        public List<SeedPoint> pointList;

        public void SetRandomSeedData(List<SeedData> datas)
        {
            foreach (SeedPoint sp in pointList)
            {
                if (sp.SeedData != null)
                {
                    sp.Index = 0;
                    sp.SunLightTimer = 0;
                    sp.GrowthBar.fillAmount = 0;

                    sp.SeedData = datas[Random.Range(0, datas.Count)];
                    sp.UpdateSeed(sp.SeedData.FirstSprite);
                }

            }
        }

        public void SetSunLight(Define.SunLightState state)
        {
            foreach (SeedPoint sp in pointList)
            {
                sp.SunLightState = state;
            }
        }
    }
}

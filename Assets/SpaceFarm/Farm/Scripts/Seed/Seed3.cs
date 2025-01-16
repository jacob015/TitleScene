using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceFarm
{
    public class Seed3 : ActionImage
    {
        public override void Execute(Transform target)
        {
            if (target.GetComponent<SeedPoint>().SeedData == null)
            {
                SoundManager.Instance.PlaySFX("SFX_Plant");

                target.gameObject.GetComponent<SeedPoint>().SeedData = GetComponent<ActionImage>().SeedData;
                target.gameObject.GetComponent<SeedPoint>().UpdateSeed(GetComponent<ActionImage>().SeedData.FirstSprite);
            }
        }
    }
}
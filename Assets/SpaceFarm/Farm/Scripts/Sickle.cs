using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceFarm
{
    public class Sickle : ActionImage
    {
        public override void Execute(Transform target)
        {
            if (target.GetComponent<SeedPoint>().SeedData != null)
            {
                SoundManager.Instance.PlaySFX("SFX_Sickle");

                if (target.gameObject.GetComponent<SeedPoint>().SeedState == Define.GrowthState.FINISH)
                {
                    //SoundManager.Instance.PlaySFX("SFX_ScoreUp");
                    GameManager.Instance.Score += target.gameObject.GetComponent<SeedPoint>().SeedData.score;
                    target.gameObject.GetComponent<SeedPoint>().SeedData = null;
                    target.gameObject.GetComponent<SeedPoint>().Init();
                    target.gameObject.GetComponent<SeedPoint>().UpdateSeed(null);
                }
                else if(target.GetComponent<SeedPoint>().SeedData.name == "DieSeedData")
                {
                    target.gameObject.GetComponent<SeedPoint>().SeedData = null;
                    target.gameObject.GetComponent<SeedPoint>().UpdateSeed(null);
                }
            }
           
        }
    }
}
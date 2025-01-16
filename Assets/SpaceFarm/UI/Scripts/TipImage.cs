using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SpaceFarm
{
    public class TipImage : MonoBehaviour
    {
        private Button _tipOnButton;
        private Button _tipCloseButton;

        private CanvasGroup _tipImage;

        void Start()
        {
            _tipImage = UIManager.Instance.GetUI<CanvasGroup>("TipImage");

            _tipOnButton = UIManager.Instance.GetUI<Button>("TipOnButton");
            _tipOnButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                _tipOnButton.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f, true, () =>
                {
                    _tipImage.SetActiveOfCanvasGroup(true);
                    _tipOnButton.gameObject.SetActive(false);
                });
            });

            _tipOnButton.gameObject.SetActive(false);

            _tipCloseButton = UIManager.Instance.GetUI<Button>("TipCloseButton");
            _tipCloseButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                _tipCloseButton.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f, true, () =>
                {
                    _tipImage.SetActiveOfCanvasGroup(false);
                    _tipOnButton.gameObject.SetActive(true);
                });
            });
        }

        void Update()
        {

        }
    }
}
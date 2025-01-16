using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFarm
{
    public static class Extention
    {
        #region SetActive
        public static void SetActiveOfCanvasGroup(this CanvasGroup cg, bool value)
        {
            if (value)
            {
                cg.alpha = 1;
                cg.blocksRaycasts = true;
                cg.interactable = true;
            }
            else
            {
                cg.alpha = 0;
                cg.blocksRaycasts = false;
                cg.interactable = false;
            }
        }
        #endregion

        #region Fade
        public static void FadeOfCanvasGroup(this CanvasGroup cg, float to, float duration, float delay = 0, Action action = null)
        {
            CoroutineRunner.Instance.RunCoroutine(Fade(cg, to, duration, delay, action));
        }

        private static IEnumerator Fade(CanvasGroup cg, float to, float duration, float delay = 0, Action action = null)
        {
            yield return new WaitForSeconds(delay);

            cg.blocksRaycasts = false;
            cg.interactable = false;

            if (to == 1)
            {
                cg.alpha = 0;
            }
            else
            {
                cg.alpha = 1;
            }

            float from = cg.alpha;
            float currentTime = 0f;

            while (currentTime < duration)
            {
                cg.alpha = Mathf.Lerp(from, to, currentTime / duration);
                currentTime += Time.deltaTime;
                yield return null;
            }

            cg.alpha = to;
            

            if (cg.alpha == 1)
            {
                cg.blocksRaycasts = true;
                cg.interactable = true;
            }
           

            action?.Invoke();
        }
        #endregion

        #region CameraMove

        public static void ShakeCamera(this Camera mainCam, float duration, float strength)
        {
            CoroutineRunner.Instance.RunCoroutine(Shake(duration, strength));
        }

        private static IEnumerator Shake(float duration, float strength)
        {
            Transform mainCam = Camera.main.transform;

            Vector3 originalPos = mainCam.localPosition;
            float shakeDuration = duration;
            float shakeStrength = strength;

            while (shakeDuration > 0)
            {
                mainCam.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeStrength;
                shakeDuration -= Time.deltaTime;

                yield return null;
            }

            mainCam.localPosition = originalPos; // 흔들림이 끝난 후 원래 위치로 되돌림
        }
        #endregion

        #region ObjectMove

        public static void ToMoveTransform(this Transform obj, Vector3 from, Vector3 to, float duration, bool isYoyo = false)
        {
            CoroutineRunner.Instance.RunCoroutine(MoveToTransform(obj, from, to, duration, isYoyo));
        }

        public static void ToMoveRectTransform(this RectTransform obj, Vector2 from, Vector2 to, float duration, bool isYoyo = false)
        {
            CoroutineRunner.Instance.RunCoroutine(MoveToRectTransform(obj, from, to, duration, isYoyo));
        }


        private static IEnumerator MoveToTransform(Transform obj, Vector3 from, Vector3 to, float duration, bool isYoyo = false)
        {
            obj.position = from;

            if (isYoyo)
            {
                duration = duration * 0.5f;
            }

            float currentTime = 0;

            while (currentTime < duration)
            {
                obj.position = Vector3.Lerp(from, to, currentTime / duration);
                currentTime += Time.deltaTime;

                yield return null;
            }

            obj.position = to;

            if (isYoyo)
            {
                CoroutineRunner.Instance.RunCoroutine(MoveToTransform(obj, to, from, duration));
            }
        }

        private static IEnumerator MoveToRectTransform(RectTransform obj, Vector2 from, Vector2 to, float duration, bool isYoyo = false)
        {
            obj.anchoredPosition = from;

            if (isYoyo)
            {
                duration = duration * 0.5f;
            }

            float currentTime = 0;

            while (currentTime < duration)
            {
                obj.anchoredPosition = Vector2.Lerp(from, to, currentTime / duration);
                currentTime += Time.deltaTime;

                yield return null;
            }

            obj.anchoredPosition = to;
           
            if (isYoyo)
            {
                CoroutineRunner.Instance.RunCoroutine(MoveToRectTransform(obj, to, from, duration));
            }
        }

        #endregion

        #region ObjectScale
        public static void ChangeScale(this Transform target, Vector3 to, float duration, bool isYoyo = false, Action action = null)
        {
            CoroutineRunner.Instance.RunCoroutine(ScaleTo(target, to, duration, isYoyo, action));
        }

        private static IEnumerator ScaleTo(Transform target, Vector3 to, float duration, bool isYoyo = false, Action action = null)
        {
            Vector3 from = target.localScale;

            if (isYoyo)
            {
                duration = duration * 0.5f;
            }

            float currentTime = 0;

            while (currentTime < duration)
            {
                target.localScale = Vector3.Lerp(from, to, currentTime / duration);
                currentTime += Time.deltaTime;

                yield return null;
            }

            target.localScale = to;

            if (isYoyo)
            {
                CoroutineRunner.Instance.RunCoroutine(ScaleTo(target, from, duration, false, action));           
            }
            else
            {
                action?.Invoke();
            }
        }
        #endregion

    }
}
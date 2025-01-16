using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PangPangPang
{
    public class UIManager : Singleton<UIManager>
    {
        private Dictionary<Type, Dictionary<string, UnityEngine.Object>> _storage =
          new Dictionary<Type, Dictionary<string, UnityEngine.Object>>();

        public Dictionary<Type, Dictionary<string, UnityEngine.Object>> Storage { get { return _storage; } }

        public T GetUI<T>(string name) where T : UnityEngine.Object //UI �������� �κ�
        {

            Dictionary<string, UnityEngine.Object> temps = new Dictionary<string, UnityEngine.Object>();
            if (_storage.TryGetValue(typeof(T), out temps) == false)
                return null;


            return temps[name] as T;
        }
    }
}
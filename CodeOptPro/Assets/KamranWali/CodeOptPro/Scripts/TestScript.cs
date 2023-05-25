using KamranWali.CodeOptPro.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KamranWali.CodeOptPro
{
    public class TestScript : BaseAwakeStart
    {
        public override void AwakeAdv()
        {
            Debug.Log($"Awake -> {gameObject.name}");
            gameObject.SetActive(true);
        }

        public override void StartAdv()
        {
            Debug.Log($"Start -> {gameObject.name}");
        }
    }
}
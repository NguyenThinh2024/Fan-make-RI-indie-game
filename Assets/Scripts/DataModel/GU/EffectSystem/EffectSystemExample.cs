using UnityEngine;
using Game.GU.EffectSystem;
using Game.GU.SOModel;

namespace Game.GU.Examples
{
    /// <summary>
    /// Ví dụ về cách sử dụng Strategy Pattern cho Effect System
    /// </summary>
    public class EffectSystemExample : MonoBehaviour
    {
        private EffectExecutor effectExecutor;
        private GameObject targetObject;

        void Start()
        {
            // Setup
            effectExecutor = gameObject.AddComponent<EffectExecutor>();
            
            // Tạo target object với các receiver
            targetObject = new GameObject("Target");
            targetObject.AddComponent<SimpleHealth>();
            targetObject.AddComponent<SimpleShield>();
            targetObject.AddComponent<SimpleBuff>();
            targetObject.AddComponent<SimpleScout>();
            targetObject.AddComponent<SimpleStorage>();

            Debug.Log("=== Effect System Strategy Pattern Example ===\n");
        }

        void Update()
        {
            // Ví dụ: Nhấn các phím khác nhau để thử các effect
            if (Input.GetKeyDown(KeyCode.D))
                TestDamageEffect();

            if (Input.GetKeyDown(KeyCode.S))
                TestShieldEffect();

            if (Input.GetKeyDown(KeyCode.B))
                TestBuffEffect();

            if (Input.GetKeyDown(KeyCode.C))
                TestScoutEffect();

            if (Input.GetKeyDown(KeyCode.T))
                TestStorageEffect();

            if (Input.GetKeyDown(KeyCode.A))
                TestAllEffects();
        }

        void TestDamageEffect()
        {
            Debug.Log("\n--- Testing Damage Effect ---");
            var damage = new Game.GU.SOModel.DamageEffect
            {
                type = "physical",
                value = 25,
                scalingRatio = 0.5f,
                cooldownSec = 1f,
                areaType = "single"
            };

            var effect = new Effect { damage = damage };
            effectExecutor.ExecuteEffect(effect, targetObject);
        }

        void TestShieldEffect()
        {
            Debug.Log("\n--- Testing Shield Effect ---");
            var shield = new Game.GU.SOModel.ShieldEffect
            {
                hp = 50,
                absorbRate = 0.75f,
                absorptionFlat = 10f,
                type = "energy",
                scalingRatio = 1f,
                affectAllies = true
            };

            var effect = new Effect { shield = shield };
            effectExecutor.ExecuteEffect(effect, targetObject);
        }

        void TestBuffEffect()
        {
            Debug.Log("\n--- Testing Buff Effect ---");
            var buff = new Game.GU.SOModel.BuffEffect
            {
                targetGu = "Self",
                stat = "Strength",
                scalingRatio = 1.5f
            };

            var effect = new Effect { buff = buff };
            effectExecutor.ExecuteEffect(effect, targetObject);
        }

        void TestScoutEffect()
        {
            Debug.Log("\n--- Testing Scout Effect ---");
            var scout = new Game.GU.SOModel.ScoutEffect
            {
                scoutRange = 10f,
                revealDurationSec = 5f,
                detectStealth = true,
                revealType = "vision"
            };

            var effect = new Effect { scout = scout };
            effectExecutor.ExecuteEffect(effect, targetObject);
        }

        void TestStorageEffect()
        {
            Debug.Log("\n--- Testing Storage Effect ---");
            var storage = new Game.GU.SOModel.StorageEffect
            {
                slots = 10,
                deny = new[] { "Weapon", "Armor" }
            };

            var effect = new Effect { storage = storage };
            effectExecutor.ExecuteEffect(effect, targetObject);
        }

        void TestAllEffects()
        {
            Debug.Log("\n--- Testing ALL Effects Together ---");
            var effect = new Effect
            {
                damage = new Game.GU.SOModel.DamageEffect
                {
                    type = "fire",
                    value = 30,
                    scalingRatio = 0.8f,
                    cooldownSec = 2f,
                    areaType = "aoe"
                },
                shield = new Game.GU.SOModel.ShieldEffect
                {
                    hp = 40,
                    absorbRate = 0.6f,
                    absorptionFlat = 5f,
                    type = "barrier",
                    scalingRatio = 1f,
                    affectAllies = true
                },
                buff = new Game.GU.SOModel.BuffEffect
                {
                    targetGu = "Self",
                    stat = "Attack",
                    scalingRatio = 2f
                },
                scout = new Game.GU.SOModel.ScoutEffect
                {
                    scoutRange = 15f,
                    revealDurationSec = 10f,
                    detectStealth = true,
                    revealType = "radar"
                },
                storage = new Game.GU.SOModel.StorageEffect
                {
                    slots = 20,
                    deny = new[] { "Key", "Quest" }
                },
                cooldownSec = 5f,
                check_GU = true
            };

            effectExecutor.ExecuteEffect(effect, targetObject);
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 200));
            GUILayout.Label("Effect System Strategy Pattern - Test Controls", GUI.skin.box);
            GUILayout.Label("D - Test Damage Effect");
            GUILayout.Label("S - Test Shield Effect");
            GUILayout.Label("B - Test Buff Effect");
            GUILayout.Label("C - Test Scout Effect");
            GUILayout.Label("T - Test Storage Effect");
            GUILayout.Label("A - Test ALL Effects");
            GUILayout.EndArea();
        }
    }
}

using UnityEngine;

namespace Game.GU.EffectSystem
{
    /// <summary>
    /// Strategy để thực thi Damage Effect
    /// </summary>
    public class DamageEffectStrategy : IEffectStrategy
    {
        public string GetEffectName() => "Damage";

        public bool CanExecute(GameObject target)
        {
            return target != null;
        }

        public void Execute(GameObject target, EffectContext context)
        {
            if (!CanExecute(target))
            {
                Debug.LogWarning($"Cannot execute {GetEffectName()} on {target?.name}");
                return;
            }

            // Tính toán damage với scaling
            float finalDamage = context.value * (1 + context.scalingRatio);

            // Áp dụng damage
            var damageReceiver = target.GetComponent<IDamageReceiver>();
            if (damageReceiver != null)
            {
                damageReceiver.TakeDamage(finalDamage);
                Debug.Log($"<color=red>[{GetEffectName()}] Dealt {finalDamage} damage to {target.name}</color>");
            }
            else
            {
                Debug.LogWarning($"Target {target.name} không có IDamageReceiver component!");
            }
        }
    }

    /// <summary>
    /// Strategy để thực thi Shield Effect
    /// </summary>
    public class ShieldEffectStrategy : IEffectStrategy
    {
        public string GetEffectName() => "Shield";

        public bool CanExecute(GameObject target)
        {
            return target != null;
        }

        public void Execute(GameObject target, EffectContext context)
        {
            if (!CanExecute(target))
            {
                Debug.LogWarning($"Cannot execute {GetEffectName()} on {target?.name}");
                return;
            }

            // Tính toán shield
            float shieldAmount = context.hp + context.absorptionFlat;
            float absorbRate = context.absorbRate;

            var shieldReceiver = target.GetComponent<IShieldReceiver>();
            if (shieldReceiver != null)
            {
                shieldReceiver.AddShield(shieldAmount, absorbRate, context.affectAllies);
                Debug.Log($"<color=blue>[{GetEffectName()}] Added {shieldAmount} shield to {target.name}</color>");
            }
            else
            {
                Debug.LogWarning($"Target {target.name} không có IShieldReceiver component!");
            }
        }
    }

    /// <summary>
    /// Strategy để thực thi Buff Effect
    /// </summary>
    public class BuffEffectStrategy : IEffectStrategy
    {
        public string GetEffectName() => "Buff";

        public bool CanExecute(GameObject target)
        {
            return target != null;
        }

        public void Execute(GameObject target, EffectContext context)
        {
            if (!CanExecute(target))
            {
                Debug.LogWarning($"Cannot execute {GetEffectName()} on {target?.name}");
                return;
            }

            var buffReceiver = target.GetComponent<IBuffReceiver>();
            if (buffReceiver != null)
            {
                buffReceiver.AddBuff(context.stat, context.value * context.scalingRatio);
                Debug.Log($"<color=green>[{GetEffectName()}] Applied buff {context.stat} +{context.value * context.scalingRatio} to {target.name}</color>");
            }
            else
            {
                Debug.LogWarning($"Target {target.name} không có IBuffReceiver component!");
            }
        }
    }

    /// <summary>
    /// Strategy để thực thi Scout Effect
    /// </summary>
    public class ScoutEffectStrategy : IEffectStrategy
    {
        public string GetEffectName() => "Scout";

        public bool CanExecute(GameObject target)
        {
            return target != null;
        }

        public void Execute(GameObject target, EffectContext context)
        {
            if (!CanExecute(target))
            {
                Debug.LogWarning($"Cannot execute {GetEffectName()} on {target?.name}");
                return;
            }

            var scoutReceiver = target.GetComponent<IScoutReceiver>();
            if (scoutReceiver != null)
            {
                scoutReceiver.Scout(context.scoutRange, context.revealDurationSec, context.detectStealth, context.revealType);
                Debug.Log($"<color=cyan>[{GetEffectName()}] Scout range {context.scoutRange} for {context.revealDurationSec}s</color>");
            }
            else
            {
                Debug.LogWarning($"Target {target.name} không có IScoutReceiver component!");
            }
        }
    }

    /// <summary>
    /// Strategy để thực thi Storage Effect
    /// </summary>
    public class StorageEffectStrategy : IEffectStrategy
    {
        public string GetEffectName() => "Storage";

        public bool CanExecute(GameObject target)
        {
            return target != null;
        }

        public void Execute(GameObject target, EffectContext context)
        {
            if (!CanExecute(target))
            {
                Debug.LogWarning($"Cannot execute {GetEffectName()} on {target?.name}");
                return;
            }

            var storageReceiver = target.GetComponent<IStorageReceiver>();
            if (storageReceiver != null)
            {
                storageReceiver.AddStorage(context.slots, context.deny);
                Debug.Log($"<color=magenta>[{GetEffectName()}] Added {context.slots} storage slots to {target.name}</color>");
            }
            else
            {
                Debug.LogWarning($"Target {target.name} không có IStorageReceiver component!");
            }
        }
    }
}

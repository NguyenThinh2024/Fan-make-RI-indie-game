using UnityEngine;

namespace Gu_Effect_Systems
{
    public class DamageEffectStrategy : IEffectStrategy
    {
        public string GetEffectName()
        {
            return "Damage";
        }

        public bool CanExecute(GameObject target)
        {
            return target != null;
        }

        public void Execute(GameObject target, EffectContext context)
        {
            if (CanExecute(target ) == false)
            {
                // Implement damage logic here
                Debug.Log($"Executing Damage effect on {target.name} with value {context.value}");
                return;
            }

            float finalDamage = context.value * (1 + context.scalingRatio);

            var damageReceiver = target.GetComponent<IDamageReceiver>();

            if (damageReceiver != null)
            {
                damageReceiver.TakeDamage(finalDamage);
                Debug.Log($"<color=red>Applied {finalDamage} damage to {target.name}</color>");
            }
            else
            {
                Debug.LogWarning($"Target {target.name} does not have a DamageReceiver component.");
            }
        }
        
    }


}

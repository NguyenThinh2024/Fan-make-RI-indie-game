using UnityEngine;
 namespace Gu_Effect_Systems
{
    public static class EffectStrategyFactory
    {
        public static IEffectStrategy CreateStrategy(string effectType)
        {
            switch (effectType?.ToLower())
            {
                case "damage":
                    return new DamageEffectStrategy();

                // case "shield":
                //     return new ShieldEffectStrategy();

                // case "buff":
                //     return new BuffEffectStrategy();

                // case "scout":
                //     return new ScoutEffectStrategy();

                // case "storage":
                //     return new StorageEffectStrategy();

                default:
                    Debug.LogWarning($"Unknown effect type: {effectType}. Returning null.");
                    return null;
            }
        }

        public static IEffectStrategy[] CreateAllStrategies()
        {
            return new IEffectStrategy[]
            {
                new DamageEffectStrategy(),
                // new ShieldEffectStrategy(),
                // new BuffEffectStrategy(),
                // new ScoutEffectStrategy(),
                // new StorageEffectStrategy()
            };
        }
    }
}
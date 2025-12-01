using UnityEngine;

namespace Game.GU.EffectSystem
{
    /// <summary>
    /// Factory để tạo Effect Strategy dựa trên loại effect
    /// </summary>
    public static class EffectStrategyFactory
    {
        /// <summary>
        /// Tạo strategy dựa trên effect type
        /// </summary>
        public static IEffectStrategy CreateStrategy(string effectType)
        {
            switch (effectType?.ToLower())
            {
                case "damage":
                    return new DamageEffectStrategy();

                case "shield":
                    return new ShieldEffectStrategy();

                case "buff":
                    return new BuffEffectStrategy();

                case "scout":
                    return new ScoutEffectStrategy();

                case "storage":
                    return new StorageEffectStrategy();

                default:
                    Debug.LogWarning($"Unknown effect type: {effectType}. Returning null.");
                    return null;
            }
        }

        /// <summary>
        /// Tạo tất cả strategies
        /// </summary>
        public static IEffectStrategy[] CreateAllStrategies()
        {
            return new IEffectStrategy[]
            {
                new DamageEffectStrategy(),
                new ShieldEffectStrategy(),
                new BuffEffectStrategy(),
                new ScoutEffectStrategy(),
                new StorageEffectStrategy()
            };
        }
    }
}

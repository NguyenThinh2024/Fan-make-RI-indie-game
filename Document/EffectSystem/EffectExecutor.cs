using UnityEngine;
using System.Collections.Generic;
using Game.GU.SOModel;

namespace Game.GU.EffectSystem
{
    /// <summary>
    /// Executor thực thi effect dựa trên Strategy Pattern
    /// </summary>
    public class EffectExecutor : MonoBehaviour
    {
        // Cache strategies để tối ưu performance
        private Dictionary<string, IEffectStrategy> strategyCache = new Dictionary<string, IEffectStrategy>();

        /// <summary>
        /// Thực thi effect từ GuData lên target
        /// </summary>
        public void ExecuteEffectFromGU(GuData guData, GameObject target)
        {
            if (guData == null || target == null)
            {
                Debug.LogError("GuData hoặc target không được null!");
                return;
            }

            ExecuteEffect(guData.effect, target);
        }

        /// <summary>
        /// Thực thi effect với context cụ thể
        /// </summary>
        public void ExecuteEffect(Effect effect, GameObject target)
        {
            if (effect == null || target == null)
            {
                Debug.LogError("Effect hoặc target không được null!");
                return;
            }

            // Kiểm tra effect có check_GU không
            if (effect.check_GU)
            {
                Debug.Log($"Effect requires GU check on {target.name}");
            }

            // Thực thi từng loại effect nếu tồn tại
            if (effect.damage != null)
                ExecuteEffectByType("damage", effect.damage, target);

            if (effect.shield != null)
                ExecuteEffectByType("shield", effect.shield, target);

            if (effect.buff != null)
                ExecuteEffectByType("buff", effect.buff, target);

            if (effect.scout != null)
                ExecuteEffectByType("scout", effect.scout, target);

            if (effect.storage != null)
                ExecuteEffectByType("storage", effect.storage, target);
        }

        /// <summary>
        /// Thực thi effect dựa trên type và data
        /// </summary>
        private void ExecuteEffectByType(string effectType, object effectData, GameObject target)
        {
            // Lấy hoặc tạo strategy
            IEffectStrategy strategy = GetOrCreateStrategy(effectType);

            if (strategy == null)
            {
                Debug.LogError($"Cannot create strategy for effect type: {effectType}");
                return;
            }

            // Kiểm tra xem effect có áp dụng được không
            if (!strategy.CanExecute(target))
            {
                Debug.LogWarning($"{strategy.GetEffectName()} cannot be applied to {target.name}");
                return;
            }

            // Chuyển đổi effect data thành EffectContext
            EffectContext context = ConvertEffectDataToContext(effectType, effectData);

            // Thực thi effect
            strategy.Execute(target, context);
        }

        /// <summary>
        /// Lấy hoặc tạo strategy từ cache
        /// </summary>
        private IEffectStrategy GetOrCreateStrategy(string effectType)
        {
            if (strategyCache.ContainsKey(effectType))
            {
                return strategyCache[effectType];
            }

            IEffectStrategy strategy = EffectStrategyFactory.CreateStrategy(effectType);
            if (strategy != null)
            {
                strategyCache[effectType] = strategy;
            }

            return strategy;
        }

        /// <summary>
        /// Chuyển đổi effect data thành EffectContext
        /// </summary>
        private EffectContext ConvertEffectDataToContext(string effectType, object effectData)
        {
            var context = new EffectContext();

            switch (effectType.ToLower())
            {
                case "damage":
                    if (effectData is Game.GU.SOModel.DamageEffect damage)
                    {
                        context.type = damage.type;
                        context.value = damage.value;
                        context.scalingRatio = damage.scalingRatio;
                        context.cooldownSec = damage.cooldownSec;
                        context.areaType = damage.areaType;
                    }
                    break;

                case "shield":
                    if (effectData is Game.GU.SOModel.ShieldEffect shield)
                    {
                        context.hp = shield.hp;
                        context.absorbRate = shield.absorbRate;
                        context.absorptionFlat = shield.absorptionFlat;
                        context.type = shield.type;
                        context.scalingRatio = shield.scalingRatio;
                        context.affectAllies = shield.affectAllies;
                    }
                    break;

                case "buff":
                    if (effectData is Game.GU.SOModel.BuffEffect buff)
                    {
                        context.targetGu = buff.targetGu;
                        context.stat = buff.stat;
                        context.scalingRatio = buff.scalingRatio;
                    }
                    break;

                case "scout":
                    if (effectData is Game.GU.SOModel.ScoutEffect scout)
                    {
                        context.scoutRange = scout.scoutRange;
                        context.revealDurationSec = scout.revealDurationSec;
                        context.detectStealth = scout.detectStealth;
                        context.revealType = scout.revealType;
                    }
                    break;

                case "storage":
                    if (effectData is Game.GU.SOModel.StorageEffect storage)
                    {
                        context.slots = storage.slots;
                        context.deny = storage.deny;
                    }
                    break;
            }

            return context;
        }

        /// <summary>
        /// Clear cache strategies
        /// </summary>
        public void ClearStrategyCache()
        {
            strategyCache.Clear();
        }
    }
}

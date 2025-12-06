using UnityEngine;

namespace Game.GU.EffectSystem
{
    /// <summary>
    /// Interface định nghĩa cách thực thi một effect
    /// </summary>
    public interface IEffectStrategy
    {
        /// <summary>
        /// Tên của effect
        /// </summary>
        string GetEffectName();

        /// <summary>
        /// Kiểm tra xem effect này có áp dụng được cho target không
        /// </summary>
        bool CanExecute(GameObject target);

        /// <summary>
        /// Thực thi effect lên target
        /// </summary>
        void Execute(GameObject target, EffectContext context);
    }

    /// <summary>
    /// Context chứa thông tin cần thiết để thực thi effect
    /// </summary>
    public class EffectContext
    {
        public float value;
        public float scalingRatio;
        public float cooldownSec;
        public string type;
        public string areaType;
        public int slots;
        public string[] deny;
        public float hp;
        public float absorbRate;
        public float absorptionFlat;
        public bool affectAllies;
        public string targetGu;
        public string stat;
        public float scoutRange;
        public float revealDurationSec;
        public bool detectStealth;
        public string revealType;
    }
}

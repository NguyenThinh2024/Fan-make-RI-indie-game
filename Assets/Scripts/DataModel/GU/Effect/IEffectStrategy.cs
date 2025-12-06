using UnityEngine;

public interface IEffectStrategy
{
    string GetEffectName();
    bool CanExecute(GameObject target);
    void Execute(GameObject target, EffectContext context);
}

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
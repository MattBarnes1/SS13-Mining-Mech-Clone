using GameData.GameDataClasses.RuleEngine;
using Microsoft.Xna.Framework;

namespace DedicatedServerFramework.MapGeneration
{
    internal class BaseStartConclusion : Conclusion
    {
        private Vector2 baseStart;
        public BaseStartConclusion(Vector2 baseStart, float Priority, object myConclusion) : base(Priority, myConclusion)
        {
            this.baseStart = baseStart;
        }

    }
}
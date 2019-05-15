using System.Diagnostics;

namespace GameData.GameDataClasses.RuleEngine
{
    public class Conclusion
    {
        public Conclusion(float Priority, object myConclusion)
        {
            this.Priority = Priority;
            this.myConclusion = myConclusion;
            Debug.Assert(this.myConclusion != null);
        }

        public float Priority { get;}
        object myConclusion;
        public virtual T GetConclusion<T>() where T : class
        {
            return myConclusion as T;
        }
    }
}
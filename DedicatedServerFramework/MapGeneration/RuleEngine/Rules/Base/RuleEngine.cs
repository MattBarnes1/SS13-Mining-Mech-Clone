using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.GameDataClasses.Maps;

namespace GameData.GameDataClasses.RuleEngine
{
    public class RuleEngine<Output> where Output : class
    {
        List<Rule> RuleSet { get; }

        public List<Rule> EditableRuleset = new List<Rule>();

        List<Conclusion> myConclusionSet = new List<Conclusion>();
        Dictionary<String, Object> ExternalData = new Dictionary<string, object>();

        public void AddDictionaryItem(string v, Object myTiles)
        {
            ExternalData.Add(v, myTiles);
        }

        public RuleEngine(List<Rule> RuleSet, List<Conclusion> myConclusions)
        {
            this.myConclusionSet = myConclusions;
            this.RuleSet = RuleSet;
            this.EditableRuleset = RuleSet;
        }

        public virtual List<Output> Decide()
        {
            EditableRuleset = new List<Rule>(RuleSet);
            bool setChanges = true;
            while (setChanges)
            {
                setChanges = false;
                List<Rule> ToRemove = new List<Rule>();
                foreach (Rule B in EditableRuleset)
                {
                    if (B.PerformRule(ExternalData, myConclusionSet))
                    {
                        setChanges = true;//if working set has changed
                        if (B.RemoveAfterUse())
                        {
                            ToRemove.Add(B);
                        }
                    }
                }
                foreach (var A in ToRemove)
                {
                    EditableRuleset.Remove(A);
                }
            }

            List<Output> myOutput = ConvertConclusion();
            return myOutput;
        }

        protected List<Conclusion> GetConclusion()
        {
            return myConclusionSet;
        }

        protected List<Output> ConvertConclusion()
        {
            List<Output> myOutput = new List<Output>();
            foreach (Conclusion A in myConclusionSet)
            {
                myOutput.Add(A.GetConclusion<Output>());
            }

            return myOutput;
        }




    }
}

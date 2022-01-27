using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_GOAP
{
    public class SellGold : GoapAction
    {
        public SellGold(string name, float cost, float duration) : base(name, cost, duration)
        {
        }

        // perform pickup action
        public override void Perform(GoapAgent agent)
        {
            base.Perform(agent);

            Console.WriteLine("Went to town and sold the gold.");

            agent.Inventory.Items["gold"] = 0;
        }
    }
}

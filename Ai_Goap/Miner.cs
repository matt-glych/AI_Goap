using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_GOAP
{
    public class Miner : GoapAgent
    {
        // agent state
        private enum State { Idle, Action, Move };
        private State state;

        // agent actions
        CollectTool collectTool = new CollectTool("Collect Tool", 1, 0.5f);
        MineGold mineGold = new MineGold("Mine Gold", 2, 2f);
        PickupGold pickupGold = new PickupGold("Pickup Gold", 1, 0.5f);
        SellGold sellGold = new SellGold("Sell Gold", 1, 1f);


        public Miner()
        {
            Initialise();
        }

        // initalise the agent
        private void Initialise()
        {
            // load goal and actions
            LoadGoal();
            LoadActions();
        }
        
        // Get a Goal
        private void LoadGoal()
        {
           // agent goal
            Goal.Add("getMoney", true);
        }

        // Get a World State
        private void LoadWorldState()
        {
            // state of inventory weapon
            GoapState toolState = new GoapState("hasTool", inventory.Items.ContainsKey("tool"));

            // generate a world state by applying effects of various states
            WorldState.ApplyEffectsHard(toolState);
        }

        // load actions
        private void LoadActions()
        {
            // -- agent actions

            // collect tool
            collectTool.Preconditions.Add("hasTool", false);
            collectTool.Effects.Add("hasTool", true);
            // mine gold
            mineGold.Preconditions.Add("hasTool", true);
            mineGold.Effects.Add("goldFound", true);
            // pickup gold
            pickupGold.Preconditions.Add("goldFound", true);
            pickupGold.Effects.Add("collectGold", true);
            // sell gold
            sellGold.Preconditions.Add("collectGold", true);
            sellGold.Effects.Add("getMoney", true);
            sellGold.Effects.Add("collectGold", false);

            AddAction(collectTool);
            AddAction(mineGold);
            AddAction(pickupGold);
            AddAction(sellGold);
        }

        // agent update loop
        public override void Update()
        {
            switch (state)
            {
                case State.Idle:
                    IdleState();
                    break;
                case State.Move:
                    MoveState();
                    break;
                case State.Action:
                    ActionState();
                    break;
            }
        }

        // -- Behaviours

        // idle state
        private void IdleState()
        {
            LoadWorldState();
            // generate a plan based on the goal and world state
            Queue<GoapAction> plan = planner.Plan(goal, worldState, availableActions);

            // if a plan is generated move into action state
            if (plan != null)
            {
                actions = plan;

                Console.WriteLine("Plan generated for goal: [ {0} ]", goal.State.First().Key);

                // print the actions
                for (int i = 0; i < actions.Count; i++)
                {
                    Console.WriteLine(" - " + (i + 1) + ": " + actions.ElementAt(i)._name);
                }

                state = State.Action;

            }
            else
            {
                Console.WriteLine("Failed to generate an action plan!");
            }
        }

        // action state
        private void ActionState()
        {
            //Console.WriteLine("Entered: ACTION state");
            // move to idle state if actions queue is empty
            if (actions.Count == 0)
            {
                //Console.WriteLine("No actions left, generating a new plan..");
                state = State.Idle;
            }
            else
            {
                GoapAction currentAction = actions.Peek();
                //Console.WriteLine("Performing ACTION: {0}", currentAction._name);
                if (currentAction.Complete)
                {
                    currentAction.Reset();
                    actions.Dequeue();
                }
                else
                {
                    if (actions.Count > 0)
                        currentAction.Perform(this);
                }
            }     
        }

        // move state
        private void MoveState()
        {

        }

      
    }
}

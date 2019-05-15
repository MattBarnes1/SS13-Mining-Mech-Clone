using System.Collections.Generic;
using GameData.ECS.Custom_Component.Player_Components;
using Nez;

namespace SS13Clone.Scenes
{
    public class PlayerMovementSystem : EntitySystem
    {

        public PlayerMovementSystem() : base(new Matcher().one(typeof(PlayerMover)))
        {

        }

        protected override void process(List<Entity> entities)
        {
            base.process(entities);
        }
    }
}
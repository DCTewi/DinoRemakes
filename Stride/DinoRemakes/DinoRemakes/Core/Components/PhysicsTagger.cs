using Stride.Engine;

namespace DinoRemakes.Core.Components
{
    public class PhysicsTagger : StartupScript
    {
        public string Tag { get; set; }

        public override void Start()
        {
            base.Start();

            foreach (var component in Entity.GetAll<PhysicsComponent>())
            {
                component.Tag = Tag;
            }
        }
    }
}


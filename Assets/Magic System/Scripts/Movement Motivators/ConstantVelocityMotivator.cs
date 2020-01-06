using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Magic_System.Scripts.Movement_Motivators
{
    class ConstantVelocityMotivator : BaseMovementMotivator
    {

        private Vector3 velocity;

        private Space space;

        public ConstantVelocityMotivator(Vector3 velocity, Space space)
        {
            this.velocity = velocity;
            this.space = space;
        }

        public override void Move(Transform transform)
        {
            transform.Translate(velocity * Time.deltaTime, space);
        }
    }
}

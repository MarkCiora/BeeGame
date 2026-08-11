using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace BeeGame;

public class TaskTargetSystem : ECSSystem
{

}

public class TaskAssignmentSystem : ECSSystem
{
    public void Update()
    {
        //establish available tasks
        var targets_set = ecs.GetSystem<TaskTargetSystem>().m_entities;
        List<int> available_targets = new();
        foreach (int id in targets_set)
        {
            ref TaskTarget task_target = ref ecs.GetComponent<TaskTarget>(id);
            if (task_target.actor != -1 && // ensure task is not assigned
                (
                    task_target.type == TaskType.Collect // ensure type is directly assignable
                )
            )
            {
                available_targets.Add(id);
            }
        }

        //assign tasks
        foreach (int id in m_entities)
        {
            //first check if any tasks available
            if (available_targets.Count == 0)
                break;

            ref TaskActor active_task_comp = ref ecs.GetComponent<TaskActor>(id);

            // only continue if task holder is inactive
            if (active_task_comp.target != -1)
                continue;

            // search up to 10 in the available targets
            // pick the closest one
            ref Transform transform = ref ecs.GetComponent<Transform>(id);
            int selectable = Math.Min(10, available_targets.Count);
            int best = available_targets[0];
            float min_sq_dist = 100000f;
            for (int target_i = 0; target_i < selectable; target_i++)
            {
                ref Transform target_transform = ref ecs.GetComponent<Transform>(available_targets[target_i]);
                float sq_dist = (transform.pos - target_transform.pos).LengthSquared();
                if (sq_dist < min_sq_dist)
                {
                    best = target_i;
                    min_sq_dist = sq_dist;
                }
            }

            // assign task and remove that target from the list
            AssignTask(id, available_targets[best]);
            available_targets.RemoveAt(best);
        }
    }

    public void AssignTask(int actor, int target)
    {
        ref TaskActor task_comp = ref ecs.GetComponent<TaskActor>(actor);
        task_comp.target = target;
        task_comp.status = TaskStatus.Active;
        ref TaskTarget task_target = ref ecs.GetComponent<TaskTarget>(target);
        task_target.actor = actor;
    }

    public void CleanupTask(int actor, int target)
    {
        ref TaskActor task_actor = ref ecs.GetComponent<TaskActor>(actor);
        ref TaskTarget task_target = ref ecs.GetComponent<TaskTarget>(target);
        task_actor.target = -1;
        task_target.actor = -1;
    }
}

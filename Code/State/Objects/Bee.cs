using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class Bee : GameObj
{
    public static Dictionary<long, Bee> bee_dict = new Dictionary<long, Bee>();

    public bool flying;

    public static readonly float max_walk_speed = 1f;
    public static readonly float max_walk_acc = 5f;
    public static readonly float max_fly_speed = 2f;
    public static readonly float max_fly_acc = 5f;

    public float speed;
    public Vector2 move_target;

    public readonly TaskNode behavior_tree;

    public Bee(Vector2 pos, float rot = 0f)
    {
        bee_dict[id] = this;
        this.pos = pos;
        this.rot = rot;
        speed = 0f;
        move_target = pos;
        move_path = new MovePath();
        behavior_tree = new TaskSelector(
            new TaskSequence(
                new TaskCondition(obj => CheckForApple((Bee)obj)),
                new ActionNode(obj => SelectApple((Bee)obj)),
                new ActionNode(obj => PlanMoveObj((Bee)obj)),
                new ActionNode(obj => PlanEatApple((Bee)obj))
            ),
            new ActionNode(obj => SitIdle((Bee)obj))
        );
    }

    public override Texture2D GetTexture()
    {
        return Textures.Bee1_Sheet;
    }

    public override void Delete()
    {
        bee_dict.Remove(id);
        move_path = null;
    }

    public static void UpdateAll()
    {
        foreach (Bee bee in bee_dict.Values) //parallel?
        {
            if (bee.deleted) continue;
            bee.behavior_tree.Update(bee);
        }
        foreach (Bee bee in bee_dict.Values) //parallel?
        {
            if (bee.deleted) continue;
            bee.Update();
        }
    }

    private void Update()
    {
        if (intent == IntentType.MovePos || intent == IntentType.MoveObj)
        {
            rot = MathZ.Atan2(move_target - pos);
            Vector2 dir = MathZ.DirFromRot(rot);
            speed = MathF.Min(max_walk_speed, speed + max_walk_acc * Time.dt);
        }
        else
        {
            speed = MathF.Max(0f, speed - max_walk_acc * Time.dt);
        }
        if (intent == IntentType.Eating)
        {
            if ((pos - selected_obj.pos).Length() <= scale * 0.5f + 0.25f)
            {
                selected_obj.FlagDelete();
            }
        }
        pos += MathZ.DirFromRot(rot) * speed * Time.dt;
    }

    // Bee thought definitions ------------------------
    public enum IntentType { Idle, MovePos, MoveObj, Eating, Collect, Deposit, Attack }
    public IntentType intent;
    public MovePath move_path;
    public Vector2 move_dest;
    public GameObj selected_obj;

    private static TaskNodeStatus PlanMoveVec(Bee bee)
    {
        if (!bee.move_path.IsReady())
        {
            bee.move_path.MakeNewPath(bee.pos, bee.move_dest, GS.grid);
        }

        if (bee.move_path.IsReady())
        {
            bee.move_target = bee.move_path.GetStep(bee.pos, bee.scale);
        }

        if (!bee.move_path.IsReady())
        {
            if ((bee.pos - bee.move_dest).Length() <= bee.scale * 0.5f + 0.25f)
            {
                return TaskNodeStatus.Success;
            }
            else
            {
                return TaskNodeStatus.Failure;
            }
        }
        else
        {
            bee.intent = IntentType.MovePos;
            return TaskNodeStatus.Running;
        }
    }

    private static TaskNodeStatus PlanMoveObj(Bee bee)
    {
        bee.move_dest = bee.selected_obj.pos;
        return PlanMoveVec(bee);
    }

    private static TaskNodeStatus SelectApple(Bee bee)
    {
        bee.selected_obj = Apple.RequestReserveClosest(bee.pos);
        if (bee.selected_obj != null) return TaskNodeStatus.Success;
        else return TaskNodeStatus.Failure;
    }
    private static TaskNodeStatus PlanEatApple(Bee bee)
    {
        if ((bee.pos - bee.selected_obj.pos).Length() <= 0.75f)
        {
            bee.intent = IntentType.Eating;
            return TaskNodeStatus.Success;
        }
        else
        {
            return TaskNodeStatus.Failure;
        }
    }

    private static TaskNodeStatus SitIdle(Bee bee)
    {
        bee.intent = IntentType.Idle;
        return TaskNodeStatus.Success;
    }

    private static bool CheckForApple(Bee bee)
    {
        return Apple.available_apples.Count != 0 || bee.selected_obj != null;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace BeeGame;

public enum TaskNodeStatus { Success, Running, Failure }

public abstract class TaskNode
{
    public abstract TaskNodeStatus Update(GameObj obj);
}

public class TaskSequence : TaskNode
{
    public readonly List<TaskNode> children;
    private int current = 0;

    public TaskSequence(params TaskNode[] nodes) => children = nodes.ToList();

    public override TaskNodeStatus Update(GameObj obj)
    {
        while (current < children.Count)
        {
            var status = children[current].Update(obj);
            if (status == TaskNodeStatus.Running) return TaskNodeStatus.Running;
            if (status == TaskNodeStatus.Failure) { current = 0; return TaskNodeStatus.Failure; }
            current++; // child succeeded
        }
        current = 0;
        return TaskNodeStatus.Success;
    }
}

public class TaskSelector : TaskNode
{
    private readonly List<TaskNode> children;
    private int current = 0;

    public TaskSelector(params TaskNode[] nodes) => children = nodes.ToList();

    public override TaskNodeStatus Update(GameObj obj)
    {
        while (current < children.Count)
        {
            var status = children[current].Update(obj);
            if (status == TaskNodeStatus.Running) return TaskNodeStatus.Running;
            if (status == TaskNodeStatus.Success) { current = 0; return TaskNodeStatus.Success; }
            current++;
        }
        current = 0;
        return TaskNodeStatus.Failure;
    }
}

public class TaskCondition : TaskNode
{
    private readonly Func<GameObj, bool> predicate;

    public TaskCondition(Func<GameObj, bool> predicate) => this.predicate = predicate;

    public override TaskNodeStatus Update(GameObj obj)
        => predicate(obj) ? TaskNodeStatus.Success : TaskNodeStatus.Failure;
}

public class ActionNode : TaskNode
{
    private readonly Func<GameObj, TaskNodeStatus> action;

    public ActionNode(Func<GameObj, TaskNodeStatus> action) => this.action = action;

    public override TaskNodeStatus Update(GameObj obj) => action(obj);
}
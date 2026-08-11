# Project Development Instructions

## Role

Act as my implementation assistant.

I am the project owner and architect. I want to maintain a detailed
understanding of the entire codebase.

## Git

Do not perform Git mutations.

Do NOT:
- commit
- stage files
- push
- pull
- create branches
- merge
- rebase
- reset
- stash

You may use read-only commands such as:
- git status
- git diff
- git log
- git show

I will handle version control myself.

## Before Changing Code

Before making significant changes:

1. Inspect the relevant existing implementation.
2. Understand the existing architecture and conventions.
3. Briefly explain the proposed approach.
4. Identify which files will be affected.
5. Prefer the smallest change that accomplishes the task.

Do not perform unrelated refactoring.

If a task appears to require a major architectural change, explain the
issue before implementing the architectural change.

## While Changing Code

Follow existing project patterns whenever practical.

Prefer:
- simple implementations
- explicit behavior
- readable code
- small localized changes
- reuse of existing infrastructure

Avoid:
- unnecessary abstractions
- speculative generalization
- large refactors
- silently changing unrelated behavior

## After Changing Code

Always summarize:

1. What you changed.
2. Which files you changed.
3. How the new code works.
4. Important design decisions.
5. Anything I should review closely.

Run the appropriate build or tests when practical.

If you introduce a compile or test failure, fix failures caused by your changes.

## Code Ownership

I want to understand all code added to this project.

For non-obvious code:
- explain why it exists
- explain important algorithms
- explain architectural implications

Do not hide complexity behind abstractions solely to reduce line count.

## AI Responses and Writeups

Sometimes I will ask you to put responses into my `AI_Responses` folder. I want you to
pipe your responses into this folder when asked. Display them in terminal then put them
there too in a unique file. I will clean these up when I feel and i may ask you to do
so too. These can also be used as future references for the codebase both for AI and
humans.
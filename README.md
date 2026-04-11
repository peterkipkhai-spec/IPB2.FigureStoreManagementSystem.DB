# IPB2 Figure Store Management System

Current step is **CA + DB** only.

## Important: why you still see conflicts on GitHub
You are viewing an older PR conflict page that still compares old commits.
That page can keep showing `<<<<<<< ======= >>>>>>>` until the PR branch is refreshed/rebased.

## What to do now
1. Pull latest branch head.
2. Open `IPB2.FigureStoreManagementSystem.sln` in Visual Studio.
3. If PR #2 still shows old conflicts, close it and create a new PR from latest branch head.

## Conflict prevention added
This repo now sets Git merge strategy to `union` for:
- `README.md`
- `*.sln`
- `*.slnx`

So future merges are less likely to stop on these text-only metadata files.


## PR conflict loop fix
If GitHub still shows old conflict screens, follow `docs/PR_CONFLICT_FIX.md` step-by-step.

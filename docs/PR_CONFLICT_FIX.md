# Fixing the recurring GitHub "Resolve conflicts" loop

If GitHub still shows conflict markers for old files, your PR branch is stale compared to `master`.

## Option A (Recommended): create a fresh branch from latest master

```bash
git fetch origin
git checkout master
git pull origin master
git checkout -b fix/ca-db-step
# cherry-pick the latest CA+DB commit(s)
git cherry-pick e7707c0
git push -u origin fix/ca-db-step
```

Then open a new PR from `fix/ca-db-step` to `master`.

## Option B: refresh current PR branch

```bash
git fetch origin
git checkout <your-pr-branch>
git merge origin/master
```

When conflict editor opens:
- For `IPB2.FigureStoreManagementSystem.DB.slnx`: keep only CA + DB project lines.
- For `README.md`: keep the short CA+DB step guide.

Then complete:

```bash
git add .
git commit -m "Resolve merge conflicts"
git push
```

## Visual Studio
Always open `IPB2.FigureStoreManagementSystem.sln`.

#!/usr/bin/env bash
set -euo pipefail

if [[ $# -ne 1 ]]; then
  echo "Usage: $0 <pr-branch-name>"
  exit 1
fi

BRANCH="$1"

git fetch origin
git checkout "$BRANCH"
git merge origin/master

echo "If conflicts appear, resolve them, then run:"
echo "  git add ."
echo "  git commit -m 'Resolve merge conflicts'"
echo "  git push"

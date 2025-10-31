# Branch Protection and Backup Guide

## Overview
This guide provides instructions for creating a protected backup branch and locking it to prevent unauthorized access.

## Problem Statement
- Take the protected branch (likely `main` or `master`)
- Create a new branch from it for backup purposes
- Lock that branch from outside users accessing the branch except for the repo owner

## Solution Steps

### Step 1: Create a Backup Branch

You can create a backup branch from your protected branch using one of these methods:

#### Method A: Using GitHub Web Interface
1. Navigate to your repository on GitHub
2. Click on the branch dropdown (usually shows `main`)
3. Type a new branch name, e.g., `backup-main-[date]` or `protected-backup`
4. Click "Create branch: backup-main-[date] from main"

#### Method B: Using Git Command Line
```bash
# Ensure you're on the protected branch
git checkout main

# Create a new backup branch
git checkout -b backup-main-$(date +%Y%m%d)

# Push the backup branch to GitHub
git push origin backup-main-$(date +%Y%m%d)
```

#### Method C: Using GitHub CLI
```bash
# Create a backup branch from main
gh api repos/:owner/:repo/git/refs -f ref='refs/heads/backup-main' -f sha=$(gh api repos/:owner/:repo/git/ref/heads/main -q .object.sha)
```

### Step 2: Lock the Branch (Branch Protection Rules)

To restrict access to the backup branch to only the repository owner, follow these steps:

#### Setting Up Branch Protection on GitHub

1. **Navigate to Repository Settings**
   - Go to your repository on GitHub
   - Click on "Settings" tab
   - In the left sidebar, click "Branches"

2. **Add Branch Protection Rule**
   - Click "Add rule" or "Add branch protection rule"
   - In "Branch name pattern", enter the exact branch name (e.g., `backup-main-*` or specific name like `protected-backup`)

3. **Configure Protection Settings**
   
   **Recommended Settings for Owner-Only Access:**
   
   - ✅ **Require a pull request before merging**
     - Enable "Require approvals" and set to 1
     - ✅ Check "Dismiss stale pull request approvals when new commits are pushed"
     - ✅ Check "Require review from Code Owners"
   
   - ✅ **Require status checks to pass before merging**
     - ✅ Check "Require branches to be up to date before merging"
   
   - ✅ **Require conversation resolution before merging**
   
   - ✅ **Require signed commits**
   
   - ✅ **Require linear history**
   
   - ✅ **Include administrators** (if you want protection to apply to admins too)
   
   - ✅ **Restrict who can push to matching branches**
     - Click "Restrict pushes that create matching branches"
     - Add only the repository owner's username
     - This is the KEY setting to lock the branch to specific users
   
   - ✅ **Allow force pushes** - UNCHECK (disable this)
   
   - ✅ **Allow deletions** - UNCHECK (disable this)

4. **Restrict Push Access (Most Important)**
   - Scroll to "Restrict who can push to matching branches"
   - Enable this option
   - Under "Search for people, teams, or apps", add ONLY:
     - The repository owner's GitHub username
     - Or a specific team that should have access
   - Leave other users out to restrict their access

5. **Save the Protection Rule**
   - Click "Create" or "Save changes"

### Step 3: Verify Protection

After setting up the branch protection:

1. Try to push to the branch with a different user account (if available) to verify access is denied
2. Check the branch settings to ensure all protection rules are active
3. Document which branch is protected and why

### Step 4: Create a CODEOWNERS File (Optional but Recommended)

Create a `.github/CODEOWNERS` file to define code ownership:

```
# CODEOWNERS file
# This file defines who owns the code in this repository

# Owner for all backup branches
backup-*    @repository-owner-username

# Owner for protected backup
protected-backup    @repository-owner-username
```

Replace `@repository-owner-username` with the actual GitHub username of the repository owner.

## Important Notes

### For Repository Owners
- Only repository owners or administrators can set up branch protection rules
- Branch protection applies to everyone who doesn't have explicit bypass permissions
- Make sure to document which branches are protected and why

### For Outside Users
- Outside users (collaborators without proper permissions) will:
  - Not be able to push directly to the protected branch
  - Not be able to delete the protected branch
  - Not be able to force push to the protected branch
  - Need to create a pull request to make changes (if allowed)

### Backup Best Practices
1. **Naming Convention**: Use clear naming for backup branches
   - Examples: `backup-main-20251031`, `backup-v1.0`, `protected-backup-stable`

2. **Regular Backups**: Create backups at important milestones
   - Before major releases
   - After significant feature completions
   - At the end of each sprint or development cycle

3. **Documentation**: Keep a log of backup branches
   - Date created
   - Purpose
   - What state of the project it represents

4. **Retention Policy**: Decide how long to keep backup branches
   - Keep critical milestone backups indefinitely
   - Clean up old backup branches after a certain period

## Alternative: Using GitHub Repository Settings

For even stricter control, you can also:

1. **Make the Repository Private**
   - Settings → General → Danger Zone → Change repository visibility
   - This ensures only invited collaborators can access the repository

2. **Manage Collaborator Permissions**
   - Settings → Collaborators and teams
   - Set appropriate permission levels (Read, Write, Admin)
   - Remove or downgrade collaborators who shouldn't access certain branches

3. **Use GitHub Actions for Automated Backups**
   - See the included `.github/workflows/backup-branch.yml` workflow
   - Automates the backup process on a schedule or manually

## Troubleshooting

### Issue: Cannot Set Branch Protection
**Solution**: Ensure you have admin or owner permissions for the repository.

### Issue: Users Can Still Push Despite Protection
**Solution**: Check that "Restrict who can push to matching branches" is enabled and only includes authorized users.

### Issue: Need to Bypass Protection Temporarily
**Solution**: Admin users can temporarily disable protection, make changes, and re-enable it.

## Summary

To lock a branch from outside users:
1. ✅ Create a backup branch from your protected branch
2. ✅ Set up branch protection rules in GitHub Settings → Branches
3. ✅ Enable "Restrict who can push to matching branches"
4. ✅ Add only the repository owner to the allowed list
5. ✅ Disable force pushes and deletions
6. ✅ Save and verify the protection is active

This ensures that only the repository owner can directly push to or modify the backup branch, while outside users are restricted.

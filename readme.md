# PML Combat Sim

_[Short description of the project goes here]_

---

## Table of Contents
- [About](#about)
- [Features](#features)
- [Getting Started](#getting-started)
- [Developer Notes](#developer-notes)

---

## About

_[Describe what the game/simulation is, its purpose, and any relevant context]_

---

## Features

- _[Feature 1]_
- _[Feature 2]_
- _[Feature 3]_

---

## Getting Started

### Requirements
- Unity 6.3 LTS (6000.3.11f1)
- _[Any other requirements]_

### Installation
1. Clone the repository
   ```bash
   git clone https://github.com/SWAGnDRAGON/PML_Combat_Sim.git
   ```
2. Open the project in Unity Hub
3. Open the scene: `Assets/Scenes/MainMenu.unity`
4. Press Play
5. Idk at some point once we do releases we can just tell them how to download / run the game

---


## Developer Notes

### Branching Strategy
- `main` - stable, production-ready only. Never commit directly.
- `[name]-[feature]` - one branch per feature, one developer per branch.

### Workflow Before Opening a PR
1. Fetch latest main
   ```bash
   git fetch origin
   ```
2. Rebase your branch onto main
   ```bash
   git rebase origin/main
   ```
3. Resolve any conflicts, then continue
   ```bash
   git rebase --continue
   ```
4. Force push your rebased branch
   ```bash
   git push --force-with-lease
   ```
5. Open a PR on GitHub

### Merging
- PRs use **squash merge only**  one clean commit per feature on main.
- Delete your branch after merge.
 
### GitHub Authentication (HTTPS)
This repo uses HTTPS for remote auth. You will need a **Personal Access Token (PAT)** - GitHub no longer accepts account passwords over HTTPS.
 
**One-time setup:**
1. Go to GitHub -> Settings -> Developer Settings -> Personal Access Tokens -> Tokens (classic)
2. Click **Generate new token**, give it a name, and check only the **`repo`** scope
3. Copy the token - you won't be able to see it again
4. In your terminal, set git to store credentials permanently:
   ```bash
   git config --global credential.helper store
   ```
5. Do a `git push` - when prompted, enter your **GitHub username** and paste the **PAT as the password**
6. Git will store it and never ask again on this machine
 
**If you need to update or reset stored credentials:**
```bash
git config --global --unset credential.helper
git config --global credential.helper store
```
Then push again to re-enter your credentials.

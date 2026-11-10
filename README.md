# Better Editor Toolbar for Unity 6

A set of custom editor toolbar widget for **Unity 6** to make development easier.

<img width="912" height="35" alt="{B13030B3-C034-4C04-873C-9B7384606AA8}" src="https://github.com/user-attachments/assets/d1a18bcf-dcc7-4763-a64e-b70f904d9061" />

---

## Features

* **Script Change Tracker:** Live count showing pending `.cs` file changes using Unity's Lifecycle API.
* **Asset Refresh & Compile:** Manual buttons to trigger `AssetDatabase.Refresh()` and force C# script recompilation.
* **Play First Scene:** Quickly launch scene index `0` from Build Settings after prompting to save modified scenes.
* **Scene Dropdown:** Instantly navigate to any scene in Build Settings or anywhere in `Assets/`.
* **Timescale Controls:** Direct slider and reset controls for `Time.timeScale`.

---

## Installation

### Via Unity Package Manager (Git URL)

1. Open Unity and go to **Window > Package Manager**.
2. Click the **`+`** button in the top-left corner and select **Add package from git URL...**
3. Paste the following URL:
   ```text
   https://github.com/Theo-Mestre/better-editor-toolbar.git
   ```

---

## Acknowledgement

This project has been inspired by [git-amend's video](https://www.youtube.com/watch?v=_UEtbsHAGIg&t=407s) on how to customize the toolbar. 
The scripts change counter was inspired by [this thread](https://discussions.unity.com/t/how-do-i-make-auto-refresh-turn-into-the-same-functionality-as-the-play-button/870829).

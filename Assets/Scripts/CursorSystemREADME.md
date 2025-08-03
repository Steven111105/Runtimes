# Custom Cursor System Setup Guide

This system provides a flexible way to implement custom cursors that change when hovering over interactive objects.

## Components

### 1. CursorManager.cs
- Singleton that manages all cursor changes
- Handles default, hover, and click cursor states
- Supports custom cursor textures with hotspots

### 2. InteractableObject.cs
- Attach to any GameObject to make it change the cursor on hover
- Supports custom cursors per object
- Handles pointer events (enter, exit, down, up)

### 3. InteractableButton.cs
- Enhanced button component with visual feedback
- Automatically adds InteractableObject component
- Provides color-based hover feedback

### 4. CursorSetup.cs
- Helper for creating default cursor textures
- Can generate simple cursors programmatically

## Setup Instructions

### Step 1: Create Cursor Textures
1. Create or import cursor sprites (32x32 pixels recommended)
2. For a pointing hand cursor, you can:
   - Import a hand cursor image
   - Use Unity's built-in cursor by leaving textures null
   - Create custom cursors using the CursorSetup script

### Step 2: Setup CursorManager
1. Create an empty GameObject in your scene
2. Add the `CursorManager` script
3. Assign your cursor textures:
   - Default Cursor: Normal arrow cursor
   - Hover Cursor: Pointing hand cursor
   - Click Cursor: Optional click state cursor
4. Set hotspots (usually Vector2.zero for top-left corner)

### Step 3: Make Objects Interactable
Option A - Using InteractableObject directly:
1. Add `InteractableObject` script to any UI element or GameObject
2. Configure cursor settings if using custom cursors per object

Option B - Using InteractableButton for UI buttons:
1. Add `InteractableButton` script to Button GameObjects
2. This automatically adds InteractableObject and provides visual feedback

### Step 4: Update Existing Scripts
For existing clickable objects like your ComputerScreenClick:
1. Add `[RequireComponent(typeof(InteractableObject))]` above the class
2. The cursor will automatically change on hover

## Quick Setup for Your Project

### For ComputerScreenClick.cs:
Your button will automatically get cursor changes when you:
1. Add CursorManager to your scene
2. The InteractableObject will be automatically added due to RequireComponent

### For Other UI Elements:
```csharp
// Make any GameObject interactable
InteractableButton.MakeInteractable(myGameObject);
```

### Example Cursor Hotspots:
- Arrow cursor: Vector2(0, 0) - top-left
- Hand cursor: Vector2(16, 8) - center-left for pointing finger
- Custom cursor: Adjust based on your sprite's focal point

## Tips

1. **Cursor Size**: 32x32 pixels works well for most games
2. **Hotspot**: This is where the "click point" of your cursor is
3. **Performance**: The system is lightweight and uses Unity's built-in cursor API
4. **Fallback**: If no custom cursor is provided, it falls back to system default
5. **UI Integration**: Works seamlessly with Unity's UI system and Event System

## Testing
1. Play your scene
2. Hover over objects with InteractableObject components
3. Cursor should change to indicate interactivity
4. Check console for any missing CursorManager warnings

## Troubleshooting
- Ensure EventSystem exists in your scene (usually auto-created with Canvas)
- CursorManager should be active and in the scene
- Check that cursor textures are set to "Cursor" import type in Inspector
- Verify hotspot positions for proper cursor alignment

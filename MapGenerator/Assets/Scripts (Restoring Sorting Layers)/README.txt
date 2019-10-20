When importing this tool from the Asset Store, there is a problem: sorting layers are missing, which are saved by default in the project settings.They are necessary for the tool to work properly.

To restore them, the SortingLayerRestoreManager and SortingLayerRestoreScriptableObject classes were created, which upon import will automatically add sorting layers and improve layers in prefabs only once.

If you read this from the project view and layers have been added, I recommend deleting this folder because its task has been completed and they are no longer needed.

If there are still no sorting layers in the project, you must add them manually. To fix this you need to add layers: MG_Ground, MG_Water and MG_Objects.

It will also be necessary to regenerate maps placed in test scenes. (Generate button)
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Tasks.Enums
{
    public enum Comands
    {
        Exit = 0,
        WorkWithUser = 1,
        WorkWithProject = 2,
        WorkWithTasksInProject = 3,
        WorkWithEpicTasks = 4,
        SaveAppState = 5,
        CreateUser = 6,
        ShowUsersList = 7,
        RemoveUser = 8,
        CreateProject = 9,
        ShowProjectsList = 10,
        RenameProject = 11,
        RemoveProject = 12,
        AddNewTask = 13,
        AssignUser = 14,
        UnassignUser = 15,
        ChangeTaskStatus = 16,
        ShowTasksList = 17,
        SortStatus = 18,
        RemoveTaskFromProject = 19,
        AddSubtaskToEpic = 20,
        RemoveSubtaskFromEpic = 21,
        ReturnToMainMenu = 666
    }
}

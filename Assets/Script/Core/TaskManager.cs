using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Ihaten
{

    [Serializable]
    public class Task
    {
        public Item item;
        public int amount;

        public bool isDone;
    }

    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private List<Task> taskList;

        private void OnEnable()
        {
            EventManager.OnGrandTotalCorrectE += CheckTask;
        }

        private void OnDisable()
        {
            EventManager.OnGrandTotalCorrectE -= CheckTask;
        }

        public List<Task> GetTaskList()
        {
            return taskList;
        }

        public Task GetTaskFromItemObject(Item item)
        {
            Task _tempTask = new Task();
            foreach (Task task in taskList)
            {
                if (task.item == item)
                    _tempTask = task;
            }

            return _tempTask;
        }

        public void SetTaskDone(Task task, bool status)
        {
            foreach (Task _task in taskList)
            {
                if (_task.item == task.item)
                {
                    task.isDone = status;
                }
            }
        }

        public bool IsItemInTaskList(Item item)
        {
            foreach (Task task in taskList)
            {
                if (task.item == item)
                    return true;
            }

            return false;
        }

        public void CheckTask()
        {
            foreach (Task task in taskList)
            {
                if (!task.isDone)
                    return;
            }

            // Win Condition here
            Debug.Log("You WIN!!!!");
            EventManager.OnFinish();
        }
    }
}

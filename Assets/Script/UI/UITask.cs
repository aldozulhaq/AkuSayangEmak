using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ihaten
{
    public class UITask : UIAnim
    {
        [SerializeField] GameObject taskItemParent;
        [SerializeField] GameObject taskItem;

        private TaskManager taskManager;

        protected override void Awake()
        {
            taskManager = FindObjectOfType<TaskManager>();

            CleanChildren();
            base.Awake();
        }

        public override void OnEnable()
        {
            InstantiateTaskItem();
            base.OnEnable();
        }

        public override void StartDisable()
        {
            CleanChildren();
            base.StartDisable();
        }

        void CleanChildren()
        {
            int childrenCount = taskItemParent.transform.childCount;

            for (int i = childrenCount - 1; i >= 0; i--)
            {
                DestroyImmediate(taskItemParent.transform.GetChild(i).gameObject);
            }
        }

        void InstantiateTaskItem()
        {
            foreach (var task in taskManager.GetTaskList())
            {
                GameObject _tempTask = Instantiate(taskItem, taskItemParent.transform);
                _tempTask.GetComponent<TaskItem>().SetTask(task.item, task.amount);
            }
        }
    }
}

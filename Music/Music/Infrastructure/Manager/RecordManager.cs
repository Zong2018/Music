using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Infrastructure.Manager
{
    public class RecordManager
    {
        public static string GetViewClassNameByViewModleName(string viewModelName)
        {
            return viewModelName.Substring(0, viewModelName.Length - 5);
        }

        public static Record CreateRecord(string viewName, params object[] param)
        {
            Record record = new Record(viewName, param);
            return record;
        }

        public const int MaxCount = 10;

        public static void RecordOperate(Record record)
        {
            if(CircularLinkedList.count == MaxCount)
            {
                CircularLinkedList.Remove(CircularLinkedList.head.data);
            }
            CircularLinkedList.Add(record);
            if (CircularLinkedList.count>1)
            {
                ViewModels.ToolBarViewModel.RecordButtonViewModel.CanBack = true;
            }
            ViewModels.ToolBarViewModel.RecordButtonViewModel.CanForward = false;
            CurrentNode = CircularLinkedList.tail;
        }

        public static Node<Record> CurrentNode {get;set;}

        public static void Forward()
        {
            if (CurrentNode != null && CurrentNode != CircularLinkedList.tail)
            {
                CurrentNode = CurrentNode.next;
                if (CurrentNode == CircularLinkedList.tail)
                {
                    ViewModels.ToolBarViewModel.RecordButtonViewModel.CanForward = false;
                }
                else
                {
                    ViewModels.ToolBarViewModel.RecordButtonViewModel.CanForward = true;
                }
                if (CurrentNode == CircularLinkedList.head)
                {
                    ViewModels.ToolBarViewModel.RecordButtonViewModel.CanBack = false;
                }
                else
                {
                    ViewModels.ToolBarViewModel.RecordButtonViewModel.CanBack = true;

                }
                CurrentNode.data.InvokeOperateAction();
            }
        }

        public static void Back()
        {
            if (CurrentNode != null && CurrentNode != CircularLinkedList.head)
            {
                CurrentNode = CurrentNode.previous;
                if (CurrentNode == CircularLinkedList.tail)
                {
                    ViewModels.ToolBarViewModel.RecordButtonViewModel.CanForward = false;
                }
                else
                {
                    ViewModels.ToolBarViewModel.RecordButtonViewModel.CanForward = true;
                }
                if (CurrentNode == CircularLinkedList.head)
                {
                    ViewModels.ToolBarViewModel.RecordButtonViewModel.CanBack = false;
                }
                else
                {
                    ViewModels.ToolBarViewModel.RecordButtonViewModel.CanBack = true;

                }
                CurrentNode.data.InvokeOperateAction();
            }
        }

        static  CircularLinkedList<Record> CircularLinkedList = new CircularLinkedList<Record>();
    }

    public class Record
    {
        public Record()
        {
        }

        public Record(string viewClassName, object[] param)
        {
            this.MainViewClassName = viewClassName;
            this.Param = param;
        }
        public string MainViewClassName { get; set; }

        public object[] Param { get; set; }

        public delegate object OperateAction(string key,params object[] param);

        public event OperateAction OperateActionHandler;

        public void InvokeOperateAction()
        {
            OperateActionHandler?.Invoke(MainViewClassName,Param);
        }

        public Record CreateOperateAction()
        {
            this.OperateActionHandler += (n, o) =>
            {
                return Infrastructure.Manager.UserControlManager.CreateIntanceAndNotifyUI(n);
            };

            return this;
        }

        public Record CreateOperateFunction()
        {
            this.OperateActionHandler += (n, o) =>
            {
                if (o.Length == 1)
                {
                    return Infrastructure.Manager.UserControlManager.CreateViewIntance(n, o);
                }
                
                if (o.Length >= 2 && o[1] is bool)
                {
                    if ((bool)o[1])
                    {
                        var view = Infrastructure.Manager.UserControlManager.CreateViewIntance(n, o[0]);
                        ViewModels.MainWindowViewModel.MainContent.SelectedItem = view;
                    }
                }
                return null;
            };

            return this;
        }

        public Record CreateOperateMultipleFunction()
        {
            this.OperateActionHandler += (n, o) =>
            {
                Infrastructure.Manager.UserControlManager.CreateIntanceAndNotifyUI(n);
                if (o.Length < 2) return null;
                var view = Infrastructure.Manager.UserControlManager.CreateViewIntance(o[0].ToString(), o[1]);
                if (o.Length == 3 && o[2] is bool)
                {
                    if ((bool)o[2])
                    {
                        ViewModels.MainWindowViewModel.MainContent.SelectedItem = view;
                    }
                }
                return view;
            };

            return this;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 继承arraylist，并扩展其功能：添加 和  检索，过时项
/// 问题：也继承了所有Arraylist的公有成员，让使用者很容易就绕过这个类实现的时间限制方法
/// 
/// 1.取消继承，子类中创建父类的一个字段，并在构造函数中初始化为父类类型
/// 2.让子类代码使用这个新定义的字段替换base继承自父类的方法
/// 3.通过委托提供父辈的成员
/// </summary>
namespace RepalceInheritanceWithDelegation
{
    public interface PerishableItem
    {
         DateTime CreationTime { get; set; }
    }


    public class PerishableContainer ////1.public class PerishableContainer : ArrayList
    {
        private ArrayList list;////1.创建父类的一个字段
        private int perishableIntervalInSeconds;
        /// 3.通过委托提供父辈的成员
        public int Count { get => list.Count; }

        public PerishableContainer()
        {
            list = new ArrayList();////1.在构造函数中初始化为父类类型
        }

        public int PerishableIntervalInSeconds { get => perishableIntervalInSeconds; set => perishableIntervalInSeconds = value; }

        public void LeaveInStorage(PerishableItem item)
        {
            list.Add(item); //list.Add(item);////base.Add(item);////2.让子类代码使用这个新定义的字段替换base继承自父类的方法
        }

        private bool HasPerished(PerishableItem item)
        {
            if(item.CreationTime.AddSeconds(perishableIntervalInSeconds) >DateTime.Now)
            {
                return false;
            }
            return true;
        }

        private PerishableItem TakeOldestFromStorage()
        {
            if (list.Count > 0) //(base.Count > 0)////2.让子类代码使用这个新定义的字段替换base继承自父类的方法
            {
                PerishableItem item = (PerishableItem)list[0];////base[0];////2.让子类代码使用这个新定义的字段替换base继承自父类的方法
                list.Remove(item); ////base.Remove(item);////2.让子类代码使用这个新定义的字段替换base继承自父类的方法
                if (!HasPerished(item)) return item;
            }
            return null;
        }

        public PerishableItem TakeFromStorage()
        {
            PerishableItem item = null;
            while (list.Count > 0)////(base.Count > 0)////2.让子类代码使用这个新定义的字段替换base继承自父类的方法
            {
                item = TakeOldestFromStorage();
                if (item != null) break;                
            }
            return item;
        }        
    }
}

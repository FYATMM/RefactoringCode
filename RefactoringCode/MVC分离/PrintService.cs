using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 核心类是一个PrintService抽象类，声明了抽象PrintJob方法
/// 具体的打印机子类来实现PrintJob方法；每当要添加支持的设备时，需要在类的层次结构中添加一个兄弟类，实现特定的PrintJob方法
/// 
/// 问题：
/// 1.连接到不同设备，必须实例化不同子类；而且由于多态，仍可以访问特定设备不同的打印服务
/// 2.支持新设备，必须向类层次结构中添加一个新类，使用者必须知道这个类名；而且要重新编译
/// 3.灵活不够，一个设备对应一个打印系统，如果同时连接多个设备，必须为每个设备实例化一个不同的打印服务
/// 4.从问题域的角度，父类子类不是特别紧密的联系在一起，子类没有重写任何父类的方法，除了特定设备对应的PrintJob方法
///     而且，还添加了很多特定设备的方法，让人怀疑他们之间的关系不是is a关系
///5.如果想重用这两台打印机的代码，必须带上PrintService，不灵活
///=》更适合让这两台打印机作为PrintService的委托（为了与物理设备通信）
/// </summary>
/// 
/// <summary>
/// 1.提取接口
///     IPrintDevice
/// 2.用委托代替继承
/// 3.提取超类（抽象类）
/// </summary>
namespace RepalceInheritanceWithDelegation
{
    public enum ServiceState
    {
        Idle,
        Processing,
        Stopped
    }

    public class PrintJob
    {
        public PrintJob()
        {
            Console.WriteLine("I'm printjob instance");
        }
    }

    /// <summary>
    ///1. 提取接口
    /// </summary>
    public interface IPrintDevice
    {
        void PrintJob(PrintJob printJob);
    }

    ////public abstract class PrintService////1.不再通过继承了，而是接口
    public class PrintService : IPrintDevice
    {
        //2.创建要委托的实例，通过接口实现不同设备生成不同实例
        private IPrintDevice device;
        public IList<PrintJob> JobsInQueue { get; set; }
        public ServiceState ServiceState { get; set;  }

        public PrintJob CreatePringJob()
        {
            return new PrintJob();
        }
        private void Print()
        {
            while (JobsInQueue.Count > 0)
            {
                PrintJob(JobsInQueue[0]);
            }

        }
        ////protected abstract void PrintJob(PrintJob job);        ////2.不再在继承体系中了，通过代理实现
        public void PrintJob(PrintJob job)
        {
            device.PrintJob( job);
        }
    }

    /// <summary>
    /// 3.提取超类（抽象类）
    /// </summary>
    public abstract class PrintDevice : IPrintDevice
    {
        public abstract bool Initialized { get; set; }
        public void PrintJob(PrintJob job)
        {
            if (!Initialized) Initialize();
            StartDocument();
            Stream renderedDocument = RenderDocument(job);
            WriteDocumentToDevice(renderedDocument);
            EndDocument();
        }
        protected abstract Stream RenderDocument(PrintJob job);
        protected abstract void WriteDocumentToDevice(Stream data);
        protected abstract void Initialize();
        protected abstract void StartDocument();
        protected abstract void EndDocument();
    }


    public class HPLaserJet : PrintDevice
    {
        public override bool Initialized { get; set; }

        //protected override void PrintJob(PrintJob job)
        //{
        //    if (!Initialized) Initialize(); 
        //    StartDocument();
        //    Stream renderedDocument = RenderDocument(job);
        //    WriteDocumentToDevice(renderedDocument);
        //    EndDocument();
        //}
         protected override Stream RenderDocument(PrintJob job)
        {
            //device specific code
            throw new NotImplementedException();
        }

        protected override void WriteDocumentToDevice(Stream data)
        {
            throw new NotImplementedException();
        }

        protected override void Initialize()
        {
            throw new NotImplementedException();
        }

        protected override void StartDocument()
        {
            throw new NotImplementedException();
        }

        protected override void EndDocument()
        {
            throw new NotImplementedException();
        }
    }


    public class LexmarkX500 : PrintDevice
    {
        public override bool Initialized { get; set; }

        protected override void EndDocument()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize()
        {
            throw new NotImplementedException();
        }

        protected override Stream RenderDocument(PrintJob job)
        {
            throw new NotImplementedException();
        }

        protected override void StartDocument()
        {
            throw new NotImplementedException();
        }

        protected override void WriteDocumentToDevice(Stream data)
        {
            throw new NotImplementedException();
        }
    }
}

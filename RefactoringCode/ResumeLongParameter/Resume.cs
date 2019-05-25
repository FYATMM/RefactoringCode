using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeLongParameter
{
    public class ContactInfo
    {
        public ContactInfo(string email, string phone, string address)
        {
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
    }

    public class Candidate
    {
        public ContactInfo ContactInfo { get; }

        public Candidate(string account, string name, DateTime birthday, ContactInfo contactInfo)
        {
            Account = account;
            Name = name;
            Birthday = birthday;
            ContactInfo = contactInfo;
        }

        public string Account { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }

        public void AddCandidate()
        {
            ////throw new NotImplementedException();
            Console.WriteLine($"account:{this.Account},name:{this.Name},birthday:{this.Birthday},email:{ContactInfo.Email},phone:{ContactInfo.Phone},address:{ContactInfo.Address}");
        }
    }

    public class Resume
    {
        public void Scan()
        {
            new Candidate("91chen", "Fengyi", new DateTime(1987,9,1), new ContactInfo("123456@163.com", "12345678901", "Qingdao")).AddCandidate();
        }

        ////V1.0 坏味道：lang parameters
        /*
       public void Scan()
        {
            AddCandidate("91chen",
                "Fengyi",
                new DateTime(1987,9,1),
                "123456@163.com",
                "12345678901",
                "Qingdao" );
        }

        public void AddCandidate(string account, string name, DateTime birthday, string email, string phone, string address)
        {
            ////throw new NotImplementedException();
            Console.WriteLine($"account:{account},name:{name},birthday:{birthday},email:{email},phone:{phone},address:{address}");
        }
         */

        /*V2.0  选中要减少参数的函数名，右击，RefactorThis->TransferParameters
         增加了一个Candidate类来收集这些参数，使其变为属性，现在AddCondidate只需要1个candidate对象来传递参数
         public class Candidate
    {
        public Candidate(string account, string name, DateTime birthday, string email, string phone, string address)
        {
            Account = account;
            Name = name;
            Birthday = birthday;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string Account { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
    }

    public class Resume
    {
        public void Scan()
        {
            AddCandidate(new Candidate("91chen", 
            "Fengyi", new DateTime(1987,9,1), 
            "123456@163.com", 
            "12345678901", 
            "Qingdao"));
        }

        public void AddCandidate(Candidate candidate)
        {
            ////throw new NotImplementedException();
            Console.WriteLine($"account:{candidate.Account},name:{candidate.Name},birthday:{candidate.Birthday},email:{candidate.Email},phone:{candidate.Phone},address:{candidate.Address}");
        }
         */

        /*V2.1          坏味道：feature envy特性依赖；resume类的方法为什么要candidate类的对象来实现？应该交给candidate来做就好,这样这些参数就是candidate内的属性
         选中要要移动的函数名，右击，RefactorThis->Move instanse method
            public class Candidate
    {
        public Candidate(string account, string name, DateTime birthday, string email, string phone, string address)
        {
            Account = account;
            Name = name;
            Birthday = birthday;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string Account { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }

        public void AddCandidate()
        {
            ////throw new NotImplementedException();
            Console.WriteLine($"account:{this.Account},name:{this.Name},birthday:{this.Birthday},email:{this.Email},phone:{this.Phone},address:{this.Address}");
        }
    }

    public class Resume
    {
        public void Scan()
        {
            new Candidate("91chen", "Fengyi", new DateTime(1987,9,1), "123456@163.com", "12345678901", "Qingdao").AddCandidate();
        }
         */

        /*V3.0 抽取类，把一类信息或方法抽取出来 ExtractClass，抽取出来后在candidate类里加了一个字段，同时参数也变成了这个字段对象的属性
         再通过封装字段变成一个属性，EncapsulateField，同时参数也变成了这个属性对象的属性，与其它参数属性比较像了
             public class ContactInfo
    {
        public ContactInfo(string email, string phone, string address)
        {
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
    }

    public class Candidate
    {
        public ContactInfo ContactInfo { get; }

        public Candidate(string account, string name, DateTime birthday, string email, string phone, string address)
        {
            Account = account;
            Name = name;
            Birthday = birthday;
            ContactInfo = new ContactInfo(email, phone, address);
        }

        public string Account { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }

        public void AddCandidate()
        {
            ////throw new NotImplementedException();
            Console.WriteLine($"account:{this.Account},name:{this.Name},birthday:{this.Birthday},email:{ContactInfo.Email},phone:{ContactInfo.Phone},address:{ContactInfo.Address}");
        }
    }

    public class Resume
    {
        public void Scan()
        {
            new Candidate("91chen", "Fengyi", new DateTime(1987,9,1), "123456@163.com", "12345678901", "Qingdao").AddCandidate();
        }
         */

        /*V4.0 希望candidate传入参数是，用contactinfo对象，而不是独立的3个参数
         提取参数，选中new 的对象及其后面的参数，introduce parameter
         
         */
    }
}

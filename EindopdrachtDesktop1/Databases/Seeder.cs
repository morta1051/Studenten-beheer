using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindopdrachtDesktop1.Model;

namespace EindopdrachtDesktop1.Databases
{
    internal class Seeder
    {
        internal  void TestData()
        {
            using Dbcontext db = new Dbcontext();
            db.Students.AddRange(new List<Student>
                {
                    new Student { Name = "Mortazah Alizada", Number = 619654526, Email = "test2gmail.com", StudentNumber = 12 }
                });
            db.SaveChanges();
        }


    }
}

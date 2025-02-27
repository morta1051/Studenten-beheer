using MenuNavigatie.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using EindopdrachtDesktop1.Model;


namespace EindopdrachtDesktop1.Model
{
    [Table("courses")]
    [Index(nameof(Id),nameof(CourseName), Name = "courses")]
    class Course : ObservableObject
    {
        [Key]
        public int Id { get; set; }
        private string _coursename;
 

        [StringLength(255), Required]
        public string CourseName
        {
            get { return _coursename; }
            set
            {
                _coursename = value;
                OnPropertyChanged();
            }
        }

        public ICollection<Student> Students { get; set; }
    }
}

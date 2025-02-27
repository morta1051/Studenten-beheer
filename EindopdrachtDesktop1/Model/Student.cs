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
    [Table("students")]
    [Index(nameof(Id), nameof(Name), nameof(Number), nameof(Email), nameof(StudentNumber), Name = "students")]
    class Student : ObservableObject
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        private string _name;
        private int _number;
        private string _email;
        private int _studentNumber;
        private int _courseId;

        [StringLength(255), Required]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        [MaxLength(11), Required]
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }
        [StringLength(255), Required]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        [Required]
        public int StudentNumber
        {
            get { return _studentNumber; }
            set
            {
                _studentNumber = value;
                OnPropertyChanged();
            }
        }
        [ForeignKey("Course"), Required]
        public int CourseID
        {
            get { return _courseId; }
            set
            {
                _courseId = value;
                OnPropertyChanged();
            }
        }
        public Course Course { get; set; }
        
    }
}

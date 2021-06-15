using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "{0} is required")]//Anotation para tornar campo obrigatorio
        [StringLength(60, MinimumLength =3,ErrorMessage ="{0} must be between {2} and {1}") ] //anotation para tamanho
        public string Name { get; set; }
        [DataType (DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "Enter a valid {0}")]
        public string Email { get; set; }
        [Display (Name = "Birth Date")] //Anotation#1
        [DataType (DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime BirthDate { get; set; }
        [Display (Name = "Base Salary")]//Anotation
        [DisplayFormat(DataFormatString = "{0:F2}")] // 0(valor do atributo):F2(formatacao) o . para separar e por causa da localizacao nos EUA
        [Range(100.0, 50000.0,ErrorMessage ="{0} must be between {1} and {2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; } //Obrigado a ser colocado portanto nao precisa colocar anotation
        public ICollection<SalesRecord> Sales = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Data
{
    public class SeedingService
    {
        private SalesWebMvcContext _context;

        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return; // DB ja foi populado
            }
            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Electronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Bob Brown", "Bob@gmail.com", new DateTime(1998, 08, 25), 3500.00, d1);
            Seller s2 = new Seller(2, "Neymar Junior", "Ney@gmail.com", new DateTime(1990, 05, 30), 1200.00, d3);
            Seller s3 = new Seller(3, "Paulo Ganso", "Ph@gmail.com", new DateTime(1990, 05, 05), 1200.00, d2);
            Seller s4 = new Seller(4, "Olavo de Carvalho", "oolavo@gmail.com", new DateTime(1987, 04, 29), 1200.00, d4);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2021, 04, 25), 3800.00, SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2021, 04, 20), 3800.00, SaleStatus.Billed, s4);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2021, 04, 23), 3900.00, SaleStatus.Canceled, s3);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2021, 07, 07), 3100.00, SaleStatus.Pending, s2);

            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4);
            _context.SalesRecord.AddRange(r1, r2, r3, r4);

            _context.SaveChanges();




        }

    }
}

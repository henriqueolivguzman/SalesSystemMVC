using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minimum, DateTime? maximum) //Feito dessa forma opcional(?) pois e possivel colocar apenas uma data 
        {
            var result = from obj in _context.SalesRecord select obj;  //Traz toda a sessao Sales record em sintaxe SQL, Objeto IQuerable,construido a partir do Db Context para consultas
            if (minimum.HasValue)
            {
                result = result.Where(x => x.Date >= minimum);
            }
            if (maximum.HasValue)
            {
                result.Where(x => x.Date <= maximum);
            }

            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }
        public async Task InsertAsync(Seller obj)       //No lugar de void foi colocado simplesmente Task   
        {            
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(a=>a.Department).FirstOrDefaultAsync(x => x.Id == id);  //FirstorDefault que acessa efetivamente o database
        }
        public async Task RemoveAsync(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();      //Em save changes que ocorre realmente a operacao de acesso ao banco de dados, portanto e nela que o async fica

        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny) 
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException dbexception)
            {
                throw new DbConcurrencyException(dbexception.Message);
            }
        }
    }
}

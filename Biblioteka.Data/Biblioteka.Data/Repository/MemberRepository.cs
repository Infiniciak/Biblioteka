using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library
{
    public interface IMemberRepository
    {
        Task<IEnumerable<MembersDto>> GetAllMembersAsync();
        Task<MembersDto?> GetByIdAsync(int id);
        Task<MembersDto?> GetByCardNumberAsync(string cardNumber);  // Added to interface
        Task AddAsync(MembersDto member);
        Task UpdateAsync(MembersDto member);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}

namespace Library
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;

        public MemberRepository(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }

        public async Task<IEnumerable<MembersDto>> GetAllMembersAsync()
        {
            return await _context.Members
                .Include(m => m.Borrow)
                .ThenInclude(b => b.Book)
                .ToListAsync();
        }

        public async Task<MembersDto?> GetByIdAsync(int id)
        {
            return await _context.Members
                .Include(m => m.Borrow)
                .ThenInclude(b => b.Book)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<MembersDto?> GetByCardNumberAsync(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Card number cannot be empty", nameof(cardNumber));

            return await _context.Members
                .FirstOrDefaultAsync(m => m.CardNumber == cardNumber);
        }

        public async Task AddAsync(MembersDto member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MembersDto member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));


            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var member = await GetByIdAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Members.AnyAsync(c => c.Id == id);
        }
    }
}
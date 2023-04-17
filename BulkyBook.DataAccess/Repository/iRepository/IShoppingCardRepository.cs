using BulkyBook.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.iRepository
{
    public interface IShoppingCardRepository : iRepository<ShoppingCard>
    {
        int IncrementCount (ShoppingCard shoppingCard, int count );
        int DecrementCount (ShoppingCard shoppingCard, int count );
    }
}

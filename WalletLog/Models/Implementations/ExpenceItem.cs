using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletLog.Models
{
    public class ExpenseItem
    {
        public string Name { get; set; } = string.Empty;

        public int Amount { get; set; } = 0;
        public ExpenseItem()
        {

        }
    }
}

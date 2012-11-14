using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yearnly.Model
{
    public partial class UserList
    {
        public string ListUrl
        {
            get
            {
                String lowerName = this.Name.Replace(" ", "-").ToLower();
                return String.Format("{0}-{1}", this.Id, lowerName);
            }
        }
    }
}

using Dapper;

namespace Twitta.Website.Models
{
    public partial class User
    {
        [Editable(false)]
        public long Id
        {
            get { return UserId; }
            set { UserId = value; }
        }
    }
}
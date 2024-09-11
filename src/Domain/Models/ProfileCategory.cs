using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Models
{
    public class ProfileCategory
    {
        public long Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string? Title { get; set; }

        public ICollection<ProfileCategoryList> ProfileCategoryLists { get; set; }



        public string? PrivacyTitle { get; set; }
        public bool Authorize { get; set; }
        public string? TokenKey { get; set; }
    }
}

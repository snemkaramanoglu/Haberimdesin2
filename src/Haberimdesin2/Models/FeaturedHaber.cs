using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haberimdesin2.Models
{
    public class FeaturedHaber
    {
        public int HaberId { get; set; }
        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }

        public string [] Images { get; set; }
        public string Id { get; set; }

        public string Title { get; set; }

        public string HeadLine { get; set; }

        public string Detail { get; set; }

        public string PrimaryImgURL { get; set; }
        public DateTime TimeStamp { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
        public int CategoryID { get; set; }

        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserImageURL { get; set; }
    }
}

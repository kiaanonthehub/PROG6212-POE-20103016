using System;
using System.Collections.Generic;

#nullable disable

namespace PlannerLibrary.DbModels
{
    public partial class TblSemester
    {
        public TblSemester()
        {
            TblTrackStudies = new HashSet<TblTrackStudy>();
        }

        public int SemesterId { get; set; }
        public DateTime StartDate { get; set; }
        public int? NumberOfWeeks { get; set; }

        public virtual ICollection<TblTrackStudy> TblTrackStudies { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PlannerLibrary.DbModels
{
    public partial class TblStudent
    {
        public TblStudent()
        {
            TblStudentModules = new HashSet<TblStudentModule>();
            TblTrackStudies = new HashSet<TblTrackStudy>();
        }

        public int StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string StudentEmail { get; set; }
        public string StudentHashPassword { get; set; }

        public virtual ICollection<TblStudentModule> TblStudentModules { get; set; }
        public virtual ICollection<TblTrackStudy> TblTrackStudies { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubjectToProf
    {
        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public int ProfessorID { get; set; }
        public Nullable<int> rating { get; set; }
    
        public virtual Professor Professor { get; set; }
        public virtual Subject1 Subject1 { get; set; }
    }
}

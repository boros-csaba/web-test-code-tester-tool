//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebTest.Test
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProblemSets
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProblemSets()
        {
            this.OrderedProblems = new HashSet<OrderedProblems>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedProblems> OrderedProblems { get; set; }
    }
}

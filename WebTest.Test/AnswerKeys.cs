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
    
    public partial class AnswerKeys
    {
        public int AnswerKeyId { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public bool FixedRowOrder { get; set; }
        public int ProblemId { get; set; }
    
        public virtual Problems Problem { get; set; }
    }
}
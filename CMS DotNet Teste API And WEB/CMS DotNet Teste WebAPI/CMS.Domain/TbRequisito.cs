//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMS.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class TbRequisito
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Vaga_Id { get; set; }
    
        public virtual TbVaga TbVaga { get; set; }
    }
}


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace FBF.Service.DBService
{

using System;
    using System.Collections.Generic;
    
public partial class tblProductFile
{

    public long ID { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public Nullable<long> ProductId { get; set; }



    public virtual tblProduct tblProduct { get; set; }

}

}
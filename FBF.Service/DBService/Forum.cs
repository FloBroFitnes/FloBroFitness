
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
    
public partial class Forum
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Forum()
    {

        this.Comments = new HashSet<Comment>();

    }


    public long ForumId { get; set; }

    public string Name { get; set; }

    public Nullable<System.DateTime> CreatedOn { get; set; }

    public bool IsActive { get; set; }

    public string Body { get; set; }

    public Nullable<long> TypeId { get; set; }

    public string Activity { get; set; }

    public Nullable<int> UserView { get; set; }

    public Nullable<long> UserId { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ForumType ForumType { get; set; }

    public virtual tblUser tblUser { get; set; }

}

}

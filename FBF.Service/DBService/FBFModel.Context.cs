﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class FloBroFitnessEntities : DbContext
{
    public FloBroFitnessEntities()
        : base("name=FloBroFitnessEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<tblCategory> tblCategories { get; set; }

    public virtual DbSet<tblProduct> tblProducts { get; set; }

    public virtual DbSet<tblProductFile> tblProductFiles { get; set; }

    public virtual DbSet<tblStatu> tblStatus { get; set; }

    public virtual DbSet<tblUser> tblUsers { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Forum> Fora { get; set; }

    public virtual DbSet<ForumType> ForumTypes { get; set; }

    public virtual DbSet<tblBlog> tblBlogs { get; set; }

    public virtual DbSet<tblBlogComment> tblBlogComments { get; set; }

}

}


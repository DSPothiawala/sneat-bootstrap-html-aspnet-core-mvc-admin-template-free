﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public bool? IsActive { get; set; }

    public string UserEmail { get; set; }

    public DateTime? Creatdate { get; set; }

    public string CreateUser { get; set; }

    public int? TypeId { get; set; }

    public string Phone { get; set; }

    public bool IsAdmin { get; set; }

    public string AndroidId { get; set; }

    public string AndroidDevice { get; set; }

    public bool? IsBiometric { get; set; }

    public bool? IsAutoSignIn { get; set; }

    public int? Pin { get; set; }

    public bool? IsScreenShot { get; set; }

    public int? CompanyId { get; set; }

    public int? SpersonId { get; set; }

    public int? LocationId { get; set; }
}
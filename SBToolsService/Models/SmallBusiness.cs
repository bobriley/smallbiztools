using System;
using System.Collections.Generic;

namespace SBToolsService.Models;

public partial class SmallBusiness
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public double? Sales { get; set; }

    public double? OwnerSalary { get; set; }

    public double? Depreciation { get; set; }

    public double? Interest { get; set; }

    public double? OwnerPersonalExpenses { get; set; }

    public double? Utilities { get; set; }

    public double? Rent { get; set; }

    public double? Payroll { get; set; }

    public double? MiscExpenses { get; set; }

    public double? Sdemultiple { get; set; }

    public double? SellableInventory { get; set; }

    public double? AskingPrice { get; set; }

    public double? Sde { get; set; }

    public double? HealthRatio { get; set; }

    public double? Sdevaluation { get; set; }

    public double? PriceDelta { get; set; }
}

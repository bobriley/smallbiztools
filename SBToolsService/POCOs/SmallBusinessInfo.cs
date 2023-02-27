namespace SBToolsService.POCOs
{
    public class SmallBusinessInfo
    {
        public string Name { get; set; } = string.Empty;

        public float Sales { get; set; }

        // Addbacks
        public float OwnerSalary { get; set; }
        public float Depreciation {  get; set; }
        public float Interest { get; set; }
        public float OwnerPersonalExpenses { get; set; }

        // Expenses
        public float Utilities { get; set; }
        public float Rent { get; set; }
        public float EmployeePayments { get; set; }
        public float MiscExpensese { get; set; }

        // Valuation Affectors
        public float SDEMultiple { get; set; }
        public float SellableInventory { get; set; }

        public float AskingPrice { get; set; }
    }
}
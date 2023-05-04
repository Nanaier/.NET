using System;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating the organizational structure
            Client client = new Client();
            Corporation enterprise = new Corporation("Enterprise");

            StructuralSubdivision salesDivision = new StructuralSubdivision("SD-SALES", "Sales Division");
            client.AddIfComposite(salesDivision, new Position("Sales Manager", 3, 5000.0M));
            client.AddIfComposite(salesDivision, new Position("Sales Executive", 6, 3000.0M));
            client.AddIfComposite(salesDivision, new Position("Sales Assistant", 12, 2000.0M));

            StructuralSubdivision marketingDivision = new StructuralSubdivision("SD-MARKETING", "Marketing Division");
            client.AddIfComposite(marketingDivision, new Position("Marketing Manager", 3, 5000.0M));
            client.AddIfComposite(marketingDivision, new Position("Marketing Executive", 6, 3000.0M));
            client.AddIfComposite(marketingDivision, new Position("Marketing Assistant", 12, 2000.0M));

            StructuralSubdivision hrDivision = new StructuralSubdivision("SD-HR", "HR Division");
            Position managerToDelete = new Position("HR Manager", 3, 5000.0M);
            client.AddIfComposite(hrDivision, managerToDelete);
            client.AddIfComposite(hrDivision, new Position("HR Executive", 6, 3000.0M));
            client.AddIfComposite(hrDivision, new Position("HR Assistant", 12, 2000.0M));

            StructuralSubdivision engineeringDivision = new StructuralSubdivision("SD-ENGINEERING", "Engineering Division");
            client.AddIfComposite(engineeringDivision, new Position("Engineering Manager", 3, 5000.0M));
            client.AddIfComposite(engineeringDivision, new Position("Software Executive", 12, 4000.0M));
            client.AddIfComposite(engineeringDivision, new Position("Hardware Assistant", 12, 4000.0M));

            StructuralSubdivision productionDivision = new StructuralSubdivision("SD-PRODUCTION", "Production Division");
            client.AddIfComposite(productionDivision, new Position("Production Manager", 3, 5000.0M));
            client.AddIfComposite(productionDivision, new Position("Production Supervisor", 6, 3000.0M));
            client.AddIfComposite(productionDivision, new Position("Production Worker", 12, 2000.0M));


            client.AddIfComposite(enterprise, salesDivision);
            client.AddIfComposite(enterprise, marketingDivision);
            client.AddIfComposite(enterprise, hrDivision);
            client.AddIfComposite(enterprise, engineeringDivision);
            client.AddIfComposite(enterprise, productionDivision);

            // Displaying the staff schedule for the enterprise
            enterprise.Display();

            client.RemoveIfComposite(hrDivision, managerToDelete);
            client.RemoveIfComposite(enterprise, productionDivision);

            Console.WriteLine("\n/--------------------------------------------------------/\n");

            enterprise.Display();
        }
    }
}

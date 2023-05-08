using System;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating the organizational structure
            Client client = new Client();
            Staff enterprise = new StructuralSubdivision("ENT", "Enterprise");

            Staff productionDivision = new StructuralSubdivision("SD-PRODUCTION", "Production Division");
            client.AddIfComposite(productionDivision, new Position("Production Manager", 3, 4000.0M));
            client.AddIfComposite(productionDivision, new Position("Production Supervisor", 6, 2000.0M));
            client.AddIfComposite(productionDivision, new Position("Production Worker", 12, 1000.0M));

            Staff salesDivision = new StructuralSubdivision("SD-SALES", "Sales Division");
            client.AddIfComposite(salesDivision, new Position("Sales Manager", 4, 5000.0M));
            client.AddIfComposite(salesDivision, new Position("Sales Executive", 2, 3000.0M));
            client.AddIfComposite(salesDivision, new Position("Sales Assistant", 11, 2000.0M));

            Staff hrDivision = new StructuralSubdivision("SD-HR", "HR Division");
            Position managerToDelete = new Position("HR Manager", 3, 6500.0M);
            client.AddIfComposite(hrDivision, managerToDelete);
            client.AddIfComposite(hrDivision, new Position("HR Executive", 7, 4000.0M));
            client.AddIfComposite(hrDivision, new Position("HR Assistant", 4, 2000.0M));

            Staff engineeringDivision = new StructuralSubdivision("SD-ENGINEERING", "Engineering Division");
            client.AddIfComposite(engineeringDivision, new Position("Engineering Manager", 3, 5000.0M));
            client.AddIfComposite(engineeringDivision, new Position("Software Executive", 12, 4000.0M));
            client.AddIfComposite(engineeringDivision, new Position("Hardware Assistant", 12, 4000.0M));

            Staff marketingDivision = new StructuralSubdivision("SD-MARKETING", "Marketing Division");
            client.AddIfComposite(marketingDivision, new Position("Marketing Manager", 1, 5000.0M));
            client.AddIfComposite(marketingDivision, new Position("Marketing Executive", 3, 3000.0M));
            client.AddIfComposite(marketingDivision, new Position("Marketing Assistant", 15, 2000.0M));

            client.AddIfComposite(productionDivision, marketingDivision);
            client.AddIfComposite(salesDivision, hrDivision);

            client.AddIfComposite(enterprise, salesDivision);
            client.AddIfComposite(enterprise, engineeringDivision);
            client.AddIfComposite(enterprise, productionDivision);

            enterprise.Display();

            Console.WriteLine("\n/--------------------------------------------------------/\n");

            //client.RemoveIfComposite(hrDivision, managerToDelete);
            client.RemoveIfComposite(salesDivision, hrDivision);
            client.RemoveIfComposite(enterprise, productionDivision);

            enterprise.Display();

        }
    }
}

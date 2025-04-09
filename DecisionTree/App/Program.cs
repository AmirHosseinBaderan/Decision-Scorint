using App.Context;
using App.Domain;
using App.Evaluator;
using App.Repository;

using var context = new DecisionTreeDbContext();
var repository = new DecisionTreeRepository(context);
var evaluator = new DecisionTreeEvaluator(repository);

while (true)
{
    Console.WriteLine("\nChoose an action:");
    Console.WriteLine("1. Add a new decision tree");
    Console.WriteLine("2. Update a node condition");
    Console.WriteLine("3. Update a node formula");
    Console.WriteLine("4. Show all nodes in a tree");
    Console.WriteLine("5. Evaluate a tree with sample data");
    Console.WriteLine("6. Exit");

    Console.Write("Enter your choice: ");
    int choice = int.Parse(Console.ReadLine());

    switch (choice)
    {
        case 1:
            Console.WriteLine("Creating a new tree...");
            var root = new DecisionNode
            {
                Condition = "purchases > 5",
                TrueBranch = new DecisionNode
                {
                    Condition = "spending > 100",
                    TrueBranch = new DecisionNode { Formula = "50 + (0.2 * spending)" },
                    FalseBranch = new DecisionNode { Formula = "30 + (0.1 * spending)" }
                },
                FalseBranch = new DecisionNode { Formula = "10 + (3 * purchases)" }
            };
            repository.AddTree(root);
            Console.WriteLine("Tree created successfully!");
            break;

        case 2:
            Console.Write("Enter the node ID to update: ");
            int nodeId = int.Parse(Console.ReadLine());
            Console.Write("Enter the new condition: ");
            string newCondition = Console.ReadLine();
            repository.UpdateNodeCondition(nodeId, newCondition);
            Console.WriteLine("Condition updated successfully.");
            break;

        case 3:
            Console.Write("Enter the node ID to update: ");
            int formulaNodeId = int.Parse(Console.ReadLine());
            Console.Write("Enter the new formula: ");
            string newFormula = Console.ReadLine();
            repository.UpdateNodeFormula(formulaNodeId, newFormula);
            Console.WriteLine("Formula updated successfully.");
            break;

        case 4:
            Console.Write("Enter the Tree ID: ");
            int treeIdToShow = int.Parse(Console.ReadLine());
            var nodes = repository.GetAllNodes(treeIdToShow);
            foreach (var node in nodes)
            {
                Console.WriteLine($"ID: {node.Id}, Condition: {node.Condition}, Formula: {node.Formula}, TrueBranchId: {node.TrueBranchId}, FalseBranchId: {node.FalseBranchId}");
            }
            break;

        case 5:
            Console.Write("Enter the Tree ID to evaluate: ");
            int evalTreeId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter customer data (e.g., purchases:5,spending:50):");
            string[] data = Console.ReadLine().Split(',');
            var customerData = data.Select(d => d.Split(':')).ToDictionary(k => k[0], v => double.Parse(v[1]));
            double result = evaluator.EvaluateTree(evalTreeId, customerData);
            Console.WriteLine($"Evaluated score: {result}");
            break;

        case 6:
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid choice!");
            break;
    }
}

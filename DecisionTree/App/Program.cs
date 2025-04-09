using App.Builder;
using App.Context;
using App.Evaluator;
using App.Repository;

using var context = new DecisionTreeDbContext();
var repository = new DecisionTreeRepository(context);
var evaluator = new DecisionTreeEvaluator(repository);
var builder = new DecisionTreeBuilder(repository);

while (true)
{
    Console.WriteLine("\nChoose an action:");
    Console.WriteLine("1. Add a new decision tree");
    Console.WriteLine("2. Update an existing decision node condition");
    Console.WriteLine("3. Show all decision nodes for a tree");
    Console.WriteLine("4. Run and test a decision tree");
    Console.WriteLine("5. Exit");

    Console.Write("Enter your choice: ");
    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Invalid input.");
        continue;
    }

    switch (choice)
    {
        case 1:
            // Add a new decision tree dynamically
            builder.AddNewTree();
            break;

        case 2:
            // Update an existing node condition
            Console.Write("Enter the Node ID to update: ");
            int nodeId = int.Parse(Console.ReadLine());
            Console.Write("Enter the new condition: ");
            string newCondition = Console.ReadLine();
            repository.UpdateNodeCondition(nodeId, newCondition);
            Console.WriteLine("Condition updated successfully.");
            break;

        case 3:
            // Show all decision nodes
            Console.Write("Enter the Tree ID: ");
            int treeIdToShow = int.Parse(Console.ReadLine());
            var nodes = repository.GetAllNodes(treeIdToShow);

            Console.WriteLine($"Decision Nodes for Tree ID {treeIdToShow}:");
            foreach (var node in nodes)
            {
                Console.WriteLine($"Node ID: {node.Id}, Condition: {node.Condition}, Score: {node.Score}, TrueBranchId: {node.TrueBranchId}, FalseBranchId: {node.FalseBranchId}");
            }
            break;

        case 4:
            // Evaluate a decision tree
            Console.Write("Enter the Tree ID: ");
            int treeId = int.Parse(Console.ReadLine());
            Console.Write("Enter the score to evaluate: ");
            int inputScore = int.Parse(Console.ReadLine());
            int result = evaluator.EvaluateTree(treeId, inputScore);
            Console.WriteLine($"The evaluated score is: {result}");
            break;

        case 5:
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

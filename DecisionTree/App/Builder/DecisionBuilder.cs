using App.Domain;
using App.Repository;

namespace App.Builder;

public class DecisionTreeBuilder
{
    private readonly IDecisionTreeRepository _repository;

    public DecisionTreeBuilder(IDecisionTreeRepository repository)
    {
        _repository = repository;
    }

    public void AddNewTree()
    {
        Console.WriteLine("Creating a new decision tree...");

        var root = BuildNode();

        _repository.AddTree(root);
        Console.WriteLine("The decision tree has been created and saved successfully.");
    }

    private DecisionNode BuildNode()
    {
        Console.Write("Enter a condition for this node (e.g., 'score > 50') or leave blank for a leaf node: ");
        string condition = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(condition))
        {
            Console.Write("Enter the score for this leaf node: ");
            int score = int.Parse(Console.ReadLine());
            return new DecisionNode { Score = score }; // Leaf node
        }

        // Create a non-leaf node
        var node = new DecisionNode { Condition = condition };

        Console.WriteLine("Building the true branch...");
        node.TrueBranch = BuildNode();

        Console.WriteLine("Building the false branch...");
        node.FalseBranch = BuildNode();

        return node;
    }
}

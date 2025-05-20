using App.Context;
using App.Domain;
using App.Evaluator;
using App.Repository;
using NodeCompiler;
using static System.Console;

using var context = new DecisionTreeDbContext();
var repository = new DecisionTreeRepository(context);
var evaluator = new DecisionTreeEvaluator(repository);

var nodes = new List<DecisionNode>();
Compiler compiler = new(nodes);

WriteLine("Enter decision tree input:");
string? input = ReadLine();
if (string.IsNullOrEmpty(input))
{
    WriteLine("Cant empty conditions");
    return;
}

// Compile input into a model
var compiledNodes = compiler.Compile(input);
compiler.AssignBranchReferences();
WriteLine("\nCompiled Decision Nodes:");
foreach (var node in compiledNodes)
    WriteLine($"ID {node.Id}: Condition({node.Condition}), True -> {node.TrueBranchId}, False -> {node.FalseBranchId}, Formula({node.Formula})");

// Decompile back to string
string decompiledOutput = compiler.Decompile(compiledNodes);
WriteLine("\nDecompiled Output:");
WriteLine(decompiledOutput);

Write("You can evaluate your model for this input [Y/N] : ");
var evaluate = ReadLine();
if (evaluate?.ToLower() == "y")
{
    WriteLine("Enter customer data (e.g., purchases:5,spending:50):");
    var evaluateInput = ReadLine();
    if (!string.IsNullOrEmpty(evaluateInput))
    {
        string[] data = evaluateInput.Split(',');
        var customerData = data.Select(d => d.Split(':')).ToDictionary(k => k[0], v => double.Parse(v[1]));
        double result = evaluator.EvaluateTree(nodes, 0, customerData);
        Console.WriteLine($"Evaluated score: {result}");
    }
    return;
}
using App.Domain;
using App.Repository;
using Flee.PublicTypes;
using System.Data;

namespace App.Evaluator;

public class DecisionTreeEvaluator
{
    private readonly IDecisionTreeRepository _repository;

    public DecisionTreeEvaluator(IDecisionTreeRepository repository)
    {
        _repository = repository;
    }

    public double EvaluateTree(int treeId, Dictionary<string, double> customerData)
    {
        var root = _repository.GetRootNode(treeId);
        if (root == null)
            throw new Exception("Tree not found!");

        var currentNode = root;

        // Traverse the tree
        while (!currentNode.IsLeaf)
        {
            var condition = ReplaceVariablesWithValues(currentNode.Condition, customerData);
            bool result = EvaluateCondition(condition);

            currentNode = result
                ? _repository.GetNode(currentNode.TrueBranchId.Value)
                : _repository.GetNode(currentNode.FalseBranchId.Value);
        }

        // Evaluate formula at the leaf node
        return EvaluateFormula(currentNode.Formula, customerData);
    }

    public double EvaluateTree(List<DecisionNode> treeNodes, int treeId, Dictionary<string, double> customerData)
    {
        // Find the root node (assuming the first node is the root)
        var root = treeNodes.Find(n => n.TreeId == treeId && n.Id == 1);
        if (root == null)
            throw new Exception("Tree not found!");

        var currentNode = root;

        // Traverse the tree
        while (!currentNode.IsLeaf)
        {
            var condition = ReplaceVariablesWithValues(currentNode.Condition, customerData);
            bool result = EvaluateCondition(condition);

            currentNode = result
                ? treeNodes.Find(n => n.Id == currentNode.TrueBranchId)
                : treeNodes.Find(n => n.Id == currentNode.FalseBranchId);
        }

        // Evaluate formula at the leaf node
        return EvaluateFormula(currentNode.Formula, customerData);
    }

    private string ReplaceVariablesWithValues(string input, Dictionary<string, double> variables)
    {
        foreach (var variable in variables)
        {
            input = input.Replace(variable.Key, variable.Value.ToString());
        }
        return input;
    }

    private bool EvaluateCondition(string condition)
    {
        return (bool)new DataTable().Compute(condition, null);
    }

    private double EvaluateFormula(string formula, Dictionary<string, double> variables)
    {
        var context = new ExpressionContext();
        foreach (var variable in variables)
        {
            context.Variables[variable.Key] = variable.Value;
        }

        var compiledFormula = context.CompileDynamic(formula);
        return Convert.ToDouble(compiledFormula.Evaluate());
    }
}

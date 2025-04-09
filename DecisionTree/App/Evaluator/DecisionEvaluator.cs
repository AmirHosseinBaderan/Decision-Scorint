using App.Repository;
using System.Data;

namespace App.Evaluator;

public class DecisionTreeEvaluator
{
    private readonly IDecisionTreeRepository _repository;

    public DecisionTreeEvaluator(IDecisionTreeRepository repository)
    {
        _repository = repository;
    }

    public int EvaluateTree(int treeId, Dictionary<string, int> customerData)
    {
        var root = _repository.GetRootNode(treeId);
        if (root == null)
            throw new Exception("Tree not found!");

        var currentNode = root;

        while (!currentNode.Score.HasValue)
        {
            var condition = ReplaceVariablesWithValues(currentNode.Condition, customerData);
            bool result = EvaluateCondition(condition);

            currentNode = result
                ? _repository.GetNode(currentNode.TrueBranchId.Value)
                : _repository.GetNode(currentNode.FalseBranchId.Value);
        }

        return currentNode.Score.Value;
    }

    private string ReplaceVariablesWithValues(string condition, Dictionary<string, int> variables)
    {
        foreach (var variable in variables)
        {
            condition = condition.Replace(variable.Key, variable.Value.ToString());
        }
        return condition;
    }

    private bool EvaluateCondition(string condition)
    {
        return (bool)new DataTable().Compute(condition, null);
    }
}

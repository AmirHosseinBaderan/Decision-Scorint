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

    public int EvaluateTree(int treeId, int inputScore)
    {
        var root = _repository.GetRootNode(treeId);
        if (root == null) throw new Exception("Tree not found.");

        var currentNode = root;
        while (!currentNode.Score.HasValue)
        {
            var condition = currentNode.Condition.Replace("score", inputScore.ToString());
            bool result = EvaluateCondition(condition);

            currentNode = result ? _repository.GetNode(currentNode.TrueBranchId.Value)
                                 : _repository.GetNode(currentNode.FalseBranchId.Value);
        }

        return currentNode.Score.Value;
    }

    private bool EvaluateCondition(string condition)
    {
        return new DataTable().Compute(condition, null).Equals(true);
    }
}

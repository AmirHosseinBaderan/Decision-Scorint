using App.Domain;

namespace App.Repository;

public interface IDecisionTreeRepository
{
    void AddTree(DecisionNode root);
    DecisionNode GetNode(int nodeId);
    DecisionNode GetRootNode(int treeId);
    List<DecisionNode> GetAllNodes(int treeId);
    void UpdateNodeCondition(int nodeId, string condition);
    void UpdateNodeFormula(int nodeId, string formula);
}
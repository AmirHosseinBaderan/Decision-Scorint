using App.Domain;

namespace App.Repository;

public interface IDecisionTreeRepository
{
    void AddTree(DecisionNode root);
    DecisionNode GetNode(int nodeId);
    List<DecisionNode> GetAllNodes(int treeId);
    void UpdateNodeCondition(int nodeId, string newCondition);
    DecisionNode GetRootNode(int treeId);
}
